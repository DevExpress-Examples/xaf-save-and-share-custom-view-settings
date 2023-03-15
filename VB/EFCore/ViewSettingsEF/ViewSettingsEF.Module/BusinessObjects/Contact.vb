Imports DevExpress.Persistent.Base
Imports DevExpress.Persistent.BaseImpl.EF

Namespace ViewSettingsEF.Module.BusinessObjects

    <DefaultClassOptions>
    Public Class Contact
        Inherits BaseObject

        Public Overridable Property FirstName As String

        Public Overridable Property LastName As String

        Public Overridable Property Age As Integer

        Public Overridable Property BirthDate As Date
    End Class
End Namespace
