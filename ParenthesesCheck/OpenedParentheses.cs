using System;
using System.Collections.Generic;
using System.Text;

namespace ParenthesesCheck
{
    /// <summary> Holds the opened parentheses in memory along with their index in the text.
    /// The OpenedParantentheses implementation uses Stacks to achieve this because they are fast.</summary>
    public interface IOpenedParentheses
    {
        void AddParenthesis(char parenthesis, int index);
        (char, int) GetLast();
        bool HasAnyOpen();
    }
    public class OpenedParantentheses : IOpenedParentheses
    {
        readonly Stack<char> openedParentheses;
        readonly Stack<int> openedParenthesesIndex;

        public OpenedParantentheses()
        {
            openedParentheses = new Stack<char>();
            openedParenthesesIndex = new Stack<int>();
        }
        public OpenedParantentheses(int length)
        {
            openedParentheses = new Stack<char>(length);
            openedParenthesesIndex = new Stack<int>(length);
        }
        public void AddParenthesis(char parenthesis, int index)
        {
            openedParentheses.Push(parenthesis);
            openedParenthesesIndex.Push(index);
        }

        public (char, int) GetLast()
        {
            return (openedParentheses.Pop(), openedParenthesesIndex.Pop());
        }

        public bool HasAnyOpen()
        {
            return openedParentheses.Count != 0;
        }
    }
}
