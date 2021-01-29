using Everlight.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;



namespace Everlight.TestCases.Runner
{
    [TestClass]
    public class TestRunner : TestBase
    {

        [TestInitialize]
        public void TestInitialize()
        {
            DataLoad.InitialiseTextContext(TestContext);
            if (dataDrivenTest)
                isTestEnabled.Value = Start();
        }

        [TestCleanup]
        public void TestCleanup()
        {

                    Driver?.Quit();
        }

        [AssemblyInitialize]
        public new static void AssemblyInit(TestContext testContext)
        {
            // call the base AssemblyInitialize - Waiting on fix by MS so this is not needed
            TestBase.AssemblyInit(testContext);
        }

        [AssemblyCleanup]
        public new static void TestSuiteCleanup()
        {
            // call the base AssemblyCleanup - Waiting on fix by MS so this is not needed
            TestBase.TestSuiteCleanup();
        }

    }
}
