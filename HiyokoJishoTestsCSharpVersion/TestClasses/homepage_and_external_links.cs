using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace HiyokoJishoTestsCSharpVersion.TestClasses
{
    [TestFixture]
    public class homepage_and_external_links
    {
        private HiyokoJishoMainPage HiyokoJishoWebpage;

        [SetUp]
        public void Homepage_and_external_links_setup()
        {
            HiyokoJishoWebpage = new HiyokoJishoMainPage();
            HiyokoJishoWebpage.navigationHelper.GetWebDriver().Navigate().GoToUrl("https://www.hiyokojisho.com/");
        }

        [TearDown]
        public void Homepage_and_external_links_teardown()
        {
            IWebDriver driver = HiyokoJishoWebpage.navigationHelper.GetWebDriver();
            driver.Quit();
        }

        [Test]
        public void Confirm_Title()
        {
            String pageTitle = HiyokoJishoWebpage.navigationHelper.GetWebDriver().Title;
            Assert.AreEqual(pageTitle, "Hiyoko Jisho");
        }

        [Test]
        public void Confirm_Page_Text()
        {
            String[] Homepage_Text;
            Homepage_Text = new string[5] { "Intermediate Japanese Word Builder Dictionary", "Enter any Kanji, Heisig Keyword, or English/Japanese sentences in the box above.", "Use the 'Build Word' button to create kanji compounds based on your search results.", "This site uses some heisig json and the Official Unofficial Jisho.org API", "Issues? New Feature Ideas?" };
            for (int i = 0; i < Homepage_Text.Length; i++)
            {
                Boolean PageTextIsVisible = HiyokoJishoWebpage.VerifyText(Homepage_Text[i]);
                Assert.True(PageTextIsVisible);
            }
        }

        [Test]
        public void Check_PageLink()
        {
            String url = HiyokoJishoWebpage.navigationHelper.GetWebDriver().Url;
            Assert.AreEqual(url, "https://www.hiyokojisho.com/");
        }

        [Test]
        public void Confirm_Page_Links()
        {
            String[] Homepage_LinkText;
            IWebDriver driver = HiyokoJishoWebpage.navigationHelper.GetWebDriver();
            Homepage_LinkText = new string[3] { "Support/Issues", "Hiyoko Jisho Github", "Andrew Tae" };
            Boolean OnTargetPage;

            for (int i = 0; i < Homepage_LinkText.Length; i++)
            {
                HiyokoJishoWebpage.ClickHiyokoJishoExternalLink(Homepage_LinkText[i]);
                driver.SwitchTo().Window(driver.WindowHandles.Last());
                String NewPageURL = driver.Url;
                OnTargetPage = false;

                if (i == 0)
                {
                    OnTargetPage = NewPageURL.Equals("https://github.com/atae/jisho-word-builder/issues");
                }
                else if (i == 1)
                {
                    OnTargetPage = NewPageURL.Equals("https://github.com/atae/jisho-word-builder");
                }
                else if (i == 2)
                {
                    OnTargetPage = NewPageURL.Equals("https://github.com/atae/");
                }

                Assert.True(OnTargetPage);
                driver.Close();
                driver.SwitchTo().Window(driver.WindowHandles.First());
            }
        }

    }
}
