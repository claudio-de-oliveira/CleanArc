using CleanArc.Models;

namespace CleanArc.GenerateCode.Application.Generators
{
    internal class CommonHandlerEntityResultGenerator : FileGenerator
    {
        private readonly EntityModel entity;

        public CommonHandlerEntityResultGenerator(string path, EntityModel entity)
            : base(path, $"{entity.Name}Result.cs")
        {
            this.entity = entity;
        }

        protected override List<string> Generate()
            => new()
            {
                $"namespace Application.Handlers.{entity.Name}.Common",
                "{",
                $"    public record {entity.Name}Result(Domain.Entity.{entity.Name} {entity.Name});",
                "}",
            };
    }
}
