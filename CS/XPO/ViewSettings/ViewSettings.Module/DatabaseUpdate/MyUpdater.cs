//,new MyUpdater(objectSpace,versionFromDB)
//            defaultRole.AddNavigationPermission(@"Application/NavigationItems/Items/Default/Items/Contact_ListView", SecurityPermissionState.Allow);
            //defaultRole.AddTypePermissionsRecursively<Contact>(SecurityOperations.CRUDAccess, SecurityPermissionState.Allow);
using System;
using System.Linq;
using DevExpress.ExpressApp;
using DevExpress.Data.Filtering;
using DevExpress.Persistent.Base;
using DevExpress.ExpressApp.Updating;
using DevExpress.Xpo;
using DevExpress.ExpressApp.Xpo;
using DevExpress.Persistent.BaseImpl;
using DevExpress.ExpressApp.Security;
using DevExpress.Persistent.BaseImpl.PermissionPolicy;

using dxTestSolution.Module.BusinessObjects;

namespace dxTestSolution.Module.DatabaseUpdate {
    // For more typical usage scenarios, be sure to check out https://documentation.devexpress.com/eXpressAppFramework/clsDevExpressExpressAppUpdatingModuleUpdatertopic.aspx
    public class MyUpdater : ModuleUpdater {
        public MyUpdater(IObjectSpace objectSpace, Version currentDBVersion) :
            base(objectSpace, currentDBVersion) {
        }
        public override void UpdateDatabaseAfterUpdateSchema() {
            base.UpdateDatabaseAfterUpdateSchema();
			//,new MyUpdater(objectSpace,versionFromDB)
            //string name = "MyName";
            //DomainObject1 theObject = ObjectSpace.FindObject<DomainObject1>(CriteriaOperator.Parse("Name=?", name));
            //if(theObject == null) {
            //    theObject = ObjectSpace.CreateObject<DomainObject1>();
            //    theObject.Name = name;
            //}
            var cnt = ObjectSpace.GetObjects<Contact>().Count;
            if(cnt > 0) {
                return;
            }
            for (int i = 0; i < 5; i++) {
                string contactName = "FirstName" + i;
                var contact = CreateObject<Contact>("FirstName", contactName);
                contact.LastName = "LastName" + i;
				contact.Age = i * 10;
            }
            //secur#0  
			ObjectSpace.CommitChanges(); //Uncomment this line to persist created object(s).
        }

        T CreateObject<T>(string propertyName,string value) {
       
            T theObject = ObjectSpace.FindObject<T>(new OperandProperty(propertyName) == value);
            if (theObject == null){
                theObject = ObjectSpace.CreateObject<T>();
                ((XPBaseObject)(Object)theObject).SetMemberValue(propertyName, value);
            }
          
            return theObject;

        }

        public override void UpdateDatabaseBeforeUpdateSchema() {
            base.UpdateDatabaseBeforeUpdateSchema();
            //if(CurrentDBVersion < new Version("1.1.0.0") && CurrentDBVersion > new Version("0.0.0.0")) {
            //    RenameColumn("DomainObject1Table", "OldColumnName", "NewColumnName");
            //}
        }
    }
}
