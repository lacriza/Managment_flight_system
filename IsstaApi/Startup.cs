using AutoMapper;
using Core.POCO;
using Infrastructure.Interfaces;
using Infrastructure.Repository;
using Infrastructure.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Web.MapperProfile;
using Web.Middleware;

namespace IsstaApi
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

      services.AddControllers();

      // Auto Mapper Configurations
      var mappingConfig = new MapperConfiguration(mc =>
      {
        mc.AddProfile(new MapperProfile());
      });

      IMapper mapper = mappingConfig.CreateMapper();
      services.AddSingleton(mapper);
      services.AddSwaggerGen(c =>
      {
        c.SwaggerDoc("v1", new OpenApiInfo { Title = "IsstaApi", Version = "v1" });
      });

      services.AddSingleton(_ => Configuration);
      services.AddScoped<IFlightRepository, FlightRepository>();
      services.AddScoped<IAirportRepository, AirportRepository>();
      services.AddScoped<IRepository<Comment>, CommentRepository>();
      services.AddScoped<IFlightManagmentService, FLightManagmentService>();
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
        app.UseSwagger();
        app.UseSwaggerUI(c =>
        {
          c.SwaggerEndpoint("v1/swagger.json", "MyAPI V1");
        });
      }

      app.UseHttpsRedirection();

      app.UseRouting();

      app.UseAuthorization();

      // global error handler
      app.UseMiddleware<ErrorHandlerMiddleware>();

      app.UseEndpoints(endpoints =>
      {
        endpoints.MapControllers();
      });
    }
  }
}
