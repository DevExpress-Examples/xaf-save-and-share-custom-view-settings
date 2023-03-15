Imports System.Configuration
Imports DevExpress.ExpressApp
Imports DevExpress.ExpressApp.Win
Imports DevExpress.Persistent.Base
Imports DevExpress.XtraEditors
Imports DevExpress.ExpressApp.Utils
Imports DevExpress.ExpressApp.Win.Utils
Imports System.Reflection

Namespace ViewSettings.Win

    Friend Module Program

        Private Function ContainsArgument(ByVal args As String(), ByVal argument As String) As Boolean
            Return args.Any(Function(arg) arg.TrimStart("/"c).TrimStart("-"c).ToLower() Is argument.ToLower())
        End Function

        ''' <summary>
        ''' The main entry point for the application.
        ''' </summary>
        <STAThread>
        Public Function Main(ByVal args As String()) As Integer
            If ContainsArgument(args, "help") OrElse ContainsArgument(args, "h") Then
                Console.WriteLine("Updates the database when its version does not match the application's version.")
                Console.WriteLine()
                Console.WriteLine($"    {Assembly.GetExecutingAssembly().GetName().Name}.exe --updateDatabase [--forceUpdate --silent]")
                Console.WriteLine()
                Console.WriteLine("--forceUpdate - Marks that the database must be updated whether its version matches the application's version or not.")
                Console.WriteLine("--silent - Marks that database update proceeds automatically and does not require any interaction with the user.")
                Console.WriteLine()
                Console.WriteLine($"Exit codes: 0 - {DBUpdaterStatus.UpdateCompleted}")
                Console.WriteLine($"            1 - {DBUpdaterStatus.UpdateError}")
                Console.WriteLine($"            2 - {DBUpdaterStatus.UpdateNotNeeded}")
                Return 0
            End If

            FrameworkSettings.DefaultSettingsCompatibilityMode = FrameworkSettingsCompatibilityMode.Latest
#If EASYTEST
        DevExpress.ExpressApp.Win.EasyTest.EasyTestRemotingRegistration.Register();
#End If
            Call WindowsFormsSettings.LoadApplicationSettings()
            Application.EnableVisualStyles()
            Application.SetCompatibleTextRenderingDefault(False)
            DevExpress.Utils.ToolTipController.DefaultController.ToolTipType = DevExpress.Utils.ToolTipType.SuperTip
            If Tracing.GetFileLocationFromSettings() Is DevExpress.Persistent.Base.FileLocation.CurrentUserApplicationDataFolder Then
                Tracing.LocalUserAppDataPath = Application.LocalUserAppDataPath
            End If

            Call Tracing.Initialize()
            Dim connectionString As String = Nothing
            If ConfigurationManager.ConnectionStrings("ConnectionString") IsNot Nothing Then
                connectionString = ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString
            End If

#If EASYTEST
        if(ConfigurationManager.ConnectionStrings["EasyTestConnectionString"] != null) {
            connectionString = ConfigurationManager.ConnectionStrings["EasyTestConnectionString"].ConnectionString;
        }
#End If
            ArgumentNullException.ThrowIfNull(connectionString)
            Dim winApplication = ApplicationBuilder.BuildApplication(connectionString)
            If ContainsArgument(args, "updateDatabase") Then
                Dim dbUpdater = New WinDBUpdater(Function() winApplication)
                Return dbUpdater.Update(forceUpdate:=ContainsArgument(args, "forceUpdate"), silent:=ContainsArgument(args, "silent"))
            End If

            Try
                winApplication.Setup()
                winApplication.Start()
            Catch e As Exception
                winApplication.StopSplash()
                winApplication.HandleException(e)
            End Try

            Return 0
        End Function
    End Module
End Namespace
