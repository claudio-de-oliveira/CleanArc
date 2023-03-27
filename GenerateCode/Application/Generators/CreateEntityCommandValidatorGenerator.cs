using CleanArc.Models;

namespace CleanArc.GenerateCode.Application.Generators
{
    internal class CreateEntityCommandValidatorGenerator : FileGenerator
    {
        private readonly EntityModel entity;

        public CreateEntityCommandValidatorGenerator(string path, EntityModel entity)
            : base(path, $"Create{entity.Name}CommandValidator.cs")
        {
            this.entity = entity;
        }

        protected override List<string> Generate()
            => new()
            {
                "using FluentValidation;",
                "",
                $"namespace Application.Handlers.{entity.Name}.Create",
                "{",
                $"    public class Create{entity.Name}CommandValidator : AbstractValidator<Create{entity.Name}Command>",
                "    {",
                "",
                $"        public Create{entity.Name}CommandValidator()",
                "        {",
                "            // RuleFor(x => x.Name)",
                "            //     .NotEmpty()",
                "            //     .MaximumLength(250);",
                "        }",
                "    }",
                "}"
            };
    }
}
