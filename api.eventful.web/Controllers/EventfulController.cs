using api.eventful.classes;
using api.eventful.classes.Eventful;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Threading.Tasks;
using System.Web.Configuration;
using System.Web.Http;

namespace api.eventful.web.Controllers
{
	public class EventfulController : ApiController
    {
		private ServiceContext _serviceContext;
		public EventfulController()
		{
			_serviceContext = new ServiceContext(WebConfigurationManager.AppSettings[Constants.EVENTFULBASEURL],
				WebConfigurationManager.AppSettings[Constants.EVENTFULAPIKEY]);
		}
		
		public async Task<IHttpActionResult> Post(SearchOption searchOptions) {

			_serviceContext.QueryString = Utilities.SearchOptionToURL(searchOptions); 

			string url = _serviceContext.ServiceEndPoint;

			string response = string.Empty;

			using (WebClient client = new WebClient())
			{
				response = await client.DownloadStringTaskAsync(new Uri(url));
				var eventfulObject = JsonConvert.DeserializeObject<RootObject>(response);

				return Ok(eventfulObject);
			}
		}		
	}
}
