using OpenQA.Selenium; // included so I could use the Selenium webdriver
using OpenQA.Selenium.Support.PageObjects; // included so I could use Page Objects
using System;

namespace WebUiTesting.RecipePuppy
{
    class BasePage
    {
        public IWebDriver driver;
        public String url = "http://www.recipepuppy.com";
        public TopMenu topMenu;
        public FooterMenu footerMenu;

        public BasePage(IWebDriver driver, String url=null)
        {
            this.driver = driver;
            PageFactory.InitElements(driver, this); // PageFactory is deprecated but simplifies things for the purposes of this example

            if (url != null)
            {
                this.url = url;
            }

            this.topMenu = new TopMenu(driver); // decided to separate the base top menu into it's own object for modularity
            this.footerMenu = new FooterMenu(driver); // decided to separate the base footer menu into it's own object for modularity
        }

        [FindsBy(How = How.XPath, Using = "//img[@title='Search Recipes by Ingredients and Keywords']")]
        public IWebElement recipePuppyImage;
    }
}
