using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MessageLibrary;

namespace NapierBankTester
{
    [TestClass]
    // This test tests the quarantine method
    public class UnitTest2
    {
        [TestMethod]
        // This unit proves the method can replace a url with <URL Quarantined>
        public void testURL1()
        {
            Quarantine myQua = new Quarantine();

            string expectedResults = "<URL Quarantined>";

            string actualResults = myQua.quarantine("www.bbcnews.co.uk");

            Assert.AreEqual(expectedResults, actualResults);
        }
        [TestMethod]
        // This unit proves the method can replace a url with <URL Quarantined>
        public void testURL2()
        {
            Quarantine myQua = new Quarantine();

            string expectedResults = "<URL Quarantined>";

            string actualResults = myQua.quarantine("www.skynews.co.uk");

            Assert.AreEqual(expectedResults, actualResults);
        }
        [TestMethod]
        // This unit proves the method can replace two urls with <URL Quarantined>
        public void testURL3()
        {
            Quarantine myQua = new Quarantine();

            string expectedResults = "<URL Quarantined> <URL Quarantined>";

            string actualResults = myQua.quarantine("www.skynews.co.uk www.bbcnews.co.uk");

            Assert.AreEqual(expectedResults, actualResults);
        }
        [TestMethod]
        // This unit proves the method cannot replace an incomplete url
        public void testURL4()
        {
            Quarantine myQua = new Quarantine();

            string expectedResults = "www.skynews";

            string actualResults = myQua.quarantine("www.skynews");

            Assert.AreEqual(expectedResults, actualResults);
        }
    }
}
