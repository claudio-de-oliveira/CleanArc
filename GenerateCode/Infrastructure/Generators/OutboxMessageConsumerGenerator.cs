using CleanArc.Models;

namespace CleanArc.GenerateCode.Infrastructure.Generators
{
    internal class OutboxMessageConsumerGenerator : FileGenerator
    {
        private readonly SolutionModel solution;

        public OutboxMessageConsumerGenerator(string path, SolutionModel solution)
            : base(path, "OutboxMessageConsumer.cs")
        {
            this.solution = solution;
        }

        protected override List<string> Generate()
            => new()
            {
                "namespace Infrastructure.Outbox",
                "{",
                $"    public sealed class {solution.Name}OutboxMessageConsumer",
                "    {",
                "        public Guid Id { get; set; }",
                "        public string Name { get; set; } = string.Empty;",
                "    }",
                "}",
            };
    }
}
