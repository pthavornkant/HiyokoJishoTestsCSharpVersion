using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;


namespace HiyokoJishoTestsCSharpVersion
{
    [TestFixture]
    public class NUnitTemplateTests
    {
        [Test]
        public void TestMethod()
        {
            Assert.Pass("Sample test passed.");
        }

        [Test]
        public void AccessTest()
        {
            IWebDriver driver = new ChromeDriver();
            driver.Navigate().GoToUrl("https://www.hiyokojisho.com/");
            String pageTitle = driver.Title;
            Assert.AreEqual(pageTitle, "Hiyoko Jisho");
            driver.Quit();
        }
    }
}
