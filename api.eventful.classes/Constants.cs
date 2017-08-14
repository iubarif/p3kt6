using System.Collections.Generic;

namespace api.eventful.classes
{
	public static class  Constants
	{
		public static string GEOCODEAPIKEY = "GEOCodeAPIKey";
		public static string GEOCODEBASEURL = "GEOCodeBaseURL";

		public static string EVENTFULAPIKEY = "EventfulAPIKey";
		public static string EVENTFULBASEURL = "EventfulBaseURL";

		public static string GEOCODESuccess = "OK";

		public static  Dictionary<string, string> POCOJsonMap = new Dictionary<string, string>() {
			{ "Address", "location" },
			{ "Radius", "within" },
			{ "Lat", "location"},
			{ "Lng", "location"},
			{ "DateStart", "date" },
			{ "DateEnd", "date" },
			{ "Category", "category" }
		};

		public static string DateStart = "DateStart";
		public static string DateEnd = "DateEnd";
		public static string Lat = "Lat";
		public static string Lng = "Lng";

		public static string DATEFormat = "yyyyMMdd";
	}
}
