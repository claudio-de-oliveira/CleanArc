using AbstractLL;

namespace CleanArc.Parser
{
    internal class Token : AbstractToken
    {
        public Token(AbstractTAG tag)
            : base(tag)
        { /* Nothing more todo */ }

        public static readonly Token
            SOLUTION = new(Tag.SOLUTION),
            PATH = new(Tag.PATH),
            PRESENTATION = new(Tag.PRESENTATION),
            APPLICATION = new(Tag.APPLICATION),
            INFRASTRUCTURE = new(Tag.INFRASTRUCTURE),
            CONTRACTS = new(Tag.CONTRACTS),
            ENTITY = new(Tag.ENTITY),
            CONTRACT = new(Tag.CONTRACT),
            TYPE = new(Tag.TYPE),
            NAME = new(Tag.NAME),
            DOMAIN = new(Tag.DOMAIN),
            VALUE = new(Tag.VALUE),
            TEST = new(Tag.TEST),
            USE = new(Tag.USE),
            PROPERTY = new(Tag.PROPERTY),
            LBRA = new(Tag.LBRA),
            RBRA = new(Tag.RBRA),
            LCOL = new(Tag.LCOL),
            RCOL = new(Tag.RCOL),
            EQUAL = new(Tag.EQUAL),
            SEMICOLON = new(Tag.SEMICOLON),
            ENDMARK = new(Tag.ENDMARK),
            EMPTY = new(Tag.EMPTY);

        public override string ToString() => $"<{GetTag()}>";
    }

    internal class StringToken : AbstractToken
    {
        public string Value { get; private set; }

        public StringToken(string txt)
            : base(Tag.STRING)
        {
            Value = txt;
        }

        public override bool HasComplement() => true;

        public override string ToString() => $"{GetTag()}: {Value}";
    }

    internal class UnknowToken : AbstractToken
    {
        public string Value { get; private set; }

        public UnknowToken(string txt)
            : base(Tag.UNKNOW)
        {
            Value = txt;
        }

        public override bool HasComplement() => true;

        public override string ToString() => $"{GetTag()}: {Value}";
    }
}
