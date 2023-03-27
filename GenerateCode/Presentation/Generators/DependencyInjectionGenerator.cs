namespace CleanArc.GenerateCode.Presentation.Generators
{
    internal class DependencyInjectionGenerator : FileGenerator
    {
        private readonly string apiName;

        public DependencyInjectionGenerator(string path, string apiName)
            : base(path, $"DependencyInjection.cs")
        {
            this.apiName = apiName;
        }

        protected override List<string> Generate()
            => new()
            {
                $"using {apiName}.Common;",
                $"using {apiName}.Common.Errors;",
                "",
                $"using Microsoft.AspNetCore.Mvc.Infrastructure;",
                "",
                $"namespace {apiName}",
                "{",
                "    public static class DependencyInjection",
                "    {",
                "        public static IServiceCollection AddPresentation(this IServiceCollection services)",
                "        {",
                "            services.AddControllers();",
                "            services.AddSingleton<ProblemDetailsFactory, ProblemDetailsFactory>();",
                "            services.AddMappings();",
                "",
                "            return services;",
                "        }",
                "    }",
                "}",
            };
    }
}
