﻿namespace CleanArc.GenerateCode.Infrastructure.Generators
{
    internal class ConvertDomainEventsToOutboxMessagesInterceptorGenerator : FileGenerator
    {
        public ConvertDomainEventsToOutboxMessagesInterceptorGenerator(string path)
            : base(path, $"ConvertDomainEventsToOutboxMessagesInterceptor.cs")
        {
        }

        protected override List<string> Generate()
            => new()
            {
                "using Domain.Base;",
                "using Microsoft.EntityFrameworkCore.Diagnostics;",
                "using Microsoft.EntityFrameworkCore;",
                "using Newtonsoft.Json;",
                "using Infrastructure.Outbox;",
                "",
                "namespace Infrastructure.Interceptors",
                "{",
                "    public sealed class ConvertDomainEventsToOutboxMessagesInterceptor",
                "         : SaveChangesInterceptor",
                "    {",
                "        public override ValueTask<InterceptionResult<int>> SavingChangesAsync(",
                "            DbContextEventData eventData,",
                "            InterceptionResult<int> result,",
                "            CancellationToken cancellationToken = default)",
                "        {",
                "            DbContext? dbContext = eventData.Context;",
                "",
                "            if (dbContext is null)",
                "                return base.SavingChangesAsync(eventData, result, cancellationToken);",
                "",
                "            var events = dbContext.ChangeTracker",
                "                .Entries<AggregateRoot>()",
                "                .Select(x => x.Entity)",
                "                .SelectMany(aggregateRoot =>",
                "                {",
                "                    var domainEvents = aggregateRoot.GetDomainEvents();",
                "",
                "                    aggregateRoot.ClearDomainEvents();",
                "",
                "                    return domainEvents;",
                "                });",
                "",
                "            var outboxMessages = events",
                "                .Select(domainEvent => new OutboxMessage",
                "                {",
                "                    Id = Guid.NewGuid(),",
                "                    OccurredOnUtc = DateTime.UtcNow,",
                "                    Type = domainEvent.GetType().Name,",
                "                    Content = JsonConvert.SerializeObject(",
                "                        domainEvent,",
                "                        new JsonSerializerSettings",
                "                        {",
                "                            TypeNameHandling = TypeNameHandling.All",
                "                        })",
                "                })",
                "                .ToList();",
                "",
                "            dbContext.Set<OutboxMessage>().AddRange(outboxMessages);",
                "",
                "            return base.SavingChangesAsync(eventData, result, cancellationToken);",
                "        }",
                "    }",
                "}",
            };
    }
}
