using CleanArc.Models;

namespace CleanArc.GenerateCode.Domain.Generators
{
    internal class ErrorsGenerator : FileGenerator
    {
        private readonly EntityModel entity;

        public ErrorsGenerator(string path, EntityModel entity)
            : base(path, $"Error.{entity.Name}.cs")
        {
            this.entity = entity;
        }

        protected override List<string> Generate()
            => new()
            {
                "using Domain.Enums;",
                "",
                "namespace Domain.Errors",
                "{",
                "    public partial class Error",
                "    {",
                $"        public static class {entity.Name}",
                "        {",
                "            public static ErrorOr.Error DataBaseError",
                "                => ErrorOr.Error.Validation(",
                $"                    code: \"{entity.Name}.DataBaseError\",",
                "                    description: \"Problemas com o banco de dados.\");",
                "            public static ErrorOr.Error DuplicateCode",
                "                => ErrorOr.Error.Conflict(",
                $"                    code: \"{entity.Name}.DuplicateCode\",",
                "                    description: \"Código já existe.\");",
                "            public static ErrorOr.Error NotFound",
                "                => ErrorOr.Error.NotFound(",
                $"                    code: \"{entity.Name}.NotFound\",",
                "                    description: \"Componente não encontrado.\");",
                "            public static ErrorOr.Error Canceled",
                "                => ErrorOr.Error.Custom(",
                "                    type: ((int)CustomErrorType.CANCELED),",
                $"                    code: \"{entity.Name}.Canceled\",",
                "                    description: \"Operação cancelada.\");",
                "            public static ErrorOr.Error Exception(string message)",
                $"                => ErrorOr.Error.Failure(code: \"{entity.Name}.Exception\", description: message);",
                "        }",
                "    }",
                "}",
            };

    }
}
