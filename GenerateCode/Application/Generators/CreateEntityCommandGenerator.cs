using CleanArc.Models;

namespace CleanArc.GenerateCode.Application.Generators
{
    internal class CreateEntityCommandGenerator : FileGenerator
    {
        private readonly EntityModel entity;

        public CreateEntityCommandGenerator(string path, EntityModel entity)
            : base(path, $"Create{entity.Name}Command.cs")
        {
            this.entity = entity;
        }

        protected override List<string> Generate()
        {
            List<string> lines = new()
            {
                "using ErrorOr;",
                "using MediatR;",
                "",
                $"namespace Application.Handlers.{entity.Name}.Create",
                "{",
                $"    public record Create{entity.Name}Command("
            };

            for (int i = 0; i < entity.Properties.Count - 1; i++)
                if (entity.Properties[i].Name != "Id")
                    lines.Add($"        {entity.Properties[i].Type} {entity.Properties[i].Name},");
            if (entity.Properties.Count > 0)
                if (entity.Properties[^1].Name != "Id")
                    lines.Add($"        {entity.Properties[^1].Type} {entity.Properties[^1].Name}");

            lines.Add("        ) : IRequest<ErrorOr<Guid>>;");
            lines.Add("}");

            return lines;
        }
    }
}