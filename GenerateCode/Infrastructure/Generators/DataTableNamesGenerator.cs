using CleanArc.Models;

namespace CleanArc.GenerateCode.Infrastructure.Generators
{
    internal class DataTableNamesGenerator : FileGenerator
    {
        private readonly SolutionModel solution;

        public DataTableNamesGenerator(string path, SolutionModel solution)
            : base(path, "TableNames.cs")
        {
            this.solution = solution;
        }

        protected override List<string> Generate()
        {
            List<string> lines = new()
            {
                "namespace Infrastructure.Data",
                "{",
                "    public static class TableNames",
                "    {",
            };

            if (solution.Domain is not null)
            {
                foreach (var obj in solution.Domain.Objects)
                {
                    if (obj is EntityModel entity)
                    {
                        lines.Add($"        public const string {entity.Name} = \"{entity.Name}\";");
                    }
                }
            }

            lines.Add("");
            lines.Add("        public const string OutboxMessages = \"OutboxMessage\";");
            lines.Add("        public const string OutboxMessageConsumers = \"OutboxMessageConsumer\";");
            lines.Add("    }");
            lines.Add("}");

            return lines;
        }
    }
}
