Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports DevExpress.Persistent.Base
Imports DevExpress.Xpo
Imports DevExpress.Persistent.BaseImpl

Namespace ViewSettingsSolution.Module.BusinessObjects
	<DefaultClassOptions>
	Public Class Contact
		Inherits BaseObject

'INSTANT VB NOTE: The field webPageAddress was renamed since Visual Basic does not allow fields to have the same name as other class members:
		Private webPageAddress_Conflict As String
'INSTANT VB NOTE: The field nickName was renamed since Visual Basic does not allow fields to have the same name as other class members:
		Private nickName_Conflict As String
'INSTANT VB NOTE: The field spouseName was renamed since Visual Basic does not allow fields to have the same name as other class members:
		Private spouseName_Conflict As String
'INSTANT VB NOTE: The field titleOfCourtesy was renamed since Visual Basic does not allow fields to have the same name as other class members:
		Private titleOfCourtesy_Conflict As TitleOfCourtesy
		Public Sub New(ByVal session As Session)
			MyBase.New(session)
		End Sub
		Public Property WebPageAddress() As String
			Get
				Return webPageAddress_Conflict
			End Get
			Set(ByVal value As String)
				SetPropertyValue("WebPageAddress", webPageAddress_Conflict, value)
			End Set
		End Property
		Public Property NickName() As String
			Get
				Return nickName_Conflict
			End Get
			Set(ByVal value As String)
				SetPropertyValue("NickName", nickName_Conflict, value)
			End Set
		End Property
		Public Property SpouseName() As String
			Get
				Return spouseName_Conflict
			End Get
			Set(ByVal value As String)
				SetPropertyValue("SpouseName", spouseName_Conflict, value)
			End Set
		End Property
		Public Property TitleOfCourtesy() As TitleOfCourtesy
			Get
				Return titleOfCourtesy_Conflict
			End Get
			Set(ByVal value As TitleOfCourtesy)
				SetPropertyValue("TitleOfCourtesy", titleOfCourtesy_Conflict, value)
			End Set
		End Property
	End Class
	Public Enum TitleOfCourtesy
		Dr
		Miss
		Mr
		Mrs
		Ms
	End Enum
End Namespace
