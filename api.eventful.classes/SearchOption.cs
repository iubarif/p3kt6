using api.eventful.classes.Geocode;
using System;

namespace api.eventful.classes
{
	public class SearchOption
	{
		public string Address { get; set; }
		public Location Location { get; set; }
		public int Radius { get; set; }
		public DateTime DateStart { get; set; }
		public DateTime  DateEnd { get; set; }
		public string Category { get; set; }
	}
}
