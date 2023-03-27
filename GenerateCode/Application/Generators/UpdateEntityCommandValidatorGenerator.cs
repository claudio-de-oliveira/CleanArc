using CleanArc.Models;

namespace CleanArc.GenerateCode.Application.Generators
{
    internal class UpdateEntityCommandValidatorGenerator : FileGenerator
    {
        private readonly EntityModel entity;

        public UpdateEntityCommandValidatorGenerator(string path, EntityModel entity)
            : base(path, $"Update{entity.Name}CommandValidator.cs")
        {
            this.entity = entity;
        }

        protected override List<string> Generate()
            => new()
            {
                "using FluentValidation;",
                "",
                $"namespace Application.Handlers.{entity.Name}.Update",
                "{",
                $"    public class Update{entity.Name}CommandValidator : AbstractValidator<Update{entity.Name}Command>",
                "    {",
                $"        public Update{entity.Name}CommandValidator()",
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
