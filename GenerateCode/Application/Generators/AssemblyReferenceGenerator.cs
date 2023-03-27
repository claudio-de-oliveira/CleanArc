namespace CleanArc.GenerateCode.Application.Generators
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
                "namespace Application",
                "{",
                "    public static class AssemblyReference",
                "    {",
                "        public static readonly Assembly Assembly = typeof(AssemblyReference).Assembly;",
                "    }",
                "}",
            };
    }
}
