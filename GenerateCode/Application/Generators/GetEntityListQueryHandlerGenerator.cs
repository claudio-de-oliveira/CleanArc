using CleanArc.Models;

namespace CleanArc.GenerateCode.Application.Generators
{
    internal class GetEntityListQueryHandlerGenerator : FileGenerator
    {
        private readonly EntityModel entity;

        public GetEntityListQueryHandlerGenerator(string path, EntityModel entity)
            : base(path, $"Get{entity.Name}ListQueryHandler.cs")
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
                $"    public class Get{entity.Name}ListQueryHandler",
                $"        : IRequestHandler<Get{entity.Name}ListQuery, ErrorOr<{entity.Name}ListResult>>",
                "    {",
                $"        private readonly I{entity.Name}Repository _repository;",
                "",
                $"        public Get{entity.Name}ListQueryHandler(I{entity.Name}Repository repository)",
                "        {",
                "            _repository = repository;",
                "        }",
                "",
                $"        public async Task<ErrorOr<{entity.Name}ListResult>> Handle(Get{entity.Name}ListQuery request, CancellationToken cancellationToken)",
                "        {",
                "            if (cancellationToken.IsCancellationRequested)",
                $"                return Domain.Errors.Error.{entity.Name}.Canceled;",
                "",
                "            try",
                "            {",
                $"                var list = await _repository.Get{entity.Name}List(cancellationToken);",
                $"                return new {entity.Name}ListResult(list);",
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
