using CleanArc.Models;

namespace CleanArc.GenerateCode.Application.Generators
{
    internal class GetEntityListQueryGenerator : FileGenerator
    {
        private readonly EntityModel entity;

        public GetEntityListQueryGenerator(string path, EntityModel entity)
            : base(path, $"Get{entity.Name}ListQuery.cs")
        {
            this.entity = entity;
        }

        protected override List<string> Generate()
            => new()
            {
                $"using Application.Handlers.{entity.Name}.Common;",
                "using ErrorOr;",
                "using MediatR;",
                "",
                $"namespace Application.Handlers.{entity.Name}.Query",
                "{",
                $"    public record Get{entity.Name}ListQuery(",
                $"        ) : IRequest<ErrorOr<{entity.Name}ListResult>>;",
                "}",
            };
    }
}
