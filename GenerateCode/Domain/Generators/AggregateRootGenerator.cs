namespace CleanArc.GenerateCode.Domain.Generators
{
    internal class AggregateRootGenerator : FileGenerator
    {
        public AggregateRootGenerator(string path)
            : base(path, $"AggregateRoot.cs")
        {
        }

        protected override List<string> Generate()
            => new()
            {
                "using Domain.DomainEvent;",
                "",
                "namespace Domain.Base",
                "{",
                "    public abstract class AggregateRoot : BaseEntity",
                "    {",
                "        private readonly List<IDomainEvent> _domainEvents = new();",
                "",
                "        protected AggregateRoot(Guid id)",
                "            : base(id)",
                "        { /* NOthing more todo */ }",
                "",
                "        public IReadOnlyCollection<IDomainEvent> GetDomainEvents()",
                "            => _domainEvents.ToList();",
                "        public void ClearDomainEvents()",
                "            => _domainEvents.Clear();",
                "        protected void RaiseDomainEvent(IDomainEvent domainEvent)",
                "            => _domainEvents.Add(domainEvent);",
                "    }",
                "}",
            };
    }
}
