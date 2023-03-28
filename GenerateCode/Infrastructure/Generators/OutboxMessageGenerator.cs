namespace CleanArc.GenerateCode.Infrastructure.Generators
{
    internal class OutboxMessageGenerator : FileGenerator
    {
        public OutboxMessageGenerator(string path)
            : base(path, "OutboxMessage.cs")
        {
        }

        protected override List<string> Generate()
            => new()
            {
                "namespace Infrastructure.Outbox",
                "{",
                "    public sealed class OutboxMessage",
                "    {",
                "        public Guid Id { get; set; }",
                "        public string Type { get; set; } = string.Empty;",
                "        public string Content { get; set; } = string.Empty;",
                "        public DateTime OccurredOnUtc { get; set; }",
                "        public DateTime? ProcessedOnUtc { get; set; }",
                "        public string? Error { get; set; }",
                "    }",
                "}",
            };
    }
}
