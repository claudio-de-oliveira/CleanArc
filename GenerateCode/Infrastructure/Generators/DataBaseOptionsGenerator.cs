namespace CleanArc.GenerateCode.Infrastructure.Generators
{
    internal class DataBaseOptionsGenerator : FileGenerator
    {
        public DataBaseOptionsGenerator(string path)
            : base(path, "DataBaseOptions.cs")
        {
        }

        protected override List<string> Generate()
            => new()
            {
                "namespace Infrastructure.Options",
                "{",
                "    public class DatabaseOptions",
                "    {",
                "        public string ConnectionString { get; set; } = string.Empty;",
                "        public int MaxRetryCount { get; set; }",
                "        public int CommandTimeout { get; set; }",
                "        public bool EnableDetailedErrors { get; set; }",
                "        public bool EnableSensitiveDataLogging { get; set; }",
                "    }",
                "}",
            };
    }
}
