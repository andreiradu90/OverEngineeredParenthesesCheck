using NUnit.Framework;
using ParenthesesCheck;

namespace Tests
{
    public class Tests
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