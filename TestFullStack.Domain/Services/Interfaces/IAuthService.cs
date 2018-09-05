using TestFullStack.Domain.DTOs;
using TestFullStack.Domain.Entities;


namespace TestFullStack.Domain.Services.Interfaces
{
    public interface IAuthService
    {
        Token Signin(LoginRequest loginRequest);
        Token Signup(User user);
    }
}
