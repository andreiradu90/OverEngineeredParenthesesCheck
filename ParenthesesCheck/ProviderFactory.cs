using System;
using System.Collections.Generic;
using System.Text;

namespace ParenthesesCheck
{
    /// <summary> Don't really need this class but I'm overengineering here so...probably plan to Get/Set the providers and Get them all again??</summary>
    public class ProviderFactory
    {
        readonly IParenthesesProvider _parenthesesProvider;//not used yet
        readonly IStringResultProvider _stringResultProvider;//not used yet
        readonly IOpenedParentheses _openedParentheses;//not used yet

        public static (IParenthesesProvider parenthesesProvider,
            IStringResultProvider stringResultProvider,
            IOpenedParentheses openedParentheses) GetDefaultProviders()
        {
            return (new ParenthesesProvider(), new StringResultProvider(), new OpenedParantentheses());
        }
        public static (IParenthesesProvider parenthesesProvider,
            IStringResultProvider stringResultProvider,
            IOpenedParentheses openedParentheses) GetDefaultProviders(int padding, int length)
        {
            return (new ParenthesesProvider(), new StringResultProvider(padding), new OpenedParantentheses(length));
        }
    }
}
