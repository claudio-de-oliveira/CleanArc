namespace CleanArc.GenerateCode.Application.Generators
{
    internal class RepositoryInterfaceGenerator : FileGenerator
    {
        private readonly string entity;

        public RepositoryInterfaceGenerator(string path, string entity)
            : base(path, $"I{entity}Repository.cs")
        {
            this.entity = entity;
        }

        protected override List<string> Generate()
            => new()
            {
                "using Domain.Entity;",
                "",
                "namespace Application.Interfaces.Repository",
                "{",
                $"    public interface I{entity}Repository",
                "    {",
                $"        Task<bool> Exists(Func<{entity}, bool> predicate, CancellationToken cancellationToken);",
                $"        Task<{entity}?> Get{entity}ById(Guid id, CancellationToken cancellationToken);",
                $"        Task<int> Get{entity}Count(CancellationToken cancellationToken);",
                $"        Task<List<{entity}>> Get{entity}List(CancellationToken cancellationToken);",
                $"        Task<Guid> Create{entity}({entity} entityToCreate, CancellationToken cancellationToken);",
                $"        Task Update{entity}({entity} entityToUpdate, CancellationToken cancellationToken);",
                $"        Task Delete{entity}(Guid id, CancellationToken cancellationToken);",
                "    }",
                "}"
            };
    }
}
