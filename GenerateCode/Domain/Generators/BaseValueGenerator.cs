namespace CleanArc.GenerateCode.Domain.Generators
{
    internal class BaseValueGenerator : FileGenerator
    {
        public BaseValueGenerator(string path)
            : base(path, $"BaseValue.cs")
        {
        }

        protected override List<string> Generate()
            => new()
            {
                "// Objects and data items in a system that do not require an identity and identity tracking.",
                "// Immutable by design",
                "",
                "namespace IdealSoft.Domain.Base",
                "{",
                "    // Learn more: https://docs.microsoft.com/en-us/dotnet/standard/microservices-architecture/microservice-ddd-cqrs-patterns/implement-value-objects",
                "    public abstract class ValueObject : IEquatable<ValueObject>",
                "    {",
                "        protected abstract IEnumerable<object> GetEqualityComponents();",
                "",
                "        protected static bool EqualOperator(ValueObject left, ValueObject right)",
                "        {",
                "            if (left is null ^ right is null)",
                "                return false;",
                "",
                "            return left?.Equals(right!) != false;",
                "        }",
                "",
                "        protected static bool NotEqualOperator(ValueObject left, ValueObject right)",
                "            => !(EqualOperator(left, right));",
                "",
                "        public override bool Equals(object? obj)",
                "        {",
                "            if (obj == null || obj.GetType() != GetType())",
                "                return false;",
                "",
                "            var other = (ValueObject)obj;",
                "",
                "            return GetEqualityComponents().SequenceEqual(other.GetEqualityComponents());",
                "        }",
                "",
                "        public override int GetHashCode()",
                "            => GetEqualityComponents()",
                "                .Select(x => x != null ? x.GetHashCode() : 0)",
                "                .Aggregate((x, y) => x ^ y);",
                "",
                "        public bool Equals(ValueObject? other)",
                "            => other is not null && Equals(other);",
                "    }",
                "}",
            };
    }
}
