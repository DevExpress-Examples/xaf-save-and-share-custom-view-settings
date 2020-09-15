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

'INSTANT VB NOTE: The field xml was renamed since Visual Basic does not allow fields to have the same name as other class members:
		Private xml_Conflict As String
'INSTANT VB NOTE: The field name was renamed since Visual Basic does not allow fields to have the same name as other class members:
		Private name_Conflict As String
'INSTANT VB NOTE: The field ownerId was renamed since Visual Basic does not allow fields to have the same name as other class members:
		Private ownerId_Conflict As String
'INSTANT VB NOTE: The field viewId was renamed since Visual Basic does not allow fields to have the same name as other class members:
		Private viewId_Conflict As String
'INSTANT VB NOTE: The field isShared was renamed since Visual Basic does not allow fields to have the same name as other class members:
		Private isShared_Conflict As Boolean
'INSTANT VB NOTE: The field objectSpace was renamed since Visual Basic does not allow fields to have the same name as other class members:
		Private objectSpace_Conflict As IObjectSpace
		Public Sub New(ByVal session As Session)
			MyBase.New(session)
		End Sub
		<Browsable(False)>
		<Size(SizeAttribute.Unlimited)>
		Public Property Xml() As String
			Get
				Return xml_Conflict
			End Get
			Set(ByVal value As String)
				SetPropertyValue("XML", xml_Conflict, value)
			End Set
		End Property
		Public Property Name() As String
			Get
				Return name_Conflict
			End Get
			Set(ByVal value As String)
				SetPropertyValue("Name", name_Conflict, value)
			End Set
		End Property
		<Browsable(False)>
		Public Property OwnerId() As String
			Get
				Return ownerId_Conflict
			End Get
			Set(ByVal value As String)
				SetPropertyValue(Of String)("OwnerId", ownerId_Conflict, value)
			End Set
		End Property
		Public Property IsShared() As Boolean
			Get
				Return isShared_Conflict
			End Get
			Set(ByVal value As Boolean)
				SetPropertyValue("IsShared", isShared_Conflict, value)
			End Set
		End Property
		<Browsable(False)>
		Public Property ViewId() As String
			Get
				Return viewId_Conflict
			End Get
			Set(ByVal value As String)
				SetPropertyValue(Of String)("ViewId", viewId_Conflict, value)
			End Set
		End Property

		Private Property IObjectSpaceLink_ObjectSpace() As IObjectSpace Implements IObjectSpaceLink.ObjectSpace
			Get
				Return objectSpace_Conflict
			End Get

			Set(ByVal value As IObjectSpace)
				objectSpace_Conflict = value
			End Set
		End Property
	End Class
End Namespace
