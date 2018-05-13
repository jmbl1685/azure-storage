using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AzureStorageExample.UnitTest
{
    [TestClass]
    public class UnitTest
    {
        [TestMethod]
        public void TestMethod1()
        {
            var _test = AzureUtilities.GetAzureBlob().InsertFilesTest();
            Assert.IsNotNull(_test);
        }
    }
}
