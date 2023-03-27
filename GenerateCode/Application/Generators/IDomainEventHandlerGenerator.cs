namespace CleanArc.GenerateCode.Application.Generators
{
    internal class IDomainEventHandlerGenerator : FileGenerator
    {
        public IDomainEventHandlerGenerator(string path)
            : base(path, "IDomainEventHandler.cs")
        {
        }

        protected override List<string> Generate()
            => new()
            {
                "using Domain.DomainEvent;",
                "using MediatR;",
                "",
                "namespace Application.Abstractions.Messaging",
                "{",
                "    public interface IDomainEventHandler<TEvent> ",
                "        : INotificationHandler<TEvent>",
                "        where TEvent : IDomainEvent",
                "    {",
                "    }",
                "}",
            };
    }
}
