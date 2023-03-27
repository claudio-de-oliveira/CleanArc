using CleanArc.Models;

namespace CleanArc.GenerateCode.Contracts.Generators
{
    internal class EntityResponseGenerator : FileGenerator
    {
        private readonly EntityModel entity;

        public EntityResponseGenerator(string path, EntityModel entity)
            : base(path, $"{entity.Name}Response.cs")
        {
            this.entity = entity;
        }

        protected override List<string> Generate()
        {
            var lines = new List<string>
            {
                $"namespace Contracts.Entity",
                "{",
                $"    public record {entity.Name}Response(",
            };
            if (entity.Properties.Count > 0)
                lines.Add("        Guid Id,");
            else
                lines.Add("        Guid Id");

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
