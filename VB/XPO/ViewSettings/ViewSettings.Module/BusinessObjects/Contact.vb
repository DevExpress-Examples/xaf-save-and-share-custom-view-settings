Imports DevExpress.Xpo
Imports System.ComponentModel
Imports DevExpress.Persistent.Base
Imports DevExpress.Persistent.BaseImpl
Imports System.Diagnostics

Namespace dxTestSolution.Module.BusinessObjects

    <DefaultClassOptions>
    Public Class Contact
        Inherits BaseObject

        Public Sub New(ByVal session As Session)
            MyBase.New(session)
        End Sub

        Public Overrides Sub AfterConstruction()
            MyBase.AfterConstruction()
        End Sub

        Private _firstName As String

        Public Property FirstName As String
            Get
                Return _firstName
            End Get

            Set(ByVal value As String)
                SetPropertyValue(NameOf(Contact.FirstName), _firstName, value)
            End Set
        End Property

        Private _lastName As String

        Public Property LastName As String
            Get
                Return _lastName
            End Get

            Set(ByVal value As String)
                SetPropertyValue(NameOf(Contact.LastName), _lastName, value)
            End Set
        End Property

        Private _age As Integer

        Public Property Age As Integer
            Get
                Return _age
            End Get

            Set(ByVal value As Integer)
                SetPropertyValue(NameOf(Contact.Age), _age, value)
            End Set
        End Property
    End Class
End Namespace
