using api.eventful.classes;
using api.eventful.classes.Eventful;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Threading.Tasks;
using System.Web;
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
				WebConfigurationManager.AppSettings[Constants.GEOCODEAPIKEY]);
		}

		public async Task<IHttpActionResult> Get(string searchOptions) {

			_serviceContext.QueryString = HttpUtility.HtmlEncode(searchOptions); ;
			
			string url = _serviceContext.ServiceEndPoint;

			string response = string.Empty;

			using (WebClient client = new WebClient())
			{
				response = await client.DownloadStringTaskAsync(new Uri(url));
				var eventfulObject = JsonConvert.DeserializeObject<RootObject>(response);

				if (eventfulObject.events.@event.Count > 0)
				{					
					return Ok(eventfulObject);
				}
				else
				{
					return NotFound();
				}
			}
		}



		//var searchOption = new SearchOption {
		//	Address = "1016 Grob Court, Victoria, BC",
		//	Category = "books",
		//	Radius=10				
		//};

		//return Ok(SearchOptionToURL(searchOption));

		//private string SearchOptionToURL(SearchOption searchOption) {

		//	string apikey = WebConfigurationManager.AppSettings["EventfulAPIKey"]; 
		//	string baseUrl = WebConfigurationManager.AppSettings["EventfulBaseURL"];

		//	Dictionary<string, string> pocoJsonMap = new Dictionary<string, string>();
		//	pocoJsonMap.Add("Address", "location");
		//	pocoJsonMap.Add("Radius", "within");
		//	pocoJsonMap.Add("DateStart", "date");
		//	pocoJsonMap.Add("DateEnd", "date");			
		//	pocoJsonMap.Add("Category", "category");

		//	var properties = typeof(SearchOption).GetProperties();

		//	StringBuilder queryString = new StringBuilder();

		//	foreach (var property in properties) {
		//		var propertyName = property.Name;
		//		var propertyValue = searchOption.GetType().GetProperty(propertyName).GetValue(searchOption);

		//		if (pocoJsonMap.ContainsKey(property.Name) && propertyValue!=null) {
		//			queryString.Append(string.Format("{0}={1}&", pocoJsonMap[property.Name],
		//				propertyValue.ToString()));
		//		}
		//	}

		//	string url = string.Format("{0}{1}{2}", baseUrl, queryString.ToString(), apikey);

		//	return url;
		//}
	}
}
