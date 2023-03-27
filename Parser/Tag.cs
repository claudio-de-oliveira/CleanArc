using AbstractLL;

namespace CleanArc.Parser
{
    internal class Tag : AbstractTAG
    {
        protected Tag(int Tag, string name, int nattribs = 0)
            : base(Tag, name, nattribs)
        { /* Nothing more todo */ }

        #region N Ã O   T E R M I N A I S
        public static readonly Tag
            Start = new(NONTERMINAL | 0, "Start"),
            MainText = new(NONTERMINAL | 1, "MainText"),
            Declaration_ = new(NONTERMINAL | 2, "Declaration'"),
            Declaration = new(NONTERMINAL | 3, "Declaration", 1),
            Solution = new(NONTERMINAL | 4, "Solution"),
            SolutionPath = new(NONTERMINAL | 5, "SolutionPath"),
            Presentation = new(NONTERMINAL | 6, "Presentation", 1),
            Application = new(NONTERMINAL | 7, "Application"),
            Infrastructure = new(NONTERMINAL | 8, "Infrastructure"),
            Contracts = new(NONTERMINAL | 9, "Contracts", 1),
            Domain = new(NONTERMINAL | 10, "Domain"),
            Test = new(NONTERMINAL | 11, "Test", 1),
            Packages = new(NONTERMINAL | 12, "Packages"),
            ListOfContractsByEntity = new(NONTERMINAL | 13, "ListOfContractsByEntity", 1),
            ListOfContractsByEntity_ = new(NONTERMINAL | 14, "ListOfContractsByEntity'", 1),
            ContractsByEntityDeclaration = new(NONTERMINAL | 15, "ContractsByEntityDeclaration", 1),
            ListOfContracts = new(NONTERMINAL | 16, "ListOfContracts"),
            ListOfContracts_ = new(NONTERMINAL | 17, "ListOfContracts'", 1),
            ContractDeclaration = new(NONTERMINAL | 18, "ContractDeclaration"),
            ListOfObjects = new(NONTERMINAL | 19, "ListOfObjects"),
            ListOfObjects_ = new(NONTERMINAL | 20, "ListOfObjects'", 1),
            Object = new(NONTERMINAL | 21, "Object"),
            Entity = new(NONTERMINAL | 22, "Entity"),
            Value = new(NONTERMINAL | 23, "Value"),
            ListOfProperties = new(NONTERMINAL | 24, "ListOfProperties"),
            ListOfProperties_ = new(NONTERMINAL | 25, "ListOfProperties'", 1),
            ListOfPackages = new(NONTERMINAL | 26, "ListOfPackages"),
            ListOfPackages_ = new(NONTERMINAL | 27, "ListOfPackages'", 1),
            PropertyDeclaration = new(NONTERMINAL | 28, "PropertyDeclaration"),
            PropertyDeclarationBody = new(NONTERMINAL | 29, "PropertyDeclarationBody"),
            OtherSources = new(NONTERMINAL | 30, "OtherSources"),
            OtherSources_ = new(NONTERMINAL | 31, "OtherSources'", 1)
            ;
        #endregion

        #region T E R M I N A I S
        public static readonly Tag
            SOLUTION = new(TERMINAL | 0, "Solution"),
            PATH = new(TERMINAL | 1, "Path"),
            PRESENTATION = new(TERMINAL | 2, "Presentation"),
            APPLICATION = new(TERMINAL | 3, "Application"),
            INFRASTRUCTURE = new(TERMINAL | 4, "Infrastructure"),
            CONTRACTS = new(TERMINAL | 5, "Contracts"),
            ENTITY = new(TERMINAL | 6, "Entity"),
            CONTRACT = new(TERMINAL | 7, "Contract"),
            TYPE = new(TERMINAL | 8, "Type"),
            NAME = new(TERMINAL | 9, "Name"),
            DOMAIN = new(TERMINAL | 10, "Domain"),
            VALUE = new(TERMINAL | 11, "Value"),
            TEST = new(TERMINAL | 12, "Test"),
            USE = new(TERMINAL | 13, "Use"),
            PROPERTY = new(TERMINAL | 14, "Property"),
            LBRA = new(TERMINAL | 15, "{"),
            RBRA = new(TERMINAL | 16, "}"),
            LCOL = new(TERMINAL | 17, "["),
            RCOL = new(TERMINAL | 18, "]"),
            STRING = new(TERMINAL | 19, "string"),
            EQUAL = new(TERMINAL | 20, "="),
            SEMICOLON = new(TERMINAL | 21, ":"),
            ENDMARK = new(TERMINAL | 22, "#"),

            EMPTY = new(TERMINAL | 23, "empty")
            ;

        public static readonly Tag
            UNKNOW = new(TERMINAL | 24, "Unknow");
        #endregion

        #region A Ç Õ E S   S E M Â N T I C A S
        public static readonly Tag
            _Done = new(SEMANTIC | 0, "@Done", 0),
            _Echo = new(SEMANTIC | 1, "@Echo", 1),
            _Skip1 = new(SEMANTIC | 2, "@Skip1", 1),

            _SolutionName = new(SEMANTIC | 3, "@SolutionName", 0),
            _SolutionPath = new(SEMANTIC | 4, "@SolutionPath", 0),

            _PresentationName = new(SEMANTIC | 5, "@PresentationName", 0),
            _PresentationFiles = new(SEMANTIC | 6, "@PresentationFiles", 1),
            _ApplicationName = new(SEMANTIC | 7, "@ApplicationName", 0),
            _ApplicationFiles = new(SEMANTIC | 8, "@ApplicationFiles", 1),
            _InfrastructureName = new(SEMANTIC | 9, "@InfrastructureName", 0),
            _InfrastructureFiles = new(SEMANTIC | 10, "@InfrastructureFiles", 1),
            _ContractsName = new(SEMANTIC | 11, "@ContractsName", 0),
            _ContractsFiles = new(SEMANTIC | 12, "@ContractsFiles", 1),
            _ContractList = new(SEMANTIC | 13, "@ContractList", 1),
            _DomainName = new(SEMANTIC | 14, "@DomainName", 0),
            _DomainFiles = new(SEMANTIC | 15, "@DomainFiles", 1),
            _DomainList = new(SEMANTIC | 16, "@DomainList", 1),
            _TestName = new(SEMANTIC | 17, "@TestName", 0),
            _TestFiles = new(SEMANTIC | 18, "@TestFiles", 1),
            _Use = new(SEMANTIC | 19, "@Use", 1),

            _EntityName = new(SEMANTIC | 20, "@EntityName", 0),
            _ContratName = new(SEMANTIC | 21, "@ContratName", 0),
            _Type = new(SEMANTIC | 22, "@Type", 0),
            _Name = new(SEMANTIC | 23, "@Name", 0),
            _DefineEntity = new(SEMANTIC | 24, "@DefineEntity", 2),
            _DefineValue = new(SEMANTIC | 25, "@DefineValue", 2),

            _CreateEmptyListOfFiles = new(SEMANTIC | 26, "@CreateEmptyListOfFiles", 0),

            _CreateListOfContractsByEntity = new(SEMANTIC | 27, "@CreateListOfContractsByEntity", 0),
            _AddContractsByEntity = new(SEMANTIC | 28, "@AddContractsByEntity", 2),

            _CreateListOfContracts = new(SEMANTIC | 29, "@CreateListOfContracts", 1),
            _AddContract = new(SEMANTIC | 30, "@AddContract", 2),
            _CreateListOfProperties = new(SEMANTIC | 31, "@CreateListOfProperties", 0),
            _AddProperty = new(SEMANTIC | 32, "@AddProperty", 2),
            _CreateListOfPackages = new(SEMANTIC | 33, "@CreateListOfPackages", 1),
            _AddPackage = new(SEMANTIC | 34, "@AddPackage", 1),
            _CreateListOfFiles = new(SEMANTIC | 35, "@CreateListOfFiles", 1),
            _AddOtherFile = new(SEMANTIC | 36, "@AddOtherFile", 1),
            _CreateListOfObjects = new(SEMANTIC | 37, "@CreateListOfObjects", 1),
            _AddObject = new(SEMANTIC | 38, "@AddObject", 2),

            _EntityContracts = new(SEMANTIC | 39, "@EntityContracts", 2),
            _ContractProperties = new(SEMANTIC | 40, "@ContractProperties", 2),
            _ValueName = new(SEMANTIC | 41, "@ValueName", 0),
            _PropertyDeclaration1 = new(SEMANTIC | 42, "@PropertyDeclaration1", 1),
            _PropertyDeclaration2 = new(SEMANTIC | 43, "@PropertyDeclaration2", 1)
            ;
        #endregion

        public override AbstractTAG Clone()
        {
            if (_inherited.Length > 0)
                return new Tag(_tag, _name, _inherited.Length);
            return this;
        }
    }
}
