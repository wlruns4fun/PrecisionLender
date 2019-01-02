using OpenQA.Selenium; // included so I could use the Selenium webdriver
using OpenQA.Selenium.Support.PageObjects; // included so I could use Page Objects
using System;
using System.Collections.Generic;

namespace WebUiTesting.RecipePuppy
{
    class ResultsPage : BasePage
    {
        private int numberOfResults;

        public ResultsPage(IWebDriver driver) : base(driver) { }

        [FindsBy(How = How.XPath, Using = "//div[@class='error']")]
        public IWebElement errorDiv;

        [FindsBy(How = How.XPath, Using = "//div[contains(text(), 'Sorry your query')]")]
        public IWebElement noResultsDiv;

        [FindsBy(How = How.Id, Using = "stats")]
        public IWebElement searchStatsDiv;

        [FindsBy(How = How.XPath, Using = "//div[@id='stats']//b[2]")]
        private readonly IWebElement numberOfResultsStr;

        public int NumberOfResults
        {
            get
            {
                try
                {
                    Int32.TryParse(numberOfResultsStr.Text, out numberOfResults);
                }
                catch
                {
                    numberOfResults = 0;
                }

                return numberOfResults;
            }
        }

        [FindsBy(How = How.XPath, Using = "//div[contains(@class, 'result')]")]
        public IList<IWebElement> resultsDivs;

        public bool ResultsContainValue(String value)
        {
            try
            {
                foreach (var recipeDiv in resultsDivs)
                {
                    if (!recipeDiv.Text.ToLower().Contains(value.ToLower()))
                    {
                        return false; // fail if any of the recipes don't contain the value
                    }
                }
            }
            catch
            {
                return false;
            }

            return true;
        }

        [FindsBy(How = How.ClassName, Using = "ings")]
        public IList<IWebElement> resultsIngredientsDivs;

        public bool IngredientsContainValue(String value)
        {
            try
            {
                foreach (var ingredientsDiv in resultsIngredientsDivs)
                {
                    if (!ingredientsDiv.Text.ToLower().Contains(value.ToLower()))
                    {
                        return false; // fail if any of the ingredients don't contain the value
                    }
                }
            }
            catch
            {
                return false;
            }

            return true;
        }

    }
}
