namespace CleanArc.GenerateCode.Domain.Generators
{
    internal class DomainEventGenerator : FileGenerator
    {
        public DomainEventGenerator(string path)
            : base(path, "DomainEvent.cs")
        {
        }

        protected override List<string> Generate()
            => new()
            {
                "namespace Domain.DomainEvent",
                "{",
                "    public abstract record DomainEvent(Guid Id) : IDomainEvent;",
                "}",
            };
    }
}
