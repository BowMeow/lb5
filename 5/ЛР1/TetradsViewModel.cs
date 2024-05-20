using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ЛР1
{
    internal class TetradsViewModel
    {
        private List<Tetrads> tetrads = new List<Tetrads>();

        public List<Tetrads> CreateTetrads(List<Tokenn> Tokens)
        {
            if (Tokens == null || Tokens.Count == 0) return null;

            Tokens.RemoveAll(token => string.IsNullOrWhiteSpace(token.RawToken));

            SearchParenthesis(ref Tokens);

            for (int i = 0; i < Tokens.Count(); i++)
            {
                if (Tokens[i].RawToken == "-" && (
                    i == 0 ||
                    Tokens[i - 1].RawToken == "+" ||
                    Tokens[i - 1].RawToken == "-" ||
                    Tokens[i - 1].RawToken == "*" ||
                    Tokens[i - 1].RawToken == "/"))
                {
                    tetrads.Add(new Tetrads("minus", Tokens[i + 1].RawToken, "", "t" + tetrads.Count));
                    Tokens[i] = new Tokenn(tetrads.Last().Result, Tokens[i].StartPos);
                    Tokens.RemoveAt(i + 1);
                    return CreateTetrads(Tokens);
                }
            }

            for (int i = 0; i < Tokens.Count(); i++)
            {
                if (Tokens[i].RawToken == "*" || Tokens[i].RawToken == "/")
                {
                    if (i == 0 || i == Tokens.Count - 1)
                    {
                        string operation1 = Tokens[i].RawToken == "*" ? "multiply" : "divide";
                        tetrads.Add(new Tetrads(operation1, "Ошибка: отсутствует один из аргументов для операции", "", ""));
                        return tetrads;
                    }
                    string operand1 = Tokens[i - 1].RawToken;
                    string operand2 = Tokens[i + 1].RawToken;
                    string operationResult = "t" + tetrads.Count;
                    string operation = Tokens[i].RawToken == "*" ? "multiply" : "divide";
                    tetrads.Add(new Tetrads(operation, operand1, operand2, operationResult));
                    Tokens[i - 1] = new Tokenn(tetrads.Last().Result, Tokens[i - 1].StartPos);
                    Tokens.RemoveAt(i);
                    Tokens.RemoveAt(i);
                    return CreateTetrads(Tokens);
                }
            }

            for (int i = 0; i < Tokens.Count(); i++)
            {
                if (Tokens[i].RawToken == "+" || Tokens[i].RawToken == "-")
                {
                    if (i == 0 || i == Tokens.Count - 1)
                    {
                        string operation1 = Tokens[i].RawToken == "+" ? "plus" : "minus";
                        tetrads.Add(new Tetrads(operation1, "Ошибка: отсутствует один из аргументов для операции", "", ""));
                        return tetrads;
                    }
                    string operand1 = Tokens[i - 1].RawToken;
                    string operand2 = Tokens[i + 1].RawToken;
                    string operationResult = "t" + tetrads.Count;
                    string operation = Tokens[i].RawToken == "+" ? "plus" : "minus";
                    tetrads.Add(new Tetrads(operation, operand1, operand2, operationResult));
                    Tokens[i - 1] = new Tokenn(tetrads.Last().Result, Tokens[i - 1].StartPos);
                    Tokens.RemoveAt(i);
                    Tokens.RemoveAt(i);
                    return CreateTetrads(Tokens);
                }
            }

            if (Tokens.Count == 3 && Tokens[1].RawToken == "=")
            {
                string operand1 = Tokens[0].RawToken;
                string operand2 = Tokens[2].RawToken;
                tetrads.Add(new Tetrads("equals", operand2, "", operand1));
                return tetrads;
            }

            return tetrads;
        }

        private void SearchParenthesis(ref List<Tokenn> Tokens)
        {
            Stack<Tokenn> stack = new Stack<Tokenn>();
            Tokenn OpenParenthesis = null;
            Tokenn CloseParenthesis = null;
            foreach (var token in Tokens)
            {
                if (token.RawToken == "(")
                {
                    if (OpenParenthesis == null)
                        OpenParenthesis = token;
                    stack.Push(token);
                }
                else if (token.RawToken == ")")
                {
                    if (stack.Count == 0)
                    {
                        // Error: closing parenthesis without an opening one
                        tetrads.Add(new Tetrads("error", "Лишняя закрывающая скобка", "", ""));
                        return;
                    }
                    stack.Pop();
                    if (stack.Count == 0)
                        CloseParenthesis = token;
                }

                if (OpenParenthesis != null && CloseParenthesis != null) break;
            }

            if (OpenParenthesis != null && CloseParenthesis != null)
            {
                var tokensBuff = Tokens.Skip(Tokens.IndexOf(OpenParenthesis) + 1)
                                       .Take(Tokens.IndexOf(CloseParenthesis) - Tokens.IndexOf(OpenParenthesis) - 1)
                                       .ToList();
                CreateTetrads(tokensBuff);

                int startIndex = Tokens.IndexOf(OpenParenthesis);
                int endIndex = Tokens.IndexOf(CloseParenthesis) + 1;
                Tokens.RemoveRange(startIndex, endIndex - startIndex);
                Tokens.Insert(startIndex, new Tokenn(tetrads.Last().Result, OpenParenthesis.StartPos));
            }
            else if (OpenParenthesis != null)
            {
                // Error: opening parenthesis without a closing one
                tetrads.Add(new Tetrads("error", "Лишняя открывающая скобка", "", ""));
                return;
            }

            foreach (var token in Tokens.ToList())
            {
                if (token.RawToken == "(") CreateTetrads(Tokens);
            }
        }
    }
}
public class Token
{
    public string RawToken { get; set; }
    public int StartPos { get; set; }

    public Token(string rawToken, int startPos)
    {
        RawToken = rawToken;
        StartPos = startPos;
    }
}
