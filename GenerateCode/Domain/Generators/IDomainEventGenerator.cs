namespace CleanArc.GenerateCode.Domain.Generators
{
    internal class IDomainEventGenerator : FileGenerator
    {
        public IDomainEventGenerator(string path)
            : base(path, "IDomainEvent.cs")
        {
        }

        protected override List<string> Generate()
            => new()
            {
                "using MediatR;",
                "",
                "namespace Domain.DomainEvent",
                "{",
                "    public interface IDomainEvent : INotification",
                "    {",
                "        public Guid Id { get; init; }",
                "    }",
                "}",
            };
    }
}
