﻿namespace CleanArc.GenerateCode.Domain.Generators
{
    internal class ProjectGenerator : FileGenerator
    {
        public ProjectGenerator(string path)
            : base(path, "Domain.csproj")
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
                "        <PackageReference Include=\"Ardalis.GuardClauses\" Version=\"4.0.1\" />",
                "        <PackageReference Include=\"MediatR\" Version=\"11.1.0\" />",
                "        <PackageReference Include=\"ErrorOr\" Version=\"1.2.1\" />",
                "        <PackageReference Include=\"Serilog\" Version=\"2.12.0\" />",
                "        <PackageReference Include=\"NETCore.MailKit\" Version=\"2.1.0\" />",
                "        <PackageReference Include=\"Microsoft.AspNetCore.Http.Features\" Version=\"5.0.17\" />",
                "    </ItemGroup>",
                "",
                "</Project>",
            };
    }
}
