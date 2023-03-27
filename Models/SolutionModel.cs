namespace CleanArc.Models
{
    internal class SolutionModel
    {
        public string Name { get; set; } = string.Empty;
        public string Path { get; set; } = string.Empty;

        public PresentationModel? Presentation { get; set; }
        public ApplicationModel? Application { get; set; }
        public DomainProjectModel? Domain { get; set; }
        public ContractProjectModel? Contracts { get; set; }
        public InfrastructureModel? Infrastructure { get; set; }
        public TestModel? Test { get; set; }

        public List<string>? Use { get; set; }
    }
}
