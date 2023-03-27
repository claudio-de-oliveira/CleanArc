using CleanArc.Models;

namespace CleanArc.GenerateCode.Infrastructure.Generators
{
    internal class ProcessOutboxMessagesJobGenerator : FileGenerator
    {
        private readonly SolutionModel solution;

        public ProcessOutboxMessagesJobGenerator(string path, SolutionModel solution)
            : base(path, "ProcessOutboxMessagesJob.cs")
        {
            this.solution = solution;
        }

        protected override List<string> Generate()
            => new()
            {
                "using Domain.DomainEvent;",
                "using Infrastructure.Data;",
                "using Infrastructure.Outbox;",
                "using MediatR;",
                "using Microsoft.EntityFrameworkCore;",
                "using Newtonsoft.Json;",
                "using Polly;",
                "using Polly.Retry;",
                "using Quartz;",
                "",
                "namespace Infrastructure.BackgroundJobs",
                "{",
                "    [DisallowConcurrentExecution]",
                "    public class ProcessOutboxMessagesJob : IJob",
                "    {",
                "        private static readonly JsonSerializerSettings JsonSerializerSettings = new()",
                "        {",
                "            TypeNameHandling = TypeNameHandling.All",
                "        };",
                "",
                $"        private readonly {solution.Name}DbContext _dbContext;",
                "        private readonly IPublisher _publisher;",
                "",
                $"        public ProcessOutboxMessagesJob({solution.Name}DbContext dbContext, IPublisher publisher)",
                "        {",
                "            _dbContext = dbContext;",
                "            _publisher = publisher;",
                "        }",
                "        public async Task Execute(IJobExecutionContext context)",
                "        {",
                $"            List<{solution.Name}OutboxMessage> messages = await _dbContext",
                $"                .Set<{solution.Name}OutboxMessage>()",
                "                .Where(m => m.ProcessedOnUtc == null && m.Error == null)",
                "                .Take(20)",
                "                .ToListAsync(context.CancellationToken);",
                "",
                $"            foreach ({solution.Name}OutboxMessage outboxMessage in messages)",
                "            {",
                "                IDomainEvent? domainEvent = JsonConvert",
                "                    .DeserializeObject<IDomainEvent>(",
                "                        outboxMessage.Content,",
                "                        JsonSerializerSettings);",
                "",
                "                if (domainEvent is null)",
                "                    continue;",
                "",
                "                AsyncRetryPolicy policy = Policy",
                "                    .Handle<Exception>()",
                "                    .WaitAndRetryAsync(",
                "                        3,",
                "                        attempt => TimeSpan.FromMilliseconds(50 * attempt));",
                "",
                "                PolicyResult result = await policy.ExecuteAndCaptureAsync(() =>",
                "                    _publisher.Publish(",
                "                        domainEvent,",
                "                        context.CancellationToken));",
                "",
                "                outboxMessage.Error = result.FinalException?.ToString();",
                "                outboxMessage.ProcessedOnUtc = DateTime.UtcNow;",
                "            }",
                "",
                "            await _dbContext.SaveChangesAsync();",
                "        }",
                "    }",
                "}",
            };
    }
}
