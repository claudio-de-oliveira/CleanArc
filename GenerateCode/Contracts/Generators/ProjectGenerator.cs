namespace CleanArc.GenerateCode.Contracts.Generators
{
    internal class ProjectGenerator : FileGenerator
    {
        public ProjectGenerator(string path)
            : base(path, "Contracts.csproj")
        {
        }

        protected override List<string> Generate()
            => new()
            {
                "<Project Sdk=\"Microsoft.NET.Sdk\">",
                "",
                "  <PropertyGroup>",
                "    <TargetFramework>net7.0</TargetFramework>",
                "    <ImplicitUsings>enable</ImplicitUsings>",
                "    <Nullable>enable</Nullable>",
                "  </PropertyGroup>",
                "",
                "  <ItemGroup>",
                "    <PackageReference Include=\"Microsoft.AspNetCore.Mvc.Core\" Version=\"2.2.5\" />",
                "  </ItemGroup>",
                "",
                "</Project>",
            };
    }
}
