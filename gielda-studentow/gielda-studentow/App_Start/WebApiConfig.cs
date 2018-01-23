using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Http;
using gielda_studentow.Controllers;
using gielda_studentow.Service.Implementation;
using gielda_studentow.Service.Interface;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.DataHandler;
using Microsoft.Owin.Security.DataHandler.Encoder;
using Microsoft.Owin.Security.DataHandler.Serializer;
using Microsoft.Owin.Security.OAuth;
using Microsoft.Practices.Unity;
using Newtonsoft.Json;
using StudentExchangeDataAccess.Context;

namespace gielda_studentow
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            // Configure Web API to use only bearer token authentication.
            config.SuppressDefaultHostAuthentication();
            config.Filters.Add(new HostAuthenticationFilter(OAuthDefaults.AuthenticationType));

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
            var jsonSerializerSettings = new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                NullValueHandling = NullValueHandling.Ignore
            };
            config.Formatters.JsonFormatter.SerializerSettings = jsonSerializerSettings;

            var container = new UnityContainer();
            container.RegisterType<IStudentExchangeDataContext, StudentExchangeDataContext>(new PerResolveLifetimeManager());

            container
                .RegisterType<IStudentService, StudentService>(new TransientLifetimeManager())
                .RegisterType<IAnnouncementService, AnnouncementService>(new TransientLifetimeManager())
                .RegisterType<IMessageService, MessageService>(new TransientLifetimeManager())
                .RegisterType<IGroupService, GroupService>(new TransientLifetimeManager())
                .RegisterType<ICourseOfStudyService, CourseOfStudyService>(new TransientLifetimeManager())
                .RegisterType<IFacultyService, FacultyService>(new TransientLifetimeManager())
                .RegisterType<IUniversityService, UniversityService>(new TransientLifetimeManager())
                .RegisterType<IAuthenticatonService, AuthenticationService>(new TransientLifetimeManager())
                .RegisterType<IProfileService, ProfileService>(new TransientLifetimeManager())
                .RegisterType<ITutorService, TutorService>(new TransientLifetimeManager())
                ;

            config.DependencyResolver = new UnityDependencyResolver.Lib.UnityWebApiDependencyResolver(container);
           
        }
    }
}
