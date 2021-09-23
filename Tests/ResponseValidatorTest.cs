using MusicService.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests
{
    [TestClass]
    public class ResponseValidatorTest
    {
        [TestMethod]
        public void IsMatch__DifferentCasing_True()
        {
            var searchString = "katyPERry";
            var responseString = "katyperry";
            var isMatch = ResponseValidator.IsMatch(searchString, responseString);

            Assert.IsTrue(isMatch);
        }

        [TestMethod]
        public void IsMatch__WithPunctuation_True()
        {
            var searchString = "katy.perry";
            var responseString = "katyperry";
            var isMatch = ResponseValidator.IsMatch(searchString, responseString);

            Assert.IsTrue(isMatch);
        }

        [TestMethod]
        public void IsMatch__WithWhitespace_True()
        {
            var searchString = "katy perry";
            var responseString = "katyperry";
            var isMatch = ResponseValidator.IsMatch(searchString, responseString);

            Assert.IsTrue(isMatch);
        }

        [TestMethod]
        public void IsMatch_DifferentSpelling_False()
        {
            var searchString = "katy perryY";
            var responseString = "katy perry";
            var isMatch = ResponseValidator.IsMatch(searchString, responseString);

            Assert.IsFalse(isMatch);
        }
    }
}
