﻿namespace CleanArc.GenerateCode.Presentation.Generators
{
    internal class AppsettingsGenerator : FileGenerator
    {
        public AppsettingsGenerator(string path)
            : base(path, "appsettings.json")
        {
        }

        protected override List<string> Generate()
            => new()
            {
                "{",
                "  \"Logging\": {",
                "    \"LogLevel\": {",
                "      \"Default\": \"Information\",",
                "      \"Microsoft.AspNetCore\": \"Warning\"",
                "    }",
                "  },",
                "  \"DatabaseOptions\": {",
                "    \"Database\": \"PraeceptorDB\",",
                "    \"MaxRetryCount\": 3,",
                "    \"CommandTimeout\": 30,",
                "    \"EnableDetailedErrors\": false,",
                "    \"EnableSensitiveDataLogging\": true",
                "  },",
                "  \"EmailConfiguration\": {",
                "    \"From\": \"clalulana@gmail.com\",",
                "    \"SmtpServer\": \"smtp.gmail.com\",",
                "    \"Username\": \"clalulana@gmail.com\",",
                "    \"Password\": \"soiiqrbppndemitd\",",
                "    \"Port\": 465",
                "  },",
                "  \"AllowedHosts\": \"*\"",
                "}",
            };
    }
}
