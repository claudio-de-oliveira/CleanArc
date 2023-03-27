namespace CleanArc.GenerateCode.Application.Generators
{
    internal class ProjectGenerator : FileGenerator
    {
        public ProjectGenerator(string path)
            : base(path, "Application.csproj")
        {
        }

        protected override List<string> Generate()
            => new()
            {
                "<Project Sdk=\"Microsoft.NET.Sdk\">",
                "",
                "    <PropertyGroup>",
                "        <TargetFramework>net7.0</TargetFramework>",
                "        <ImplicitUsings>enable</ImplicitUsings>",
                "        <Nullable>enable</Nullable>",
                "    </PropertyGroup>",
                "",
                "    <ItemGroup>",
                "        <PackageReference Include=\"FluentValidation.DependencyInjectionExtensions\" Version=\"11.4.0\" />",
                "        <PackageReference Include=\"MediatR.Extensions.Microsoft.DependencyInjection\" Version=\"11.0.0\" />",
                "        <PackageReference Include=\"Ardalis.GuardClauses\" Version=\"4.0.1\" />",
                "        <PackageReference Include=\"ErrorOr\" Version=\"1.2.1\" />",
                "        <PackageReference Include=\"Microsoft.Extensions.FileProviders.Abstractions\" Version=\"7.0.0\" />",
                "        <PackageReference Include=\"Serilog\" Version=\"2.12.0\" />",
                "    </ItemGroup>",
                "",
                "    <ItemGroup>",
                "        <ProjectReference Include=\"..\\Domain\\Domain.csproj\" />",
                "    </ItemGroup>",
                "",
                "</Project>",
            };
    }
}
