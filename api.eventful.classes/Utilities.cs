using System;
using System.Collections.Generic;
using System.Text;

namespace api.eventful.classes
{
	public static class Utilities
	{
		/// <summary>
		/// Extension Method for Dictionary <string, T>.
		/// If dictionary.containsKey(key) then Remove that pair.and add new given key value pair.
		/// </summary>
		/// <typeparam name="T">Any type</typeparam>
		/// <param name="dictionary">this Dictionary</param>
		/// <param name="key">Key</param>
		/// <param name="value">Value</param>
		public static void AddToDictionary<T>(this Dictionary<string,T> dictionary, string key, T value ) {
			if (dictionary.ContainsKey(key))
				dictionary.Remove(key);

			dictionary.Add(key,value);
		}

		/// <summary>
		/// For a given SearchOption object generates Query String
		/// </summary>
		/// <param name="searchOption"></param>
		/// <returns>query string</returns>
		public static string SearchOptionToURL(SearchOption searchOption)
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
					if (propertyName == Constants.Lat || propertyName == Constants.Lng)
					{
						geoCordinate.AddToDictionary(propertyName, propertyValue.ToString());
					}
					else if (propertyValue.GetType() != typeof(DateTime))
					{
						queryStringParts.AddToDictionary(pocoJsonMap[propertyName], propertyValue.ToString());
					}
					else
					{
						DateTime date;

						if (DateTime.TryParse(propertyValue.ToString(), out date))
						{
							dateStore.AddToDictionary(propertyName, date.ToString(Constants.DATEFormat));
						}
						else {
							throw new Exception("Invalid DateTime.!!");
						}
					}
				}
			}

			// Prepare Geo coordinate object. Ex:  32.746682,-117.162741 
			if (geoCordinate.Count == 2
				&& geoCordinate.ContainsKey(Constants.Lat)
				&& geoCordinate.ContainsKey(Constants.Lng)
				&& pocoJsonMap.ContainsKey(Constants.Lat)
				)
			{
				var geoCords = string.Format("{0},{1}", geoCordinate[Constants.Lat], geoCordinate[Constants.Lng]);

				queryStringParts.AddToDictionary(pocoJsonMap[Constants.Lat], geoCords);
			}
			else
			{
				throw new Exception("Invalid Latitude or Longitude..");
			}

			// Prepare date range YYYYMMDD-YYYYMMDD.Ex:  20170101 - 20170102
			if (dateStore.Count == 2
				&& dateStore.ContainsKey(Constants.DateStart) 
				&& dateStore.ContainsKey(Constants.DateEnd)
				&& pocoJsonMap.ContainsKey(Constants.DateStart)
				)
			{
				var dateQs = string.Format("{0}-{1}", dateStore[Constants.DateStart], dateStore[Constants.DateEnd]);

				queryStringParts.AddToDictionary(pocoJsonMap[Constants.DateStart], dateQs);
			}
			else
			{
				throw new Exception("Invalid dates..");
			}

			// Go through all search parameter and prepare query string
			foreach (var pair in queryStringParts)
			{
				queryString.Append(string.Format("{0}{1}={2}", queryString.Length == 0 ? "" : "&", pair.Key, pair.Value));
			}

			return queryString.ToString();
		}
	}
}
