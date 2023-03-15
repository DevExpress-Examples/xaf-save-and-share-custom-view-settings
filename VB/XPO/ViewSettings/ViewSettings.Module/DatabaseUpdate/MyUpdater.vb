',new MyUpdater(objectSpace,versionFromDB)
'            defaultRole.AddNavigationPermission(@"Application/NavigationItems/Items/Default/Items/Contact_ListView", SecurityPermissionState.Allow);
'defaultRole.AddTypePermissionsRecursively<Contact>(SecurityOperations.CRUDAccess, SecurityPermissionState.Allow);
Imports System
Imports DevExpress.ExpressApp
Imports DevExpress.Data.Filtering
Imports DevExpress.ExpressApp.Updating
Imports DevExpress.Xpo
Imports dxTestSolution.Module.BusinessObjects

Namespace dxTestSolution.Module.DatabaseUpdate

    ' For more typical usage scenarios, be sure to check out https://documentation.devexpress.com/eXpressAppFramework/clsDevExpressExpressAppUpdatingModuleUpdatertopic.aspx
    Public Class MyUpdater
        Inherits ModuleUpdater

        Public Sub New(ByVal objectSpace As IObjectSpace, ByVal currentDBVersion As Version)
            MyBase.New(objectSpace, currentDBVersion)
        End Sub

        Public Overrides Sub UpdateDatabaseAfterUpdateSchema()
            MyBase.UpdateDatabaseAfterUpdateSchema()
            ',new MyUpdater(objectSpace,versionFromDB)
            'string name = "MyName";
            'DomainObject1 theObject = ObjectSpace.FindObject<DomainObject1>(CriteriaOperator.Parse("Name=?", name));
            'if(theObject == null) {
            '    theObject = ObjectSpace.CreateObject<DomainObject1>();
            '    theObject.Name = name;
            '}
            Dim cnt = ObjectSpace.GetObjects(Of Contact)().Count
            If cnt > 0 Then
                Return
            End If

            For i As Integer = 0 To 5 - 1
                Dim contactName As String = "FirstName" & i
                Dim contact = CreateObject(Of Contact)("FirstName", contactName)
                contact.LastName = "LastName" & i
                contact.Age = i * 10
            Next

            'secur#0  
            ObjectSpace.CommitChanges() 'Uncomment this line to persist created object(s).
        End Sub

        Private Function CreateObject(Of T)(ByVal propertyName As String, ByVal value As String) As T
            Dim theObject As T = ObjectSpace.FindObject(Of T)(New OperandProperty(propertyName) Is value)
            If theObject Is Nothing Then
                theObject = ObjectSpace.CreateObject(Of T)()
                Call CType(CObj(theObject), XPBaseObject).SetMemberValue(propertyName, value)
            End If

            Return theObject
        End Function

        Public Overrides Sub UpdateDatabaseBeforeUpdateSchema()
            MyBase.UpdateDatabaseBeforeUpdateSchema()
        'if(CurrentDBVersion < new Version("1.1.0.0") && CurrentDBVersion > new Version("0.0.0.0")) {
        '    RenameColumn("DomainObject1Table", "OldColumnName", "NewColumnName");
        '}
        End Sub
    End Class
End Namespace
