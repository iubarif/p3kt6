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

			searchOptions.Units = WebConfigurationManager.AppSettings[Constants.UNITS];
			searchOptions.page_size = int.Parse(WebConfigurationManager.AppSettings[Constants.PAGESIZE]);

			_serviceContext.QueryString = Utilities.SearchOptionToURL(searchOptions); 

			string url = _serviceContext.ServiceEndPoint;

			string response = string.Empty;

			using (WebClient client = new WebClient())
			{
				response = await client.DownloadStringTaskAsync(new Uri(url));
				var eventfulObject = JsonConvert.DeserializeObject<EventfulRootObject>(response);

				if (eventfulObject.events == null)
					return NotFound();
				else
					return Ok(eventfulObject);
			}
		}		
	}
}
