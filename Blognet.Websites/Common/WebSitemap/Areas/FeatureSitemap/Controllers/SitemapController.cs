using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Blognet.Websites.Common.Sitemap.Areas.FeatureSitemap.Controllers
{
    [Area("FeatureSitemap")]
    public class SitemapController : Controller
    {
        public IActionResult Index()
        {
            return Ok();
        }
    }
}
