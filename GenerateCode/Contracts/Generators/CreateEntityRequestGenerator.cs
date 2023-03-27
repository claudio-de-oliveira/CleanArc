using CleanArc.Models;

namespace CleanArc.GenerateCode.Contracts.Generators
{
    internal class CreateEntityRequestGenerator : FileGenerator
    {
        private readonly EntityModel entity;

        public CreateEntityRequestGenerator(string path, EntityModel entity)
            : base(path, $"Create{entity.Name}Request.cs")
        {
            this.entity = entity;
        }

        protected override List<string> Generate()
        {
            var lines = new List<string>
            {
                $"namespace Contracts.Entity",
                "{",
                $"    public record Create{entity.Name}Request(",
            };
            for (int i = 0; i < entity.Properties.Count - 1; i++)
                lines.Add($"        {entity.Properties[i].Type} {entity.Properties[i].Name},");
            if (entity.Properties.Count > 0)
                lines.Add($"        {entity.Properties[^1].Type} {entity.Properties[^1].Name}");
            lines.Add("        );");
            lines.Add("}");

            return lines;
        }
    }
}
