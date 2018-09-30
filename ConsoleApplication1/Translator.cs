using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TranslatorNS
{
    class Translator
    {
        private enum Token
        {
            Number, SumOp, MulOp, If, Else, End,
            DO_SYM, ELSE_SYM, IF_SYM, WHILE_SYM, LBRA, RBRA, LPAR, RPAR,
            PLUS, MINUS, LESS, SEMI, EQUAL, INT, ID, EOI
        };

        private Dictionary<string, string> myDict = new Dictionary<string, string>
        {
            {"if", "если"},
            {"{", "левая_скобка"},
            {"while", "пока"}
        };

        private static char[] separ = { ' ', '\n', '\t' };

        private int pos;
        private string code;

        public Translator()
        {
            pos = 0;
            code = "";
        }

        private class Element
        {
            public Token type;
            public string value;
        }

        private Element GetToken()
        {
            Element res = new Element();
            res.value = "";

            while (true)
            {
                if (pos >= code.Length) break;
                if (!separ.Contains<char>(code[pos]))
                    break;

                pos++;
            }


            if (pos >= code.Length)
            {
                res.type = Token.End;
                return res;
            }

            if (Char.IsDigit(code[pos]))
            {
                res.type = Token.Number;
                while (Char.IsDigit(code[pos]))
                {
                    res.value += code[pos];
                    pos++;
                    if (pos >= code.Length)
                        break;
                }
            }
            else
                if (code[pos] == '+' || code[pos] == '-')
                {
                    res.type = Token.SumOp;
                    res.value += code[pos];
                    pos++;
                }
                else
                    if (code[pos] == '*' || code[pos] == '/')
                    {
                        res.type = Token.MulOp;
                        res.value += code[pos];
                        pos++;
                    }
                    else
                        if (char.IsLetter(code[pos]))
                        {
                            while (Char.IsLetter(code[pos]))
                            {
                                res.value += code[pos];
                                pos++;
                                if (pos >= code.Length)
                                    break;
                            }
                        }

            return res;
        }

        private Element NextToken()
        {
            int oldPos = pos;
            Element res = GetToken();

            pos = oldPos;
            return res;
        }

        private int Expr()
        {
            int res = Term();
            while (NextToken().type == Token.SumOp)
            {
                GetToken();
                res = res + Term();
            }
            return res;
        }

        private int Term()
        {
            Element elem = GetToken();
            int res = int.Parse(elem.value);

            while (NextToken().type == Token.MulOp)
            {
                GetToken();
                Element elem2 = GetToken();
                res = res * int.Parse(elem2.value);
            }
            return res;
        }


        public string Translate(string _code)
        {
            code = _code;
            string res = "";

            List<Element> elements = new List<Element>();
            while (true)
            {
                Element elem = GetToken();
                if (elem.type == Token.End)
                    break;
                elements.Add(elem);
            }

            foreach (var elem in elements)
            {
                string value = elem.value;
                if (myDict.ContainsKey(elem.value))
                {
                    value = myDict[elem.value];
                }
                res = res + value + " ";                
            }

            return res;
        }


    }
}
