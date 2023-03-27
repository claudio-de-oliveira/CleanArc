namespace CleanArc.GenerateCode.Presentation.Generators
{
    internal class WellcomeControllerGenerator : FileGenerator
    {
        private readonly string rootspace;
        private readonly string message;

        public WellcomeControllerGenerator(string path, string rootspace, string message)
            : base(path, "WellcomeController.cs")
        {
            this.rootspace = rootspace;
            this.message = message;
        }

        protected override List<string> Generate()
            => new()
            {
                "using Microsoft.AspNetCore.Mvc;",
                "",
                $"namespace {rootspace}.Controllers",
                "{",
                "    [ApiController]",
                "    [Route(\"[controller]\")]",
                "    public class WellcomeController : ControllerBase",
                "    {",
                "        [HttpGet]",
                "        public string Get()",
                "        {",
                $"            return \"{message}\";",
                "        }",
                "    }",
                "}",
            };
    }
}
