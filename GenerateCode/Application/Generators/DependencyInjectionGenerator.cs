﻿namespace CleanArc.GenerateCode.Application.Generators
{
    internal class DependencyInjectionGenerator : FileGenerator
    {
        public DependencyInjectionGenerator(string path)
            : base(path, "DependencyInjection.cs")
        {
        }

        protected override List<string> Generate()
            => new()
            {
                "using Application.Behaviors;",
                "using FluentValidation;",
                "using MediatR;",
                "using Microsoft.Extensions.DependencyInjection;",
                "using System.Reflection;",
                "",
                "namespace Application",
                "{",
                "    public static class DependencyInjection",
                "    {",
                "        public static IServiceCollection AddApplication(this IServiceCollection services)",
                "        {",
                "            services.AddMediatR(",
                "                typeof(DependencyInjection).Assembly",
                "                );",
                "            services.AddScoped(",
                "                typeof(IPipelineBehavior<,>),",
                "                typeof(ValidationBehavior<,>)",
                "            );",
                "            services.AddValidatorsFromAssembly(",
                "                Assembly.GetExecutingAssembly()",
                "                );",
                "",
                "            return services;",
                "        }",
                "    }",
                "}",
            };
    }
}
