using CleanArc.Models;

namespace CleanArc.GenerateCode.Infrastructure.Generators
{
    internal class OutboxMessageGenerator : FileGenerator
    {
        private readonly SolutionModel solutionModel;
        public OutboxMessageGenerator(string path, SolutionModel solutionModel)
            : base(path, "OutboxMessage.cs")
        {
            this.solutionModel = solutionModel;
        }

        protected override List<string> Generate()
            => new()
            {
                "namespace Infrastructure.Outbox",
                "{",
                $"    public sealed class {solutionModel.Name}OutboxMessage",
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
