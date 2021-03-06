using TestFullStack.Domain.DTOs;
using TestFullStack.Domain.Entities;
using TestFullStack.Domain.Injection;
using TestFullStack.Domain.Repositories.Interfaces;
using TestFullStack.Domain.Services;
using TestFullStack.Domain.Services.Interfaces;
using TestFullStack.Domain.Utils;
using Xunit;

namespace TestFullStack.Test
{
    
    public class UnitTestAuth
    {
        IAuthService authService;
        IRepository<User> repository;

        public UnitTestAuth()
        {
            var kernel = Start.BindKernel();
            
            authService = kernel.Get<IAuthService>();
            repository = kernel.Get<IRepository<User>>();
        }

        [Fact]
        public void TestSignin()
        {

            var user = new User();
            user.Name = "Fake Name";
            user.Password = CryptoTools.ComputeHashMd5("blablabla");
            user.Username = "fake";

            repository.Insert(user);
            repository.Save();

            var loginRequest = new LoginRequest();
            loginRequest.Username = "fake";
            loginRequest.Password = "blablabla";

            Token token = authService.Signin(loginRequest);

            Assert.True(token != null, "Signin implemented!");
        }

        [Fact]
        public void TestSignup()
        {
            var user = new User();
            user.Name = "Fake Name 2";
            user.Password = "blablabla02";
            user.Username = "fake02";

            Token token = authService.Signup(user);

            Assert.True(token != null, "Signup implemented!");
        }
    }
}
