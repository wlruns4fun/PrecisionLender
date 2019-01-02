using OpenQA.Selenium; // included so I could use the Selenium webdriver
using OpenQA.Selenium.Support.PageObjects; // included so I could use Page Objects
using System;

namespace WebUiTesting.RecipePuppy
{
    class FooterMenu
    {
        private IWebDriver driver;

        public FooterMenu(IWebDriver driver)
        {
            this.driver = driver;
            PageFactory.InitElements(driver, this); // PageFactory is deprecated but simplifies things for the purposes of this example
        }

        // About:

        [FindsBy(How = How.LinkText, Using = "About Us")]
        public IWebElement aboutUsLink;

        [FindsBy(How = How.LinkText, Using = "Contact Us")]
        public IWebElement contactUsLink;

        [FindsBy(How = How.LinkText, Using = "Privacy Policy")]
        public IWebElement privacyPolicyLink;

        [FindsBy(How = How.LinkText, Using = "Blog")]
        public IWebElement blogLink;



        // On the Web:

        [FindsBy(How = How.LinkText, Using = "Facebook")]
        public IWebElement facebookLink;

        [FindsBy(How = How.LinkText, Using = "Twitter")]
        public IWebElement twitterLink;

        [FindsBy(How = How.LinkText, Using = "Twitter Reciper Search Bot")]
        public IWebElement twitterRecipeSearchBotLink;



        // Tools:

        [FindsBy(How = How.LinkText, Using = "Add to your Website")]
        public IWebElement addToYourWebsiteLink;

        [FindsBy(How = How.LinkText, Using = "API")]
        public IWebElement apiLink;

        [FindsBy(How = How.LinkText, Using = "Search alongside Google")]
        public IWebElement searchAlongsideGoogleLink;

        [FindsBy(How = How.LinkText, Using = "Recipe Puppy for iPhone")]
        public IWebElement recipePuppyForIphoneLink;

        [FindsBy(How = How.LinkText, Using = "Vegetarian Search")]
        public IWebElement vegetarianSearchLink;

        [FindsBy(How = How.LinkText, Using = "Vegan Search")]
        public IWebElement veganSearchLink;



        // More:

        [FindsBy(How = How.LinkText, Using = "Submit your Recipe")]
        public IWebElement submitYourRecipeLink;

        [FindsBy(How = How.LinkText, Using = "Cooking Q&A")]
        public IWebElement cookingQAndALink;

        [FindsBy(How = How.LinkText, Using = "Online Grocery Delivery")]
        public IWebElement onlineGroceryDeliveryLink;

        [FindsBy(How = How.LinkText, Using = "Restaurant Gift Certificates")]
        public IWebElement restaurantGiftCertificatesLink;

        [FindsBy(How = How.LinkText, Using = "Store")]
        public IWebElement storeLink;



        // Recipe Puppy:

        [FindsBy(How = How.LinkText, Using = "Daily Recipes Email")]
        public IWebElement dailyRecipesEmailLink;
    }
}
