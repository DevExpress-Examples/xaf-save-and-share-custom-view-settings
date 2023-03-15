Imports DevExpress.ExpressApp
Imports DevExpress.ExpressApp.ApplicationBuilder
Imports DevExpress.ExpressApp.Win.ApplicationBuilder
Imports DevExpress.ExpressApp.Security
Imports DevExpress.ExpressApp.Win
Imports DevExpress.Persistent.BaseImpl.PermissionPolicy
Imports DevExpress.ExpressApp.Design

Namespace ViewSettings.Win

    Public Class ApplicationBuilder
        Implements IDesignTimeApplicationFactory

        Public Shared Function BuildApplication(ByVal connectionString As String) As WinApplication
            Dim builder = WinApplication.CreateBuilder()
            builder.UseApplication(Of Win.ViewSettingsWindowsFormsApplication)()
            ConditionalAppearanceApplicationBuilderExtensions.AddConditionalAppearance(Of DevExpress.ExpressApp.Win.ApplicationBuilder.IWinApplicationBuilder)(builder.Modules).AddValidation(Function(options)
                options.AllowValidationDetailsAccess = False
            End Function).AddViewVariants().Add(Of ViewSettings.Module.ViewSettingsModule)().Add(Of ViewSettingsWinModule)()
            builder.ObjectSpaceProviders.AddSecuredXpo(Function(application, options)
                options.ConnectionString = connectionString
            End Function).AddNonPersistent()
            builder.Security.UseIntegratedMode(Sub(options)
                options.RoleType = GetType(PermissionPolicyRole)
                options.UserType = GetType(ViewSettings.Module.BusinessObjects.ApplicationUser)
                options.UserLoginInfoType = GetType(ViewSettings.Module.BusinessObjects.ApplicationUserLoginInfo)
                DevExpress.ExpressApp.Security.SecurityOptionsExtensions.UseXpoPermissionsCaching(options)
            End Sub).UsePasswordAuthentication()
            builder.AddBuildStep(Function(application)
                application.ConnectionString = connectionString
#If DEBUG
                If System.Diagnostics.Debugger.IsAttached AndAlso application.CheckCompatibilityType Is CheckCompatibilityType.DatabaseSchema Then
                    application.DatabaseUpdateMode = DevExpress.ExpressApp.DatabaseUpdateMode.UpdateDatabaseAlways
                End If
#End If
            End Function)
            Dim winApplication = builder.Build()
            Return winApplication
        End Function

        Private Function Create() As XafApplication Implements IDesignTimeApplicationFactory.Create
            Return BuildApplication(XafApplication.DesignTimeConnectionString)
        End Function
    End Class
End Namespace
