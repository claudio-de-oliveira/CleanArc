using CleanArc.Models;

namespace CleanArc.GenerateCode.Application.Generators
{
    internal class GetEntityListCountQueryHandlerGenerator : FileGenerator
    {
        private readonly EntityModel entity;

        public GetEntityListCountQueryHandlerGenerator(string path, EntityModel entity)
            : base(path, $"Get{entity.Name}CountQueryHandler.cs")
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
                $"    public class Get{entity.Name}CountQueryHandler",
                $"        : IRequestHandler<Get{entity.Name}CountQuery, ErrorOr<int>>",
                "    {",
                $"        private readonly I{entity.Name}Repository _repository;",
                "",
                $"        public Get{entity.Name}CountQueryHandler(I{entity.Name}Repository repository)",
                "        {",
                "            _repository = repository;",
                "        }",
                "",
                $"        public async Task<ErrorOr<int>> Handle(Get{entity.Name}CountQuery request, CancellationToken cancellationToken)",
                "        {",
                "            if (cancellationToken.IsCancellationRequested)",
                $"                return Domain.Errors.Error.{entity.Name}.Canceled;",
                "",
                "            try",
                "            {",
                $"                var count = await _repository.Get{entity.Name}Count(cancellationToken);",
                "                return count;",
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
