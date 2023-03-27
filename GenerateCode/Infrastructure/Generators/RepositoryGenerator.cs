namespace CleanArc.GenerateCode.Infrastructure.Generators
{
    internal class RepositoryGenerator : FileGenerator
    {
        private readonly string entity;
        private readonly string dbContext;

        public RepositoryGenerator(string path, string entity, string dbContext)
            : base(path, $"{entity}Repository.cs")
        {
            this.entity = entity;
            this.dbContext = dbContext;
        }

        protected override List<string> Generate()
            => new()
            {
                "using AbstractRepository;",
                "using Domain.Entity;",
                "using Application.Interfaces.Repository;",
                "using Infrastructure.Data;",
                "",
                "namespace Infrastructure.Services.Repository",
                "{",
                $"    public class {entity}Repository : AbstractRepository<{entity}>, I{entity}Repository",
                "    {",
                $"        private readonly I{entity}Repository _repository;",
                "",
                $"        public {entity}Repository({dbContext} dbContext)",
                $"            : base(dbContext)",
                "        {",
                $"            _repository = new {entity}Repository(dbContext);",
                "        }",
                "",
                $"        public async Task<bool> Exists(Func<{entity}, bool> predicate, CancellationToken cancellationToken)",
                "            => await ReadDefault(predicate, cancellationToken) is not null;",
                $"        public async Task<{entity}?> Get{entity}ById(Guid id, CancellationToken cancellationToken)",
                "            => await ReadDefault(o => o.Id == id, cancellationToken);",
                $"        public async Task<int> Get{entity}Count(CancellationToken cancellationToken)",
                "            => await Count(o => true, cancellationToken);",
                $"        public async Task<List<{entity}>> Get{entity}List(CancellationToken cancellationToken)",
                "            => await ListDefault(o => true, cancellationToken);",
                "",
                $"        public async Task<Guid> Create{entity}({entity} entityToCreate, CancellationToken cancellationToken)",
                "        { ",
                "            var entityCreated = await CreateDefault(entityToCreate, cancellationToken);",
                "",
                "            if (entityCreated is not null)",
                "                return entityCreated.Id;",
                "            return Guid.Empty;",
                "        }",
                $"        public async Task Update{entity}({entity} entityToUpdate, CancellationToken cancellationToken)",
                "        {",
                "            DetachLocal(o => o.Id == entityToUpdate.Id);",
                "            await UpdateDefault(entityToUpdate, cancellationToken);",
                "        }",
                $"        public async Task Delete{entity}(Guid id, CancellationToken cancellationToken)",
                "        {",
                "            var entityToDelete = await ReadDefault(o => o.Id == id, cancellationToken);",
                "            if (entityToDelete is not null)",
                "                 await DeleteDefault(entityToDelete, cancellationToken);",
                "        }",
                "    }",
                "}"
            };
    }
}
