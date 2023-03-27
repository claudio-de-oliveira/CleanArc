namespace CleanArc.GenerateCode.Infrastructure.Generators
{
    internal class DataBaseOptionsSetupGenerator : FileGenerator
    {
        public DataBaseOptionsSetupGenerator(string path)
            : base(path, "DataBaseOptionsSetup.cs")
        {
        }

        protected override List<string> Generate()
            => new()
            {
                "using Ardalis.GuardClauses;",
                "",
                "using Microsoft.Extensions.Configuration;",
                "using Microsoft.Extensions.Options;",
                "",
                "using Serilog;",
                "",
                "namespace Infrastructure.Options",
                "{",
                "    public class DatabaseOptionsSetup : IConfigureOptions<DatabaseOptions>",
                "    {",
                "        private const string ConfigurationSectionName = \"DatabaseOptions\";",
                "",
                "        private readonly IConfiguration _configuration;",
                "",
                "        public DatabaseOptionsSetup(IConfiguration configuration)",
                "        {",
                "            _configuration = configuration;",
                "        }",
                "",
                "        public void Configure(DatabaseOptions options)",
                "        {",
                "            var server = _configuration[\"DbServer\"] ?? \"localhost\";",
                "            var port = _configuration[\"DbPort\"] ?? \"1433\";",
                "            var user = _configuration[\"DbUser\"] ?? \"SA\";",
                "            var password = _configuration[\"DbPassword\"] ?? \"@Eaafe_301\";",
                "",
                "            var section = _configuration.GetSection(ConfigurationSectionName);",
                "            // var CommandTimeout = section[\"CommandTimeout\"] ?? \"30\";",
                "            // var MaxRetryCount = section[\"MaxRetryCount\"] ?? \"3\";",
                "            var database = section[\"Database\"];",
                "            ",
                "            if (database is null)",
                "                Log.Error(\"Não foi encontrado o nome do Banco de Dados\");",
                "",
                "            if (port != \"1433\")",
                "                server += $\",{port}\";",
                "",
                "            var connectionString =",
                "                $\"Data Source={server};\" +",
                "                $\"Initial Catalog={database};\" +",
                "                $\"User ID={user};\" +",
                "                $\"Password={password};\" +",
                "                $\"Encrypt=False;\" +",
                "                $\"TrustServerCertificate=False;\";",
                "",
                "            _configuration.GetSection(ConfigurationSectionName).Bind(options);",
                "",
                "            options.ConnectionString = connectionString;",
                "        }",
                "    }",
                "}",
            };
    }
}
