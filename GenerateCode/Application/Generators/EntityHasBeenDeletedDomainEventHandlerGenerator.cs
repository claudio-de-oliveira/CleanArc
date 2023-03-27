using CleanArc.Models;

namespace CleanArc.GenerateCode.Application.Generators
{
    internal class EntityHasBeenDeletedDomainEventHandlerGenerator : FileGenerator
    {
        private readonly EntityModel entity;

        public EntityHasBeenDeletedDomainEventHandlerGenerator(string path, EntityModel entity)
            : base(path, $"{entity.Name}HasBeenDeletedDomainEventHandler.cs")
        {
            this.entity = entity;
        }

        protected override List<string> Generate()
            => new()
            {
                "using Application.Abstractions.Messaging;",
                "using Application.Interfaces.Repository;",
                $"using Domain.DomainEvent.{entity.Name};",
                "using Ardalis.GuardClauses;",
                "using Serilog;",
                "",
                $"namespace Application.Handlers.{entity.Name}.Events",
                "{",
                $"    internal sealed class {entity.Name}HasBeenDeletedDomainEventHandler",
                $"        : IDomainEventHandler<{entity.Name}HasBeenDeletedDomainEvent>",
                "    {",
                $"        private readonly I{entity.Name}Repository _repository;",
                "",
                $"        public {entity.Name}HasBeenDeletedDomainEventHandler(",
                $"            I{entity.Name}Repository repository",
                "            )",
                "        {",
                "            _repository = repository;",
                "        }",
                "",
                $"        public async Task Handle({entity.Name}HasBeenDeletedDomainEvent notification, CancellationToken cancellationToken)",
                "        {",
                $"            Log.Warning($\"Foi excluída uma instância de {entity.Name}\");",
                "        }",
                "    }",
                "}",
            };
    }
}
