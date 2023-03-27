using CleanArc.Models;

namespace CleanArc.GenerateCode.Infrastructure.Generators
{
    internal class DbContextGenerator : FileGenerator
    {
        private readonly SolutionModel solution;

        public DbContextGenerator(string path, SolutionModel solution)
            : base(path, $"{solution.Name}DbContext.cs")
        {
            this.solution = solution;
        }

        protected override List<string> Generate()
            => new()
            {
                "using Infrastructure.Configurations;",
                "using Infrastructure.Data.Outbox;",
                "using Microsoft.EntityFrameworkCore;",
                "using Infrastructure.Configurations;",
                "",
                "namespace Infrastructure.Data",
                "{",
                $"    public class {solution.Name}DbContext : DbContext",
                "    {",
                $"        public {solution.Name}DbContext(DbContextOptions<{solution.Name}DbContext> options)",
                "            : base(options)",
                "        { /* Nothing more todo */ }",
                "",
                "        protected override void OnModelCreating(ModelBuilder modelBuilder)",
                "        {",
                "            modelBuilder.ApplyConfigurationsFromAssembly(AssemblyReference.Assembly);",
                "        }",
                "    }",
                "}",
            };
    }
}
