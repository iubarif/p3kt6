﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace api.eventful.web.Controllers
{
    public class CategoriesController : ApiController
    {        
		public HttpResponseMessage Get()
		{
			var path = HttpContext.Current.Server.MapPath(@"/StaticData/categories.json");
			var json = System.IO.File.ReadAllText(path);

			return new HttpResponseMessage()
			{
				Content = new StringContent(json, Encoding.UTF8, "application/json"),
				StatusCode = HttpStatusCode.OK
			};
		}
	}
}
