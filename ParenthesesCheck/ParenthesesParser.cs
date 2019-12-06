using System;
using System.Collections.Generic;

namespace ParenthesesCheck
{
    public static class ParenthesesParser
    {
        private static readonly Dictionary<char, char> parentesesStartDict = 
            new Dictionary<char, char>{
                { '(', ')' },
                { '{', '}' },
                { '[', ']' }
            };
        private static readonly HashSet<char> parentesesEndSet =
            new HashSet<char>{
                { ')' },
                { '}' }, 
                { ']' }
            };
        private static readonly int RESULT_PADDING = 3;
        private static readonly int RESULT_LENGTH = RESULT_PADDING * 2 + 1;

        public static bool TryParse(string input, out string result)
        {
            result = "";
            Stack<char> openedParentheses = new Stack<char>(input.Length);
            Stack<int> openedParenthesesIndex = new Stack<int>(input.Length);
            int index = 0;

            foreach (char c in input)
            {
                if (parentesesStartDict.ContainsKey(c))
                {
                    //a parenthesis was opened
                    openedParentheses.Push(c);
                    openedParenthesesIndex.Push(index);
                }
                else if (parentesesEndSet.Contains(c))
                {
                    if(openedParentheses.Count == 0)
                    {
                        //found an end paranthesis without a start one
                        result = GetResultString(input, index);
                        return false;
                    }
                    else if (!c.Equals(parentesesStartDict[openedParentheses.Pop()]))
                    {
                        //found a different closed parenthesis
                        result = GetResultString(input, index);
                        return false;
                    }
                    else
                    {
                        openedParenthesesIndex.Pop();
                    }                    
                }
                index++;
            }
            if(openedParentheses.Count != 0)
            {
                //found unclosed paranthesis
                result = GetResultString(input, openedParenthesesIndex.Pop());
                return false;
            }
            return true;
        }

        private static string GetResultString(string input, int index)
        {
            int resStartIndex = index >= RESULT_PADDING ? index - RESULT_PADDING : 0;
            int resLength = index < input.Length - RESULT_PADDING ? index + RESULT_PADDING + 1 : input.Length; //this is actually the EndIndex. The resLength is based on this
            resLength = resLength - resStartIndex;
            return input.Substring(resStartIndex, resLength);
        }
    }
}
