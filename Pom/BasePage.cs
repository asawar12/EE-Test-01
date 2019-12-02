using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TP.Utilities;

namespace TP.Pom
{
    public class BasePage
    {
        public static IWebDriver baseDriver;
        public static WebDriverWait baseWait;
        public By _hotelBookingPage = (By.XPath("/html/body/div[1]/div[1]/h1"));

        public BasePage()
        {
            baseDriver = Driver.driver;
            baseWait = Driver.Wait;
        }

        public void Click(By _locator)
        {
            Find(_locator).Click();
        }

        public void EnterText(By _locator, string inputText)
        {
            GetClickableElement(_locator).SendKeys(inputText);
        }

        public void SelectValue(By _locator, string optionValue)
        {
            new SelectElement(baseDriver.FindElement(_locator)).SelectByText(optionValue);
        }

        protected IWebElement Find(By Locator)
        {
            var _locator = GetClickableElement((Locator));
            return baseDriver.FindElement(Locator);
        }

        protected IWebElement GetClickableElement(By Locator)
        {
            return baseWait.Until(ExpectedConditions.ElementToBeClickable(Locator));
        }

        protected IWebElement GetVisibleElement(By Locator)
        {
            return baseWait.Until(ExpectedConditions.ElementIsVisible(Locator));
        }

        public bool Isdisplayed(By Locator)
        {
            return (GetVisibleElement(Locator)).Displayed;
        }
    }
}
