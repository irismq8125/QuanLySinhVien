using System.Net.Http.Headers;

namespace QuanLySinhVien.Areas.Admin.Models.TaiKhoan
{
    public class AuthHandled : DelegatingHandler
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public AuthHandled(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var httpContext = _httpContextAccessor.HttpContext;
            if(httpContext != null)
            {
                var token = httpContext.Request.Cookies["Token"];
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }
            return await base.SendAsync(request, cancellationToken); 
        }
    }
}
