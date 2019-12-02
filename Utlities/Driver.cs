using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Support.UI;
using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP.Utilities
{
    public class Driver
    {

        public static IWebDriver driver;
        public static WebDriverWait Wait;
        public static string RootUrl;

        public static void OpenBrowser(string selectedBrowser)
        {
            switch (selectedBrowser.ToLower())

            {
                case "chrome":

                    driver = new ChromeDriver();
                    driver.Manage().Window.Maximize();
                    break;

                case "ie":

                    driver = new InternetExplorerDriver();
                    driver.Manage().Window.Maximize();
                    break;

                default:
                    Debug.Print("unknown browser selected");
                    break;
            }

            Wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
        }

        public static void ShutDown()
        {
            driver.Quit();
        }

        public static void NavigateTo(string targetUrl)
        {
            driver.Navigate().GoToUrl(targetUrl);
        }
    }
}