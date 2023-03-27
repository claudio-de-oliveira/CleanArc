namespace CleanArc.GenerateCode.Infrastructure.Generators
{
    internal class AssemblyReferenceGenerator : FileGenerator
    {
        public AssemblyReferenceGenerator(string path)
            : base(path, "AssemblyReference.cs")
        {
        }

        protected override List<string> Generate()
            => new()
            {
                "using System.Reflection;",
                "",
                "namespace Infrastructure",
                "{",
                "    public static class AssemblyReference",
                "    {",
                "        public static readonly Assembly Assembly = typeof(AssemblyReference).Assembly;",
                "    }",
                "}",
            };
    }
}
