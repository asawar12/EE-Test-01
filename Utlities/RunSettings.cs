using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP.Utilities
{
    [TestClass]
    public class RunSettings
    {

        private static TestContext _testContext;

        #region Class Properties
        public static TestContext TestContext { get { return TestContext1; } }

        public static TestContext TestContext1 { get => _testContext; set => _testContext = value; }

        public string Browser => (TestContext.Properties["Browser"] ?? "Chrome").ToString();
        
        #endregion

        [AssemblyInitialize]
        public static void AssemblyInt(TestContext context)
        {
            TestContext1 = context;
        }
    }
}
