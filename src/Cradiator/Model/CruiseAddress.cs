using System;
using System.Collections.Generic;
using System.Linq;
using Cradiator.Config;
using Cradiator.Extensions;
using Ninject;

namespace Cradiator.Model
{
	public class CruiseAddress
	{
	    [Inject]
		public CruiseAddress(IConfigSettings settings)
		{
			Url = settings.URL;
		}

		public CruiseAddress(string url)
		{
			Url = url;
		}

		public string Url { get; set; }

        public IEnumerable<Uri> UriList
        {
            get
            {
                return from url in Url.Split(new [] {'|', ' ', ';'})
                       select new CruiseAddress(url).Uri;
            }
        }

		public Uri Uri
		{
			get 
			{
				var uri = Url.Trim();

				if (uri.Contains("ccnet") && !uri.EndsWith("/XmlStatusReport.aspx"))
					uri += "/XmlStatusReport.aspx";

				return new Uri(uri);
			}
		}

		public bool Valid
		{
			get { return !Url.IsEmpty(); }
		}

        public bool Invalid
        {
            get { return !Valid; }
        }

		public bool IsDebug
		{
			get { return Valid && Url.ToLower().StartsWith("debug"); }
		}
	}
}