using CleanArc.GenerateCode.Application;
using CleanArc.GenerateCode.Contracts;
using CleanArc.GenerateCode.Domain;
using CleanArc.GenerateCode.Infrastructure;
using CleanArc.GenerateCode.Presentation;

namespace CleanArc
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var parser = new Parser.Parser();

            string text =
                "Solution = \"IdealSoft\"\n" +
                "Path = \"D:\\users\\clalu\\Source\\repos\\IdealSoftX\"\n" +
                "Presentation = \"Presentation\"  { \"F1\" \"F2\" }\n" +
                "Application = \"Application\"  { \"F1\" \"F2\" }\n" +
                "Infrastructure = \"Infrastructure\" { \"F1\" \"F2\" }\n" +
                "Contracts = \"Contracts\"\n" +
                "{\n" +
                "    Entity = \"Produto\"\n" +
                "    {\n" +
                "        Contract = \"CreateProdutoRequest\"\n" +

                "            [\n" +
                "                Property:\n" +
                "                    type = \"string\"\n" +
                "                    name = \"Name\"\n" +
                "                Property:\n" +
                "                    type = \"decimal\"\n" +
                "                    name = \"ValorUnitario\"\n" +
                "                Property:\n" +
                "                    type = \"int\"\n" +
                "                    name = \"Quantidade\"\n" +

                "            ]\n" +
                "        Contract = \"UpdateProdutoRequest\"\n" +
                "            [\n" +
                "                Property:\n" +
                "                    type = \"Guid\"\n" +
                "                    name = \"Id\"\n" +
                "                Property:\n" +
                "                    type = \"string\"\n" +
                "                    name = \"Name\"\n" +
                "                Property:\n" +

                "                    type = \"decimal\"\n" +
                "                    name = \"ValorUnitario\"\n" +
                "                Property:\n" +
                "                    type = \"int\"\n" +
                "                    name = \"Quantidade\"\n" +
                "            ]\n" +
                "        Contract = \"DeleteProduto\"\n" +
                "            [\n" +
                "                Property:\n" +
                "                    type = \"Guid\"\n" +

                "                    name = \"Id\"\n" +
                "            ]\n" +
                "        Contract = \"ProdutoResult\"\n" +
                "            [\n" +
                "                Property:\n" +
                "                    type = \"Guid\"\n" +
                "                    name = \"Id\"\n" +
                "                Property:\n" +
                "                    type = \"string\"\n" +
                "                    name = \"Name\"\n" +

                "                Property:\n" +
                "                    type = \"decimal\"\n" +
                "                    name = \"ValorUnitario\"\n" +
                "                Property:\n" +
                "                    type = \"int\"\n" +
                "                    name = \"Quantidade\"\n" +
                "            ]\n" +
                // "    Entity = \"Entity2\"\n" +
                // "    {\n" +
                // "        Contract = \"Contract21\"\n" +
                // "            [\n" +
                // "                Property:\n" +
                // "                    type = \"Contract21Type\"\n" +
                // "                    name = \"Contract21Name\"\n" +
                // "            ]\n" +
                // "        Contract = \"Contract22\"\n" +
                // "            [\n" +
                // "                Property:\n" +
                // "                    type = \"Contract22Type\"\n" +
                // "                    name = \"Contract22Name\"\n" +
                // "            ]\n" +
                "    }\n" +
                "}\n" +

                "{ \"F1\" \"F2\" }\n" +
                "Domain = \"Domain\"\n" +
                "{\n" +
                "    Value = \"Value11\"\n" +
                "    [\n" +
                "        Property:\n" +
                "            type = \"Value11Type\"\n" +
                "            name = \"Value11Name\"\n" +
                "    ]\n" +
                "\n" +

                "    Entity = \"Produto\"\n" +
                "    [\n" +
                "        Property:\n" +
                "            type = \"string\"\n" +
                "            name = \"Name\"\n" +
                "        Property:\n" +
                "            type = \"decimal\"\n" +
                "            name = \"ValorUnitario\"\n" +
                "        Property:\n" +
                "            type = \"int\"\n" +

                "            name = \"Quantidade\"\n" +
                "    ]\n" +
                "    Entity = \"Carrinho\"\n" +
                "        [\n" +
                "            Property:\n" +
                "                type = \"List<string>\"\n" +
                "                name = \"Produtos\"\n" +
                "            Property:\n" +
                "                type = \"decimal\"\n" +
                "                name = \"ValorTotal\"\n" +

                "        ]\n" +
                "    Entity = \"Cliente\"\n" +
                "        [\n" +
                "            Property:\n" +
                "                type = \"string\"\n" +
                "                name = \"Nome\"\n" +
                "            Property:\n" +
                "                type = \"string\"\n" +
                "                name = \"Email\"\n" +
                "            Property:\n" +
                "                type = \"int\"\n" +
                "                name = \"Idade\"\n" +
                "            Property:\n" +
                "                type = \"string\"\n" +
                "                name = \"Telefone\"\n" +
                "            Property:\n" +
                "                type = \"string\"\n" +
                "                name = \"Endereco\"\n" +
                "        ]\n" +
                "}\n" +
                "{ \"F1\" \"F2\" }\n" +
                "Test = \"Test1\"\n" +
                "{ \"F1\" \"F2\" }\n" +
                "Use\n" +
                "{\n" +
                "    \"Polly\"\n" +
                "    \"Mail\"\n" +
                "    \"Quartz\"\n" +
                "}\n" +
                "#";

            parser.Parse(text, null);
            var solution = parser.Result;
            if (solution is not null)
            {
                var random = new Random();

                int https = 7000 + random.Next(100);
                int http = 5000 + random.Next(100);

                DomainGenerateCode.CreateProject(solution);
                InfrastructureGenerateCode.CreateProject(solution);
                ApplicationGenerateCode.CreateProject(solution);
                PresentationGenerateCode.CreateProject(solution, https, http);
                ContractsGenerateCode.CreateProject(solution, http);
            }
        }
    }
}