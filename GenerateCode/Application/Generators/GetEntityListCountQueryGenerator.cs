using CleanArc.Models;

namespace CleanArc.GenerateCode.Application.Generators
{
    internal class GetEntityListCountQueryGenerator : FileGenerator
    {
        private readonly EntityModel entity;

        public GetEntityListCountQueryGenerator(string path, EntityModel entity)
            : base(path, $"Get{entity.Name}CountQuery.cs")
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
                $"    public record Get{entity.Name}CountQuery(",
                $"        ) : IRequest<ErrorOr<int>>;",
                "}",
            };
    }
}
