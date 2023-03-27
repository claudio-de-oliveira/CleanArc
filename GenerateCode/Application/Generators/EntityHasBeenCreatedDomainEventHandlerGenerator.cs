using CleanArc.Models;

namespace CleanArc.GenerateCode.Application.Generators
{
    internal class EntityHasBeenCreatedDomainEventHandlerGenerator : FileGenerator
    {
        private readonly EntityModel entity;

        public EntityHasBeenCreatedDomainEventHandlerGenerator(string path, EntityModel entity)
            : base(path, $"{entity.Name}HasBeenCreatedDomainEventHandler.cs")
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
                $"    internal sealed class {entity.Name}HasBeenCreatedDomainEventHandler",
                $"        : IDomainEventHandler<{entity.Name}HasBeenCreatedDomainEvent>",
                "    {",
                $"        private readonly I{entity.Name}Repository _repository;",
                "",
                $"        public {entity.Name}HasBeenCreatedDomainEventHandler(",
                $"            I{entity.Name}Repository repository",
                "            )",
                "        {",
                "            _repository = repository;",
                "        }",
                "",
                $"        public async Task Handle({entity.Name}HasBeenCreatedDomainEvent notification, CancellationToken cancellationToken)",
                "        {",
                $"            Log.Warning($\"Foi criada uma instância de {entity.Name}\");",
                "        }",
                "    }",
                "}",
            };
    }
}
