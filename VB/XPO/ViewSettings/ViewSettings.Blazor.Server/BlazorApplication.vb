Imports DevExpress.ExpressApp
Imports DevExpress.ExpressApp.ApplicationBuilder
Imports DevExpress.ExpressApp.Blazor
Imports DevExpress.ExpressApp.Security
Imports DevExpress.ExpressApp.Security.ClientServer
Imports DevExpress.ExpressApp.SystemModule
Imports DevExpress.ExpressApp.Xpo

Namespace ViewSettings.Blazor.Server

    Public Class ViewSettingsBlazorApplication
        Inherits BlazorApplication

        Public Sub New()
            ApplicationName = "ViewSettings"
            CheckCompatibilityType = DevExpress.ExpressApp.CheckCompatibilityType.DatabaseSchema
            DatabaseVersionMismatch += AddressOf ViewSettingsBlazorApplication_DatabaseVersionMismatch
        End Sub

        Protected Overrides Sub OnSetupStarted()
            MyBase.OnSetupStarted()
#If DEBUG
            If System.Diagnostics.Debugger.IsAttached AndAlso CheckCompatibilityType Is CheckCompatibilityType.DatabaseSchema Then
                DatabaseUpdateMode = DatabaseUpdateMode.UpdateDatabaseAlways
            End If
#End If
        End Sub

        Private Sub ViewSettingsBlazorApplication_DatabaseVersionMismatch(ByVal sender As Object, ByVal e As DatabaseVersionMismatchEventArgs)
#If EASYTEST
        e.Updater.Update();
        e.Handled = true;
#Else
            If System.Diagnostics.Debugger.IsAttached Then
                e.Updater.Update()
                e.Handled = True
            Else
                Dim message As String = "The application cannot connect to the specified database, " & "because the database doesn't exist, its version is older " & "than that of the application or its schema does not match " & "the ORM data model structure. To avoid this error, use one " & "of the solutions from the https://www.devexpress.com/kb=T367835 KB Article."
                If e.CompatibilityError IsNot Nothing AndAlso e.CompatibilityError.Exception IsNot Nothing Then
                    message += Global.Microsoft.VisualBasic.Constants.vbCrLf & Global.Microsoft.VisualBasic.Constants.vbCrLf & "Inner exception: " & e.CompatibilityError.Exception.Message
                End If

                Throw New InvalidOperationException(message)
            End If
#End If
        End Sub
    End Class
End Namespace
