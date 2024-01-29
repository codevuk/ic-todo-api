using TodoApp.ViewModels;

namespace TodoAPI.Services;

public interface IAuthService
{
    Task Register(RegisterRequest request);

    Task<string> Login(LoginRequest request);
}