using CleanArc.Models;

namespace CleanArc.GenerateCode.Application.Generators
{
    internal class EntityHasBeenUpdatedDomainEventHandlerGenerator : FileGenerator
    {
        private readonly EntityModel entity;

        public EntityHasBeenUpdatedDomainEventHandlerGenerator(string path, EntityModel entity)
            : base(path, $"{entity.Name}HasBeenUpdatedDomainEventHandler.cs")
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
                $"    internal sealed class {entity.Name}HasBeenUpdatedDomainEventHandler",
                $"        : IDomainEventHandler<{entity.Name}HasBeenUpdatedDomainEvent>",
                "    {",
                $"        private readonly I{entity.Name}Repository _repository;",
                "",
                $"        public {entity.Name}HasBeenUpdatedDomainEventHandler(",
                $"            I{entity.Name}Repository repository",
                "            )",
                "        {",
                "            _repository = repository;",
                "        }",
                "",
                $"        public async Task Handle({entity.Name}HasBeenUpdatedDomainEvent notification, CancellationToken cancellationToken)",
                "        {",
                $"            Log.Warning($\"Foi atualizada uma instância de {entity.Name}\");",
                "        }",
                "    }",
                "}",
            };
    }
}
