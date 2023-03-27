namespace CleanArc.Models
{
    internal class ContractProjectModel
    {
        public string Name { get; set; } = default!;
        public List<ContractEntityModel> Entities { get; set; } = default!;
        public List<string> OtherFiles { get; set; } = default!;
    }
}
