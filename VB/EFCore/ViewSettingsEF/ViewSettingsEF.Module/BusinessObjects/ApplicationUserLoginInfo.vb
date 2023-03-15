Imports System.ComponentModel
Imports System.ComponentModel.DataAnnotations
Imports System.ComponentModel.DataAnnotations.Schema
Imports DevExpress.ExpressApp.ConditionalAppearance
Imports DevExpress.ExpressApp.Security

Namespace ViewSettingsEF.Module.BusinessObjects

    <Table("PermissionPolicyUserLoginInfo")>
    Public Class ApplicationUserLoginInfo
        Implements ISecurityUserLoginInfo

        Public Sub New()
        End Sub

        <Browsable(False)>
        Public Overridable Property ID As Guid

        <Appearance("PasswordProvider", Enabled:=False, Criteria:="!(IsNewObject(this)) and LoginProviderName == '" & SecurityDefaults.PasswordAuthentication & "'", Context:="DetailView")>
        Public Overridable Property LoginProviderName As String Implements ISecurityUserLoginInfo.LoginProviderName

        <Appearance("PasswordProviderUserKey", Enabled:=False, Criteria:="!(IsNewObject(this)) and LoginProviderName == '" & SecurityDefaults.PasswordAuthentication & "'", Context:="DetailView")>
        Public Overridable Property ProviderUserKey As String Implements ISecurityUserLoginInfo.ProviderUserKey

        <Browsable(False)>
        Public Overridable Property UserForeignKey As Guid

        <Required>
        <ForeignKey(NameOf(ApplicationUserLoginInfo.UserForeignKey))>
        Public Overridable Property UserProp As ApplicationUser

        Private ReadOnly Property User As Object Implements ISecurityUserLoginInfo.User
            Get
                Return UserProp
            End Get
        End Property
    End Class
End Namespace
