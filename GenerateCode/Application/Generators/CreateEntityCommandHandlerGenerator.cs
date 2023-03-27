using CleanArc.Models;

namespace CleanArc.GenerateCode.Application.Generators
{
    internal class CreateEntityCommandHandlerGenerator : FileGenerator
    {
        private readonly EntityModel entity;

        public CreateEntityCommandHandlerGenerator(string path, EntityModel entity)
            : base(path, $"Create{entity.Name}CommandHandler.cs")
        {
            this.entity = entity;
        }

        protected override List<string> Generate()
        {
            List<string> lines = new()
            {
                "using Application.Interfaces.Repository;",
                "using ErrorOr;",
                "using MediatR;",
                "",
                $"namespace Application.Handlers.{entity.Name}.Create",
                "{",
                $"    public class Create{entity.Name}CommandHandler",
                $"        : IRequestHandler<Create{entity.Name}Command, ErrorOr<Guid>>",
                "    {",
                $"        private readonly I{entity.Name}Repository _repository;",
                "",
                $"        public Create{entity.Name}CommandHandler(I{entity.Name}Repository repository)",
                "        {",
                "            _repository = repository;",
                "        }",
                "",
                $"        public async Task<ErrorOr<Guid>> Handle(Create{entity.Name}Command request, CancellationToken cancellationToken)",
                "        {",
                "            if (cancellationToken.IsCancellationRequested)",
                $"                return Domain.Errors.Error.{entity.Name}.Canceled;",
                "",
                "            try",
                "            {",
                $"                var entity = Domain.Entity.{entity.Name}.Create("
            };

            for (int i = 0; i < entity.Properties.Count - 1; i++)
                if (entity.Properties[i].Name != "Id")
                    lines.Add($"                    request.{entity.Properties[i].Name},");
            if (entity.Properties.Count > 0)
                if (entity.Properties[^1].Name != "Id")
                    lines.Add($"                    request.{entity.Properties[^1].Name}");

            lines.Add("                    );");
            lines.Add("");
            lines.Add($"                var id = await _repository.Create{entity.Name}(entity, cancellationToken);");
            lines.Add("");
            lines.Add("                return id;");
            lines.Add("            }");
            lines.Add("            catch (Exception ex)");
            lines.Add("            {");
            lines.Add($"                return Domain.Errors.Error.{entity.Name}.Exception(ex.Message);");
            lines.Add("            }");
            lines.Add("        }");
            lines.Add("    }");
            lines.Add("}");

            return lines;
        }
    }
}
