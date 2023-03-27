using CleanArc.Models;

namespace CleanArc.GenerateCode.Application.Generators
{
    internal class DeleteEntityCommandHandlerGenerator : FileGenerator
    {
        private readonly EntityModel entity;

        public DeleteEntityCommandHandlerGenerator(string path, EntityModel entity)
            : base(path, $"Delete{entity.Name}CommandHandler.cs")
        {
            this.entity = entity;
        }

        protected override List<string> Generate()
            => new()
            {
                $"using Application.Handlers.{entity.Name}.Common;",
                "using Application.Interfaces.Repository;",
                "using ErrorOr;",
                "using MediatR;",
                "",
                $"namespace Application.Handlers.{entity.Name}.Delete",
                "{",
                $"    public class Delete{entity.Name}CommandHandler",
                $"        : IRequestHandler<Delete{entity.Name}Command, ErrorOr<{entity.Name}Result>>",
                "    {",
                $"        private readonly I{entity.Name}Repository _repository;",
                "",
                $"        public Delete{entity.Name}CommandHandler(I{entity.Name}Repository repository)",
                "        {",
                "            _repository = repository;",
                "        }",
                "",
                $"        public async Task<ErrorOr<{entity.Name}Result>> Handle(Delete{entity.Name}Command request, CancellationToken cancellationToken)",
                "        {",
                "            if (cancellationToken.IsCancellationRequested)",
                $"                return Domain.Errors.Error.{entity.Name}.Canceled;",
                "",
                "            try",
                "            {",
                $"                var entity = await _repository.Get{entity.Name}ById(request.Id, cancellationToken);",
                "                if (entity is null)",
                $"                    return Domain.Errors.Error.{entity.Name}.NotFound;",
                "",
                $"                await _repository.Delete{entity.Name}(request.Id, cancellationToken);",
                "",
                "                entity.Delete();",
                "",
                $"                return new {entity.Name}Result(entity);",
                "            }",
                "            catch (Exception ex)",
                "            {",
                $"                return Domain.Errors.Error.{entity.Name}.Exception(ex.Message);",
                "            }",
                "        }",
                "    }",
                "}",
            };
    }
}
