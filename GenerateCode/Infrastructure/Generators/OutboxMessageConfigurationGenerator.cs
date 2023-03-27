using CleanArc.Models;

namespace CleanArc.GenerateCode.Infrastructure.Generators
{
    internal class OutboxMessageConfigurationGenerator : FileGenerator
    {
        private readonly SolutionModel solution;

        public OutboxMessageConfigurationGenerator(string path, SolutionModel solution)
            : base(path, "OutboxMessageConfiguration.cs")
        {
            this.solution = solution;
        }

        protected override List<string> Generate()
            => new()
            {
                "using Microsoft.EntityFrameworkCore.Metadata.Builders;",
                "using Microsoft.EntityFrameworkCore;",
                "using Infrastructure.Outbox;",
                "",
                "namespace Infrastructure.Data.Outbox",
                "{",
                $"    internal sealed class {solution.Name}OutboxMessageConfiguration",
                $"        : IEntityTypeConfiguration<{solution.Name}OutboxMessage>",
                "    {",
                $"        public void Configure(EntityTypeBuilder<{solution.Name}OutboxMessage> modelBuilder)",
                "        {",
                "            modelBuilder",
                $"                .ToTable(TableNames.{solution.Name}OutboxMessages);",
                "",
                "            modelBuilder",
                "                .HasKey(x => x.Id);",
                "        }",
                "    }",
                "}",
            };
    }
}
