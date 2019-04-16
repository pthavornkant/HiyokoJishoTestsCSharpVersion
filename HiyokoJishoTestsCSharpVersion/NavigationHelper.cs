using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HiyokoJishoTestsCSharpVersion
{
    class NavigationHelper
    {
        //Default Wait Time
        private const int WAIT_TIME = 20;
        private IWebDriver driver;
        
        public IWebDriver GetWebDriver()
        {
            return this.driver;
        }
        /*TODO: implement the following commands but in C# code
         * private static Wait <WebDriver> wait = null;
         private static Wait <WebDriver> elementExistsWait = null;

         // Default wait time
         private static final int WAIT_TIME = 60;

         // Default poll interval
         private static final int POLL_INTERVAL = 1;

         // Wait time to check if an element exists
         private static final int ELEMENT_EXISTS_WAIT_TIME = 5;
         */

        public NavigationHelper()
        {
            driver = new ChromeDriver();

            //TODO: create waits and loggers here as well
        }

        public void GoTo(String url)
        {
            Assert.AreNotEqual(url, null);
            driver.Navigate().GoToUrl(url);
        }

        public void clickElement(By locator)
        {
            driver.FindElement(locator).Click();
        }

        public void input(By locator, String input)
        {
            driver.FindElement(locator).SendKeys(input);
        }

        public Boolean isElementPresent(By locator)
        {
            //TODO: Finish this method
            return false;
        }

    }
}
