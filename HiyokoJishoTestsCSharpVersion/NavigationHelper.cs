using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras;
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

        public void ClickElement(By locator)
        {
            driver.FindElement(locator).Click();
        }

        public void KeyInput(By locator, String input)
        {
            driver.FindElement(locator).SendKeys(input);
        }

        public Boolean IsElementPresent(By locator)
        {
            IWebElement element = new WebDriverWait(driver, TimeSpan.FromSeconds(WAIT_TIME)).Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(locator));
            return element != null;
        }

        public Boolean IsElementClickable(By locator)
        {
            IWebElement element = new WebDriverWait(driver, TimeSpan.FromSeconds(WAIT_TIME)).Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(locator));
            return element != null;
        }

        public String GetCurrentURL()
        {
            return driver.Url;
        }

    }
}
