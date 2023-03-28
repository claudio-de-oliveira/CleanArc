using CleanArc.Models;

namespace CleanArc.GenerateCode.Infrastructure.Generators
{
    internal class ConfigurationGenerator : FileGenerator
    {
        private readonly EntityModel entity;

        public ConfigurationGenerator(string path, EntityModel entity)
            : base(path, $"{entity.Name}Configuration.cs")
        {
            this.entity = entity;
        }

        protected override List<string> Generate()
            => new()
            {
                "using Domain.Entity;",
                "using Microsoft.EntityFrameworkCore;",
                "using Microsoft.EntityFrameworkCore.Metadata.Builders;",
                "using Infrastructure.Data;",
                "",
                "namespace Infrastructure.Configurations",
                "{",
                $"    internal sealed class {entity.Name}Configuration : IEntityTypeConfiguration<{entity.Name}>",
                "    {",
                $"        public void Configure(EntityTypeBuilder<{entity.Name}> modelBuilder)",
                "        {",
                "            modelBuilder",
                $"                .ToTable(TableNames.{entity.Name});",
                "",
                "            modelBuilder",
                "                .HasKey(x => x.Id);",
                "",
                "            // TODO: Complete the code",
                "",
                "            SeedData(modelBuilder);",
                "        }",
                "",
                $"        private static void SeedData(EntityTypeBuilder<{entity.Name}> modelBuilder)",
                "        {",
                "            // modelBuilder",
                "            //     .HasData(....);",
                "        }",
                "    }",
                "}",
            };
    }
}
