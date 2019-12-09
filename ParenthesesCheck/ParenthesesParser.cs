
using System;
using System.Collections.Generic;

namespace ParenthesesCheck
{
    /// <summary> A default use of the InternalParenthesesParser that satisfies the original given requirements of the task.
    /// For extending, use other implementations(not implemented) of the interfaces/other providers</summary>
    public static class ParenthesesParser
    {
        public static bool TryParse(string input, out string result)
        {
            var (parenthesesProvider, stringResultProvider, openedParentheses) = ProviderFactory.GetDefaultProviders(3, input.Length);
            InternalParenthesesParser internalParser = new InternalParenthesesParser(
                parenthesesProvider, 
                stringResultProvider, 
                openedParentheses);
            return internalParser.TryParse(input, out result);
        }
    }
}
