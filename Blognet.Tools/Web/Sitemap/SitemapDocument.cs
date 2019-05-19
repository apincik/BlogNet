using System;
using System.Collections.Generic;
using System.Xml.Linq;

namespace Blognet.Tools.Web.Sitemap
{
    public class SitemapDocument
    {
        public string Filename {
            get {
                return Filename;
            }
            set {
                if (String.IsNullOrEmpty(value))
                {
                    throw new Exception("Sitemap filename cannot be empty or null.");
                }
            }
        }

        private const string ROOT_NAMESPACE = "http://www.sitemaps.org/schemas/sitemap/0.9";
        private XDocument _document;
        private List<XElement> _elements = new List<XElement>();

        public SitemapDocument()
        {
            this.Filename = "sitemap.xml";
        }

        public void AddUrl(UrlElement element)
        {
            _elements.Add(element.GetElement());
        }

        //<urlset xmlns = "http://www.sitemaps.org/schemas/sitemap/0.9" >
            
        /// <summary>
        /// Generates sitemap XML file.
        /// </summary>
        /// <returns></returns>
        public XDocument GenerateSitemap()
        {
            // Init document
            _document = new XDocument();
            // Set UrlSet
            XNamespace rootNamespace = ROOT_NAMESPACE;
            var urlSet = new XElement(rootNamespace + "urlset");
            // Add elements into UrlSet
            _elements.ForEach(e => urlSet.Add(e));
            // Add urlSet into the document
            _document.Add(urlSet);

            return _document;
        }
    }
}
