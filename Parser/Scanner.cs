using AbstractLL;

using CleanArc.Models;

namespace CleanArc.Parser
{
    internal class Scanner : AbstractScanner<SolutionModel>
    {
        private static readonly Dictionary<string, Token> reservedWords = new()
        {
            { "SOLUTION", Token.SOLUTION },
            { "PATH", Token.PATH },
            { "PRESENTATION", Token.PRESENTATION },
            { "APPLICATION", Token.APPLICATION },
            { "INFRASTRUCTURE", Token.INFRASTRUCTURE },
            { "CONTRACTS", Token.CONTRACTS },
            { "ENTITY", Token.ENTITY },
            { "CONTRACT", Token.CONTRACT },
            { "TYPE", Token.TYPE },
            { "NAME", Token.NAME },
            { "DOMAIN", Token.DOMAIN },
            { "VALUE", Token.VALUE },
            { "TEST", Token.TEST },
            { "USE", Token.USE },
            { "PROPERTY", Token.PROPERTY },
        };

        public override AbstractToken NextToken(AbstractEnvironment<SolutionModel> environment)
        {
            string lexema = "";
            int state = 0;
            char ch;

            if (environment.EndOfText)
                return Token.EMPTY;

            while (true)
            {
                switch (state)
                {
                    case 0:
                        ch = environment.NextChar();

                        if (char.IsWhiteSpace(ch))
                        {
                            if (environment.EndOfText)
                                return Token.EMPTY;

                            state = 0;
                            break;
                        }
                        if (char.IsLetter(ch))
                        {
                            lexema = "" + ch;
                            state = 1;
                            break;
                        }
                        if (ch == '\"')
                        {
                            lexema = "";
                            state = 3;
                            break;
                        }

                        if (ch == '[')
                        {
                            state = 5;
                            break;
                        }
                        if (ch == ']')
                        {
                            state = 6;
                            break;
                        }
                        if (ch == '{')
                        {
                            state = 7;
                            break;
                        }
                        if (ch == '}')
                        {
                            state = 8;
                            break;
                        }
                        if (ch == ':')
                        {
                            state = 9;
                            break;
                        }
                        if (ch == '=')
                        {
                            state = 10;
                            break;
                        }
                        if (ch == '#')
                        {
                            state = 11;
                            break;
                        }
                        // TEXT
                        lexema = "" + ch;
                        state = 7;
                        break;

                    case 1:
                        ch = environment.NextChar();

                        if (char.IsLetter(ch))
                        {
                            lexema += ch;
                            state = 1;
                            break;
                        }
                        state = 2;
                        break;

                    case 2:
                        environment.Retract();

                        lexema = lexema.ToUpper();
                        if (reservedWords.ContainsKey(lexema))
                            return reservedWords[lexema];
                        else
                            return new UnknowToken(lexema);

                    case 3:
                        ch = environment.NextChar();

                        if (ch == '\"')
                        {
                            state = 4;
                            break;
                        }
                        if (ch == '\n')
                        {
                            state = 99;
                            break;
                        }

                        lexema += ch;
                        state = 3;
                        break;

                    case 4:
                        return new StringToken(lexema);

                    case 5:
                        return Token.LCOL;
                    case 6:
                        return Token.RCOL;
                    case 7:
                        return Token.LBRA;
                    case 8:
                        return Token.RBRA;
                    case 9:
                        return Token.SEMICOLON;
                    case 10:
                        return Token.EQUAL;
                    case 11:
                        return Token.ENDMARK;

                    case 99:
                        environment.Retract();

                        // Invalid Token
                        return new UnknowToken(lexema);
                }
            }
        }
    }
}
