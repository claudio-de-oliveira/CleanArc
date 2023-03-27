namespace CleanArc.GenerateCode.Presentation.Generators
{
    internal class LaunchSettingsGenerator : FileGenerator
    {
        private readonly int httpsPort;
        private readonly int httpPort;

        public LaunchSettingsGenerator(string path, int httpsPort, int httpPort)
            : base(path, "launchSettings.json")
        {
            this.httpsPort = httpsPort;
            this.httpPort = httpPort;
        }

        protected override List<string> Generate()
            => new()
            {
                "{",
                "  \"profiles\": {",
                "    \"https\": {",
                "      \"commandName\": \"Project\",",
                "      \"launchBrowser\": true,",
                "      \"launchUrl\": \"Wellcome\",",
                "      \"environmentVariables\": {",
                "        \"ASPNETCORE_ENVIRONMENT\": \"Development\"",
                "      },",
                "      \"dotnetRunMessages\": true,",
                $"      \"applicationUrl\": \"https://localhost:{httpsPort}\"",
                "    },",
                "    \"http\": {",
                "      \"commandName\": \"Project\",",
                "      \"launchBrowser\": true,",
                "      \"launchUrl\": \"Wellcome\",",
                "      \"environmentVariables\": {",
                "        \"ASPNETCORE_ENVIRONMENT\": \"Development\"",
                "      },",
                "      \"dotnetRunMessages\": true,",
                $"      \"applicationUrl\": \"http://localhost:{httpPort}\"",
                "    }",
                "  },",
                "  \"$schema\": \"https://json.schemastore.org/launchsettings.json\",",
                "}",
            };
    }
}
