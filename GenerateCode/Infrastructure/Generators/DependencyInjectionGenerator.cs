using CleanArc.Models;

namespace CleanArc.GenerateCode.Infrastructure.Generators
{
    internal class DependencyInjectionGenerator : FileGenerator
    {
        private readonly string dbContext;
        private readonly SolutionModel solutionModel;

        public DependencyInjectionGenerator(string path, SolutionModel solutionModel, string dbContext)
            : base(path, $"DependencyInjection.cs")
        {
            this.dbContext = dbContext;
            this.solutionModel = solutionModel;
        }

        protected override List<string> Generate()
        {
            var lines = new List<string>()
            {
                "using Application.Interfaces;",
                "using Infrastructure.Data;",
                "using Infrastructure.Options;",
                "using Infrastructure.Persistence;",
                "using Ardalis.GuardClauses;",
                "using Infrastructure.Interceptors;",
                "using Quartz;",
                "using Infrastructure.BackgroundJobs;",
                "using Infrastructure.Email;",
                "using Microsoft.Extensions.Options;",
                "using Infrastructure.Services.Repository;",
                "using Application.Interfaces.Repository;",
                "",
                "using Microsoft.EntityFrameworkCore;",
                "using Microsoft.Extensions.Configuration;",
                "using Microsoft.Extensions.DependencyInjection;",
                "using Microsoft.Extensions.Options;",
                "",
                "using Serilog;",
                "using Serilog.Events;",
                "using Serilog.Sinks.SystemConsole.Themes;",
                "",
                "using System.Reflection;",
                "",
                "namespace Infrastructure",
                "{",
                "    public static class DependencyInjection",
                "    {",
                "        public static IServiceCollection AddInfrastructure(",
                "            this IServiceCollection services,",
                "            ConfigurationManager configuration",
                "            )",
                "        {",
                "            InstallSerilog(configuration);",
                "",
                "            // https://www.youtube.com/watch?v=bN57EDYD6M0&t=476s",
                "            services.ConfigureOptions<DatabaseOptionsSetup>();",
            };


            if (solutionModel.Domain is not null)
            {
                if (solutionModel.Use is not null && solutionModel.Use.Contains("POLLY"))
                {
                    lines.Add("");
                    lines.Add("            services.AddSingleton<ConvertDomainEventsToOutboxMessagesInterceptor>();");
                    lines.Add("");
                    lines.Add($"            services.AddDbContext<{dbContext}>(");
                    lines.Add("                (serviceProvider, dbContextOptionsBuilder) =>");
                    lines.Add("                {");
                    lines.Add("                    var databaseOptions = serviceProvider.GetService<IOptions<DatabaseOptions>>()!.Value;");
                    lines.Add("");
                    lines.Add("                    // Console.WriteLine(\"#######################################################\");");
                    lines.Add("                    // Console.WriteLine(databaseOptions.ConnectionString);");
                    lines.Add("                    // Console.WriteLine(\"######################################################\");");
                    lines.Add("");
                    lines.Add("                    var inteceptor = serviceProvider.GetService<ConvertDomainEventsToOutboxMessagesInterceptor>();");
                    lines.Add("                    Guard.Against.Null(inteceptor);");
                    lines.Add("");
                    lines.Add("                    dbContextOptionsBuilder.UseSqlServer(databaseOptions.ConnectionString, sqlServerAction =>");
                    lines.Add("                    {");
                    lines.Add("                        sqlServerAction.EnableRetryOnFailure(databaseOptions.MaxRetryCount);");
                    lines.Add("                        sqlServerAction.CommandTimeout(databaseOptions.CommandTimeout);");
                    lines.Add("                    })");
                    lines.Add("                    .AddInterceptors(inteceptor);");
                    lines.Add("");
                    lines.Add("                    dbContextOptionsBuilder.EnableDetailedErrors(databaseOptions.EnableDetailedErrors);");
                    lines.Add("                    dbContextOptionsBuilder.EnableSensitiveDataLogging(databaseOptions.EnableSensitiveDataLogging);");
                    lines.Add("                });");
                    lines.Add("");
                }

                if (solutionModel.Use is not null && solutionModel.Use.Contains("QUARTZ"))
                {
                    lines.Add("            /* AGENDAMENTO DE TAREFAS */");
                    lines.Add("            services.AddQuartz(configure =>");
                    lines.Add("            {");
                    lines.Add("                var jobKey = new JobKey(nameof(ProcessOutboxMessagesJob));");
                    lines.Add("            ");
                    lines.Add("                configure");
                    lines.Add("                    .AddJob<ProcessOutboxMessagesJob>(jobKey)");
                    lines.Add("                    .AddTrigger(");
                    lines.Add("                        trigger =>");
                    lines.Add("                            trigger.ForJob(jobKey)");
                    lines.Add("                                .WithSimpleSchedule(");
                    lines.Add("                                    schedule =>");
                    lines.Add("                                        schedule.WithIntervalInSeconds(10)");
                    lines.Add("                                            .RepeatForever()));");
                    lines.Add("            ");
                    lines.Add("                configure.UseMicrosoftDependencyInjectionJobFactory();");
                    lines.Add("            });");
                    lines.Add("            ");
                    lines.Add("            services.AddQuartzHostedService();");
                    lines.Add("");
                }

                foreach (var obj in solutionModel.Domain.Objects)
                {
                    if (obj is EntityModel entity)
                    {
                        lines.Add($"            services.AddScoped<I{entity.Name}Repository, {entity.Name}Repository>();");
                    }
                }
            }

            lines.Add("");
            lines.Add("            return services;");
            lines.Add("        }");

            lines.Add("");

            if (solutionModel.Use is not null && solutionModel.Use.Contains("MAIL"))
            {
                lines.Add("        public static IServiceCollection InstallEmail(");
                lines.Add("            this IServiceCollection services,");
                lines.Add("            ConfigurationManager configuration)");
                lines.Add("        {");
                lines.Add("            var emailConfig = configuration");
                lines.Add("                .GetSection(\"EmailConfiguration\")");
                lines.Add("                .Get<EmailConfiguration>();");
                lines.Add("            Guard.Against.Null(emailConfig);");
                lines.Add("            services.AddSingleton(emailConfig);");
                lines.Add("            services.AddScoped<IEmailSender, EmailSender>();");
                lines.Add("");
                lines.Add("            services.Configure<FormOptions>(o =>");
                lines.Add("            {");
                lines.Add("                o.ValueLengthLimit = int.MaxValue;");
                lines.Add("                o.MultipartBodyLengthLimit = int.MaxValue;");
                lines.Add("                o.MemoryBufferThreshold = int.MaxValue;");
                lines.Add("            });");
                lines.Add("");
                lines.Add("            return services;");
                lines.Add("        }");
                lines.Add("");
            }

            lines.Add("        private static void InstallSerilog(ConfigurationManager _)");
            lines.Add("        {");
            lines.Add("            Log.Logger = new LoggerConfiguration()");
            lines.Add("                .MinimumLevel.Override(\"Microsoft\", LogEventLevel.Warning)");
            lines.Add("                .MinimumLevel.Override(\"Microsoft.Hosting.Lifetime\", LogEventLevel.Information)");
            lines.Add("                .MinimumLevel.Override(\"System\", LogEventLevel.Warning)");
            lines.Add("                .MinimumLevel.Override(\"Microsoft.AspNetCore.Authentication\", LogEventLevel.Information)");
            lines.Add("                .Enrich.FromLogContext()");
            lines.Add("                .WriteTo.Console(");
            lines.Add("                    outputTemplate: \"[{Timestamp:HH:mm:ss} {Level}] {SourceContext}{NewLine}{Message:lj}{NewLine}{Exception}{NewLine}\",");
            lines.Add("                    theme: SystemConsoleTheme.Colored");
            lines.Add("                    )");
            lines.Add("                .CreateLogger();");
            lines.Add("        }");
            lines.Add("    }");
            lines.Add("}");

            return lines;
        }
    }
}
