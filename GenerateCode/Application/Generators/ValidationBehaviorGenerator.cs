﻿namespace CleanArc.GenerateCode.Application.Generators
{
    internal class ValidationBehaviorGenerator : FileGenerator
    {
        public ValidationBehaviorGenerator(string path)
            : base(path, "ValidationBehavior.cs")
        {
        }

        protected override List<string> Generate()
            => new()
            {
                "using ErrorOr;",
                "using FluentValidation;",
                "using MediatR;",
                "",
                "namespace Application.Behaviors",
                "{",
                "    public class ValidationBehavior<TRequest, TResponse>",
                "        : IPipelineBehavior<TRequest, TResponse>",
                "        where TRequest : IRequest<TResponse>",
                "        where TResponse : IErrorOr",
                "    {",
                "        private readonly IValidator<TRequest>? _validator;",
                "",
                "        public ValidationBehavior(IValidator<TRequest>? validator = null)",
                "        {",
                "            _validator = validator;",
                "        }",
                "",
                "        public async Task<TResponse> Handle(",
                "            TRequest request,",
                "            RequestHandlerDelegate<TResponse> next,",
                "            CancellationToken cancellationToken",
                "            )",
                "        {",
                "            if (_validator is null)",
                "                return await next();",
                "",
                "            // before handler",
                "            var validationResult = await _validator.ValidateAsync(request, cancellationToken);",
                "            if (validationResult.IsValid)",
                "                return await next();",
                "",
                "            // after handler",
                "            var errors = validationResult.Errors",
                "                .ConvertAll(validationFailure => Error.Validation(",
                "                    validationFailure.PropertyName,",
                "                    validationFailure.ErrorMessage",
                "                    ));",
                "",
                "            return (dynamic)errors;",
                "        }",
                "    }",
                "}",
            };
    }
}
