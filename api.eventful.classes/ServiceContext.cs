namespace api.eventful.classes
{
	public class ServiceContext
	{
		public ServiceContext(string baseUrl,string apiKey)
		{
			this.BaseURL = baseUrl;
			this.APIKey = apiKey;
		}

		public string BaseURL { get;}
		public string APIKey { get;}

		public string QueryString { get; set; }
		public string ServiceEndPoint
		{
			get
			{
				if (!string.IsNullOrEmpty(this.BaseURL) &&
					!string.IsNullOrEmpty(this.APIKey) &&
					!string.IsNullOrEmpty(this.QueryString)
					) {
					return string.Format("{0}{1}&{2}", this.BaseURL, this.QueryString, this.APIKey);
				}

				return string.Empty;

			}
		}
			
	}
}
