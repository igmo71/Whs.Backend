using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Whs.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class BaseController : ControllerBase
    {
        private IMediator _mediator;
        protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>();

        internal Guid UserId => !User.Identity.IsAuthenticated ? Guid.Empty : Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
    }
}
