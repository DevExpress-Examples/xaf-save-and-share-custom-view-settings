Imports System.ComponentModel
Imports DevExpress.ExpressApp.Security
Imports DevExpress.Persistent.BaseImpl.PermissionPolicy
Imports DevExpress.Xpo

Namespace ViewSettings.Module.BusinessObjects

    <MapInheritance(MapInheritanceType.ParentTable)>
    <DefaultProperty(NameOf(PermissionPolicyUser.UserName))>
    Public Class ApplicationUser
        Inherits PermissionPolicyUser
        Implements ISecurityUserWithLoginInfo

        Public Sub New(ByVal session As Session)
            MyBase.New(session)
        End Sub

        <Browsable(False)>
        <Aggregated, Association("User-LoginInfo")>
        Public ReadOnly Property LoginInfo As XPCollection(Of ApplicationUserLoginInfo)
            Get
                Return GetCollection(Of ApplicationUserLoginInfo)(NameOf(ApplicationUser.LoginInfo))
            End Get
        End Property

        Private ReadOnly Property UserLogins As IEnumerable(Of ISecurityUserLoginInfo)
            Get
                Return LoginInfo.OfType(Of ISecurityUserLoginInfo)()
            End Get
        End Property

        Private Function CreateUserLoginInfo(ByVal loginProviderName As String, ByVal providerUserKey As String) As ISecurityUserLoginInfo Implements ISecurityUserWithLoginInfo.CreateUserLoginInfo
            Dim result As ApplicationUserLoginInfo = New ApplicationUserLoginInfo(Session)
            result.LoginProviderName = loginProviderName
            result.ProviderUserKey = providerUserKey
            result.UserProp = Me
            Return result
        End Function
    End Class
End Namespace
