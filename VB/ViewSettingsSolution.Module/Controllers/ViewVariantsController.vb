﻿Imports System
Imports System.Collections.Generic
Imports DevExpress.ExpressApp
Imports DevExpress.ExpressApp.Utils
Imports DevExpress.ExpressApp.Model
Imports ViewSettingsSolution.Module.BusinessObjects
Imports DevExpress.Data.Filtering
Imports DevExpress.ExpressApp.Actions
Imports DevExpress.ExpressApp.Templates

Namespace ViewSettingsSolution.Module.Controllers
	Public Class ViewVariantsController
		Inherits ViewController

		Private DeleteViewVariantAction As SimpleAction
		Private CreateViewVariantAction As PopupWindowShowAction
		Private SelectViewVariantAction As SingleChoiceAction
		Private UpdateCurrentViewVariantAction As SimpleAction
		Private UpdateDefaultSettingsWithSelectedVariantAction As SimpleAction
		Private defaultUserSettings As String
		Private isLayoutProcessed As Boolean = False
		Private isDefaultViewSelected As Boolean = False
		Private lastSelectedItem As ChoiceActionItem = Nothing

		Private Sub SetDifferences(ByVal xml As String, ByVal model As IModelView)
			Dim differences As New Dictionary(Of String, String)()
			differences.Add("", xml)
			UserDifferencesHelper.SetUserDifferences(model, differences)
		End Sub
		Private Function TryLoadViewVariantFromXML(ByVal xml As String) As Boolean
			Dim result As Boolean = False
			Dim savedView As View = Frame.View
			isLayoutProcessed = True
			For Each controller As ISupportUpdate In Frame.Controllers
				controller.BeginUpdate()
			Next controller
			Try
				If Frame.SetView(Nothing, True, Nothing, False) Then
					If isDefaultViewSelected Then
						UpdateDefaultSettings(savedView.Model)
						isDefaultViewSelected = False
					End If
					SetDifferences(xml, savedView.Model)
					savedView.LoadModel(False)
					Frame.SetView(savedView)
					result = True
				End If
			Finally
				For Each controller As ISupportUpdate In Frame.Controllers
					controller.EndUpdate()
				Next controller
			End Try
			isLayoutProcessed = False
			Return result
		End Function
		Private Sub SaveViewVariantToXML(ByVal store As SettingsStore)
			isLayoutProcessed = True
			View.SaveModel()
			isLayoutProcessed = False
			store.Xml = UserDifferencesHelper.GetUserDifferences(View.Model)("")
			DirectCast(store, IObjectSpaceLink).ObjectSpace.CommitChanges()
		End Sub
		Private Sub UpdateDefaultSettings(ByVal model As IModelView)
			defaultUserSettings = UserDifferencesHelper.GetUserDifferences(model)("")
		End Sub
		Private Sub UpdateActions(ByVal itemToSelectCaption As String)
			SelectViewVariantAction.Items.Clear()
			lastSelectedItem = Nothing
			Dim criteria As CriteriaOperator = CriteriaOperator.Parse("([ViewId] = ?) And ([IsShared] Or [OwnerId] is null Or [OwnerId] = ?)", View.Id, SecuritySystem.CurrentUserId.ToString())
			Dim objectSpace As IObjectSpace = Application.CreateObjectSpace(GetType(SettingsStore))
			For Each item As SettingsStore In objectSpace.GetObjects(Of SettingsStore)(criteria)
				SelectViewVariantAction.Items.Add(New ChoiceActionItem(item.Name, item))
			Next item
			If SelectViewVariantAction.Items.Count > 0 Then
				Dim defaultItem As New ChoiceActionItem("Default", Nothing)
				SelectViewVariantAction.Items.Add(defaultItem)
				Dim itemToSelect As ChoiceActionItem = SelectViewVariantAction.Items.FindItemByID(itemToSelectCaption)
				SelectViewVariantAction.SelectedItem = If(itemToSelect IsNot Nothing, itemToSelect, defaultItem)
				lastSelectedItem = SelectViewVariantAction.SelectedItem
			End If
			UpdateActionsActive()
		End Sub
		Private Sub UpdateActionsActive()
			Dim isActive As Boolean = SelectViewVariantAction.Items.Count > 0 AndAlso SelectViewVariantAction.SelectedItem.Data IsNot Nothing
			DeleteViewVariantAction.Active("HasVariants") = isActive
			UpdateCurrentViewVariantAction.Active("HasVariants") = isActive
			UpdateDefaultSettingsWithSelectedVariantAction.Active("HasVariants") = isActive
		End Sub
		Private Sub DeleteViewVariantAction_Execute(ByVal sender As Object, ByVal e As SimpleActionExecuteEventArgs)
			Dim currentLayoutItem As IObjectSpaceLink = TryCast(SelectViewVariantAction.SelectedItem.Data, IObjectSpaceLink)
			If TryLoadViewVariantFromXML(defaultUserSettings) Then
				Dim os As IObjectSpace = currentLayoutItem.ObjectSpace
				os.Delete(currentLayoutItem)
				os.CommitChanges()
				isDefaultViewSelected = True
				UpdateActions(Nothing)
			End If
		End Sub
		Private Sub CreateViewVariantAction_CustomizePopupWindowParams(ByVal sender As Object, ByVal e As CustomizePopupWindowParamsEventArgs)
			Dim objectSpace As IObjectSpace = e.Application.CreateObjectSpace(GetType(SettingsStore))
			e.View = e.Application.CreateDetailView(objectSpace, objectSpace.CreateObject(Of SettingsStore)())
		End Sub
		Private Sub CreateViewVariantAction_Execute(ByVal sender As Object, ByVal e As PopupWindowShowActionExecuteEventArgs)
			Dim store As SettingsStore = CType(e.PopupWindowViewCurrentObject, SettingsStore)
			If SecuritySystem.CurrentUserId IsNot Nothing Then
				store.OwnerId = SecuritySystem.CurrentUserId.ToString()
			End If
			store.ViewId = View.Id
			SaveViewVariantToXML(store)
			Dim itemToSelectCaption As String = store.Name
			isDefaultViewSelected = False
			UpdateActions(itemToSelectCaption)
		End Sub
		Private Sub SelectViewVariantAction_Execute(ByVal sender As Object, ByVal e As SingleChoiceActionExecuteEventArgs)
			Dim isVariantChanged As Boolean = False
			Dim currentItem As ChoiceActionItem = SelectViewVariantAction.SelectedItem
			If currentItem.Data IsNot Nothing Then
				If TryLoadViewVariantFromXML(CType(currentItem.Data, SettingsStore).Xml) Then
					isVariantChanged = True
				End If
			Else
				If TryLoadViewVariantFromXML(defaultUserSettings) Then
					isDefaultViewSelected = True
					isVariantChanged = True
				End If
			End If
			SelectViewVariantAction.SelectedItem = If(isVariantChanged, currentItem, lastSelectedItem)
			UpdateActionsActive()
		End Sub
		Private Sub UpdateCurrentViewVariantAction_Execute(ByVal sender As Object, ByVal e As SimpleActionExecuteEventArgs)
			SaveViewVariantToXML(TryCast(SelectViewVariantAction.SelectedItem.Data, SettingsStore))
		End Sub
		Private Sub UpdateDefaultSettingsWithSelectedVariantAction_Execute(ByVal sender As Object, ByVal e As SimpleActionExecuteEventArgs)
			SaveViewVariantToXML(TryCast(SelectViewVariantAction.SelectedItem.Data, SettingsStore))
			UpdateDefaultSettings(View.Model)
		End Sub
		Private Sub View_ModelSaving(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs)
			If Not isLayoutProcessed AndAlso Not isDefaultViewSelected Then
				SetDifferences(defaultUserSettings, View.Model)
				e.Cancel = True
			End If
		End Sub
		Protected Overrides Sub OnActivated()
			MyBase.OnActivated()
			AddHandler View.ModelSaving, AddressOf View_ModelSaving
			If Not isLayoutProcessed Then
				UpdateDefaultSettings(View.Model)
				isDefaultViewSelected = True
				UpdateActions(Nothing)
			End If
		End Sub
		Protected Overrides Sub OnDeactivated()
			RemoveHandler View.ModelSaving, AddressOf View_ModelSaving
			MyBase.OnDeactivated()
		End Sub
		Public Sub New()
			Me.TargetViewNesting = Nesting.Root
			Me.DeleteViewVariantAction = New SimpleAction(Me, "DeleteViewVariant", "Edit")
			AddHandler DeleteViewVariantAction.Execute, AddressOf DeleteViewVariantAction_Execute

			Me.CreateViewVariantAction = New PopupWindowShowAction(Me, "SaveAsNewViewVariant", "Edit")
			AddHandler CreateViewVariantAction.CustomizePopupWindowParams, AddressOf CreateViewVariantAction_CustomizePopupWindowParams
			AddHandler CreateViewVariantAction.Execute, AddressOf CreateViewVariantAction_Execute

			Me.SelectViewVariantAction = New SingleChoiceAction(Me, "SelectViewVariant", "Edit")
			Me.SelectViewVariantAction.PaintStyle = ActionItemPaintStyle.Caption
			AddHandler SelectViewVariantAction.Execute, AddressOf SelectViewVariantAction_Execute

			Me.UpdateCurrentViewVariantAction = New SimpleAction(Me, "UpdateCurrentViewVariant", "Edit")
			AddHandler UpdateCurrentViewVariantAction.Execute, AddressOf UpdateCurrentViewVariantAction_Execute

			Me.UpdateDefaultSettingsWithSelectedVariantAction = New SimpleAction(Me, "UpdateDefaultViewVariant", "Edit")
			AddHandler UpdateDefaultSettingsWithSelectedVariantAction.Execute, AddressOf UpdateDefaultSettingsWithSelectedVariantAction_Execute

			Me.Actions.Add(Me.DeleteViewVariantAction)
			Me.Actions.Add(Me.CreateViewVariantAction)
			Me.Actions.Add(Me.SelectViewVariantAction)
			Me.Actions.Add(Me.UpdateCurrentViewVariantAction)
			Me.Actions.Add(Me.UpdateDefaultSettingsWithSelectedVariantAction)
		End Sub
	End Class
End Namespace
