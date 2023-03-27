using CleanArc.GenerateCode.Application.Generators;
using CleanArc.Models;

namespace CleanArc.GenerateCode.Application
{
    internal static class ApplicationGenerateCode
    {
        public static void CreateProject(SolutionModel solution)
        {
            var root = Path.Combine(solution.Path, "Application");

            var repositoryPath = Path.Combine(root, "Interfaces\\Repository");
            var handlersPath = Path.Combine(root, "Handlers");

            if (solution.Domain is not null)
            {
                foreach (var obj in solution.Domain.Objects)
                {
                    if (obj is EntityModel entity)
                    {
                        string pth = repositoryPath;

                        new RepositoryInterfaceGenerator(pth, entity.Name).CreateFile();

                        pth = $"{handlersPath}\\{entity.Name}\\Common";
                        new CommonHandlerEntityListResultGenerator(pth, entity).CreateFile();
                        new CommonHandlerEntityResultGenerator(pth, entity).CreateFile();

                        pth = $"{handlersPath}\\{entity.Name}\\Create";
                        new CreateEntityCommandGenerator(pth, entity).CreateFile();
                        new CreateEntityCommandHandlerGenerator(pth, entity).CreateFile();
                        new CreateEntityCommandValidatorGenerator(pth, entity).CreateFile();

                        pth = $"{handlersPath}\\{entity.Name}\\Update";
                        new UpdateEntityCommandGenerator(pth, entity).CreateFile();
                        new UpdateEntityCommandHandlerGenerator(pth, entity).CreateFile();
                        new UpdateEntityCommandValidatorGenerator(pth, entity).CreateFile();

                        pth = $"{handlersPath}\\{entity.Name}\\Delete";
                        new DeleteEntityCommandGenerator(pth, entity).CreateFile();
                        new DeleteEntityCommandHandlerGenerator(pth, entity).CreateFile();

                        pth = $"{handlersPath}\\{entity.Name}\\Queries";
                        new GetEntityByIdQueryGenerator(pth, entity).CreateFile();
                        new GetEntityByIdQueryHandlerGenerator(pth, entity).CreateFile();
                        new GetEntityListQueryGenerator(pth, entity).CreateFile();
                        new GetEntityListQueryHandlerGenerator(pth, entity).CreateFile();
                        new GetEntityListCountQueryGenerator(pth, entity).CreateFile();
                        new GetEntityListCountQueryHandlerGenerator(pth, entity).CreateFile();

                        new ValidationBehaviorGenerator($"{handlersPath}\\Behaviors").CreateFile();

                        if (solution.Use is not null && solution.Use.Contains("POLLY"))
                        {
                            pth = $"{handlersPath}\\{entity.Name}\\Events";
                            new EntityHasBeenCreatedDomainEventHandlerGenerator(pth, entity).CreateFile();
                            new EntityHasBeenDeletedDomainEventHandlerGenerator(pth, entity).CreateFile();
                            new EntityHasBeenUpdatedDomainEventHandlerGenerator(pth, entity).CreateFile();

                            pth = $"{handlersPath}\\Abstractions\\Messaging";
                            new IDomainEventHandlerGenerator(pth).CreateFile();
                        }

                        new AssemblyReferenceGenerator(root).CreateFile();
                        new DependencyInjectionGenerator(root).CreateFile();

                        new ProjectGenerator(root).CreateFile();
                    }
                }
            }

            new IEmailSenderGenerator(Path.Combine(root, "Interfaces\\Email")).CreateFile();

        }

    }
}
