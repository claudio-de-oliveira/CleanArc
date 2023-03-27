using CleanArc.Models;

namespace CleanArc.GenerateCode.Contracts.Generators
{
    internal class UpdateEntityRequestGenerator : FileGenerator
    {
        private readonly EntityModel entity;

        public UpdateEntityRequestGenerator(string path, EntityModel entity)
            : base(path, $"Update{entity.Name}Request.cs")
        {
            this.entity = entity;
        }

        protected override List<string> Generate()
        {
            var lines = new List<string>
            {
                $"namespace Contracts.Entity",
                "{",
                $"    public record Update{entity.Name}Request(",
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
