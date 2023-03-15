Imports DevExpress.Persistent.BaseImpl
Imports DevExpress.ExpressApp
Imports System.ComponentModel
Imports DevExpress.Xpo

Namespace ViewSettingsSolution.Module.BusinessObjects

    Public Class SettingsStore
        Inherits BaseObject
        Implements IObjectSpaceLink

        Private xmlField As String

        Private nameField As String

        Private ownerIdField As String

        Private viewIdField As String

        Private isSharedField As Boolean

        Private objectSpaceField As IObjectSpace

        Public Sub New(ByVal session As Session)
            MyBase.New(session)
        End Sub

        <Browsable(False)>
        <Size(SizeAttribute.Unlimited)>
        Public Property Xml As String
            Get
                Return xmlField
            End Get

            Set(ByVal value As String)
                SetPropertyValue("XML", xmlField, value)
            End Set
        End Property

        Public Property Name As String
            Get
                Return nameField
            End Get

            Set(ByVal value As String)
                SetPropertyValue("Name", nameField, value)
            End Set
        End Property

        <Browsable(False)>
        Public Property OwnerId As String
            Get
                Return ownerIdField
            End Get

            Set(ByVal value As String)
                SetPropertyValue(Of String)("OwnerId", ownerIdField, value)
            End Set
        End Property

        Public Property IsShared As Boolean
            Get
                Return isSharedField
            End Get

            Set(ByVal value As Boolean)
                SetPropertyValue("IsShared", isSharedField, value)
            End Set
        End Property

        <Browsable(False)>
        Public Property ViewId As String
            Get
                Return viewIdField
            End Get

            Set(ByVal value As String)
                SetPropertyValue(Of String)("ViewId", viewIdField, value)
            End Set
        End Property

        Private Property ObjectSpace As IObjectSpace Implements IObjectSpaceLink.ObjectSpace
            Get
                Return objectSpaceField
            End Get

            Set(ByVal value As IObjectSpace)
                objectSpaceField = value
            End Set
        End Property
    End Class
End Namespace
