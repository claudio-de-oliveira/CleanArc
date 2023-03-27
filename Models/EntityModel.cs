namespace CleanArc.Models
{
    internal class EntityModel : ObjectModel
    {
        public string Name { get; set; } = string.Empty;
        public List<PropertyModel> Properties { get; set; } = default!;
    }
}
