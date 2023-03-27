﻿using CleanArc.Models;

namespace CleanArc.GenerateCode.Presentation.Generators
{
    internal class PresentationProblemDetailsFactoryGenerator : FileGenerator
    {
        private readonly string rootspace;
        private readonly SolutionModel solution;

        public PresentationProblemDetailsFactoryGenerator(string path, string rootspace, SolutionModel solution)
            : base(path, $"{solution.Presentation!.Name}ProblemDetailsFactory.cs")
        {
            this.rootspace = rootspace;
            this.solution = solution;
        }

        protected override List<string> Generate()
            => new()
            {
                $"using {rootspace}.Common.Http;",
                "",
                "using ErrorOr;",
                "",
                "using Microsoft.AspNetCore.Mvc;",
                "using Microsoft.AspNetCore.Mvc.Infrastructure;",
                "using Microsoft.AspNetCore.Mvc.ModelBinding;",
                "using Microsoft.Extensions.Options;",
                "",
                "using System.Diagnostics;",
                "",
                $"namespace {rootspace}.Common.Errors",
                "{",
                $"    public class {solution.Presentation!.Name}ProblemDetailsFactory : ProblemDetailsFactory",
                "    {",
                "        private readonly ApiBehaviorOptions _options;",
                "",
                $"        public {solution.Presentation!.Name}ProblemDetailsFactory(IOptions<ApiBehaviorOptions> options)",
                "        {",
                "            _options = options?.Value ?? throw new ArgumentNullException(nameof(options));",
                "        }",
                "",
                "        public override ProblemDetails CreateProblemDetails(",
                "            HttpContext httpContext,",
                "            int? statusCode = null,",
                "            string? title = null,",
                "            string? type = null,",
                "            string? detail = null,",
                "            string? instance = null)",
                "        {",
                "            statusCode ??= 500;",
                "",
                "            var problemDetails = new ProblemDetails",
                "            {",
                "                Status = statusCode,",
                "                Type = type,",
                "                Detail = detail,",
                "                Instance = instance",
                "            };",
                "",
                "            if (title is not null)",
                "            {",
                "                // For validation problem details, don't override the default title with null.",
                "                problemDetails.Title = title;",
                "            }",
                "",
                "            ApplyProblemDetailsDefaults(httpContext, problemDetails, statusCode.Value);",
                "",
                "            return problemDetails;",
                "        }",
                "",
                "        public override ValidationProblemDetails CreateValidationProblemDetails(",
                "            HttpContext httpContext,",
                "            ModelStateDictionary modelStateDictionary,",
                "            int? statusCode = null,",
                "            string? title = null,",
                "            string? type = null,",
                "            string? detail = null,",
                "            string? instance = null)",
                "        {",
                "            // throw new NotImplementedException();",
                "            return new ValidationProblemDetails(modelStateDictionary)",
                "            {",
                "                Status = statusCode,",
                "                Title = title,",
                "                Type = type,",
                "                Detail = detail,",
                "                Instance = instance,",
                "            };",
                "        }",
                "",
                "        private void ApplyProblemDetailsDefaults(HttpContext httpContext, ProblemDetails problemDetails, int statusCode)",
                "        {",
                "            problemDetails.Status ??= statusCode;",
                "",
                "            if (_options.ClientErrorMapping.TryGetValue(statusCode, out var clientErrorData))",
                "            {",
                "                problemDetails.Title ??= clientErrorData.Title;",
                "                problemDetails.Type ??= clientErrorData.Link;",
                "            }",
                "",
                "            var traceId = Activity.Current?.Id ?? httpContext?.TraceIdentifier;",
                "            if (traceId is not null)",
                "            {",
                "                problemDetails.Extensions[\"traceId\"] = traceId;",
                "            }",
                "",
                "            var errors = httpContext?.Items[HttpContextItemKeys.Errors] as List<Error>;",
                "",
                "            if (errors is not null)",
                "            {",
                "                problemDetails.Extensions.Add(\"errorCodes\", errors.Select(e => e.Code));",
                "            }",
                "        }",
                "    }",
                "}",
            };
    }
}