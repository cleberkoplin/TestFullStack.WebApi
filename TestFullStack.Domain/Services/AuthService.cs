using TestFullStack.Domain.Entities;
using TestFullStack.Domain.Services.Interfaces;
using TestFullStack.Domain.DTOs;
using TestFullStack.Domain.Utils;

namespace TestFullStack.Domain.Services
{
    public class AuthService : IAuthService
    {
        IUserService _userService;

        public AuthService(IUserService userService)
        {
            _userService = userService;
        }


        public Token Signin(LoginRequest loginRequest)
        {
            var password = CryptoTools.ComputeHashMd5(loginRequest.Password);
            var user = _userService.Get(loginRequest.Username);
            if (user == null) return null;

            if (user.Password == password)
            {
                //criado um token específico para ficar mais simples e performático
                //o mesmo será utilizado na validação das requisições
                var token = new Token
                {
                    Id = user.Id,
                    Username = user.Username,
                    Password = user.Password
                };

                return token;
            }

            return null;
        }

        public Token Signup(User user)
        {
            user.Password = CryptoTools.ComputeHashMd5(user.Password);
            _userService.Save(user);

            if (user.Id > 0)
            {
                var token = new Token
                {
                    Id = user.Id,
                    Username = user.Username,
                    Password = user.Password
                };

                return token;
            }
            return null;            
        }


    }
}
