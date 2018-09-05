using Microsoft.EntityFrameworkCore;
using SimpleInjector;
using TestFullStack.Domain.Base;
using TestFullStack.Domain.Injection;
using TestFullStack.Domain.Repositories;
using TestFullStack.Domain.Repositories.Interfaces;
using TestFullStack.Domain.Services;
using TestFullStack.Domain.Services.Interfaces;

namespace TestFullStack.Test
{
    public class Start
    {
        public static void BindKernel()
        {
            Kernel.StartKernel();
            var kernel = Kernel.GetKernel();

            kernel.Register<DbContext>(() => {
                var opBuilder = new DbContextOptionsBuilder<TestFullStackContext>();
                opBuilder.UseInMemoryDatabase("test-db");
                return new TestFullStackContext(opBuilder.Options);
            }, Lifestyle.Transient);

            //repository
            Kernel.Bind(typeof(IRepository<>), typeof(Repository<>));
            Kernel.Bind(typeof(IQueryRepository<>), typeof(QueryRepository<>));

            //services
            Kernel.Bind<IAuthService, AuthService>();
            Kernel.Bind<IOrderService, OrderService>();
            Kernel.Bind<IProductService, ProductService>();
            Kernel.Bind<IUserService, UserService>();
        }
    }
}
