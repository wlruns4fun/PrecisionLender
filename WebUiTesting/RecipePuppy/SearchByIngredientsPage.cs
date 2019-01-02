using OpenQA.Selenium; // included so I could use the Selenium webdriver
using OpenQA.Selenium.Support.PageObjects; // included so I could use Page Objects
using System;
using System.Collections.Generic;

namespace WebUiTesting.RecipePuppy
{
    class SearchByIngredientsPage : BasePage
    {
        public SearchByIngredientsPage(IWebDriver driver) : base(driver) { }

        [FindsBy(How = How.Id, Using = "addIng")]
        public IWebElement ingredientsTextField;

        [FindsBy(How = How.ClassName, Using = "ac_results")]
        public IWebElement ingredientsDropDown;

        [FindsBy(How = How.XPath, Using = "//div[@class='ac_results']//li")]
        public IList<IWebElement> ingredientsDropDownResults;

        public bool IngredientsDropDownContainsValue(String value)
        {
            try
            {
                foreach (var ingredient in ingredientsDropDownResults)
                {
                    if (!ingredient.Text.ToLower().Contains(value.ToLower()))
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

        public void IngredientsDropDownSelectValue(String value)
        {
            foreach (var ingredient in ingredientsDropDownResults)
            {
                if (ingredient.Text.ToLower().Equals(value.ToLower()))
                {
                    ingredient.Click();
                    break; // theoretically, there could be duplicate values so only select the first one
                }
            }
        }

        [FindsBy(How = How.XPath, Using = "//div[@class='searchbox']//input[@type='submit']")]
        public IWebElement searchButton;

        [FindsBy(How = How.LinkText, Using = "Advanced Search")]
        public IWebElement advancedSearchLink;
    }
}
