namespace CleanArc.GenerateCode.Presentation.Generators
{
    internal class HttpContextItemKeysGenerator : FileGenerator
    {
        private readonly string rootspace;

        public HttpContextItemKeysGenerator(string path, string rootspace)
            : base(path, "HttpContextItemKeys.cs")
        {
            this.rootspace = rootspace;
        }

        protected override List<string> Generate()
            => new()
            {
                $"namespace {rootspace}.Common.Http",
                "{",
                "    public static class HttpContextItemKeys",
                "    {",
                "        public const string Errors = \"errors\";",
                "    }",
                "}",
            };
    }
}
