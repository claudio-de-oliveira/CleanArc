namespace CleanArc.Models
{
    internal class ContractModel
    {
        public string Name { get; set; } = default!;
        public List<PropertyModel> Properties { get; set; } = default!;
    }
}