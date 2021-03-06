﻿using System.Collections.Generic;

namespace api.eventful.classes
{
	public static class  Constants
	{
		public static string GEOCODEAPIKEY = "GEOCodeAPIKey";
		public static string GEOCODEBASEURL = "GEOCodeBaseURL";

		public static string EVENTFULAPIKEY = "EventfulAPIKey";
		public static string EVENTFULBASEURL = "EventfulBaseURL";

		public static string UNITS = "Units";
		public static string GEOCODESuccess = "OK";
		public static string PAGESIZE = "PageSize";

		public static  Dictionary<string, string> POCOJsonMap = new Dictionary<string, string>() {
			{ "Address", "location" },
			{ "Radius", "within" },
			{ "Lat", "location"},
			{ "Lng", "location"},
			{ "DateStart", "date" },
			{ "DateEnd", "date" },
			{ "Category", "category" },
			{ "page_number", "page_number" },
			{ "page_size", "page_size" },
			{ "page_count", "page_count" },
			{ "Units", "units" }			
		};

		public static string DateStart = "DateStart";
		public static string DateEnd = "DateEnd";
		public static string Lat = "Lat";
		public static string Lng = "Lng";

		public static string DATEFormat = "yyyyMMdd";
	}
}
