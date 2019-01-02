using OpenQA.Selenium; // included so I could use the Selenium webdriver
using OpenQA.Selenium.Support.PageObjects; // included so I could use Page Objects
using System;

namespace WebUiTesting.RecipePuppy
{
    class TopMenu
    {
        private IWebDriver driver;

        public TopMenu(IWebDriver driver)
        {
            this.driver = driver;
            PageFactory.InitElements(driver, this); // PageFactory is deprecated but simplifies things for the purposes of this example
        }

        [FindsBy(How = How.XPath, Using = "//div[@class='network']//img[1]")]
        public IWebElement recipeLabsLogo;

        [FindsBy(How = How.LinkText, Using = "Create a Recipe")]
        public IWebElement createARecipeLink;

        [FindsBy(How = How.LinkText, Using = "Daily Recipes by Email")]
        public IWebElement dailyRecipesByEmailLink;

        [FindsBy(How = How.LinkText, Using = "Ingredient Pairings")]
        public IWebElement ingredientPairingsLink;

        [FindsBy(How = How.LinkText, Using = "Restaurant Coupons")]
        public IWebElement restaurantCouponsLink;

        [FindsBy(How = How.LinkText, Using = "Recipe Search by Ingredients")]
        public IWebElement recipeSearchByIngredientsLink;
    }
}
