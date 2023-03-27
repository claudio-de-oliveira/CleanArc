namespace CleanArc.GenerateCode.Presentation.Generators
{
    internal class ProjectGenerator : FileGenerator
    {
        public ProjectGenerator(string path)
            : base(path, "Presentation.csproj")
        {
        }

        protected override List<string> Generate()
            => new()
            {
                "<Project Sdk=\"Microsoft.NET.Sdk.Web\">",
                "",
                "	<PropertyGroup>",
                "		<TargetFramework>net7.0</TargetFramework>",
                "		<ImplicitUsings>enable</ImplicitUsings>",
                "		<Nullable>enable</Nullable>",
                $"		<UserSecretsId>{Guid.NewGuid()}</UserSecretsId>",
                "	</PropertyGroup>",
                "",
                "	<ItemGroup>",
                "		<PackageReference Include=\"Mapster\" Version=\"7.3.0\" />",
                "		<PackageReference Include=\"Mapster.DependencyInjection\" Version=\"1.0.0\" />",
                "		<PackageReference Include=\"Serilog.Sinks.Console\" Version=\"4.1.0\" />",
                "		<PackageReference Include=\"Microsoft.AspNetCore.Mvc.Abstractions\" Version=\"2.2.0\" />",
                "	</ItemGroup>",
                "",
                "	<ItemGroup>",
                "		<ProjectReference Include=\"..\\Application\\Application.csproj\" />",
                "		<ProjectReference Include=\"..\\Contracts\\Contracts.csproj\" />",
                "		<ProjectReference Include=\"..\\Infrastructure\\Infrastructure.csproj\" />",
                "	</ItemGroup>",
                "",
                "</Project>",
            };
    }
}
