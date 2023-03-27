using CleanArc.Models;

namespace CleanArc.GenerateCode.Application.Generators
{
    internal class GetEntityByIdQueryGenerator : FileGenerator
    {
        private readonly EntityModel entity;

        public GetEntityByIdQueryGenerator(string path, EntityModel entity)
            : base(path, $"Get{entity.Name}ByIdQuery.cs")
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
                $"    public record Get{entity.Name}ByIdQuery(",
                "        Guid Id",
                $"        ) : IRequest<ErrorOr<{entity.Name}Result>>;",
                "}",
            };
    }
}
