using CleanArc.Models;

namespace CleanArc.GenerateCode.Application.Generators
{
    internal class GetEntityByIdQueryHandlerGenerator : FileGenerator
    {
        private readonly EntityModel entity;

        public GetEntityByIdQueryHandlerGenerator(string path, EntityModel entity)
            : base(path, $"Get{entity.Name}ByIdQueryHandler.cs")
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
                $"namespace Application.Handlers.{entity.Name}.Query",
                "{",
                $"    public class Get{entity.Name}ByIdQueryHandler",
                $"        : IRequestHandler<Get{entity.Name}ByIdQuery, ErrorOr<{entity.Name}Result>>",
                "    {",
                $"        private readonly I{entity.Name}Repository _repository;",
                "",
                $"        public Get{entity.Name}ByIdQueryHandler(I{entity.Name}Repository repository)",
                "        {",
                "            _repository = repository;",
                "        }",
                "",
                $"        public async Task<ErrorOr<{entity.Name}Result>> Handle(Get{entity.Name}ByIdQuery request, CancellationToken cancellationToken)",
                "        {",
                "            if (cancellationToken.IsCancellationRequested)",
                $"                return Domain.Errors.Error.{entity.Name}.Canceled;",
                "",
                "            try",
                "            {",
                $"                var entity = await _repository.Get{entity.Name}ById(request.Id, cancellationToken);",
                "                if (entity is null)",
                $"                    return Domain.Errors.Error.{entity.Name}.NotFound;",
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
