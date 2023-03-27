namespace CleanArc.Models
{
    internal class ContractEntityModel
    {
        public string Name { get; set; } = default!;
        public List<ContractModel> Contracts { get; set; } = default!;
    }
}
