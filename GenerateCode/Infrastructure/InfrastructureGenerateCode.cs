using CleanArc.GenerateCode.Infrastructure.Generators;
using CleanArc.Models;

namespace CleanArc.GenerateCode.Infrastructure
{
    internal static class InfrastructureGenerateCode
    {
        public static void CreateProject(SolutionModel solution)
        {
            var path = Path.Combine(solution.Path, "Infrastructure");

            if (solution.Domain is not null)
            {
                var repositoryPath = Path.Combine(path, "Services");

                foreach (var obj in solution.Domain.Objects)
                {
                    if (obj is EntityModel entity)
                    {
                        new RepositoryGenerator(repositoryPath, entity.Name, $"{solution.Name}DbContext").CreateFile();
                        new ConfigurationGenerator(Path.Combine(path, "Configurations"), entity).CreateFile();
                    }
                }
            }

            new DependencyInjectionGenerator(path, solution, $"{solution.Name}DbContext").CreateFile();
            new AssemblyReferenceGenerator(path).CreateFile();
            new ConvertDomainEventsToOutboxMessagesInterceptorGenerator(Path.Combine(path, "Interceptors")).CreateFile();
            new IdempotentDomainEventHandlerGenerator(Path.Combine(path, "Idempotence"), solution).CreateFile();
            new EmailSenderGenerator(Path.Combine(path, "Email")).CreateFile();

            new ProcessOutboxMessagesJobGenerator(Path.Combine(path, "Outbox"), solution).CreateFile();
            new OutboxMessageConfigurationGenerator(Path.Combine(path, "Data\\Outbox"), solution).CreateFile();
            new OutboxMessageGenerator(Path.Combine(path, "Outbox"), solution).CreateFile();
            new OutboxMessageConsumerGenerator(Path.Combine(path, "Outbox"), solution).CreateFile();

            var pth = Path.Combine(path, "Data");
            new DataTableNamesGenerator(pth, solution).CreateFile();
            new DbContextFactoryGenerator(pth, solution).CreateFile();
            new DbContextGenerator(pth, solution).CreateFile();

            pth = Path.Combine(path, "Options");
            new DataBaseOptionsGenerator(pth).CreateFile();
            new DataBaseOptionsSetupGenerator(pth).CreateFile();

            new ProjectGenerator(path).CreateFile();
        }
    }
}
