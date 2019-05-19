using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace Blognet.Cms.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("CorsPolicy")]
    public class SitemapController : ControllerBase
    {
        private IMediator _mediator;

        public SitemapController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [Route("/")]
        [Produces("application/json")]
        public async Task<IActionResult> FindAllArticles([FromHeader]string projectDomain)
        {
            return Ok();
        }

 
    }
}
