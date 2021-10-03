using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace OnlineLibrary.Extensions
{
    public class HttpContextExtensions
    {
        private readonly IHttpContextAccessor _contextAccessor;

        public HttpContextExtensions(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
        }

        public string GetAuthenticatedUserId()
        {
            return _contextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
        }
    }
}
