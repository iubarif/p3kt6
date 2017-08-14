using api.eventful.classes;
using api.eventful.classes.Eventful;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
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
				WebConfigurationManager.AppSettings[Constants.EVENTFULAPIKEY]);
		}
		
		public async Task<IHttpActionResult> Post(SearchOption searchOptions) {

			_serviceContext.QueryString = SearchOptionToURL(searchOptions); 

			string url = _serviceContext.ServiceEndPoint;

			string response = string.Empty;

			using (WebClient client = new WebClient())
			{
				response = await client.DownloadStringTaskAsync(new Uri(url));
				var eventfulObject = JsonConvert.DeserializeObject<RootObject>(response);

				return Ok(eventfulObject);
			}
		}

		private string SearchOptionToURL(SearchOption searchOption)
		{
			Dictionary<string, string> pocoJsonMap = Constants.POCOJsonMap;
			Dictionary<string, string> queryStringParts = new Dictionary<string, string>();
			Dictionary<string, string> dateStore = new Dictionary<string, string>();
			Dictionary<string, string> geoCordinate = new Dictionary<string, string>();

			var properties = typeof(SearchOption).GetProperties();

			StringBuilder queryString = new StringBuilder();

			foreach (var property in properties)
			{
				var propertyName = property.Name;
				var propertyValue = searchOption.GetType().GetProperty(propertyName).GetValue(searchOption);

				if (pocoJsonMap.ContainsKey(propertyName) && propertyValue != null)
				{
					if(propertyName == Constants.Lat || propertyName == Constants.Lng) {
						
						if (geoCordinate.ContainsKey(propertyName))
							geoCordinate.Remove(propertyName);
						else
							geoCordinate.Add(propertyName, propertyValue.ToString());
					}
					else if (propertyValue.GetType() != typeof(DateTime))
					{

						var currentKey = pocoJsonMap[propertyName];

						if (queryStringParts.ContainsKey(currentKey))
							queryStringParts.Remove(currentKey);
						else
							queryStringParts.Add(currentKey, propertyValue.ToString());
					}
					else {
						DateTime date;

						if (DateTime.TryParse(propertyValue.ToString(),out date))
						{
							if (dateStore.ContainsKey(propertyName))
								dateStore.Remove(propertyName);
							else
								dateStore.Add(propertyName, date.ToString(Constants.DATEFormat));
						} 
					}
				}
			}

			// http://api.eventful.com/json/events/search?location=32.746682, -117.162741&within=1&category=books
			if (geoCordinate.Count == 2
				&& geoCordinate.ContainsKey(Constants.Lat)
				&& geoCordinate.ContainsKey(Constants.Lng)
				&& pocoJsonMap.ContainsKey(Constants.Lat)
				)
			{
				var geoCords = string.Format("{0},{1}", geoCordinate[Constants.Lat], geoCordinate[Constants.Lng]);
				var currentKey = pocoJsonMap[Constants.Lat];

				if (queryStringParts.ContainsKey(currentKey))
					queryStringParts.Remove(currentKey);

				queryStringParts.Add(currentKey, geoCords);
			}
			else
			{
				throw new Exception("Invalid Latitude or Longitude..");
			}




			if (dateStore.Count == 2
				&& dateStore.ContainsKey(Constants.DateStart)   // "DateStart" 
				&& dateStore.ContainsKey(Constants.DateEnd)     //	"DateEnd"
				&& pocoJsonMap.ContainsKey(Constants.DateStart) //	"DateStart"
				)
			{
				var dateQs = string.Format("{0}-{1}", dateStore[Constants.DateStart], dateStore[Constants.DateEnd]);
				var currentKey = pocoJsonMap[Constants.DateStart];

				if (queryStringParts.ContainsKey(currentKey))
					queryStringParts.Remove(currentKey);

				queryStringParts.Add(currentKey, dateQs);
			}
			else
			{
				throw new Exception("Invalid dates..");
			}

			foreach (var pair in queryStringParts) {
				queryString.Append(string.Format("{0}{1}={2}", queryString.Length==0?"":"&", pair.Key, pair.Value));
			}

			return queryString.ToString();
		}

		private void AddToDictionary() { }
	}
}
