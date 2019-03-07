using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.ExceptionHandling;
using Unity;
using Unity.Lifetime;
using WebAPI_ForGitHub.DataAccessLayer;
using WebAPI_ForGitHub.Helper;
using WebAPI_ForGitHub.Services;

namespace WebAPI_ForGitHub
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            var container = new UnityContainer();
            container.RegisterType<IProductsDAL, ProductsDAL>(new HierarchicalLifetimeManager());
            container.RegisterType<IProductServices, ProductServices>(new HierarchicalLifetimeManager());
            config.DependencyResolver = new UnityResolver(container);
            config.Services.Add(typeof(IExceptionLogger), new GLobalExceptionLogger());
            config.Services.Replace(typeof(IExceptionHandler), new GlobalExceptionHandler());
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
