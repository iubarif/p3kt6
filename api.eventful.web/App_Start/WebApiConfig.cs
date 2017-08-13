using api.eventful.web.ExceptionFilters;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.ExceptionHandling;

namespace api.eventful.web
{
	public static class WebApiConfig
	{
		public static void Register(HttpConfiguration config)
		{
			// Web API configuration and services

			// Web API routes
			config.MapHttpAttributeRoutes();

			config.Routes.MapHttpRoute(
				name: "DefaultApi",
				routeTemplate: "api/{controller}/{id}",
				defaults: new { id = RouteParameter.Optional }
			);

			config.Filters.Add(new GlobalExceptionFilter());

			// Added Custom Logger.  
			config.Services.Replace(typeof(IExceptionLogger), new GlobalExceptionLogger());
			
			var json = config.Formatters.JsonFormatter;

			// Default media type : Json
			json.SupportedMediaTypes.Add(new System.Net.Http.Headers.MediaTypeHeaderValue("text/html"));

			// JSON Formater Camel Case
			json.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
		}
	}
}
