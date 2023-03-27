using CleanArc.GenerateCode.Contracts.Generators;
using CleanArc.Models;

namespace CleanArc.GenerateCode.Contracts
{
    internal class ContractsGenerateCode
    {
        public static void CreateProject(SolutionModel solution, int port)
        {
            var path = Path.Combine(solution.Path, "Contracts");

            if (solution.Contracts is not null)
            {
                var contractsPath = Path.Combine(path, "Entity");
                var testPath = Path.Combine(path, "SimpleTest");

                foreach (var obj in solution.Domain!.Objects)
                {
                    if (obj is EntityModel entity)
                    {
                        new UpdateEntityRequestGenerator(Path.Combine(contractsPath, entity.Name), entity).CreateFile();
                        new CreateEntityRequestGenerator(Path.Combine(contractsPath, entity.Name), entity).CreateFile();
                        new EntityResponseGenerator(Path.Combine(contractsPath, entity.Name), entity).CreateFile();
                        new EntityRequestSimpleTestGenerator(testPath, port, entity).CreateFile();
                    }
                }

                new ProjectGenerator(path).CreateFile();
            }
        }
    }
}
