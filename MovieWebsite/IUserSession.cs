using Microsoft.AspNetCore.Http;
using Models;
using MovieSite;

namespace MovieViewer
{
    public interface IUserSession
    {
        Account GetSession { get; }
        void SetSession(Account account);
    }
    public class UserSession : IUserSession
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserSession(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        public Account GetSession => _httpContextAccessor.HttpContext.Session.GetObject<Account>("User");
        public void SetSession(Account account) => _httpContextAccessor.HttpContext.Session.SetObject("User", account);

    }

}
