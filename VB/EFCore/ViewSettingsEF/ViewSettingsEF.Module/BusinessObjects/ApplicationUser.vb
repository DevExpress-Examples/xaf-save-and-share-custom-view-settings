Imports System.Collections.ObjectModel
Imports System.ComponentModel
Imports DevExpress.ExpressApp
Imports DevExpress.ExpressApp.Security
Imports DevExpress.Persistent.BaseImpl.EF.PermissionPolicy

Namespace ViewSettingsEF.Module.BusinessObjects

    <DefaultProperty(NameOf(PermissionPolicyUser.UserName))>
    Public Class ApplicationUser
        Inherits PermissionPolicyUser
        Implements ISecurityUserWithLoginInfo

        Public Sub New()
            MyBase.New()
            Me.UserLogins = New ObservableCollection(Of ApplicationUserLoginInfo)()
        End Sub

        <Browsable(False)>
        <DC.Aggregated>
        Public Overridable Property UserLogins As IList(Of ApplicationUserLoginInfo)

        Private ReadOnly Property UserLogins As IEnumerable(Of ISecurityUserLoginInfo)
            Get
                Return Me.UserLogins.OfType(Of ISecurityUserLoginInfo)()
            End Get
        End Property

        Private Function CreateUserLoginInfo(ByVal loginProviderName As String, ByVal providerUserKey As String) As ISecurityUserLoginInfo Implements ISecurityUserWithLoginInfo.CreateUserLoginInfo
            Dim result As ApplicationUserLoginInfo = CType(Me, IObjectSpaceLink).ObjectSpace.CreateObject(Of ApplicationUserLoginInfo)()
            result.LoginProviderName = loginProviderName
            result.ProviderUserKey = providerUserKey
            result.UserProp = Me
            Return result
        End Function
    End Class
End Namespace
