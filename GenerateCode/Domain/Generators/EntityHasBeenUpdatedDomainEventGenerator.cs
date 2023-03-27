using CleanArc.Models;

namespace CleanArc.GenerateCode.Domain.Generators
{
    internal class EntityHasBeenUpdatedDomainEventGenerator : FileGenerator
    {
        private readonly EntityModel entity;

        public EntityHasBeenUpdatedDomainEventGenerator(string path, EntityModel entity)
            : base(path, $"{entity.Name}HasBeenUpdatedDomainEvent.cs")
        {
            this.entity = entity;
        }

        protected override List<string> Generate()
            => new()
            {
                $"namespace Domain.DomainEvent.{entity.Name}",
                "{",
                $"    public sealed record {entity.Name}HasBeenUpdatedDomainEvent(",
                "        Guid Id,",
                "        Guid EntityId",
                "        ) : DomainEvent(Id);",
                "}",
            };
    }
}
