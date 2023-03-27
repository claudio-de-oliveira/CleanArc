using CleanArc.Models;

namespace CleanArc.GenerateCode.Domain.Generators
{
    internal class EntityHasBeenCreatedDomainEventGenerator : FileGenerator
    {
        private readonly EntityModel entity;

        public EntityHasBeenCreatedDomainEventGenerator(string path, EntityModel entity)
            : base(path, $"{entity.Name}HasBeenCreatedDomainEvent.cs")
        {
            this.entity = entity;
        }

        protected override List<string> Generate()
            => new()
            {
                $"namespace Domain.DomainEvent.{entity.Name}",
                "{",
                $"    public sealed record {entity.Name}HasBeenCreatedDomainEvent(",
                "        Guid Id,",
                "        Guid EntityId",
                "        ) : DomainEvent(Id);",
                "}",
            };
    }
}

