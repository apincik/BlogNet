using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Blognet.Cms.Core.Articles.Queries.GetWebForwardedArticle;
using Blognet.Cms.Core.Articles.Models;

namespace Blognet.Cms.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("CorsPolicy")]
    public class ForwardController : ControllerBase
    {
        private IMediator _mediator;

        public ForwardController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [Route("{mask}")]
        [ProducesResponseType(typeof(WebArticleSlugModel), 200)]
        public async Task<IActionResult> GetForwardedArticleByMask([FromHeader]string projectDomain, string mask)
        {
            WebArticleModel result = await _mediator.Send(new GetWebForwardedArticleQuery
            {
                ProjectDomain = projectDomain,
                Mask = mask
            });

            if (result == null)
            {
                return NotFound();
            }

            return Ok(new WebArticleSlugModel {
                Slug = result.Article.Slug
            });
        }
    }
}
