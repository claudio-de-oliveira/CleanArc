namespace CleanArc.GenerateCode.Infrastructure.Generators
{
    internal class ProjectGenerator : FileGenerator
    {
        public ProjectGenerator(string path)
            : base(path, "Infrastructure.csproj")
        {
        }

        protected override List<string> Generate()
            => new()
            {
                "<Project Sdk=\"Microsoft.NET.Sdk\">",
                "",
                "	<PropertyGroup>",
                "		<TargetFramework>net7.0</TargetFramework>",
                "		<ImplicitUsings>enable</ImplicitUsings>",
                "		<Nullable>enable</Nullable>",
                "	</PropertyGroup>",
                "",
                "	<ItemGroup>",
                "		<PackageReference Include=\"Ardalis.GuardClauses\" Version=\"4.0.1\" />",
                "		<PackageReference Include=\"clalulana.AbstractRepository\" Version=\"1.0.0\" />",
                "		<PackageReference Include=\"FluentValidation\" Version=\"11.5.1\" />",
                "		<PackageReference Include=\"Microsoft.AspNetCore.Http\" Version=\"2.2.2\" />",
                "		<PackageReference Include=\"Microsoft.AspNetCore.Identity\" Version=\"2.2.0\" />",
                "		<PackageReference Include=\"Microsoft.AspNetCore.Identity.EntityFrameworkCore\" Version=\"7.0.3\" />",
                "		<PackageReference Include=\"Microsoft.EntityFrameworkCore\" Version=\"7.0.3\" />",
                "		<PackageReference Include=\"Microsoft.EntityFrameworkCore.Relational\" Version=\"7.0.3\" />",
                "		<PackageReference Include=\"Microsoft.EntityFrameworkCore.SqlServer\" Version=\"7.0.3\" />",
                "		<PackageReference Include=\"Microsoft.EntityFrameworkCore.Tools\" Version=\"7.0.3\">",
                "			<PrivateAssets>all</PrivateAssets>",
                "			<IncludeAssets>runtime; build; native; contentfiles; analyzers; buildtransitive</IncludeAssets>",
                "		</PackageReference>",
                "		<PackageReference Include=\"Microsoft.Extensions.Configuration\" Version=\"7.0.0\" />",
                "		<PackageReference Include=\"Microsoft.Extensions.Hosting\" Version=\"7.0.1\" />",
                "		<PackageReference Include=\"Microsoft.Extensions.Options.ConfigurationExtensions\" Version=\"7.0.0\" />",
                "		<PackageReference Include=\"Microsoft.Extensions.DependencyInjection.Abstractions\" Version=\"7.0.0\" />",
                "		<PackageReference Include=\"NETCore.MailKit\" Version=\"2.1.0\" />",
                "		<PackageReference Include=\"Newtonsoft.Json\" Version=\"13.0.3\" />",
                "		<PackageReference Include=\"Polly\" Version=\"7.2.3\" />",
                "		<PackageReference Include=\"Quartz\" Version=\"3.6.2\" />",
                "		<PackageReference Include=\"Quartz.Extensions.DependencyInjection\" Version=\"3.6.2\" />",
                "		<PackageReference Include=\"Quartz.Extensions.Hosting\" Version=\"3.6.2\" />",
                "		<PackageReference Include=\"Serilog\" Version=\"2.12.0\" />",
                "		<PackageReference Include=\"Serilog.Sinks.Console\" Version=\"4.1.0\" />",
                "	</ItemGroup>",
                "",
                "	<ItemGroup>",
                "		<ProjectReference Include=\"..\\Application\\Application.csproj\" />",
                "	</ItemGroup>",
                "",
                "</Project>",
            };
    }
}
