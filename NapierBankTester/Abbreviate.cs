using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MessageLibrary;

namespace NapierBankTester
{
    [TestClass]
    // This unit test tests the abbreviate method
    public class UnitTest1
    {
        [TestMethod]
        // This test proves that the method can replace a word in the message
        public void testabbreviate()
        {
            Abbreviate myAbbreviate = new Abbreviate();

            string expectedResults = "LOL<Laughing out loud>";

            string actualResults = myAbbreviate.abbreviate("LOL");

            Assert.AreEqual(expectedResults, actualResults);
        }

        [TestMethod]
        // This test proves that the method can replace a word in the message
        public void testabbreviate2()
        {
            Abbreviate myAbbreviate = new Abbreviate();

            string expectedResults = "B4<Before>";

            string actualResults = myAbbreviate.abbreviate("B4");

            Assert.AreEqual(expectedResults, actualResults);
        }

        [TestMethod]
        // This test proves that the method can replace more than one word in the message
        public void testabbreviate3()
        {
            Abbreviate myAbbreviate = new Abbreviate();

            string expectedResults = "B4<Before> LOL<Laughing out loud>";

            string actualResults = myAbbreviate.abbreviate("B4 LOL");

            Assert.AreEqual(expectedResults, actualResults);
        }
        [TestMethod]
        // This test ensures that only capped textspeak is expanded
        public void testabbreviate4()
        {
            Abbreviate myAbbreviate = new Abbreviate();

            string expectedResults = "lol";

            string actualResults = myAbbreviate.abbreviate("lol");

            Assert.AreEqual(expectedResults, actualResults);
        }
    }
}
