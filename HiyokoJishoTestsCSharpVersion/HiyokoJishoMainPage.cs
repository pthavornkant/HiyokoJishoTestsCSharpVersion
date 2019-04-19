using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HiyokoJishoTestsCSharpVersion
{
    class HiyokoJishoMainPage
    {
        public NavigationHelper navigationHelper = new NavigationHelper();
        //LOCATORS START
        private By support_and_issues = By.LinkText("Support/Issues");
        private By hiyokoJishoGitHub = By.LinkText("Hiyoko Jisho Github");
        private By andrewTae = By.LinkText("Andrew Tae");

        //Search Boxes
        //TODO: Convert XPath to CSS
        private By searchBoxLocator = By.CssSelector("form > input");
        private By bwSearchBoxLocator = By.CssSelector("div > input");

        //Buttons
        private By heisigAdd = By.CssSelector("div.heisig > div > div:nth-child(1) > div > button:nth-child(1)");
        private By heisigAddNew = By.CssSelector("div.heisig > div > div:nth-child(1) > div > button:nth-child(1)");
        private By heisigSearch = By.CssSelector("div.heisig > div > div:nth - child(1) > div > button:nth - child(1)");

        private By jishoAdd = By.CssSelector("div:nth-child(3) > div > div.col-md-3.col-xs-6 > div.sense-button-container > button:nth-child(1)");
        private By jishoAddNew = By.CssSelector("div:nth-child(3) > div > div.col-md-3.col-xs-6 > div.sense-button-container > button:nth-child(2)");

        private By clearBuilt = By.CssSelector("div.buttons > button:nth-child(2)");
        private By searchBuilt = By.CssSelector("div.buttons > button:nth-child(1)");

        private By historyList = By.CssSelector("div.history-widget-container > button");
        private By clearHistoryButton = By.CssSelector("div.history-item-button > p:last-of-type");
        //End Buttons

        public void GoTo(String url)
        {
            navigationHelper.GoTo(url);
        }

        public Boolean VerifyNoSearchResults(String expectedSearchResults)
        {
            Console.WriteLine("Verifying text: " + expectedSearchResults);
            Boolean textVerificationResult = navigationHelper.IsElementPresent(By.XPath("//h3[contains(text(), '" + expectedSearchResults + "')]"));
            Console.WriteLine(textVerificationResult);

            return textVerificationResult;
        }

        public Boolean VerifyText(String pageText)
        {
            //Verify text exists on page.
            Console.WriteLine("Now verifying: " + pageText);
            return navigationHelper.IsElementPresent(By.XPath("//*[contains(text(),\"" + pageText + "\")]"));
        }

        public String GetSearchText(String sb_locator)
        {
            if (sb_locator.Equals("search bar"))
            {
                IWebElement searchBox = navigationHelper.GetWebDriver().FindElement(searchBoxLocator);
                String searchText = searchBox.GetAttribute("value");
                return searchText;
            }
            else if (sb_locator.Equals("built word bar"))
            {
                IWebElement searchBox = navigationHelper.GetWebDriver().FindElement(bwSearchBoxLocator);
                String searchText = searchBox.GetAttribute("value");
                return searchText;
            }
            else return "faulty locator";
        }

        public void ClickHiyokoJishoExternalLink(String linkText)
        {
            //CLICK ON THE LINK THAT MATCHES THE TEXT OF THE LINK
            By externalLink = By.XPath("//a[contains(@rel,'noreferrer noopener') and contains(text(),'" + linkText + "')]");

            if (navigationHelper.IsElementClickable(externalLink))
            {
                navigationHelper.ClickElement(externalLink);
            }
            else
            {
                //navigationHelper.GetLogDevice().error("Invalid link text. " + linkText + " is not valid, clickable text on HiyokoJisho.");
            }
        }

        public void ClickHiyokoJishoButton(String button_text)
        {
            //For the Heisig and Jisho Add/Search buttons, their locators are a bit wonky without also adding in another search parameter,
            // but we can still handle them here, albeit a bit messily.
            if (button_text.Equals("Heisig Add to Built Word"))
            {
                navigationHelper.ClickElement(heisigAdd);
            }
            else if (button_text.Equals("Heisig Add to New Built Word"))
            {
                navigationHelper.ClickElement(heisigAddNew);
            }
            else if (button_text.Equals("Search Word"))
            {
                navigationHelper.ClickElement(heisigSearch);
            }
            else if (button_text.Equals("Jisho Add to Built Word"))
            {
                navigationHelper.ClickElement(jishoAdd);
            }
            else if (button_text.Equals("Jisho Add to New Built Word"))
            {
                navigationHelper.ClickElement(jishoAddNew);
            }
            else if (button_text.Equals("Clear History"))
            {
                //this looks like a button, but was coded as a link, so we handle it differently
                navigationHelper.ClickElement(clearHistoryButton);
            }
            else
            {//The locator naming is intuitive, the button properly contains the text within the button.
                By buttonLocator = By.XPath("//button[contains(text(), '" + button_text + "')]");
                navigationHelper.ClickElement(buttonLocator);
            }
        }

        public void enterSearchText(String textToSearch, String searchBarLocator)
        {
            if (searchBarLocator.Equals("search bar"))
            {
                navigationHelper.KeyInput(searchBoxLocator, textToSearch);
            }
            else if (searchBarLocator.Equals("built word search bar"))
            {
                navigationHelper.KeyInput(bwSearchBoxLocator, textToSearch);
            }

        }

        public Boolean verifyHiyokoJishoTitle(String pageTitle)
        {
            Boolean expectedTitle = false;
            if ((pageTitle.ToLower().Equals(navigationHelper.GetWebDriver().Title.ToLower().Contains(pageTitle.ToLower()))))
            {
                expectedTitle = true;
            }
            return expectedTitle;
        }

        public Boolean verifyHeisigSearchResults(String language, String textToSearch)
        {
            if (language.Equals("English"))
            {
                Console.WriteLine("Verifying text: " + textToSearch);
                Boolean textVerificationResult = navigationHelper.IsElementPresent(By.XPath("//div[@class='animated fadeIn']//p[text()='" + textToSearch + "']"));
                Console.WriteLine(textVerificationResult);

                return textVerificationResult;
            }

            else if (language.Equals("Japanese"))
            {
                Console.WriteLine("Verifying text: " + textToSearch);
                Boolean textVerificationResult = navigationHelper.IsElementPresent(By.XPath("//div[@class='animated fadeIn']//h2[text()[contains(., '" + textToSearch + "')]]"));
                Console.WriteLine(textVerificationResult);

                return textVerificationResult;
            }
            else return false;
        }

        public Boolean verifyJishoSearchResults(String language, String textToSearch)
        {
            if (language.Equals("English"))
            {
                Console.WriteLine("Verifying text: " + textToSearch);
                Boolean textVerificationResult = navigationHelper.IsElementPresent(By.XPath("//div[@class='sense-entry' and text()[contains(., '" + textToSearch + "')]]"));
                //Console.WriteLine(textVerificationResult);

                return textVerificationResult;
            }
            else if (language.Equals("Japanese"))
            {
                Console.WriteLine("Verifying text: " + textToSearch);
                Boolean textVerificationResult = navigationHelper.IsElementPresent(By.XPath("//div[@class='entry-word']//h2[text()[contains(., '" + textToSearch + "')]]"));
                Console.WriteLine(textVerificationResult);

                return textVerificationResult;
            }
            else return false;
        }

        public Boolean verifyEmptySearchResults()
        {
            //Verify that the Heisig Search Results container and Jisho Search Results containers are not present.
            //These locators should not exist on the page if you press the clear button.
            if (navigationHelper.GetWebDriver().FindElements(By.XPath("//div[@class='container animated fadeIn']//div[@class='heisig']")).Count < 1 &&
                    navigationHelper.GetWebDriver().FindElements(By.XPath("//div[@class='container animated fadeIn']//h3[@class='jisho']")).Count < 1)
            {
                return true;
            }
            else return false;
        }

        public void clearSearchBarText()
        {
            navigationHelper.GetWebDriver().FindElement(searchBoxLocator).Clear();
        }

        public Boolean verifyBuiltWordSearchBarContains(String addedKanji)
        {
            String bwBoxText = navigationHelper.GetWebDriver().FindElement(bwSearchBoxLocator).Text;
            if (bwBoxText.Contains(addedKanji))
            {
                return true;
            }
            else return false;
        }

        public Boolean verifyBuiltWordSearchBarDisplays(String single_or_compound_kanji)
        {
            String bwBoxText = navigationHelper.GetWebDriver().FindElement(bwSearchBoxLocator).Text;
            if (bwBoxText.Equals(single_or_compound_kanji))
            {
                return true;
            }
            else return false;
        }

        public Boolean builtWordButtonIsAbsent(String button_locator)
        {
            if (button_locator.Equals("Search Built Word"))
            {
                if (navigationHelper.GetWebDriver().FindElements(clearBuilt).Count < 1)
                {
                    return true;
                }
                else return false;
            }
            else if (button_locator.Equals("Clear Words"))
            {
                if (navigationHelper.GetWebDriver().FindElements(searchBuilt).Count < 1)
                {
                    return true;
                }
                else return false;
            }
            else return false;
        }

        public Boolean checkHistoryExpansion(String expanded)
        {
            Boolean expansion = false;
            if (expanded.Equals("should not"))
            {
                if (navigationHelper.GetWebDriver().FindElements(historyList).Count < 1)
                {
                    expansion = true; //Actually means not expanded, but labeling as true for coding purposes.
                }
            }
            else if (expanded.Equals("should"))
            {
                if (navigationHelper.GetWebDriver().FindElements(historyList).Count > 0)
                {
                    expansion = true; //Means we have expanded history list.
                }
            }
            return expansion;
        }

        public Boolean verifyHistoryContains(String historic_result)
        {
            Boolean result_exists = false;
            if (historic_result.Equals("No Results"))
            {
                if (navigationHelper.GetWebDriver().FindElements(By.XPath("//div[@class='history-item' and contains(text(), ' No Results')]")).Count > 0)
                {
                    result_exists = true;
                }
                else
                {
                    Console.WriteLine(navigationHelper.GetWebDriver().FindElement(By.XPath("//div[@class='history-item' and contains(text(), ' No Results')]")));
                }
            }
            else if (historic_result.Equals("Your Search History"))
            {
                if (navigationHelper.GetWebDriver().FindElements(By.XPath("//div[@class='history-item history-item-header']//p//strong[text()[contains(., '" + historic_result + "')]]")).Count > 0)
                {
                    result_exists = true;
                }
                else
                {
                    Console.WriteLine(navigationHelper.GetWebDriver().FindElement(By.XPath("//div[@class='history-item' and contains(text(), ' No Results')]")));
                }
            }
            else
            { //we are looking for an actual result
                if (navigationHelper.GetWebDriver().FindElements(By.XPath("//div[@class='history-item history-item-button']//p[text()[contains(., '" + historic_result + "')]]")).Count > 0)
                {
                    result_exists = true;
                }
                else
                {
                    Console.WriteLine(navigationHelper.GetWebDriver().FindElement(By.XPath("//div[@class='history-item history-item-button']//p[text()[contains(., '" + historic_result + "')]]")));
                }
            }

            return result_exists;
        }

        public void clickHistoryResult(String historic_result)
        {
            navigationHelper.GetWebDriver().FindElement(By.XPath("//div[@class='history-item history-item-button']//p[text()[contains(., '" + historic_result + "')]]")).Click();
            //Add a wait here
        }
    }
}
