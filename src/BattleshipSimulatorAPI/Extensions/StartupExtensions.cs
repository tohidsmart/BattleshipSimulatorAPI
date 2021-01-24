using BattleshipSimulatorAPI.Controllers;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Microsoft.Extensions.Configuration;
using Battleship.Command.Invoker;
using Battleship.Command.Receiver;

namespace BattleshipSimulatorAPI.Extensions
{
    public static class StartupExtensions
    {

        public static void ResolveDependencies(this IServiceCollection services)
        { 
            services.AddSingleton<IPlayer, SinglePlayer>();
            services.AddSingleton<ISimulator, BattleshipSimulator>();
        }

        public static void ConfigureExternalDependencies(this IServiceCollection services)
        {
            services.AddSwaggerGen(setup =>
            {
                setup.SwaggerDoc("v1", new OpenApiInfo { Title = "Battleship API", Version = "v1" });

            });

            services.AddSwaggerGenNewtonsoftSupport();



            services.AddControllers().AddFluentValidation(fv =>
                fv.RegisterValidatorsFromAssemblyContaining<SimulatorController>());
        }

        public static void ConfigureCORS(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("", builder =>
                {
                    builder.SetIsOriginAllowed((host) => true).
                   WithOrigins().
                   AllowCredentials()
                   .AllowAnyHeader()
                   .AllowAnyMethod();
                });
            });
        }

    }
}
