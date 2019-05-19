using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Blognet.Cms.Core.MenuItems.Queries.GetWebMenu;
using Blognet.Cms.Core.MenuItems.Models;

namespace Blognet.Cms.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("CorsPolicy")]
    public class MenuController : Controller
    {
        private IMediator _mediator;

        public MenuController(IMediator mediator)
        {
            _mediator = mediator;
        }


        [HttpGet("")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(WebMenuModel), 200)]
        public async Task<IActionResult> GetMenu([FromHeader]string projectDomain)
        {
            WebMenuModel result = await _mediator.Send(new GetWebMenuQuery
            {
                ProjectDomain = projectDomain,
            });

            return Ok(result);
        }
    }
}