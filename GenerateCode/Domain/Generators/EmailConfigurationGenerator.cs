namespace CleanArc.GenerateCode.Domain.Generators
{
    internal class EmailConfigurationGenerator : FileGenerator
    {
        public EmailConfigurationGenerator(string path)
            : base(path, "EmailConfiguration.cs")
        {
        }

        protected override List<string> Generate()
            => new()
            {
                "namespace Domain.Email",
                "{",
                "    public class EmailConfiguration",
                "    {",
                "        public string From { get; set; } = default!;",
                "        public string SmtpServer { get; set; } = default!;",
                "        public string UserName { get; set; } = default!;",
                "        public string Password { get; set; } = default!;",
                "        public int Port { get; set; }",
                "    }",
                "}",
            };
    }
}
