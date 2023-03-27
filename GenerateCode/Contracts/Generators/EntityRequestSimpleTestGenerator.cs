using CleanArc.Models;

namespace CleanArc.GenerateCode.Contracts.Generators
{
    internal class EntityRequestSimpleTestGenerator : FileGenerator
    {
        private readonly int port;
        private readonly EntityModel entity;

        public EntityRequestSimpleTestGenerator(string path, int port, EntityModel entity)
            : base(path, $"{entity.Name}RequestSimpleTest.http")
        {
            this.port = port;
            this.entity = entity;
        }

        protected override List<string> Generate()
            => new()
            {
                "# Require https://github.com/madskristensen/RestClientVS",
                "# ---------------------------------------------------------------",
                "@hostname=localhost",
                $"@port={port}",
                "@host=http://{{hostname}}:{{@port}}",
                "@contentType=application/json",
                "",
                "## C R E A T E ###########################################",
                $"POST {{{{host}}}}/{entity.Name}/create",
                "Content-Type: {{contentType}}",
                "",
                "{",
                "}",
                "",
                "## U P D A T E ###########################################",
                $"PUT {{{{host}}}}/{entity.Name}/update",
                "Content-Type: {{contentType}}",
                "",
                "{",
                "}",
                "",
                "## G E T #################################################",
                $"GET {{{{host}}}}/{entity.Name}/get/id/{{{{00000000-0000-0000-0000-000000000000}}}}",
                "Content-Type: {{contentType}}",
                "",
                "## G E T #################################################",
                $"GET {{{{host}}}}/{entity.Name}/get/count",
                "Content-Type: {{contentType}}",
                "",
                "## G E T #################################################",
                $"GET {{{{host}}}}/{entity.Name}/get/list",
                "Content-Type: {{contentType}}",
                "",
                "## D E L E T E ###########################################",
                $"DELETE {{{{host}}}}/{entity.Name}/delete/{{{{00000000-0000-0000-0000-000000000000}}}}",
                "Content-Type: {{contentType}}",
         };
    }
}
