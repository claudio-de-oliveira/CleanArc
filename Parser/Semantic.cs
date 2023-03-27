using AbstractLL;

using CleanArc.Models;

namespace CleanArc.Parser
{
    internal class Semantic : AbstractSemantic<SolutionModel>
    {
        public override void Inicializa()
        {
            // nothing todo
        }

        public override void Execute(AbstractTAG action, Stack<AbstractTAG> stk, Stack<AbstractToken> tokens, AbstractEnvironment<SolutionModel> environment)
        {
            if (action == Tag._Done)
            {
                Console.WriteLine("Cheguei ao Done");
            }
            else if (action == Tag._Echo)
            {
                stk.Peek().SetAttribute(0, action.GetAttribute(0));
            }
            else if (action == Tag._Skip1)
            {
                stk.ElementAt(1).SetAttribute(0, action.GetAttribute(0));
            }
            else if (action == Tag._SolutionName)
            {
                var token = (StringToken)tokens.Pop();

                Console.WriteLine($"Solution name: {token.Value}");
                ((SolutionEnvironment)environment).Solution.Name = token.Value;
            }
            else if (action == Tag._SolutionPath)
            {
                var token = (StringToken)tokens.Pop();

                Console.WriteLine($"Solution path: {token.Value}");
                ((SolutionEnvironment)environment).Solution.Path = token.Value;
            }
            else if (action == Tag._PresentationName)
            {
                var token = (StringToken)tokens.Pop();

                if (((SolutionEnvironment)environment).Solution.Presentation is null)
                    ((SolutionEnvironment)environment).Solution.Presentation = new();

                ((SolutionEnvironment)environment).Solution.Presentation!.Name = token.Value;
            }
            else if (action == Tag._ApplicationName)
            {
                var token = (StringToken)tokens.Pop();

                if (((SolutionEnvironment)environment).Solution.Application is null)
                    ((SolutionEnvironment)environment).Solution.Application = new();

                ((SolutionEnvironment)environment).Solution.Application!.Name = token.Value;
            }
            else if (action == Tag._InfrastructureName)
            {
                var token = (StringToken)tokens.Pop();

                if (((SolutionEnvironment)environment).Solution.Infrastructure is null)
                    ((SolutionEnvironment)environment).Solution.Infrastructure = new();

                ((SolutionEnvironment)environment).Solution.Infrastructure!.Name = token.Value;
            }
            else if (action == Tag._ContractsName)
            {
                var token = (StringToken)tokens.Pop();

                if (((SolutionEnvironment)environment).Solution.Contracts is null)
                    ((SolutionEnvironment)environment).Solution.Contracts = new();

                var project = new ContractProjectModel { Name = token.Value };

                ((SolutionEnvironment)environment).Solution.Contracts = project;
            }
            else if (action == Tag._DomainName)
            {
                var token = (StringToken)tokens.Pop();

                if (((SolutionEnvironment)environment).Solution.Domain is null)
                    ((SolutionEnvironment)environment).Solution.Domain = new();

                ((SolutionEnvironment)environment).Solution.Domain!.Name = token.Value;
            }
            else if (action == Tag._TestName)
            {
                var token = (StringToken)tokens.Pop();

                if (((SolutionEnvironment)environment).Solution.Test is null)
                    ((SolutionEnvironment)environment).Solution.Test = new();

                ((SolutionEnvironment)environment).Solution.Test!.Name = token.Value;
            }

            else if (action == Tag._PresentationFiles)
            {
                ((SolutionEnvironment)environment).Solution.Presentation!.OtherFiles =
                    (List<string>)action.GetAttribute(0);
            }
            else if (action == Tag._ApplicationFiles)
            {
                ((SolutionEnvironment)environment).Solution.Application!.OtherFiles =
                    (List<string>)action.GetAttribute(0);
            }
            else if (action == Tag._InfrastructureFiles)
            {
                ((SolutionEnvironment)environment).Solution.Infrastructure!.OtherFiles =
                     (List<string>)action.GetAttribute(0);
            }
            else if (action == Tag._ContractsFiles)
            {
                ((SolutionEnvironment)environment).Solution.Contracts!.OtherFiles =
                     (List<string>)action.GetAttribute(0);
            }
            else if (action == Tag._DomainFiles)
            {
                ((SolutionEnvironment)environment).Solution.Domain!.OtherFiles =
                    (List<string>)action.GetAttribute(0);
            }
            else if (action == Tag._TestFiles)
            {
                ((SolutionEnvironment)environment).Solution.Test!.OtherFiles =
                    (List<string>)action.GetAttribute(0);
            }

            else if (action == Tag._ContractList)
            {
                ((SolutionEnvironment)environment).Solution.Contracts!.Entities =
                    (List<ContractEntityModel>)action.GetAttribute(0);
            }
            else if (action == Tag._DomainList)
            {
                ((SolutionEnvironment)environment).Solution.Domain!.Objects =
                    (List<ObjectModel>)action.GetAttribute(0);
            }
            else if (action == Tag._ContratName)
            {
                var token = (StringToken)tokens.Pop();
                stk.ElementAt(1).SetAttribute(1, token.Value);
            }
            else if (action == Tag._Type)
            {
                var token = (StringToken)tokens.Pop();
                stk.ElementAt(3).SetAttribute(0, token.Value);
            }
            else if (action == Tag._Name)
            {
                var token = (StringToken)tokens.Pop();
                stk.ElementAt(3).SetAttribute(0, token.Value);
            }
            else if (action == Tag._CreateListOfFiles)
            {
                stk.Peek().SetAttribute(0, new List<string>());
            }
            else if (action == Tag._CreateEmptyListOfFiles)
            {
                stk.Peek().SetAttribute(0, new List<string>());
            }
            else if (action == Tag._CreateListOfContractsByEntity)
            {
                stk.Peek().SetAttribute(0, new List<ContractEntityModel>());
            }
            else if (action == Tag._CreateListOfContracts)
            {
                stk.Peek().SetAttribute(0, new List<ContractModel>());
            }
            else if (action == Tag._CreateListOfProperties)
            {
                stk.Peek().SetAttribute(0, new List<PropertyModel>());
            }
            else if (action == Tag._AddContractsByEntity)
            {
                var contracts = (ContractEntityModel)action.GetAttribute(0);
                var list = (List<ContractEntityModel>)action.GetAttribute(1);

                list.Add(contracts);

                stk.Peek().SetAttribute(0, list);
            }
            else if (action == Tag._AddContract)
            {
                var contract = (ContractModel)action.GetAttribute(0);
                var list = (List<ContractModel>)action.GetAttribute(1);

                list.Add(contract);

                stk.Peek().SetAttribute(0, list);
            }
            else if (action == Tag._AddProperty)
            {
                var property = (PropertyModel)action.GetAttribute(0);
                var list = (List<PropertyModel>)action.GetAttribute(1);

                list.Add(property);

                stk.Peek().SetAttribute(0, list);
            }
            else if (action == Tag._CreateListOfPackages)
            {
                stk.Peek().SetAttribute(0, new List<string>());
            }
            else if (action == Tag._AddPackage)
            {
                var use = (StringToken)tokens.Pop();
                var list = (List<string>)action.GetAttribute(0);

                list.Add(use.Value.ToUpper());

                stk.Peek().SetAttribute(0, list);
            }
            else if (action == Tag._Use)
            {
                ((SolutionEnvironment)environment).Solution.Use =
                    (List<string>)action.GetAttribute(0);
            }
            else if (action == Tag._CreateListOfObjects)
            {
                stk.Peek().SetAttribute(0, new List<ObjectModel>());
            }
            else if (action == Tag._AddObject)
            {
                var obj = (ObjectModel)action.GetAttribute(0);
                var list = (List<ObjectModel>)action.GetAttribute(1);

                list.Add(obj);

                stk.Peek().SetAttribute(0, list);
            }
            else if (action == Tag._EntityContracts)
            {
                var list = (List<ContractModel>)action.GetAttribute(0);
                var name = (string)action.GetAttribute(1);

                var contracts = new ContractEntityModel { Name = name, Contracts = list };

                stk.Peek().SetAttribute(0, contracts);
            }
            else if (action == Tag._ContractProperties)
            {
                var list = (List<PropertyModel>)action.GetAttribute(0);
                var name = (string)action.GetAttribute(1);

                var contract = new ContractModel() { Name = name, Properties = list };

                stk.Peek().SetAttribute(0, contract);
            }
            else if (action == Tag._EntityName)
            {
                var token = (StringToken)tokens.Pop();

                stk.ElementAt(1).SetAttribute(1, token.Value);
            }
            else if (action == Tag._ValueName)
            {
                var token = (StringToken)tokens.Pop();

                stk.ElementAt(1).SetAttribute(1, token.Value);
            }
            else if (action == Tag._DefineEntity)
            {
                var entityName = (string)action.GetAttribute(1);
                var entityProperties = (List<PropertyModel>)action.GetAttribute(0);

                var model = new EntityModel { Name = entityName, Properties = entityProperties };

                stk.Peek().SetAttribute(0, model);
            }
            else if (action == Tag._DefineValue)
            {
                var valueName = (string)action.GetAttribute(1);
                var valueProperties = (List<PropertyModel>)action.GetAttribute(0);

                var model = new ValueModel { Name = valueName, Properties = valueProperties };

                stk.Peek().SetAttribute(0, model);
            }
            else if (action == Tag._PropertyDeclaration1)
            {
                var name = (StringToken)tokens.Pop();
                var type = (string)action.GetAttribute(0);

                var property = new PropertyModel { Name = name.Value, Type = type };

                stk.Peek().SetAttribute(0, property);
            }
            else if (action == Tag._PropertyDeclaration2)
            {
                var type = (StringToken)tokens.Pop();
                var name = (string)action.GetAttribute(0);

                var property = new PropertyModel { Name = name, Type = type.Value };

                stk.Peek().SetAttribute(0, property);
            }
            else if (action == Tag._AddOtherFile)
            {
                var file = (StringToken)tokens.Pop();
                var list = (List<string>)action.GetAttribute(0);

                list.Add(file.Value);

                stk.Peek().SetAttribute(0, list);
            }
        }
    }
}
