using Newtonsoft.Json;
using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Configuration;
using System.Web.Http;
using api.eventful.classes;
using api.eventful.classes.Geocode;
using api.eventful.web.ExceptionFilters;

namespace api.eventful.web.Controllers
{
	public class GeocodeController : ApiController
    {
		private ServiceContext _serviceContext;

		public GeocodeController()
		{
			_serviceContext = new ServiceContext(WebConfigurationManager.AppSettings[Constants.GEOCODEBASEURL],
				WebConfigurationManager.AppSettings[Constants.GEOCODEAPIKEY]);
		}

		public async Task<IHttpActionResult>  Get(string address)
		{
			//throw new Exception("My Error ....");

			address = HttpUtility.HtmlEncode(address);
			
			_serviceContext.QueryString = address;

			string url = _serviceContext.ServiceEndPoint; 

			string response = string.Empty;

			using (WebClient client = new WebClient())
			{
				response = await client.DownloadStringTaskAsync(new Uri(url));
				var geoCodeRecord = JsonConvert.DeserializeObject<RootObject>(response);

				if (geoCodeRecord.status.Equals(Constants.GEOCODESuccess, StringComparison.OrdinalIgnoreCase)) // "OK"
				{
					var lng = geoCodeRecord.results.Select(e => e.geometry.location.lng).FirstOrDefault();
					var lat = geoCodeRecord.results.Select(e => e.geometry.location.lat).FirstOrDefault();

					return Ok(new Location { lat = lat ,lng= lng });
				}
				else
				{
					return BadRequest(geoCodeRecord.status);
				}
			}			
		}
	}
}
