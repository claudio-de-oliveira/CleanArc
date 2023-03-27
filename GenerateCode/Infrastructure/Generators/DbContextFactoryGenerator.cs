using CleanArc.Models;

namespace CleanArc.GenerateCode.Infrastructure.Generators
{
    internal class DbContextFactoryGenerator : FileGenerator
    {
        private readonly SolutionModel solution;

        public DbContextFactoryGenerator(string path, SolutionModel solution)
            : base(path, $"{solution.Name}DbContextFactory.cs")
        {
            this.solution = solution;
        }

        protected override List<string> Generate()
            => new()
            {
                "using Microsoft.EntityFrameworkCore;",
                "using Microsoft.EntityFrameworkCore.Design;",
                "",
                "namespace Infrastructure.Data",
                "{",
                $"    public class {solution.Name}DbContextFactory",
                $"        : IDesignTimeDbContextFactory<{solution.Name}DbContext>",
                "    {",
                $"        public {solution.Name}DbContext CreateDbContext(string[] args)",
                "        {",
                $"            var optionsBuilder = new DbContextOptionsBuilder<{solution.Name}DbContext>();",
                "",
                "            optionsBuilder.UseSqlServer(",
                "                \"Data Source=localhost,1433;\" +",
                $"                \"Initial Catalog={solution.Name}DB;\" +",
                "                \"User ID=SA;\" +",
                "                \"Password=@Pa$$w0rd; // Alterar\" +",
                "                $\"Encrypt=False;\" +",
                "                $\"TrustServerCertificate=False;\"",
                "                );",
                "",
                $"            return new {solution.Name}DbContext(optionsBuilder.Options);",
                "        }",
                "    }",
                "}",
            };
    }
}
