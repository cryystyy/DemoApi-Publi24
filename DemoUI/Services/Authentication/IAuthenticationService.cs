using DemoUI.Services.Base;

namespace DemoUI.Services.Authentication
{
    public interface IAuthenticationService
    {
        Task<bool> AuthenticateAsync(LoginUserDto loginModel);
        Task<Guid> GetUserIdAsync();
        Task<string> GetUserEmailAsync();
        public Task Logout();
    }
}
