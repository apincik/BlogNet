using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using WebApi.CommandResults;
using WebApi.Commands;

namespace WebApi.Controllers
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
        [ProducesResponseType(typeof(CategoryArticlesResult), 200)]
        public async Task<IActionResult> FindCategoryArticles([FromHeader]string projectDomain, string title = "news", int limit = 10, int page = 0)
        {
            CategoryArticlesResult result = await _mediator.Send(new GetArticlesByCategoryCommand
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
        [ProducesResponseType(typeof(ArticleResult), 200)]
        public async Task<IActionResult> GetArticleBySlug([FromHeader]string projectDomain, string slug)
        {
            ArticleResult result = await _mediator.Send(new GetArticleBySlugCommand
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
    }
}
