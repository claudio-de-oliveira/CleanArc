namespace CleanArc.GenerateCode.Infrastructure.Generators
{
    internal class OutboxMessageConsumerGenerator : FileGenerator
    {
        public OutboxMessageConsumerGenerator(string path)
            : base(path, "OutboxMessageConsumer.cs")
        {
        }

        protected override List<string> Generate()
            => new()
            {
                "namespace Infrastructure.Outbox",
                "{",
                "    public sealed class OutboxMessageConsumer",
                "    {",
                "        public Guid Id { get; set; }",
                "        public string Name { get; set; } = string.Empty;",
                "    }",
                "}",
            };
    }
}
