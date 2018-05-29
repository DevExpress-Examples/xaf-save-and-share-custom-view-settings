using System;
using System.Collections.Generic;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Utils;
using DevExpress.ExpressApp.Model;
using ViewSettingsSolution.Module.BusinessObjects;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.Templates;

namespace ViewSettingsSolution.Module.Controllers {
    public class ViewVariantsController : ViewController {
        private SimpleAction DeleteViewVariantAction;
        private PopupWindowShowAction CreateViewVariantAction;
        private SingleChoiceAction SelectViewVariantAction;
        private SimpleAction UpdateCurrentViewVariantAction;
        private SimpleAction UpdateDefaultSettingsWithSelectedVariantAction;
        private string defaultUserSettings;
        private Boolean isLayoutProcessed = false;
        private Boolean isDefaultViewSelected = false;
        private ChoiceActionItem lastSelectedItem = null;

        private void SetDifferences(string xml, IModelView model) {
            Dictionary<string, string> differences = new Dictionary<string, string>();
            differences.Add("", xml);
            UserDifferencesHelper.SetUserDifferences(model, differences);
        }
        private Boolean TryLoadViewVariantFromXML(string xml) {
            Boolean result = false;
            View savedView = Frame.View;
            isLayoutProcessed = true;
            if(Frame.SetView(null, true, null, false)) {
                if(isDefaultViewSelected) {
                    UpdateDefaultSettings(savedView.Model);
                    isDefaultViewSelected = false;
                }
                SetDifferences(xml, savedView.Model);
                savedView.LoadModel(false);
                Frame.SetView(savedView);
                result = true;
            }
            isLayoutProcessed = false;
            return result;
        }
        private void SaveViewVariantToXML(SettingsStore store) {
            isLayoutProcessed = true;
            View.SaveModel();
            isLayoutProcessed = false;
            store.Xml = UserDifferencesHelper.GetUserDifferences(View.Model)[""];
            ((IObjectSpaceLink)store).ObjectSpace.CommitChanges();
        }
        private void UpdateDefaultSettings(IModelView model) {
            defaultUserSettings = UserDifferencesHelper.GetUserDifferences(model)[""];
        }
        private void UpdateActions(string itemToSelectCaption) {
            SelectViewVariantAction.Items.Clear();
            lastSelectedItem = null;
            CriteriaOperator criteria = CriteriaOperator.Parse("([ViewId] = ?) And ([IsShared] Or [OwnerId] is null Or [OwnerId] = ?)", View.Id, SecuritySystem.CurrentUserId);
            IObjectSpace objectSpace = Application.CreateObjectSpace(typeof(SettingsStore));
            foreach(SettingsStore item in objectSpace.GetObjects<SettingsStore>(criteria)) {
                SelectViewVariantAction.Items.Add(new ChoiceActionItem(item.Name, item));
            }
            if(SelectViewVariantAction.Items.Count > 0) {
                ChoiceActionItem defaultItem = new ChoiceActionItem("Default", null);
                SelectViewVariantAction.Items.Add(defaultItem);
                ChoiceActionItem itemToSelect = SelectViewVariantAction.Items.FindItemByID(itemToSelectCaption);
                SelectViewVariantAction.SelectedItem = (itemToSelect != null) ? itemToSelect : defaultItem;
                lastSelectedItem = SelectViewVariantAction.SelectedItem;
            }
            UpdateActionsActive();
        }
        private void UpdateActionsActive() {
            Boolean isActive = SelectViewVariantAction.Items.Count > 0 && SelectViewVariantAction.SelectedItem.Data != null;
            DeleteViewVariantAction.Active["HasVariants"] = isActive;
            UpdateCurrentViewVariantAction.Active["HasVariants"] = isActive;
            UpdateDefaultSettingsWithSelectedVariantAction.Active["HasVariants"] = isActive;
        }
        private void DeleteViewVariantAction_Execute(object sender, SimpleActionExecuteEventArgs e) {
            IObjectSpaceLink currentLayoutItem = SelectViewVariantAction.SelectedItem.Data as IObjectSpaceLink;
            if(TryLoadViewVariantFromXML(defaultUserSettings)) {
                IObjectSpace os = currentLayoutItem.ObjectSpace;
                os.Delete(currentLayoutItem);
                os.CommitChanges();
                isDefaultViewSelected = true;
                UpdateActions(null);
            }
        }
        private void CreateViewVariantAction_CustomizePopupWindowParams(object sender, CustomizePopupWindowParamsEventArgs e) {
            IObjectSpace objectSpace = e.Application.CreateObjectSpace(typeof(SettingsStore));
            e.View = e.Application.CreateDetailView(objectSpace, objectSpace.CreateObject<SettingsStore>());
        }
        private void CreateViewVariantAction_Execute(object sender, PopupWindowShowActionExecuteEventArgs e) {
            SettingsStore store = (SettingsStore)e.PopupWindowViewCurrentObject;
            if(SecuritySystem.CurrentUserId != null) {
                store.OwnerId = SecuritySystem.CurrentUserId.ToString();
            }
            store.ViewId = View.Id;
            SaveViewVariantToXML(store);
            string itemToSelectCaption = store.Name;
            isDefaultViewSelected = false;
            UpdateActions(itemToSelectCaption);
        }
        private void SelectViewVariantAction_Execute(object sender, SingleChoiceActionExecuteEventArgs e) {
            bool isVariantChanged = false;
            ChoiceActionItem currentItem = SelectViewVariantAction.SelectedItem;
            if(currentItem.Data != null) {
                if(TryLoadViewVariantFromXML(((SettingsStore)currentItem.Data).Xml)) {
                    isVariantChanged = true;
                }
            }
            else {
                if(TryLoadViewVariantFromXML(defaultUserSettings)) {
                    isDefaultViewSelected = true;
                    isVariantChanged = true;
                }
            }
            SelectViewVariantAction.SelectedItem = isVariantChanged ? currentItem : lastSelectedItem;
            UpdateActionsActive();
        }
        private void UpdateCurrentViewVariantAction_Execute(object sender, SimpleActionExecuteEventArgs e) {
            SaveViewVariantToXML(SelectViewVariantAction.SelectedItem.Data as SettingsStore);
        }
        private void UpdateDefaultSettingsWithSelectedVariantAction_Execute(object sender, SimpleActionExecuteEventArgs e) {
            SaveViewVariantToXML(SelectViewVariantAction.SelectedItem.Data as SettingsStore);
            UpdateDefaultSettings(View.Model);
        }
        private void View_ModelSaving(object sender, System.ComponentModel.CancelEventArgs e) {
            if(!isLayoutProcessed && !isDefaultViewSelected) {
                SetDifferences(defaultUserSettings, View.Model);
                e.Cancel = true;
            }
        }
        protected override void OnActivated() {
            base.OnActivated();
            View.ModelSaving += View_ModelSaving;
            if(!isLayoutProcessed) {
                UpdateDefaultSettings(View.Model);
                isDefaultViewSelected = true;
                UpdateActions(null);
            }
        }
        protected override void OnDeactivated() {
            View.ModelSaving -= View_ModelSaving;
            base.OnDeactivated();
        }
        public ViewVariantsController() {
            this.TargetViewNesting = Nesting.Root;
            this.DeleteViewVariantAction = new SimpleAction(this, "DeleteViewVariant", "Edit");
            this.DeleteViewVariantAction.Execute += new SimpleActionExecuteEventHandler(this.DeleteViewVariantAction_Execute);

            this.CreateViewVariantAction = new PopupWindowShowAction(this, "SaveAsNewViewVariant", "Edit");
            this.CreateViewVariantAction.CustomizePopupWindowParams += new CustomizePopupWindowParamsEventHandler(this.CreateViewVariantAction_CustomizePopupWindowParams);
            this.CreateViewVariantAction.Execute += new PopupWindowShowActionExecuteEventHandler(this.CreateViewVariantAction_Execute);

            this.SelectViewVariantAction = new SingleChoiceAction(this, "SelectViewVariant", "Edit");
            this.SelectViewVariantAction.PaintStyle = ActionItemPaintStyle.Caption;
            this.SelectViewVariantAction.Execute += new SingleChoiceActionExecuteEventHandler(this.SelectViewVariantAction_Execute);

            this.UpdateCurrentViewVariantAction = new SimpleAction(this, "UpdateCurrentViewVariant", "Edit");
            this.UpdateCurrentViewVariantAction.Execute += new SimpleActionExecuteEventHandler(this.UpdateCurrentViewVariantAction_Execute);

            this.UpdateDefaultSettingsWithSelectedVariantAction  = new SimpleAction(this, "UpdateDefaultViewVariant", "Edit");
            UpdateDefaultSettingsWithSelectedVariantAction.Execute += UpdateDefaultSettingsWithSelectedVariantAction_Execute;

            this.Actions.Add(this.DeleteViewVariantAction);
            this.Actions.Add(this.CreateViewVariantAction);
            this.Actions.Add(this.SelectViewVariantAction);
            this.Actions.Add(this.UpdateCurrentViewVariantAction);
            this.Actions.Add(this.UpdateDefaultSettingsWithSelectedVariantAction);
        }
    }
}
