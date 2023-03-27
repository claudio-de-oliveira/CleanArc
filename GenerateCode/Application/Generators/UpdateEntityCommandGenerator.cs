using CleanArc.Models;

namespace CleanArc.GenerateCode.Application.Generators
{
    internal class UpdateEntityCommandGenerator : FileGenerator
    {
        private readonly EntityModel entity;

        public UpdateEntityCommandGenerator(string path, EntityModel entity)
            : base(path, $"Create{entity.Name}Command.cs")
        {
            this.entity = entity;
        }

        protected override List<string> Generate()
        {
            List<string> lines = new()
            {
                $"using Application.Handlers.{entity.Name}.Common;",
                "using ErrorOr;",
                "using MediatR;",
                "",
                $"namespace Application.Handlers.{entity.Name}.Update",
                "{",
                $"    public record Update{entity.Name}Command("
            };
            if (entity.Properties.Count > 0)
                lines.Add("        Guid Id,");
            else
                lines.Add("        Guid Id");

            for (int i = 0; i < entity.Properties.Count - 1; i++)
                lines.Add($"        {entity.Properties[i].Type} {entity.Properties[i].Name},");
            if (entity.Properties.Count > 0)
                lines.Add($"        {entity.Properties[^1].Type} {entity.Properties[^1].Name}");

            lines.Add($"        ) : IRequest<ErrorOr<{entity.Name}Result>>;");
            lines.Add("}");

            return lines;
        }
    }
}