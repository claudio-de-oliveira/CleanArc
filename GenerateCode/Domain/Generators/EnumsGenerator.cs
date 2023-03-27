namespace CleanArc.GenerateCode.Domain.Generators
{
    internal class EnumsGenerator : FileGenerator
    {
        public EnumsGenerator(string path)
            : base(path, $"CustomErrorType.cs")
        {
        }

        protected override List<string> Generate()
            => new()
            {
                "namespace Domain.Enums",
                "{",
                "    public enum CustomErrorType",
                "    {",
                "        CANCELED = 1",
                "    }",
                "}",
            };

    }
}
