using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Configuration;
using System.Web.Http;
using api.eventful.classes;
using api.eventful.classes.Geocode;

namespace api.eventful.web.Controllers
{
    public class GeocodeController : ApiController
    {
		public async Task<IHttpActionResult>  Get(string address)
		{
			address = HttpUtility.HtmlEncode(address);

			string apikey = WebConfigurationManager.AppSettings["GEOCodeAPIKey"]; // "&key=AIzaSyADQVmqQa8yJvTecspICTBFowA79JHxV-E";
			string baseUrl = WebConfigurationManager.AppSettings["GEOCodeBaseURL"]; //@"https://maps.googleapis.com/maps/api/geocode/json?address=";
			string url = string.Format("{0}{1}&{2}", baseUrl, address, apikey);

			string response = string.Empty;

			using (WebClient client = new WebClient())
			{
				response = await client.DownloadStringTaskAsync(new Uri(url));
				var geoCodeRecord = JsonConvert.DeserializeObject<RootObject>(response);

				if (geoCodeRecord.status == "OK")
				{
					var lng = geoCodeRecord.results.Select(e => e.geometry.location.lng).FirstOrDefault();
					var lat = geoCodeRecord.results.Select(e => e.geometry.location.lat).FirstOrDefault();

					//Console.WriteLine("Return OK. Lang : {0} , Lat : {1}", lng, lat);

					return Ok(new Location { lat = lat ,lng= lng });
				}
				else
				{
					//Console.WriteLine("Error Code : {0}", geoCodeRecord.status);
					return NotFound();
				}
			}

			return NotFound();
		}
	}
}
