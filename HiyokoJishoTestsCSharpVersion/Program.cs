using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HiyokoJishoTestsCSharpVersion
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World.");

            IWebDriver driver = new ChromeDriver();
            driver.Navigate().GoToUrl("https://www.google.com");
            driver.FindElement(By.Name("q")).SendKeys("Hello World");
            driver.Quit();
            //driver.Navigate().GoToUrl("https://www.hiyokojisho.org");

        }
    }
}
