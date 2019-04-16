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
        private By searchBoxLocator = By.XPath("//form[@class='search']//input");
        private By bwSearchBoxLocator = By.XPath("//div[@class='builtWord']//input[@type='text']");

        //Buttons
        private By heisigAdd = By.XPath("//*[@id=\"root\"]/div/div[5]/div[1]/div/div[1]/div/button[1]");
        private By heisigAddNew = By.XPath("//*[@id=\"root\"]/div/div[5]/div[1]/div/div[1]/div/button[2]");
        private By heisigSearch = By.XPath("//*[@id=\"root\"]/div/div[5]/div[1]/div/div[1]/div/button[3]");

        private By jishoAdd = By.XPath("//*[@id=\"root\"]/div/div[5]/div[2]/div/div[1]/div[2]/button[1]");
        private By jishoAddNew = By.XPath("//*[@id=\"root\"]/div/div[5]/div[2]/div/div[1]/div[2]/button[2]");

        private By clearBuilt = By.XPath("//div[@class='buttons']//button[@type='button']");
        private By searchBuilt = By.XPath("//div[@class='buttons']//button[@type='submit']");

        private By historyList = By.XPath("//div[@class='history-widget fadeInRight']");
        private By clearHistoryButton = By.XPath("//div[@class='history-item history-item-button']//p[text()[contains(., 'Clear History')]]");
        //End Buttons

        public void GoTo(String url)
        {
            navigationHelper.GoTo(url);
        }

        public Boolean verifyNoSearchResults(String expectedSearchResults)
        {
            Console.WriteLine("Verifying text: " + expectedSearchResults);
            Boolean textVerificationResult = navigationHelper.isElementPresent(By.XPath("//h3[contains(text(), '" + expectedSearchResults + "')]"));
            Console.WriteLine(textVerificationResult);

            return textVerificationResult;
        }

        public Boolean verifyText(String pageText)
        {
            //Verify text exists on page.
            Console.WriteLine("Now verifying: " + pageText);
            return navigationHelper.isElementPresent(By.XPath("//*[contains(text(),\"" + pageText + "\")]"));
        }

        public String getSearchText(String sb_locator)
        {
            if (sb_locator.Equals("search bar"))
            {
                IWebElement searchBox = navigationHelper.getWebDriver().findElement(searchBoxLocator);
                String searchText = searchBox.getAttribute("value");
                return searchText;
            }
            else if (sb_locator.Equals("built word bar"))
            {
                IWebElement searchBox = navigationHelper.getWebDriver().findElement(bwSearchBoxLocator);
                String searchText = searchBox.getAttribute("value");
                return searchText;
            }
            else return "faulty locator";
        }

        public void clickHiyokoJishoExternalLink(String linkText)
        {
            //CLICK ON THE LINK THAT MATCHES THE TEXT OF THE LINK
            By externalLink = By.XPath("//a[contains(@rel,'noreferrer noopener') and contains(text(),'" + linkText + "')]");

            if (navigationHelper.isElementClickable(externalLink))
            {
                navigationHelper.clickElement(externalLink);
            }
            else
            {
                navigationHelper.getLogDevice().error("Invalid link text. " + linkText + " is not valid, clickable text on HiyokoJisho.");
            }
        }

        public void clickHiyokoJishoButton(String button_text)
        {
            //For the Heisig and Jisho Add/Search buttons, their locators are a bit wonky without also adding in another search parameter,
            // but we can still handle them here, albeit a bit messily.
            if (button_text.Equals("Heisig Add to Built Word"))
            {
                navigationHelper.clickElement(heisigAdd);
            }
            else if (button_text.Equals("Heisig Add to New Built Word"))
            {
                navigationHelper.clickElement(heisigAddNew);
            }
            else if (button_text.Equals("Search Word"))
            {
                navigationHelper.clickElement(heisigSearch);
            }
            else if (button_text.Equals("Jisho Add to Built Word"))
            {
                navigationHelper.clickElement(jishoAdd);
            }
            else if (button_text.Equals("Jisho Add to New Built Word"))
            {
                navigationHelper.clickElement(jishoAddNew);
            }
            else if (button_text.Equals("Clear History"))
            {
                //this looks like a button, but was coded as a link, so we handle it differently
                navigationHelper.clickElement(clearHistoryButton);
            }
            else
            {//The locator naming is intuitive, the button properly contains the text within the button.
                By buttonLocator = By.XPath("//button[contains(text(), '" + button_text + "')]");
                navigationHelper.clickElement(buttonLocator);
            }
        }

        public void enterSearchText(String textToSearch, String searchBarLocator)
        {
            if (searchBarLocator.Equals("search bar"))
            {
                navigationHelper.input(searchBoxLocator, textToSearch);
            }
            else if (searchBarLocator.Equals("built word search bar"))
            {
                navigationHelper.input(bwSearchBoxLocator, textToSearch);
            }

        }

        public Boolean verifyHiyokoJishoTitle(String pageTitle)
        {
            Boolean expectedTitle = false;
            if ((pageTitle.ToLower().Equals(navigationHelper.getWebDriver().getTitle().toLowerCase().contains(pageTitle.toLowerCase())))
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
                Boolean textVerificationResult = navigationHelper.isElementPresent(By.XPath("//div[@class='animated fadeIn']//p[text()='" + textToSearch + "']"));
                Console.WriteLine(textVerificationResult);

                return textVerificationResult;
            }

            else if (language.Equals("Japanese"))
            {
                Console.WriteLine("Verifying text: " + textToSearch);
                Boolean textVerificationResult = navigationHelper.isElementPresent(By.XPath("//div[@class='animated fadeIn']//h2[text()[contains(., '" + textToSearch + "')]]"));
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
                Boolean textVerificationResult = navigationHelper.isElementPresent(By.XPath("//div[@class='sense-entry' and text()[contains(., '" + textToSearch + "')]]"));
                //Console.WriteLine(textVerificationResult);

                return textVerificationResult;
            }
            else if (language.Equals("Japanese"))
            {
                Console.WriteLine("Verifying text: " + textToSearch);
                Boolean textVerificationResult = navigationHelper.isElementPresent(By.XPath("//div[@class='entry-word']//h2[text()[contains(., '" + textToSearch + "')]]"));
                Console.WriteLine(textVerificationResult);

                return textVerificationResult;
            }
            else return false;
        }

        public Boolean verifyEmptySearchResults()
        {
            //Verify that the Heisig Search Results container and Jisho Search Results containers are not present.
            //These locators should not exist on the page if you press the clear button.
            if (navigationHelper.getWebDriver().findElements(By.XPath("//div[@class='container animated fadeIn']//div[@class='heisig']")).size() < 1 &&
                    navigationHelper.getWebDriver().findElements(By.XPath("//div[@class='container animated fadeIn']//h3[@class='jisho']")).size() < 1)
            {
                return true;
            }
            else return false;
        }

        public void clearSearchBarText()
        {
            navigationHelper.getWebDriver().findElement(searchBoxLocator).clear();
        }

        public Boolean verifyBuiltWordSearchBarContains(String addedKanji)
        {
            String bwBoxText = navigationHelper.getWebDriver().findElement(bwSearchBoxLocator).getText();
            if (bwBoxText.Contains(addedKanji))
            {
                return true;
            }
            else return false;
        }

        public Boolean verifyBuiltWordSearchBarDisplays(String single_or_compound_kanji)
        {
            String bwBoxText = navigationHelper.getWebDriver().findElement(bwSearchBoxLocator).getText();
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
                if (navigationHelper.getWebDriver().findElements(clearBuilt).size() < 1)
                {
                    return true;
                }
                else return false;
            }
            else if (button_locator.Equals("Clear Words"))
            {
                if (navigationHelper.getWebDriver().findElements(searchBuilt).size() < 1)
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
                if (navigationHelper.getWebDriver().findElements(historyList).size() < 1)
                {
                    expansion = true; //Actually means not expanded, but labeling as true for coding purposes.
                }
            }
            else if (expanded.Equals("should"))
            {
                if (navigationHelper.getWebDriver().findElements(historyList).size() > 0)
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
                if (navigationHelper.getWebDriver().findElements(By.XPath("//div[@class='history-item' and contains(text(), ' No Results')]")).size() > 0)
                {
                    result_exists = true;
                }
                else
                {
                    Console.WriteLine(navigationHelper.getWebDriver().findElement(By.XPath("//div[@class='history-item' and contains(text(), ' No Results')]")));
                }
            }
            else if (historic_result.Equals("Your Search History"))
            {
                if (navigationHelper.getWebDriver().findElements(By.XPath("//div[@class='history-item history-item-header']//p//strong[text()[contains(., '" + historic_result + "')]]")).size() > 0)
                {
                    result_exists = true;
                }
                else
                {
                    Console.WriteLine(navigationHelper.getWebDriver().findElement(By.XPath("//div[@class='history-item' and contains(text(), ' No Results')]")));
                }
            }
            else
            { //we are looking for an actual result
                if (navigationHelper.getWebDriver().findElements(By.XPath("//div[@class='history-item history-item-button']//p[text()[contains(., '" + historic_result + "')]]")).size() > 0)
                {
                    result_exists = true;
                }
                else
                {
                    Console.WriteLine(navigationHelper.getWebDriver().findElement(By.XPath("//div[@class='history-item history-item-button']//p[text()[contains(., '" + historic_result + "')]]")));
                }
            }

            return result_exists;
        }

        public void clickHistoryResult(String historic_result)
        {
            navigationHelper.getWebDriver().findElement(By.XPath("//div[@class='history-item history-item-button']//p[text()[contains(., '" + historic_result + "')]]")).click();
            //Add a wait here
        }
    }
}
