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

        private enum StatementType { VAR, CST, ADD, SUB, LT, SET,
        IF1, IF2, WHILE, DO, EMPTY, SEQ, EXPR, PROG, };

        private Dictionary<string, string> myDict = new Dictionary<string, string>
        {
            {"if", "если"},
            {"{", "левая_скобка"},
            {"while", "пока"}
        };

        private Dictionary<string, Token> myDictST = new Dictionary<string, Token>
        {
            {"if", Token.If},
            {"{", Token.LBRA},
            {"while", Token.WHILE_SYM}
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

        StatementType Statement()
        {
            StatementType res = StatementType.EMPTY;
            Element elem = NextToken();
            if (elem.type == Token.If)
            {
                res = StatementType.IF1;
                GetToken();
                ParenExpr();
                Statement();
            }
            elem = NextToken();
            if (elem.type == Token.Else)
            {
                GetToken();
                Statement();
            }

            return res;
        }

        StatementType ParenExpr()
        {
            StatementType res = StatementType.EMPTY;
            Element elem = GetToken();
            if (elem.type != Token.LBRA)
                throw new Exception();
            Expr();
            elem = GetToken();
            if (elem.type != Token.RBRA)
                throw new Exception();
            return res;
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

        //<term> ::= <id> | <int> | <paren-expr>
        private StatementType Term()
        {           
            Element elem = NextToken();
            StatementType res = StatementType.EMPTY;
            if (elem.type == Token.ID)
            {
                GetToken();
                return StatementType.VAR;            
            }

            if (elem.type == Token.Number)
            {
                GetToken();
                return StatementType.CST;
            }

            res = ParenExpr();

            return res;
        }

        //<sum>  ::= <term> | <sum> "+" <term> | <sum> "-" <term>
        private StatementType Sum()
        {
            StatementType res = Term();
            

            return res;
        }


        //<test> ::= <sum> | <sum> "<" <sum>
        private StatementType Test()
        {
            StatementType res = Sum();
            Element elem = NextToken();
            if (elem.type == Token.ID)
                return Test();
            else
            {

            }

            return res;
        }

        /* <expr> ::= <test> | <id> "=" <expr> */
        private StatementType Expr()
        {
            StatementType res = StatementType.EMPTY;
            Element elem = NextToken();
            if (elem.type != Token.ID)
                return Test();
            res = Test();
            if (res == StatementType.VAR && NextToken().type == Token.EQUAL)
            {
                GetToken();
                res = StatementType.SET;
                Expr();
            }


            return res;
        }

        //private int Term()
        //{
        //    Element elem = GetToken();
        //    int res = int.Parse(elem.value);

        //    while (NextToken().type == Token.MulOp)
        //    {
        //        GetToken();
        //        Element elem2 = GetToken();
        //        res = res * int.Parse(elem2.value);
        //    }
        //    return res;
        //}


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
