Imports DevExpress.Persistent.Base
Imports DevExpress.Xpo
Imports DevExpress.Persistent.BaseImpl

Namespace ViewSettingsSolution.Module.BusinessObjects

    <DefaultClassOptions>
    Public Class Contact
        Inherits BaseObject

        Private webPageAddressField As String

        Private nickNameField As String

        Private spouseNameField As String

        Private titleOfCourtesyField As TitleOfCourtesy

        Public Sub New(ByVal session As Session)
            MyBase.New(session)
        End Sub

        Public Property WebPageAddress As String
            Get
                Return webPageAddressField
            End Get

            Set(ByVal value As String)
                SetPropertyValue("WebPageAddress", webPageAddressField, value)
            End Set
        End Property

        Public Property NickName As String
            Get
                Return nickNameField
            End Get

            Set(ByVal value As String)
                SetPropertyValue("NickName", nickNameField, value)
            End Set
        End Property

        Public Property SpouseName As String
            Get
                Return spouseNameField
            End Get

            Set(ByVal value As String)
                SetPropertyValue("SpouseName", spouseNameField, value)
            End Set
        End Property

        Public Property TitleOfCourtesy As TitleOfCourtesy
            Get
                Return titleOfCourtesyField
            End Get

            Set(ByVal value As TitleOfCourtesy)
                SetPropertyValue("TitleOfCourtesy", titleOfCourtesyField, value)
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
