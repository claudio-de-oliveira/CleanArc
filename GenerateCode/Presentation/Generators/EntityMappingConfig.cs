using CleanArc.Models;

namespace CleanArc.GenerateCode.Presentation.Generators
{
    internal class EntityMappingConfig : FileGenerator
    {
        private readonly string rootspace;
        private readonly EntityModel entity;

        public EntityMappingConfig(string path, string rootspace, EntityModel entity)
            : base(path, $"{entity.Name}MappingConfig.cs")
        {
            this.rootspace = rootspace;
            this.entity = entity;
        }

        protected override List<string> Generate()
            => new()
            {
                $"using Application.Handlers.{entity.Name}.Common;",
                $"using Application.Handlers.{entity.Name}.Create;",
                $"using Application.Handlers.{entity.Name}.Query;",
                $"using Application.Handlers.{entity.Name}.Update;",
                "",
                $"using Contracts.Entity.{entity.Name};",
                "",
                "using Mapster;",
                "",
                $"namespace {rootspace}.Common.Mapping",
                "{",
                $"    public class {entity.Name}MappingConfig : IRegister",
                "    {",
                "        public void Register(TypeAdapterConfig config)",
                "        {",
                $"            config.NewConfig<Create{entity.Name}Request, Create{entity.Name}Command>();",
                $"            config.NewConfig<Update{entity.Name}Request, Update{entity.Name}Command>();",
                $"            config.NewConfig<{entity.Name}Result, {entity.Name}Response>()",
                $"                .Map(dest => dest, src => src.{entity.Name});",
                "        }",
                "    }",
                "}",
            };
    }
}
