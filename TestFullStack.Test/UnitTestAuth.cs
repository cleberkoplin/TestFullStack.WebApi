using TestFullStack.Domain.DTOs;
using TestFullStack.Domain.Entities;
using TestFullStack.Domain.Injection;
using TestFullStack.Domain.Repositories.Interfaces;
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
            Start.BindKernel();
            authService = Kernel.Get<IAuthService>();
            repository = Kernel.Get<IRepository<User>>();
        }

        [Fact]
        public void TestSignin()
        {

            var user = new User();
            user.Name = "Fake Name";
            user.Password = CryptoTools.ComputeHashMd5("blablabla"); ;
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
            user.Name = "Fake Name";
            user.Password = "blablabla";
            user.Username = "fake";

            Token token = authService.Signup(user);

            Assert.True(token != null, "Signup implemented!");
        }
    }
}
