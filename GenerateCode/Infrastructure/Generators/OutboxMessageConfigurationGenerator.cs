namespace CleanArc.GenerateCode.Infrastructure.Generators
{
    internal class OutboxMessageConfigurationGenerator : FileGenerator
    {
        public OutboxMessageConfigurationGenerator(string path)
            : base(path, "OutboxMessageConfiguration.cs")
        {
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
                "    internal sealed class OutboxMessageConfiguration",
                "        : IEntityTypeConfiguration<OutboxMessage>",
                "    {",
                "        public void Configure(EntityTypeBuilder<OutboxMessage> modelBuilder)",
                "        {",
                "            modelBuilder",
                "                .ToTable(TableNames.OutboxMessages);",
                "",
                "            modelBuilder",
                "                .HasKey(x => x.Id);",
                "        }",
                "    }",
                "}",
            };
    }
}
