using CleanArc.Models;

namespace CleanArc.GenerateCode.Presentation.Generators
{
    internal class EntityControllerGenerator : FileGenerator
    {
        private readonly string rootspace;
        private readonly EntityModel entity;

        public EntityControllerGenerator(string path, string rootspace, EntityModel entity)
            : base(path, $"{entity.Name}Controller.cs")
        {
            this.rootspace = rootspace;
            this.entity = entity;
        }

        protected override List<string> Generate()
            => new()
            {
                $"using {rootspace}.Base;",
                "",
                $"using Application.Handlers.{entity.Name}.Common;",
                $"using Application.Handlers.{entity.Name}.Create;",
                $"using Application.Handlers.{entity.Name}.Delete;",
                $"using Application.Handlers.{entity.Name}.Query;",
                $"using Application.Handlers.{entity.Name}.Update;",
                "",
                $"using Contracts.Entity.{entity.Name};",
                "",
                "using ErrorOr;",
                "",
                "using MapsterMapper;",
                "",
                "using MediatR;",
                "",
                "using Microsoft.AspNetCore.Mvc;",
                "",
                $"namespace {rootspace}.Controllers",
                "{",
                $"    [Route(\"{entity.Name.ToLower()}\")]",
                $"    public class {entity.Name}Controller : ApiController",
                "    {",
                "        private readonly ISender _mediator;",
                "        private readonly IMapper _mapper;",
                "",
                $"        public {entity.Name}Controller(ISender mediator, IMapper mapper)",
                "        {",
                "            _mediator = mediator;",
                "            _mapper = mapper;",
                "        }",
                "",
                "        [HttpGet(\"get/id/{id}\")]",
                $"        public async Task<IActionResult> Get{entity.Name}ById(Guid id)",
                "        {",
                $"            var query = new Get{entity.Name}ByIdQuery(",
                "                id",
                "                );",
                "",
                $"            ErrorOr<{entity.Name}Result> result = await _mediator.Send(query);",
                "",
                "            return result.Match(",
                $"                result => Ok(_mapper.Map<{entity.Name}Response>(result)),",
                "                errors => Problem(errors)",
                "                );",
                "        }",
                "",
                "        [HttpPut(\"update\")]",
                $"        public async Task<IActionResult> Update{entity.Name}([FromBody] Update{entity.Name}Request request)",
                "        {",
                $"            var command = _mapper.Map<Update{entity.Name}Command>(request);",
                "",
                $"            ErrorOr<{entity.Name}Result> result = await _mediator.Send(command);",
                "",
                "            return result.Match(",
                "                result => NoContent(),",
                "                errors => Problem(errors)",
                "                );",
                "        }",
                "",
                "        [HttpPost(\"create\")]",
                $"        public async Task<IActionResult> Create{entity.Name}([FromBody] Create{entity.Name}Request request)",
                "        {",
                $"            var command = _mapper.Map<Create{entity.Name}Command>(request);",
                "",
                "            ErrorOr<Guid> result = await _mediator.Send(command);",
                "",
                "            return result.Match(",
                "                // Use CreatedAtAction to return 201 CreatedAtAction",
                $"                id => CreatedAtAction(nameof(Create{entity.Name}), id),",
                "                errors => Problem(errors)",
                "                );",
                "        }",
                "",
                "        [HttpDelete(\"delete/{id}\")]",
                $"        public async Task<IActionResult> Delete{entity.Name}(Guid id)",
                "        {",
                $"            var command = new Delete{entity.Name}Command(id);",
                "",
                $"            ErrorOr<{entity.Name}Result> result = await _mediator.Send(command);",
                "",
                "            return result.Match(",
                "                result => NoContent(),",
                "                errors => Problem(errors)",
                "                );",
                "        }",
                "    }",
                "}",
            };
    }
}
