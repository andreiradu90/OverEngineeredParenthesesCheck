using NUnit.Framework;
using ParenthesesCheck;

namespace Tests
{
    public class OpenedParantenthesesTests
    {
        [Test]
        public void Test_AddGet()
        {
            char value = 'a';
            int index = 0;
            OpenedParantentheses openedParantentheses = new OpenedParantentheses();
            openedParantentheses.AddParenthesis(value, index);
            var (lastVal, lastIndex) = openedParantentheses.GetLast();
            Assert.IsTrue(lastVal.Equals(value));
            Assert.IsTrue(lastIndex == index);
        }
        [Test]
        public void Test_ShouldBeEmpty()
        {
            OpenedParantentheses openedParantentheses = new OpenedParantentheses();
            Assert.IsFalse(openedParantentheses.HasAnyOpen());
        }
    }
    public class StringResultProviderTests
    {
        [Test]
        public void Test_Padding_ShouldReturn_1_Character()
        {
            StringResultProvider stringResultProvider = new StringResultProvider(0);
            string input = "abc";
            int index = 1;
            string result = stringResultProvider.GetResultString(input, index);

            Assert.IsTrue(result.Length == 1);
        }
        [Test]
        public void Test_Value_ShouldReturn_b_Character()
        {
            StringResultProvider stringResultProvider = new StringResultProvider(0);
            string input = "abc";
            int index = 1;
            string result = stringResultProvider.GetResultString(input, index);

            Assert.IsTrue(result.Equals("b"));
        }
        [Test]
        public void Test_Padding_AtStartOfString()
        {
            StringResultProvider stringResultProvider = new StringResultProvider(2);
            string input = "abc";
            int index = 0;
            string result = stringResultProvider.GetResultString(input, index);

            Assert.IsTrue(result.Equals(input));
        }
        [Test]
        public void Test_Padding_AtEndOfString()
        {
            StringResultProvider stringResultProvider = new StringResultProvider(2);
            string input = "abc";
            int index = 2;
            string result = stringResultProvider.GetResultString(input, index);

            Assert.IsTrue(result.Equals(input));
        }
    }
    public class InternalParenthesesParserTests
    {
        private IParenthesesProvider _parenthesesProvider;
        private IStringResultProvider _stringResultProvider;
        private IOpenedParentheses _openedParentheses;
        private IInternalParenthesesParser _internalParenthesesParser;
        [SetUp]
        public void Setup()
        {
            _parenthesesProvider = new ParenthesesProvider();
            _stringResultProvider = new StringResultProvider(0);
            _openedParentheses = new OpenedParantentheses();
            _internalParenthesesParser = new InternalParenthesesParser(_parenthesesProvider, _stringResultProvider, _openedParentheses);
        }
        [Test]
        public void Test_No_Parentheses()
        {
            string input = "some text";
            bool parsed = _internalParenthesesParser.TryParse(input, out string result);
            Assert.IsTrue(parsed);
        }
        [Test]
        public void Test_Correct_Parentheses()
        {
            string input = "([{}])";
            bool parsed = _internalParenthesesParser.TryParse(input, out string result);
            Assert.IsTrue(parsed);
        }
    }
    public class ParenthesesParserTests
    {

        [Test]
        public void Test_No_Parentheses()
        {
            string input = "some text";
            bool parsed = ParenthesesParser.TryParse(input, out string result);
            Assert.IsTrue(parsed);
        }

        [Test]
        public void Test_Correct_Parentheses()
        {
            string input = "([{}])";
            bool parsed = ParenthesesParser.TryParse(input, out string result);
            Assert.IsTrue(parsed);
        }

        [Test]
        public void AssignmentTest_ShouldPass_Or_NOT()
        {
            string input = "( demo text ( [test] ( just a small ( text ( with all kind of { parentheses in {( different ( locations ) who ) then } also } with ) each other ) should ) match ) or ) not... )";
            bool parsed = ParenthesesParser.TryParse(input, out string result);
            Assert.IsFalse(parsed);
        }

        [Test]
        public void AssignmentTest_ShouldPass()
        {
            string input = ":(( demo text ( [test] ( just a small ( text ( with all kind of { parentheses in {( different ( locations ) who ) then } also } with ) each other ) should ) match ) or ) not... :)";
            bool parsed = ParenthesesParser.TryParse(input, out string result);
            Assert.IsTrue(parsed);
        }

        [Test]
        public void AssignmentTest_ShouldFail()
        {
            string input = "this ( is [ a ) text ] where the parentheses are ) incorrect.";
            bool parsed = ParenthesesParser.TryParse(input, out string result);
            Assert.IsFalse(parsed);
        }

        [Test]
        public void AssignmentTest_ShouldFailResult()
        {
            string input = "this ( is [ a ) text ] where the parentheses are ) incorrect.";
            bool parsed = ParenthesesParser.TryParse(input, out string result);
            Assert.IsTrue(result.Equals(" a ) te"));
        }

        [Test]
        public void Test_EndParenthesisWithoutStart_SouldFail()
        {
            string input = "this ) is";
            bool parsed = ParenthesesParser.TryParse(input, out string result);
            Assert.IsFalse(parsed);
        }

        [Test]
        public void Test_EndParenthesisWithoutStart_SouldFailResult()
        {
            string input = "this ) is";
            bool parsed = ParenthesesParser.TryParse(input, out string result);
            Assert.IsTrue(result.Equals("is ) is"));
        }

        [Test]
        public void Test_StartParenthesisWithoutEnd_SouldFail()
        {
            string input = "this ( is";
            bool parsed = ParenthesesParser.TryParse(input, out string result);
            Assert.IsFalse(parsed);
        }

        [Test]
        public void Test_StartParenthesisWithoutEnd_SouldFailResult()
        {
            string input = "this ( is";
            bool parsed = ParenthesesParser.TryParse(input, out string result);
            Assert.IsTrue(result.Equals("is ( is"));
        }
        

        [Test]
        public void Test_FailResult_At_Start_Of_Input()
        {
            string input = "}his is";
            bool parsed = ParenthesesParser.TryParse(input, out string result);
            Assert.IsTrue(result.Equals("}his"));
        }

        [Test]
        public void Test_FailResult_At_End_Of_Input()
        {
            string input = "this i(";
            bool parsed = ParenthesesParser.TryParse(input, out string result);
            Assert.IsTrue(result.Equals("s i("));
        }

        [Test]
        public void Test_FailResult_SmallText()
        {
            string input = "ab(cb";
            bool parsed = ParenthesesParser.TryParse(input, out string result);
            Assert.IsTrue(result.Equals("ab(cb"));
        }
    }
}