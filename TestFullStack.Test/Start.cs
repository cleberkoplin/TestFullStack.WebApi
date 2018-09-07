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
        public static Kernel BindKernel()
        {
            Kernel kernel = new Kernel();
            kernel.StartKernel();

            kernel.GetKernel().Register<DbContext>(() => {
                var opBuilder = new DbContextOptionsBuilder<TestFullStackContext>();
                opBuilder.UseInMemoryDatabase("test-db");
                return new TestFullStackContext(opBuilder.Options);
            }, Lifestyle.Transient);

            //repository
            kernel.Bind(typeof(IRepository<>), typeof(Repository<>));
            kernel.Bind(typeof(IQueryRepository<>), typeof(QueryRepository<>));

            //services
            kernel.Bind<IAuthService, AuthService>();
            kernel.Bind<IOrderService, OrderService>();
            kernel.Bind<IProductService, ProductService>();
            kernel.Bind<IUserService, UserService>();


            return kernel;
        }
    }
}
