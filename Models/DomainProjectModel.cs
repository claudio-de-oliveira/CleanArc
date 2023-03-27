namespace CleanArc.Models
{
    internal class DomainProjectModel
    {
        public string Name { get; set; } = default!;
        public List<ObjectModel> Objects { get; set; } = default!;
        public List<string> OtherFiles { get; set; } = default!;
    }
}
