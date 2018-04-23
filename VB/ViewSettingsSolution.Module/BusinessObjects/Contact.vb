Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports DevExpress.Persistent.Base
Imports DevExpress.Xpo
Imports DevExpress.Persistent.BaseImpl

Namespace ViewSettingsSolution.Module.BusinessObjects
    <DefaultClassOptions> _
    Public Class Contact
        Inherits BaseObject


        Private webPageAddress_Renamed As String

        Private nickName_Renamed As String

        Private spouseName_Renamed As String

        Private titleOfCourtesy_Renamed As TitleOfCourtesy
        Public Sub New(ByVal session As Session)
            MyBase.New(session)
        End Sub
        Public Property WebPageAddress() As String
            Get
                Return webPageAddress_Renamed
            End Get
            Set(ByVal value As String)
                SetPropertyValue("WebPageAddress", webPageAddress_Renamed, value)
            End Set
        End Property
        Public Property NickName() As String
            Get
                Return nickName_Renamed
            End Get
            Set(ByVal value As String)
                SetPropertyValue("NickName", nickName_Renamed, value)
            End Set
        End Property
        Public Property SpouseName() As String
            Get
                Return spouseName_Renamed
            End Get
            Set(ByVal value As String)
                SetPropertyValue("SpouseName", spouseName_Renamed, value)
            End Set
        End Property
        Public Property TitleOfCourtesy() As TitleOfCourtesy
            Get
                Return titleOfCourtesy_Renamed
            End Get
            Set(ByVal value As TitleOfCourtesy)
                SetPropertyValue("TitleOfCourtesy", titleOfCourtesy_Renamed, value)
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
