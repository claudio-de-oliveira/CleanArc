namespace CleanArc.GenerateCode.Presentation.Generators
{
    internal class AssemblyReferenceGenerator : FileGenerator
    {
        private readonly string rootspace;

        public AssemblyReferenceGenerator(string path, string rootspace)
            : base(path, "AssemblyReference.cs")
        {
            this.rootspace = rootspace;
        }

        protected override List<string> Generate()
            => new()
            {
                "using System.Reflection;",
                "",
                $"namespace {rootspace}",
                "{",
                "    public static class AssemblyReference",
                "    {",
                "        public static readonly Assembly Assembly = typeof(AssemblyReference).Assembly;",
                "    }",
                "}",
            };
    }
}
