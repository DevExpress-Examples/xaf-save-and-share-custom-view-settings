Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports DevExpress.Persistent.BaseImpl
Imports DevExpress.ExpressApp
Imports System.ComponentModel
Imports DevExpress.Xpo

Namespace ViewSettingsSolution.Module.BusinessObjects
    Public Class SettingsStore
        Inherits BaseObject
        Implements IObjectSpaceLink


        Private xml_Renamed As String

        Private name_Renamed As String

        Private ownerId_Renamed As String

        Private viewId_Renamed As String

        Private isShared_Renamed As Boolean

        Private objectSpace_Renamed As IObjectSpace
        Public Sub New(ByVal session As Session)
            MyBase.New(session)
        End Sub
        <Browsable(False), Size(SizeAttribute.Unlimited)> _
        Public Property Xml() As String
            Get
                Return xml_Renamed
            End Get
            Set(ByVal value As String)
                SetPropertyValue("XML", xml_Renamed, value)
            End Set
        End Property
        Public Property Name() As String
            Get
                Return name_Renamed
            End Get
            Set(ByVal value As String)
                SetPropertyValue("Name", name_Renamed, value)
            End Set
        End Property
        <Browsable(False)> _
        Public Property OwnerId() As String
            Get
                Return ownerId_Renamed
            End Get
            Set(ByVal value As String)
                SetPropertyValue(Of String)("OwnerId", ownerId_Renamed, value)
            End Set
        End Property
        Public Property IsShared() As Boolean
            Get
                Return isShared_Renamed
            End Get
            Set(ByVal value As Boolean)
                SetPropertyValue("IsShared", isShared_Renamed, value)
            End Set
        End Property
        <Browsable(False)> _
        Public Property ViewId() As String
            Get
                Return viewId_Renamed
            End Get
            Set(ByVal value As String)
                SetPropertyValue(Of String)("ViewId", viewId_Renamed, value)
            End Set
        End Property

        Private Property IObjectSpaceLink_ObjectSpace() As IObjectSpace Implements IObjectSpaceLink.ObjectSpace
            Get
                Return objectSpace_Renamed
            End Get

            Set(ByVal value As IObjectSpace)
                objectSpace_Renamed = value
            End Set
        End Property
    End Class
End Namespace
