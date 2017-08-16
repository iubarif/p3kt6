using api.eventful.classes.Geocode;
using System;

namespace api.eventful.classes
{
	public class SearchOption
	{
		public string Address { get; set; }
		public double Lat { get; set; }
		public double Lng { get; set; }
		public int Radius { get; set; }
		public string DateStart { get; set; }
		public string  DateEnd { get; set; }
		public string Category { get; set; }
	}
}
