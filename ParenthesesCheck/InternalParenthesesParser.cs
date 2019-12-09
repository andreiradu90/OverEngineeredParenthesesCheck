using System;
using System.Collections.Generic;
using System.Text;

namespace ParenthesesCheck
{
    /// <summary> Main logic of the parser.
    /// Uses and assumes given dependencies are implemented correctly per give use case.</summary>
    public interface IInternalParenthesesParser
    {
        bool TryParse(string input, out string result);
    }
    public class InternalParenthesesParser : IInternalParenthesesParser
    {
        private readonly IParenthesesProvider _parenthesesProvider;
        private readonly IStringResultProvider _stringResultProvider;
        private readonly IOpenedParentheses _openedParentheses;
        public InternalParenthesesParser(IParenthesesProvider parenthesesProvider, 
            IStringResultProvider stringResultProvider, 
            IOpenedParentheses openedParentheses)
        {
            _parenthesesProvider = parenthesesProvider;
            _stringResultProvider = stringResultProvider;
            _openedParentheses = openedParentheses;
        }
        public bool TryParse(string input, out string result)
        {
            result = "";
            int index = 0;
            
            foreach (char c in input)
            {
                if (_parenthesesProvider.IsStartParenthesis(c))
                {
                    //a parenthesis was opened
                    _openedParentheses.AddParenthesis(c, index);
                }
                else if (_parenthesesProvider.IsEndParenthesis(c))
                {                    
                    if (!_openedParentheses.HasAnyOpen())
                    {
                        //found an end paranthesis without a start one
                        result = _stringResultProvider.GetResultString(input, index);
                        return false;
                    }
                    (char lastOpened, int lastIndex) = _openedParentheses.GetLast();
                    if (!c.Equals(_parenthesesProvider.GetEndParenthesis(lastOpened)))
                    {
                        //found a different closed parenthesis
                        result = _stringResultProvider.GetResultString(input, index);
                        return false;
                    }
                }
                index++;
            }
            if (_openedParentheses.HasAnyOpen())
            {
                //found unclosed paranthesis
                (char lastOpened, int lastIndex) = _openedParentheses.GetLast();
                result = _stringResultProvider.GetResultString(input, lastIndex);
                return false;
            }
            return true;
        }
    }
}
