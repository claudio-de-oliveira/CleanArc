using CleanArc.Models;

namespace CleanArc.GenerateCode.Application.Generators
{
    internal class CommonHandlerEntityListResultGenerator : FileGenerator
    {
        private readonly EntityModel entity;

        public CommonHandlerEntityListResultGenerator(string path, EntityModel entity)
            : base(path, $"{entity.Name}ListResult.cs")
        {
            this.entity = entity;
        }

        protected override List<string> Generate()
            => new()
            {
                $"namespace Application.Handlers.{entity.Name}.Common",
                "{",
                $"    public record {entity.Name}ListResult(List<Domain.Entity.{entity.Name}> List);",
                "}",
            };
    }
}
