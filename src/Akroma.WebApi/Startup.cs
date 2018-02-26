using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Akroma.Domain.Blocks.Services;
using Akroma.Domain.Transactions.Services;
using Akroma.Persistence.SQL.Repositories;
using Akroma.Persistence.SQL;
using Akroma.WebApi.Middlewares;
using Brickweave.Cqrs.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.PlatformAbstractions;
using Nethereum.JsonRpc.Client;
using Nethereum.Web3;
using Swashbuckle.AspNetCore.Swagger;

namespace Akroma.WebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<AkromaContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("AkromaConnectionString"),
                    x => x.MigrationsAssembly("Akroma.Persistence.SQL")));
            services.AddTransient<IBlocksRepository, SqlServerBlocksRepository>();
            services.AddTransient<ITransactionsRepository, SqlServerTransactionsRepository>();
            services.AddSingleton<Nethereum.Web3.Web3>((_) => new Nethereum.Web3.Web3(new RpcClient(new Uri("https://rpc.akroma.io"))));

            services.AddCqrs(AppDomain
                .CurrentDomain
                .GetAssemblies()
                .Where(a => a.FullName.StartsWith("Akroma"))
                .Where(a => a.FullName.Contains("Domain"))
                .ToArray()
            );

            services.AddCors();
            services.AddMvc();

            services.AddSwaggerGen(c =>
            {
                c.DescribeAllEnumsAsStrings();
                c.DescribeStringEnumsInCamelCase();
                c.SwaggerDoc("v1", new Info { Title = "Akroma API", Version = "v0.1.0" });
                var filePath = Path.Combine(PlatformServices.Default.Application.ApplicationBasePath, "Akroma.WebApi.xml");
                c.IncludeXmlComments(filePath);
            });
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseErrorHandlingMiddleware();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.RoutePrefix = "docs";
                c.DocumentTitle = "Akroma API";
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Akroma API");
            });

            app.UseCors(builder => builder
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowCredentials());

            app.UseMvc();
        }
    }
}
