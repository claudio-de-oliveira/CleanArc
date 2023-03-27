using CleanArc.Models;

namespace CleanArc.GenerateCode.Domain.Generators
{
    internal class EntityGenerator : FileGenerator
    {
        private readonly EntityModel entity;

        public EntityGenerator(string path, EntityModel entity)
            : base(path, $"{entity.Name}.cs")
        {
            this.entity = entity;
        }

        protected override List<string> Generate()
        {
            List<string> lines = new()
            {
                "using Domain.Base;",
                $"using Domain.DomainEvent.{entity.Name};",
                "",
                "using Serilog;",
                "",
                "using System.ComponentModel.DataAnnotations;",
                "using System.ComponentModel.DataAnnotations.Schema;",
                "",
                "namespace Domain.Entity",
                "{",
                $"    public class {entity.Name} : AggregateRoot",
                "    {",
                $"        private {entity.Name}(Guid id)",
                "            : base(id)",
                "        {",
                "        }",
            };

            lines.Add("");

            foreach (var property in entity.Properties)
            {
                lines.Add($"        public {property.Type} {property.Name} {{ get; private set; }}");
            }

            lines.Add("");

            lines.Add($"        public static {entity.Name} Create(");
            for (int i = 0; i < entity.Properties.Count - 1; i++)
            {
                lines.Add($"            {entity.Properties[i].Type} {entity.Properties[i].Name.ToLower()},");
            }
            if (entity.Properties.Count > 0)
                lines.Add($"            {entity.Properties[^1].Type} {entity.Properties[^1].Name.ToLower()}");
            lines.Add("            )");
            lines.Add("        {");
            lines.Add($"            var entity = new {entity.Name}(Guid.NewGuid())");
            lines.Add("            {");
            for (int i = 0; i < entity.Properties.Count - 1; i++)
            {
                lines.Add($"                {entity.Properties[i].Name} = {entity.Properties[i].Name.ToLower()},");
            }
            if (entity.Properties.Count > 0)
                lines.Add($"                {entity.Properties[^1].Name} = {entity.Properties[^1].Name.ToLower()}");
            lines.Add("            };");

            lines.Add("");

            lines.Add($"            Log.Debug(\"Raising domain event: {entity.Name} has been CREATED\");");
            lines.Add($"            entity.RaiseDomainEvent(new {entity.Name}HasBeenCreatedDomainEvent(Guid.NewGuid(), entity.Id));");

            lines.Add("");

            lines.Add("            return entity;");
            lines.Add("        }");


            lines.Add("");


            lines.Add($"        public {entity.Name} Update(");

            for (int i = 0; i < entity.Properties.Count - 1; i++)
            {
                lines.Add($"            {entity.Properties[i].Type} {entity.Properties[i].Name.ToLower()},");
            }
            if (entity.Properties.Count > 0)
                lines.Add($"            {entity.Properties[^1].Type} {entity.Properties[^1].Name.ToLower()}");

            lines.Add("            )");
            lines.Add("        {");
            lines.Add($"            var entity = new {entity.Name}(this.Id)");
            lines.Add("            {");

            for (int i = 0; i < entity.Properties.Count - 1; i++)
            {
                lines.Add($"                {entity.Properties[i].Name} = {entity.Properties[i].Name.ToLower()},");
            }
            if (entity.Properties.Count > 0)
                lines.Add($"                {entity.Properties[^1].Name} = {entity.Properties[^1].Name.ToLower()}");

            lines.Add("            };");
            lines.Add("");
            lines.Add($"            Log.Debug(\"Raising domain event: {entity.Name} has been UPDATED\");");
            lines.Add($"            entity.RaiseDomainEvent(new {entity.Name}HasBeenUpdatedDomainEvent(Guid.NewGuid(), this.Id));");
            lines.Add("");
            lines.Add("            return entity;");
            lines.Add("        }");


            lines.Add("");


            lines.Add($"        public {entity.Name} Delete()");
            lines.Add("        {");
            lines.Add($"            Log.Debug(\"Raising domain event: {entity.Name} has been DELETED\");");
            lines.Add($"            RaiseDomainEvent(new {entity.Name}HasBeenDeletedDomainEvent(Guid.NewGuid(), this.Id));");
            lines.Add("            return this;");
            lines.Add("        }");

            lines.Add("    }");
            lines.Add("}");

            return lines;
        }
    }
}
