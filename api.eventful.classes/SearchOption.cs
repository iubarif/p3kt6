using api.eventful.classes.Geocode;
using System;

namespace api.eventful.classes
{
	public class SearchOption
	{
		// Search metadata
		public string Address { get; set; }
		public double Lat { get; set; }
		public double Lng { get; set; }
		public int Radius { get; set; }
		public string DateStart { get; set; }
		public string  DateEnd { get; set; }
		public string Category { get; set; }
		public string Units { get; set; }

		// Page payload
		public int page_number { get; set; }
		public int page_size { get; set; }		
		public int page_count { get; set; }
	}
}
