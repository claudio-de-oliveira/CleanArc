using CleanArc.GenerateCode.Presentation.Generators;
using CleanArc.Models;

namespace CleanArc.GenerateCode.Presentation
{
    internal class PresentationGenerateCode
    {
        public static void CreateProject(SolutionModel solution, int https, int http)
        {
            var path = Path.Combine(solution.Path, "Presentation");

            if (solution.Presentation is not null)
            {
                var controllersPath = Path.Combine(path, "Controllers");
                var mappingPath = Path.Combine(path, "Common\\Mapping");

                foreach (var obj in solution.Domain!.Objects)
                {
                    if (obj is EntityModel entity)
                    {
                        new EntityControllerGenerator(controllersPath, "Presentation", entity).CreateFile();
                        new EntityMappingConfig(mappingPath, "Presentation", entity).CreateFile();
                    }
                }
            }

            new WellcomeControllerGenerator(path, solution.Presentation!.Name, $"Seja bem vindo ao sistema {solution.Name}.").CreateFile();

            new DependencyInjectionGenerator(path, solution.Presentation!.Name).CreateFile();
            new AssemblyReferenceGenerator(path, solution.Presentation!.Name).CreateFile();
            new AppsettingsGenerator(path).CreateFile();
            new ApiControllerGenerator(path, solution.Presentation!.Name).CreateFile();
            new HttpContextItemKeysGenerator(Path.Combine(path, "Common\\Http"), solution.Presentation!.Name).CreateFile();
            new PresentationProblemDetailsFactoryGenerator(Path.Combine(path, "Common\\Errors"), solution.Presentation!.Name, solution).CreateFile();
            new MappingDependencyInjectionGenerator(Path.Combine(path, "Common"), solution.Presentation!.Name).CreateFile();
            new LaunchSettingsGenerator(Path.Combine(path, "Properties"), https, http).CreateFile();

            new ProjectGenerator(path).CreateFile();
        }
    }
}
