using CleanArc.Models;

namespace CleanArc.GenerateCode.Infrastructure.Generators
{
    internal class IdempotentDomainEventHandlerGenerator : FileGenerator
    {
        private readonly SolutionModel solution;

        public IdempotentDomainEventHandlerGenerator(string path, SolutionModel solution)
            : base(path, "IdempotentDomainEventHandler.cs")
        {
            this.solution = solution;
        }

        protected override List<string> Generate()
            => new()
            {
                "using Application.Abstractions.Messaging;",
                "using Ardalis.GuardClauses;",
                "using Domain.DomainEvent;",
                "using Infrastructure.Data;",
                "using Infrastructure.Outbox;",
                "using MediatR;",
                "using Microsoft.EntityFrameworkCore;",
                "",
                "namespace Infrastructure.Idempotence",
                "{",
                "    public sealed class IdempotentDomainEventHandler<TDomainEvent> : IDomainEventHandler<TDomainEvent>",
                "        where TDomainEvent : IDomainEvent",
                "    {",
                "        private readonly INotificationHandler<TDomainEvent> _decorated;",
                $"        private readonly {solution.Name}DbContext _dbContext;",
                "",
                "        public IdempotentDomainEventHandler(",
                "            INotificationHandler<TDomainEvent> decorated,",
                $"            {solution.Name}DbContext dbContext)",
                "        {",
                "            _decorated = decorated;",
                "            _dbContext = dbContext;",
                "        }",
                "",
                "        public async Task Handle(TDomainEvent notification, CancellationToken cancellationToken)",
                "        {",
                "            string consumer = _decorated.GetType().Name;",
                "",
                $"            var table = _dbContext.Set<{solution.Name}OutboxMessageConsumer>();",
                "            Guard.Against.Null(table);",
                "",
                $"            if (await _dbContext.Set<{solution.Name}OutboxMessageConsumer>()",
                "                    .AnyAsync(",
                "                        outboxMessageConsumer =>",
                "                            outboxMessageConsumer.Id == notification.Id &&",
                "                            outboxMessageConsumer.Name == consumer,",
                "                        cancellationToken))",
                "            {",
                "                return;",
                "            }",
                "",
                "            await _decorated.Handle(notification, cancellationToken);",
                "",
                $"            _dbContext.Set<{solution.Name}OutboxMessageConsumer>()",
                $"                .Add(new {solution.Name}OutboxMessageConsumer",
                "                {",
                "                    Id = notification.Id,",
                "                    Name = consumer",
                "                });",
                "",
                "            await _dbContext.SaveChangesAsync(cancellationToken);",
                "        }",
                "    }",
                "}",
            };
    }
}
