Imports System
Imports System.Configuration
Imports System.Windows.Forms
Imports DevExpress.ExpressApp
Imports DevExpress.ExpressApp.Security
Imports DevExpress.Persistent.Base

Namespace ViewSettingsSolution.Win

    Friend Module Program

        ''' <summary>
        ''' The main entry point for the application.
        ''' </summary>
        <STAThread>
        Sub Main()
#If EASYTEST
            DevExpress.ExpressApp.Win.EasyTest.EasyTestRemotingRegistration.Register();
#End If
            Call Application.EnableVisualStyles()
            Application.SetCompatibleTextRenderingDefault(False)
            EditModelPermission.AlwaysGranted = System.Diagnostics.Debugger.IsAttached
            If Tracing.GetFileLocationFromSettings() = DevExpress.Persistent.Base.FileLocation.CurrentUserApplicationDataFolder Then
                Tracing.LocalUserAppDataPath = Application.LocalUserAppDataPath
            End If

            Call Tracing.Initialize()
            Dim winApplication As ViewSettingsSolutionWindowsFormsApplication = New ViewSettingsSolutionWindowsFormsApplication()
            ' Refer to the https://documentation.devexpress.com/eXpressAppFramework/CustomDocument112680.aspx help article for more details on how to provide a custom splash form.
            'winApplication.SplashScreen = new DevExpress.ExpressApp.Win.Utils.DXSplashScreen("YourSplashImage.png");
            Call SecurityAdapterHelper.Enable()
            If ConfigurationManager.ConnectionStrings("ConnectionString") IsNot Nothing Then
                winApplication.ConnectionString = ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString
            End If

#If EASYTEST
            if(ConfigurationManager.ConnectionStrings["EasyTestConnectionString"] != null) {
                winApplication.ConnectionString = ConfigurationManager.ConnectionStrings["EasyTestConnectionString"].ConnectionString;
            }
#End If
            If System.Diagnostics.Debugger.IsAttached AndAlso winApplication.CheckCompatibilityType = CheckCompatibilityType.DatabaseSchema Then
                winApplication.DatabaseUpdateMode = DatabaseUpdateMode.UpdateDatabaseAlways
            End If

            Try
                winApplication.Setup()
                winApplication.Start()
            Catch e As Exception
                winApplication.HandleException(e)
            End Try
        End Sub
    End Module
End Namespace
