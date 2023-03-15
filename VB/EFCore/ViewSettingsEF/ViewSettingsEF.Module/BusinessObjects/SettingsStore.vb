Imports System.ComponentModel
Imports DevExpress.Persistent.BaseImpl.EF

Namespace ViewSettingsEF.Module.BusinessObjects

    Public Class SettingsStore
        Inherits BaseObject

        <Browsable(False)>
        Public Overridable Property Xml As String

        Public Overridable Property Name As String

        <Browsable(False)>
        Public Overridable Property OwnerId As String

        Public Overridable Property IsShared As Boolean

        <Browsable(False)>
        Public Overridable Property ViewId As String
    End Class
End Namespace
