using CleanArc.Models;

namespace CleanArc.GenerateCode.Application.Generators
{
    internal class DeleteEntityCommandGenerator : FileGenerator
    {
        private readonly EntityModel entity;

        public DeleteEntityCommandGenerator(string path, EntityModel entity)
            : base(path, $"Delete{entity.Name}Command.cs")
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
                $"namespace Application.Handlers.{entity.Name}.Delete",
                "{",
                $"   public record Delete{entity.Name}Command(",
                "       Guid Id",
                $"       ) : IRequest<ErrorOr<{entity.Name}Result>>;",
                "}"
            };
    }
}
