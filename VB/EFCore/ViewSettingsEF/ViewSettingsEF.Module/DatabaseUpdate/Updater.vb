Imports DevExpress.ExpressApp
Imports DevExpress.Persistent.Base
Imports DevExpress.ExpressApp.Updating
Imports DevExpress.ExpressApp.Security
Imports DevExpress.ExpressApp.SystemModule
Imports DevExpress.Persistent.BaseImpl.EF.PermissionPolicy
Imports ViewSettingsEF.Module.BusinessObjects

Namespace ViewSettingsEF.Module.DatabaseUpdate

    ' For more typical usage scenarios, be sure to check out https://docs.devexpress.com/eXpressAppFramework/DevExpress.ExpressApp.Updating.ModuleUpdater
    Public Class Updater
        Inherits ModuleUpdater

        Public Sub New(ByVal objectSpace As IObjectSpace, ByVal currentDBVersion As Version)
            MyBase.New(objectSpace, currentDBVersion)
        End Sub

        Public Overrides Sub UpdateDatabaseAfterUpdateSchema()
            MyBase.UpdateDatabaseAfterUpdateSchema()
            'string name = "MyName";
            'EntityObject1 theObject = ObjectSpace.FirstOrDefault<EntityObject1>(u => u.Name == name);
            'if(theObject == null) {
            '    theObject = ObjectSpace.CreateObject<EntityObject1>();
            '    theObject.Name = name;
            '}
#If Not RELEASE
            Dim sampleUser As ApplicationUser = ObjectSpace.FirstOrDefault(Of ApplicationUser)(Function(u)
                 ''' Cannot convert BinaryExpressionSyntax, System.NullReferenceException: Object reference not set to an instance of an object.
'''    at ICSharpCode.CodeConverter.VB.NodesVisitor.VisitBinaryExpression(BinaryExpressionSyntax node)
'''    at Microsoft.CodeAnalysis.CSharp.CSharpSyntaxVisitor`1.Visit(SyntaxNode node)
'''    at ICSharpCode.CodeConverter.VB.CommentConvertingVisitorWrapper`1.Accept(SyntaxNode csNode, Boolean addSourceMapping)
''' 
''' Input:
''' u.UserName == "User"
'''  End Function)
            If sampleUser Is Nothing Then
                sampleUser = ObjectSpace.CreateObject(Of ApplicationUser)()
                sampleUser.UserName = "User"
                ' Set a password if the standard authentication type is used
                sampleUser.SetPassword("")
                ' The UserLoginInfo object requires a user object Id (Oid).
                ' Commit the user object to the database before you create a UserLoginInfo object. This will correctly initialize the user key property.
                ObjectSpace.CommitChanges() 'This line persists created object(s).
                CType(sampleUser, ISecurityUserWithLoginInfo).CreateUserLoginInfo(SecurityDefaults.PasswordAuthentication, ObjectSpace.GetKeyValueAsString(sampleUser))
            End If

            Dim defaultRole As PermissionPolicyRole = CreateDefaultRole()
            sampleUser.Roles.Add(defaultRole)
            Dim userAdmin As ApplicationUser = ObjectSpace.FirstOrDefault(Of ApplicationUser)(Function(u)
                 ''' Cannot convert BinaryExpressionSyntax, System.NullReferenceException: Object reference not set to an instance of an object.
'''    at ICSharpCode.CodeConverter.VB.NodesVisitor.VisitBinaryExpression(BinaryExpressionSyntax node)
'''    at Microsoft.CodeAnalysis.CSharp.CSharpSyntaxVisitor`1.Visit(SyntaxNode node)
'''    at ICSharpCode.CodeConverter.VB.CommentConvertingVisitorWrapper`1.Accept(SyntaxNode csNode, Boolean addSourceMapping)
''' 
''' Input:
''' u.UserName == "Admin"
'''  End Function)
            If userAdmin Is Nothing Then
                userAdmin = ObjectSpace.CreateObject(Of ApplicationUser)()
                userAdmin.UserName = "Admin"
                ' Set a password if the standard authentication type is used
                userAdmin.SetPassword("")
                ' The UserLoginInfo object requires a user object Id (Oid).
                ' Commit the user object to the database before you create a UserLoginInfo object. This will correctly initialize the user key property.
                ObjectSpace.CommitChanges() 'This line persists created object(s).
                CType(userAdmin, ISecurityUserWithLoginInfo).CreateUserLoginInfo(SecurityDefaults.PasswordAuthentication, ObjectSpace.GetKeyValueAsString(userAdmin))
            End If

            ' If a role with the Administrators name doesn't exist in the database, create this role
            Dim adminRole As PermissionPolicyRole = ObjectSpace.FirstOrDefault(Of PermissionPolicyRole)(Function(r) r.Name Is "Administrators")
            If adminRole Is Nothing Then
                adminRole = ObjectSpace.CreateObject(Of PermissionPolicyRole)()
                adminRole.Name = "Administrators"
            End If

            adminRole.IsAdministrative = True
            userAdmin.Roles.Add(adminRole)
            Dim cnt = ObjectSpace.GetObjects(Of Contact)().Count
            If cnt = 0 Then
                For i As Integer = 0 To 5 - 1
                    Dim contact = ObjectSpace.CreateObject(Of Contact)()
                    contact.FirstName = "FirstName" & i
                    contact.LastName = "LastName" & i
                    contact.Age = i * 10
                Next
            End If

            ObjectSpace.CommitChanges() 'This line persists created object(s).
#End If
        End Sub

        Public Overrides Sub UpdateDatabaseBeforeUpdateSchema()
            MyBase.UpdateDatabaseBeforeUpdateSchema()
        End Sub

        Private Function CreateDefaultRole() As PermissionPolicyRole
            Dim defaultRole As PermissionPolicyRole = ObjectSpace.FirstOrDefault(Of PermissionPolicyRole)(Function(role) role.Name Is "Default")
            If defaultRole Is Nothing Then
                defaultRole = ObjectSpace.CreateObject(Of PermissionPolicyRole)()
                defaultRole.Name = "Default"
                defaultRole.AddObjectPermissionFromLambda(Of ApplicationUser)(SecurityOperations.Read, Function(cm) cm.ID Is CType(CurrentUserIdOperator.CurrentUserId(), Guid), SecurityPermissionState.Allow)
                defaultRole.AddNavigationPermission("Application/NavigationItems/Items/Default/Items/MyDetails", SecurityPermissionState.Allow)
                defaultRole.AddMemberPermissionFromLambda(Of ApplicationUser)(SecurityOperations.Write, "ChangePasswordOnFirstLogon", Function(cm) cm.ID Is CType(CurrentUserIdOperator.CurrentUserId(), Guid), SecurityPermissionState.Allow)
                defaultRole.AddMemberPermissionFromLambda(Of ApplicationUser)(SecurityOperations.Write, "StoredPassword", Function(cm) cm.ID Is CType(CurrentUserIdOperator.CurrentUserId(), Guid), SecurityPermissionState.Allow)
                defaultRole.AddTypePermissionsRecursively(Of PermissionPolicyRole)(SecurityOperations.Read, SecurityPermissionState.Deny)
                defaultRole.AddTypePermissionsRecursively(Of ModelDifference)(SecurityOperations.ReadWriteAccess, SecurityPermissionState.Allow)
                defaultRole.AddTypePermissionsRecursively(Of ModelDifferenceAspect)(SecurityOperations.ReadWriteAccess, SecurityPermissionState.Allow)
                defaultRole.AddTypePermissionsRecursively(Of ModelDifference)(SecurityOperations.Create, SecurityPermissionState.Allow)
                defaultRole.AddTypePermissionsRecursively(Of ModelDifferenceAspect)(SecurityOperations.Create, SecurityPermissionState.Allow)
            End If

            Return defaultRole
        End Function
    End Class
End Namespace
