using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TestFullStack.Domain.Services;
using TestFullStack.Domain.Base;
using TestFullStack.Domain.Repositories;
using TestFullStack.Domain.Repositories.Interfaces;
using TestFullStack.Domain.Services.Interfaces;
using TestFullStack.Domain.Utils;
using TestFullStack.Domain.Injection;
using SimpleInjector;

namespace TestFullStack.WebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(o => o.AddPolicy("AllowAll", builder =>
            {
                builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
            }));

            //Use to codefirst by Entity Framework
            services.AddDbContext<TestFullStackContext>(options => options.UseSqlServer(Configuration.GetConnectionString("TestFullStack")));

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            BindKernel();
        }

        private void BindKernel()
        {
            Kernel.StartKernel();

            var kernel = Kernel.GetKernel();

            kernel.Register<DbContext>(() => {
                var opBuilder = new DbContextOptionsBuilder<TestFullStackContext>();
                opBuilder.UseSqlServer(Configuration.GetConnectionString("TestFullStack"));
                return new TestFullStackContext(opBuilder.Options);
            }, Lifestyle.Singleton);

            //repository
            Kernel.Bind(typeof(IRepository<>), typeof(Repository<>));
            Kernel.Bind(typeof(IQueryRepository<>), typeof(QueryRepository<>));

            //services
            Kernel.Bind<IAuthService, AuthService>();
            Kernel.Bind<IOrderService, OrderService>();
            Kernel.Bind<IProductService, ProductService>();
            Kernel.Bind<IUserService, UserService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc();

            ApiToken.GenerateSecret();
        }
    }
}
