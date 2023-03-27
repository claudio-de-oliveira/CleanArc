namespace CleanArc.GenerateCode.Presentation.Generators
{
    internal class MappingDependencyInjectionGenerator : FileGenerator
    {
        private readonly string rootspace;

        public MappingDependencyInjectionGenerator(string path, string rootspace)
            : base(path, "MappingDependencyInjection.cs")
        {
            this.rootspace = rootspace;
        }

        protected override List<string> Generate()
            => new()
            {
                "using Mapster;",
                "using MapsterMapper;",
                "using System.Reflection;",
                "",
                $"namespace {rootspace}.Common",
                "{",
                "    public static class MappingDependencyInjection",
                "    {",
                "        public static IServiceCollection AddMappings(this IServiceCollection services)",
                "        {",
                "            var config = TypeAdapterConfig.GlobalSettings;",
                "            config.Scan(Assembly.GetExecutingAssembly());",
                "",
                "            services.AddSingleton(config);",
                "            services.AddScoped<IMapper, ServiceMapper>();",
                "",
                "            return services;",
                "        }",
                "    }",
                "}",
            };
    }
}
