namespace CleanArc.GenerateCode.Domain.Generators
{
    internal class BaseEntityGenerator : FileGenerator
    {
        public BaseEntityGenerator(string path)
            : base(path, $"BaseEntity.cs")
        {
        }

        protected override List<string> Generate()
            => new()
            {
                "namespace Domain.Base",
                "{",
                "    public abstract class BaseEntity : IEquatable<BaseEntity>",
                "    {",
                "        public Guid Id { get; private init; }",
                "",
                "        protected BaseEntity(Guid id)",
                "            => Id = id;",
                "",
                "        public bool Equals(BaseEntity? other)",
                "        {",
                "            if (other is null || other.GetType() != GetType())",
                "                return false;",
                "",
                "            return Id == other.Id;",
                "        }",
                "",
                "        public override bool Equals(object? other)",
                "        {",
                "            if (other is null || other.GetType() != GetType() || other is not BaseEntity entity)",
                "                return false;",
                "",
                "            return Id == entity.Id;",
                "        }",
                "",
                "        public override int GetHashCode()",
                "            => Id.GetHashCode() * 17;",
                "",
                "        public static bool operator ==(BaseEntity? left, BaseEntity? right)",
                "            => left is not null && right is not null && left.Equals(right);",
                "        public static bool operator !=(BaseEntity? left, BaseEntity? right)",
                "            => !(left == right);",
                "    }",
                "}",
            };
    }
}
