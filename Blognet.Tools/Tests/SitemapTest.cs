using System;
using System.Xml.Linq;
using Xunit;
using Blognet.Tools.Web.Sitemap;

[assembly: CollectionBehavior(MaxParallelThreads = 1)]
namespace Blognet.Tools.ToolsTests
{
    public class SitemapTest
    {
        [Fact(DisplayName = "Sitemap_ShouldReturnXDocument")]
        public void Sitemap_ShouldReturnXDocument()
        {
            var sitemap = new SitemapDocument();
            var date = new DateTime(2019, 5, 4);
            sitemap.AddUrl(new UrlElement(@"http://localhost", date, ChangeFrequency.MONTHLY, Priority.MEDIUM));

            Assert.IsType<XDocument>(sitemap.GenerateSitemap());
            Assert.NotEmpty(sitemap.GenerateSitemap().ToString());
        }

        [Fact(DisplayName = "Sitemap_ShouldThrowExceptionOnEmptyLocation")]
        public void Sitemap_ShouldThrowExceptionOnEmptyLocation()
        {
            var sitemap = new SitemapDocument();
            var date = new DateTime(2019, 5, 4);
            Assert.Throws<Exception>(() => sitemap.AddUrl(new UrlElement("", date, ChangeFrequency.MONTHLY, Priority.MEDIUM)));
        }
    }
}
