using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace InventoryAPI
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            //config.SuppressDefaultHostAuthentication();
            //config.Filters.Add(new HostAuthenticationFilter(OAuthDefaults.AuthenticationType));

            // Web API routes
            config.MapHttpAttributeRoutes();

            // used for submitting a POST operation to place an order
            config.Routes.MapHttpRoute(
                name: "Shop",
                routeTemplate: "api/{controller}/{action}/{product}",
                defaults: new
                {
                    product = RouteParameter.Optional,
                    quantity = RouteParameter.Optional
                }
            );

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            // remove the XML formatter
            var appXmlType = config.Formatters.XmlFormatter.SupportedMediaTypes.FirstOrDefault(t => t.MediaType == "application/xml");
            config.Formatters.XmlFormatter.SupportedMediaTypes.Remove(appXmlType);
        }
    }
}
