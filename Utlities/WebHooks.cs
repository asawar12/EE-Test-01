
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechTalk.SpecFlow;
using TP.Utilities;

namespace TP.Utilities
{
    [Binding]
    public class WebHooks
    {
        public static RunSettings _runSettings = new RunSettings();

        [BeforeFeature()]
        public static void StartBrowser()
        {
            Driver.OpenBrowser(_runSettings.Browser);
        }

        [AfterFeature()]
        public static void CloseBrowser()
        {
            Driver.ShutDown();
        }
    }
}
