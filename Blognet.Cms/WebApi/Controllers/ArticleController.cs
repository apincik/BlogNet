using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Blognet.Cms.Core.Articles.Queries.GetWebCategoryArticles;
using Blognet.Cms.Core.Articles.Models;
using Blognet.Cms.Core.Articles.Queries.GetWebArticle;

namespace Blognet.Cms.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("CorsPolicy")]
    public class ArticleController : ControllerBase
    {
        private IMediator _mediator;

        public ArticleController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [Route("Category/{title}/{limit}/{page}")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(WebCategoryArticlesModel), 200)]
        public async Task<IActionResult> FindCategoryArticles([FromHeader]string projectDomain, string title = "news", int limit = 10, int page = 0)
        {
            WebCategoryArticlesModel result = await _mediator.Send(new GetWebCategoryArticlesQuery
            {
                ProjectDomain = projectDomain,
                CategoryTitle = title,
                Limit = limit,
                Page = page
            });

            return Ok(result);
        }

        [HttpGet]
        [Route("{slug}")]
        [ProducesResponseType(typeof(WebArticleModel), 200)]
        public async Task<IActionResult> GetArticleBySlug([FromHeader]string projectDomain, string slug)
        {
            WebArticleModel result = await _mediator.Send(new GetWebArticleQuery
            {
                ProjectDomain = projectDomain,
                Slug = slug
            });

            if(result.Article == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        [HttpGet]
        [Route("Slug/{id}")]
        [ProducesResponseType(typeof(WebArticleSlugModel), 200)]
        public async Task<IActionResult> GetArticleSlugById([FromHeader]string projectDomain, int id)
        {
            WebArticleSlugModel result = await _mediator.Send(new GetWebArticleSlugQuery
            {
                ProjectDomain = projectDomain,
                Id = id
            });
            if (result.Slug == null)
            {
                return NotFound();
            }

            return Ok(result);
        }
    }
}
