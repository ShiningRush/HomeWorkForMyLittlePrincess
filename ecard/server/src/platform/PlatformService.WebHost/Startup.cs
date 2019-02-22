using Abp;
using Abp.Dependency;
using Abp.WebApi.Configuration;
using Microsoft.Owin;
using Microsoft.Owin.Security.Infrastructure;
using Microsoft.Owin.Security.OAuth;
using Owin;
using PlatformService.Host.AppStart;
using PlatformService.Host.HttpPreHandle;
using PlatformService.WebHost;
using Swashbuckle.Application;
using Swashbuckle.Swagger;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using Microsoft.Owin.StaticFiles;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using Microsoft.Owin.FileSystems;
using Microsoft.Owin.Extensions;
using PlatformService.BridgeComponent.EntityFramework;
using System.Xml;
using System.Web.Configuration;
using PlatformService.WebHost.ApiControllers;

[assembly: OwinStartup(typeof(PlatformService.Host.Startup))]
namespace PlatformService.Host
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            using (var abpBootstrapper = AbpBootstrapper.Create<PlatformServiceWebHostModule>())
            {
                abpBootstrapper.InitHostAbpSetting();

                app.UseFileServer();
                app.UseStaticFiles(new StaticFileOptions()
                {
                    ServeUnknownFileTypes = true
                });

                app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());
                app.Use<HttpContextPreHandler>();

                // Setup Authorization Server
                app.UseOAuthAuthorizationServer(new OAuthAuthorizationServerOptions
                {
                    AuthorizeEndpointPath = new PathString(Paths.AuthorizePath),
                    TokenEndpointPath = new PathString(Paths.TokenPath),
                    ApplicationCanDisplayErrors = true,
                    AccessTokenExpireTimeSpan = TimeSpan.FromMinutes(10),
                    AllowInsecureHttp = true,
                    // Authorization server provider which controls the lifecycle of Authorization Server
                    Provider = new OAuthAuthorizationServerProvider
                    {
                        OnValidateClientRedirectUri = AuthorizaActions.ValidateClientRedirectUri,
                        OnValidateClientAuthentication = AuthorizaActions.ValidateClientAuthentication,
                        OnGrantResourceOwnerCredentials = AuthorizaActions.GrantResourceOwnerCredentials,
                        OnGrantCustomExtension = AuthorizaActions.GrantCustomCredetails,
                        OnGrantClientCredentials = AuthorizaActions.GrantClientCredetails
                    },

                    // Authorization code provider which creates and receives authorization code
                    AuthorizationCodeProvider = new AuthenticationTokenProvider
                    {
                        OnCreate = AuthorizaActions.CreateAuthenticationCode,
                        OnReceive = AuthorizaActions.ReceiveAuthenticationCode,
                    },

                    // Refresh token provider which creates and receives referesh token
                    RefreshTokenProvider = new AuthenticationTokenProvider
                    {
                        OnCreate = AuthorizaActions.CreateRefreshToken,
                        OnReceive = AuthorizaActions.ReceiveRefreshToken,
                    }
                });

                // Configure Web API for self - host.
                HttpConfiguration config = IocManager.Instance.Resolve<IAbpWebApiConfiguration>().HttpConfiguration;
                config.Routes.MapHttpRoute(
                    name: "DefaultApi",
                    routeTemplate: "subapi/{controller}/{action}/{id}",
                    defaults: new { id = RouteParameter.Optional });

                config.EnableSwagger(c =>
                {
                    c.OAuth2("oauth2")
                    .Description("OAuth2 接口授权验证")
                    .Flow("password")
                    .AuthorizationUrl("/Api/Authorization/Authorize")
                    .TokenUrl("/Api/Authorization/Token");

                    c.OperationFilter<AssignOAuth2SecurityRequirements>();
                    c.SingleApiVersion("v1", "Clear.ECardSystem.Api");
                    var xmlFiles = Directory.GetFiles(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Plugins"), "*.xml", SearchOption.AllDirectories);
                    foreach(var aXmlFile in xmlFiles)
                        c.IncludeXmlComments(aXmlFile);

                }).EnableSwaggerUi(c=>{ 
                    c.EnableOAuth2Support("swagger", "test-realm", "Swagger UI");
                });

                app.UseWebApi(config);
                app.UseStageMarker(PipelineStage.Authorize);

                app.Run(context =>
                {
                    context.Response.ContentType = "text/plain";
                    return context.Response.WriteAsync("Hello, world.");
                });

                // 程序自检
                abpBootstrapper.IocManager.Resolve<SystemInitController>().GetDbStatus();
            }
        }
        public class AssignOAuth2SecurityRequirements : IOperationFilter
        {
            public void Apply(Operation operation, SchemaRegistry schemaRegistry, ApiDescription apiDescription)
            {
                var actFilters = apiDescription.ActionDescriptor.GetFilterPipeline();
                var allowsAnonymous = actFilters.Select(f => f.Instance).OfType<OverrideAuthorizationAttribute>().Any();
                if (allowsAnonymous)
                    return; // must be an anonymous method

                if (operation.security == null)
                    operation.security = new List<IDictionary<string, IEnumerable<string>>>();

                var oAuthRequirements = new Dictionary<string, IEnumerable<string>>
                                        {
                                            {"oauth2", new List<string> { "read", "write"}}
                                        };

                operation.security.Add(oAuthRequirements);
            }
        }
    }
}
