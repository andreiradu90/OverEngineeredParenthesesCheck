using System;
using System.Collections.Generic;
using System.Text;

namespace ParenthesesCheck
{
    /// <summary> Gets the wanted result from a string at an index.
    /// The below implementation satisfies the original task requirements.</summary>
    public interface IStringResultProvider
    {
        string GetResultString(string input, int index);
    }
    public class StringResultProvider : IStringResultProvider
    {
        private readonly int _resultPadding;
        public StringResultProvider()
        {
            _resultPadding = 0;
        }
        public StringResultProvider(int padding)
        {
            _resultPadding = padding;
        }
        public string GetResultString(string input, int index)
        {
            int resStartIndex = index >= _resultPadding ? index - _resultPadding : 0;
            int resLength = index < input.Length - _resultPadding ? index + _resultPadding + 1 : input.Length; //this is actually the EndIndex. The resLength is based on this
            resLength = resLength - resStartIndex;
            return input.Substring(resStartIndex, resLength);
        }
    }
}
