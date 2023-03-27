using CleanArc.GenerateCode.Domain.Generators;
using CleanArc.Models;

namespace CleanArc.GenerateCode.Domain
{
    internal class DomainGenerateCode
    {
        public static void CreateProject(SolutionModel solution)
        {
            var path = Path.Combine(solution.Path, "Domain");

            if (solution.Domain is not null)
            {
                var repositoryPath = Path.Combine(path, "Entities");
                var errosPath = Path.Combine(path, "Errors");

                foreach (var obj in solution.Domain.Objects)
                {
                    if (obj is EntityModel entity)
                    {
                        new EntityGenerator(repositoryPath, entity).CreateFile();
                        new ErrorsGenerator(errosPath, entity).CreateFile();

                        if (solution.Use is not null && solution.Use.Contains("POLLY"))
                        {
                            var pth = $"{path}\\DomainEvent\\{entity.Name}";
                            new EntityHasBeenCreatedDomainEventGenerator(pth, entity).CreateFile();
                            new EntityHasBeenDeletedDomainEventGenerator(pth, entity).CreateFile();
                            new EntityHasBeenUpdatedDomainEventGenerator(pth, entity).CreateFile();
                        }
                    }
                }
                new IDomainEventGenerator($"{path}\\DomainEvent").CreateFile();
                new DomainEventGenerator($"{path}\\DomainEvent").CreateFile();

                new MessageGenerator($"{path}\\Mail").CreateFile();
                new EmailConfigurationGenerator($"{path}\\Mail").CreateFile();

                new BaseEntityGenerator($"{path}\\Base").CreateFile();
                new BaseValueGenerator($"{path}\\Base").CreateFile();
                new AggregateRootGenerator($"{path}\\Base").CreateFile();
                new EnumsGenerator($"{path}\\Enums").CreateFile();
                new AssemblyReferenceGenerator(path).CreateFile();
                new ProjectGenerator(path).CreateFile();
            }
        }
    }
}
