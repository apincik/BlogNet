using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;

namespace Blognet.Tools.Web.Sitemap
{
    public class UrlElement
    {
        // @review In case of chaning Sitemap namespace, change also url element namespace or property visibility. 
        private const string ELEMENT_NAMESPACE = "http://www.sitemaps.org/schemas/sitemap/0.9";

        private string _location;
        private DateTime _lastModified;
        private ChangeFrequency _changeFrequency;
        private Priority _priority;

        public UrlElement(string location, DateTime lastModified, ChangeFrequency changeFrequency, Priority priority)
        {
            this._location = location;
            this._lastModified = lastModified;
            this._changeFrequency = changeFrequency;
            this._priority = priority;
        }

        /// <summary>
        /// Generate sitemap Url element.
        /// </summary>
        /// <returns></returns>
        public XElement GetElement()
        {
            if(String.IsNullOrEmpty(_location))
            {
                throw new Exception("Invalid sitemap url element data.");
            }

            XNamespace urlNamespace = ELEMENT_NAMESPACE;
            return new XElement(urlNamespace + "url",
                new XElement(urlNamespace + "loc", _location),
                new XElement(urlNamespace + "lastmod", _lastModified.ToString("yyyy-mm-ddThh:mm:ss:zzz")),
                new XElement(urlNamespace + "changefreq", Utils.GetChangeFrequency(_changeFrequency)),
                new XElement(urlNamespace + "priority", Utils.GetPriority(_priority))
            );
        }

        
    }
}
