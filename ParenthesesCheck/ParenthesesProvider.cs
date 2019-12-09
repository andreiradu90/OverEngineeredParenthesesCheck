using System;
using System.Collections.Generic;
using System.Text;

namespace ParenthesesCheck
{
    /// <summary> Logic of what is a start parenthesis and an end parenthesis.
    /// The below implementation of the interface holds the basic example.</summary>
    public interface IParenthesesProvider
    {
        bool IsStartParenthesis(char c);
        bool IsEndParenthesis(char c);
        char GetEndParenthesis(char c);
    }
    public class ParenthesesProvider : IParenthesesProvider
    {
        //default parentheses
        private static readonly Dictionary<char, char> parenthesesStartDict =
            new Dictionary<char, char>{
                { '(', ')' },
                { '{', '}' },
                { '[', ']' }
            };
        private static readonly HashSet<char> parenthesesEndSet =
            new HashSet<char>{
                { ')' },
                { '}' },
                { ']' }
            };

        public char GetEndParenthesis(char c)
        {
            return parenthesesStartDict[c];
        }

        public bool IsEndParenthesis(char c)
        {
            return parenthesesEndSet.Contains(c);                
        }

        public bool IsStartParenthesis(char c)
        {
            return parenthesesStartDict.ContainsKey(c);
        }
    }
}
