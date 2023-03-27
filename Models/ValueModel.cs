namespace CleanArc.Models
{
    internal class ValueModel : ObjectModel
    {
        public string Name { get; set; } = string.Empty;
        public List<PropertyModel> Properties { get; set; } = default!;
    }
}
