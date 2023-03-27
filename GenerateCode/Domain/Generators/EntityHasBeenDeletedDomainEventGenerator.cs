using CleanArc.Models;

namespace CleanArc.GenerateCode.Domain.Generators
{
    internal class EntityHasBeenDeletedDomainEventGenerator : FileGenerator
    {
        private readonly EntityModel entity;

        public EntityHasBeenDeletedDomainEventGenerator(string path, EntityModel entity)
            : base(path, $"{entity.Name}HasBeenDeletedDomainEvent.cs")
        {
            this.entity = entity;
        }

        protected override List<string> Generate()
            => new()
            {
                $"namespace Domain.DomainEvent.{entity.Name}",
                "{",
                $"    public sealed record {entity.Name}HasBeenDeletedDomainEvent(",
                "        Guid Id,",
                "        Guid EntityId",
                "        ) : DomainEvent(Id);",
                "}",
            };
    }
}
