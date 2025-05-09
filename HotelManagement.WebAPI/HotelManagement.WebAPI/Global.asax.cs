using System;
using System.Data.Entity;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;
using HotelManagement.WebAPI.Data;
using HotelManagement.WebAPI;

namespace HotelManagement.WebAPI
{
    public class WebApiApplication : HttpApplication
    {
        protected void Application_Start()
        {
            // Initialize database
            Database.SetInitializer(new DbInitializer());

            // Register Web API configuration
            GlobalConfiguration.Configure(WebApiConfig.Register);

            // Register routes
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }
    }
}