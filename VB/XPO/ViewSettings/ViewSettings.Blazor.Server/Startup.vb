Imports DevExpress.ExpressApp.Security
Imports DevExpress.ExpressApp.ApplicationBuilder
Imports DevExpress.ExpressApp.Blazor.ApplicationBuilder
Imports DevExpress.ExpressApp.Blazor.Services
Imports DevExpress.Persistent.Base
Imports Microsoft.AspNetCore.Authentication.Cookies
Imports Microsoft.AspNetCore.Components.Server.Circuits
Imports DevExpress.ExpressApp.Xpo
Imports ViewSettings.Blazor.Server.Services
Imports DevExpress.Persistent.BaseImpl.PermissionPolicy
Imports DevExpress.ExpressApp.Core

Namespace ViewSettings.Blazor.Server

    Public Class Startup

        Public Sub New(ByVal configuration As IConfiguration)
            Me.Configuration = configuration
        End Sub

        Public ReadOnly Property Configuration As IConfiguration

        ' This method gets called by the runtime. Use this method to add services to the container.
        ' For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        Public Sub ConfigureServices(ByVal services As IServiceCollection)
            services.AddSingleton(GetType(Microsoft.AspNetCore.SignalR.HubConnectionHandler(Of )), GetType(ProxyHubConnectionHandler(Of )))
            services.AddRazorPages()
            services.AddServerSideBlazor()
            services.AddHttpContextAccessor()
            services.AddScoped(Of CircuitHandler, CircuitHandlerProxy)()
            services.AddXaf(Configuration, Function(builder)
                builder.UseApplication(Of ViewSettingsBlazorApplication)()
                builder.Modules.AddConditionalAppearance().AddValidation(Function(options)
                    options.AllowValidationDetailsAccess = False
                End Function).AddViewVariants().Add(Of ViewSettings.Module.ViewSettingsModule)().Add(Of ViewSettingsBlazorModule)()
                builder.ObjectSpaceProviders.AddSecuredXpo(Function(serviceProvider, options)
                    Dim connectionString As String = Nothing
                    If Configuration.GetConnectionString("ConnectionString") IsNot Nothing Then
                        connectionString = Configuration.GetConnectionString("ConnectionString")
                    End If

#If EASYTEST
                    if(Configuration.GetConnectionString("EasyTestConnectionString") != null) {
                        connectionString = Configuration.GetConnectionString("EasyTestConnectionString");
                    }
#End If
                    ArgumentNullException.ThrowIfNull(connectionString)
                    options.ConnectionString = connectionString
                    options.ThreadSafe = True
                    options.UseSharedDataStoreProvider = True
                End Function).AddNonPersistent()
                builder.Security.UseIntegratedMode(Function(options)
                    options.RoleType = GetType(PermissionPolicyRole)
                    ' ApplicationUser descends from PermissionPolicyUser and supports the OAuth authentication. For more information, refer to the following topic: https://docs.devexpress.com/eXpressAppFramework/402197
                    ' If your application uses PermissionPolicyUser or a custom user type, set the UserType property as follows:
                    options.UserType = GetType(ViewSettings.Module.BusinessObjects.ApplicationUser)
                    ' ApplicationUserLoginInfo is only necessary for applications that use the ApplicationUser user type.
                    ' If you use PermissionPolicyUser or a custom user type, comment out the following line:
                    options.UserLoginInfoType = GetType(ViewSettings.Module.BusinessObjects.ApplicationUserLoginInfo)
                    options.UseXpoPermissionsCaching()
                End Function).AddPasswordAuthentication(Function(options)
                    options.IsSupportChangePassword = True
                End Function)
            End Function)
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(Function(options)
                options.LoginPath = "/LoginPage"
            End Function)
        End Sub

        ' This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        Public Sub Configure(ByVal app As IApplicationBuilder, ByVal env As IWebHostEnvironment)
            If env.IsDevelopment() Then
                app.UseDeveloperExceptionPage()
            Else
                app.UseExceptionHandler("/Error")
                ' The default HSTS value is 30 days. To change this for production scenarios, see: https://aka.ms/aspnetcore-hsts.
                app.UseHsts()
            End If

            app.UseHttpsRedirection()
            app.UseRequestLocalization()
            app.UseStaticFiles()
            app.UseRouting()
            app.UseAuthentication()
            app.UseAuthorization()
            app.UseXaf()
            app.UseEndpoints(Function(endpoints)
                endpoints.MapXafEndpoints()
                endpoints.MapBlazorHub()
                endpoints.MapFallbackToPage("/_Host")
                endpoints.MapControllers()
            End Function)
        End Sub
    End Class
End Namespace
