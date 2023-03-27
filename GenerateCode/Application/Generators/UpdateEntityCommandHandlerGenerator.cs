using CleanArc.Models;

namespace CleanArc.GenerateCode.Application.Generators
{
    internal class UpdateEntityCommandHandlerGenerator : FileGenerator
    {
        private readonly EntityModel entity;

        public UpdateEntityCommandHandlerGenerator(string path, EntityModel entity)
            : base(path, $"Update{entity.Name}CommandHandler.cs")
        {
            this.entity = entity;
        }

        protected override List<string> Generate()
        {
            List<string> lines = new()
            {
                $"using Application.Handlers.{entity.Name}.Common;",
                "using Application.Interfaces.Repository;",
                "using ErrorOr;",
                "using MediatR;",
                "",
                $"namespace Application.Handlers.{entity.Name}.Update",
                "{",
                $"    public class Update{entity.Name}CommandHandler",
                $"        : IRequestHandler<Update{entity.Name}Command, ErrorOr<{entity.Name}Result>>",
                "    {",
                $"        private readonly I{entity.Name}Repository _repository;",
                "",
                $"        public Update{entity.Name}CommandHandler(I{entity.Name}Repository repository)",
                "        {",
                "            _repository = repository;",
                "        }",
                "",
                $"        public async Task<ErrorOr<{entity.Name}Result>> Handle(Update{entity.Name}Command request, CancellationToken cancellationToken)",
                "        {",
                "            if (cancellationToken.IsCancellationRequested)",
                $"                return Domain.Errors.Error.{entity.Name}.Canceled;",
                "",
                "            try",
                "            {",
                $"                var entity = await _repository.Get{entity.Name}ById(request.Id, cancellationToken);",
                "                if (entity is null)",
                $"                    return Domain.Errors.Error.{entity.Name}.NotFound;",
                "",
                "                var updated = entity.Update(",
            };
            for (int i = 0; i < entity.Properties.Count - 1; i++)
                if (entity.Properties[i].Name != "Id")
                    lines!.Add($"                    request.{entity.Properties[i].Name},");
            if (entity.Properties.Count > 0)
                if (entity.Properties[^1].Name != "Id")
                    lines!.Add($"                    request.{entity.Properties[^1].Name}");

            lines.Add("                    );");
            lines.Add("");
            lines.Add($"                await _repository.Update{entity.Name}(updated, cancellationToken);");
            lines.Add("");
            lines.Add($"                return new {entity.Name}Result(updated);");
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
