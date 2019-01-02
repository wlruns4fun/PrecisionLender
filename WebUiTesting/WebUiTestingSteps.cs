using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium; // included so I could use the Selenium webdriver
using OpenQA.Selenium.Chrome; // included so I could use the Chrome browser
using System;
using TechTalk.SpecFlow; // included so I could use Gherkin
using WebUiTesting.RecipePuppy;

namespace WebUiTesting
{
    [Binding]
    public class WebUiTestingSteps
    {
        private IWebDriver driver;
        private SearchByIngredientsPage searchByIngredientsPage;
        private ResultsPage resultsPage;

        [Before]
        public void Setup()
        {
            driver = new ChromeDriver(); // just chose Chrome as the default browser
            // TODO: extend tests to also execute on multiple browsers

            driver.Manage().Window.Maximize();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10); // set the default timeout value

            searchByIngredientsPage = null;
            resultsPage = null;
        }

        [After]
        public void TearDown()
        {
            if (ScenarioContext.Current.TestError != null)
            {
                // TODO: extend tests to also take a screenshot if test fails
            }

            if (driver != null)
            {
                driver.Quit(); // would normally try to keep the same browser window open between test scenarios
                // and only close on error to decrease the overall test run time, but OK for these quick examples  
            }
        }

        [Given("I go to the (?:Recipe Puppy |)Search by Ingredients page")]
        public void GoToSearchByIngredientsPage()
        {
            searchByIngredientsPage = new SearchByIngredientsPage(driver);
            driver.Url = searchByIngredientsPage.url;
            Assert.IsTrue(searchByIngredientsPage.ingredientsTextField.Displayed);
        }

        [Given("I enter (?:a value of |)'(.*)'(?: on the Search by Ingredients page|)")]
        [When("I enter (?:a value of |)'(.*)'(?: on the Search by Ingredients page|)")]
        public void SearchByIngredientsEnterValue(String value)
        {
            searchByIngredientsPage = new SearchByIngredientsPage(driver);
            searchByIngredientsPage.ingredientsTextField.Clear();
            searchByIngredientsPage.ingredientsTextField.SendKeys(value);
            Assert.AreEqual(value, searchByIngredientsPage.ingredientsTextField.GetAttribute("value"));
        }

        [When("I select the drop-down value of '(.*)'")]
        public void SearchByIngredientsSelectDropDownValue(String value)
        {
            searchByIngredientsPage = new SearchByIngredientsPage(driver);
            searchByIngredientsPage.IngredientsDropDownSelectValue(value);
        }

        [When("I (?:attempt to |)perform a search(?: on the Search by Ingredients page|)")]
        public void SearchByIngredientsPerformSearch()
        {
            searchByIngredientsPage = new SearchByIngredientsPage(driver);
            searchByIngredientsPage.searchButton.Click();
        }

        [When("I click the Recipe Puppy logo(?: on the Search by Ingredients page|)")]
        public void SearchByIngredientsClickLogo()
        {
            searchByIngredientsPage = new SearchByIngredientsPage(driver);
            searchByIngredientsPage.recipePuppyImage.Click();
        }

        [Then("the search field is equal to '(.*)'(?: on the Search by Ingredients page|)")]
        public void SearchByIngredientsVerifyValue(String value)
        {
            searchByIngredientsPage = new SearchByIngredientsPage(driver);
            Assert.AreEqual(value, searchByIngredientsPage.ingredientsTextField.GetAttribute("value").Trim());
        }

        [Then("the ingredients should contain (?:a value of |)'(.*)'")]
        public void VerifyIngredientsContainValue(String value)
        {
            resultsPage = new ResultsPage(driver);
            Assert.IsTrue(resultsPage.IngredientsContainValue(value));
        }

        [Then("the ingredients should not contain (?:a value of |)'(.*)'")]
        public void VerifyIngredientsDoNotContainValue(String value)
        {
            resultsPage = new ResultsPage(driver);
            Assert.IsFalse(resultsPage.IngredientsContainValue(value));
        }

        [Then("the (?:ingredients |)search drop-down contains '(.*)'")]
        public void VerifyIngredientsDropDownValues(String value)
        {
            searchByIngredientsPage = new SearchByIngredientsPage(driver);
            Assert.IsTrue(searchByIngredientsPage.IngredientsDropDownContainsValue(value));
        }

        [Then("no search was performed(?: on the Search by Ingredients page|)")]
        // [ExpectedException(typeof(NoSuchElementException))] annotation only available for unit test methods
        public void VerifyNoSearchWasPerformed()
        {
            searchByIngredientsPage = new SearchByIngredientsPage(driver);
            Assert.AreEqual("", searchByIngredientsPage.ingredientsTextField.GetAttribute("value"));

            Exception expectedException = null; // can manually catch (expected) exceptions, instead
            try
            {
                resultsPage = new ResultsPage(driver);
                Assert.IsFalse(resultsPage.noResultsDiv.Displayed); // results page objects should not exist
                Assert.IsFalse(resultsPage.errorDiv.Displayed); // results page objects should not exist
                Assert.IsFalse(resultsPage.searchStatsDiv.Displayed); // results page objects should not exist
            }
            catch (NoSuchElementException actualException)
            {
                expectedException = actualException;
            }

            Assert.IsNotNull(expectedException);
        }

        [Then("no results were returned")]
        public void VerifyNoResults()
        {
            resultsPage = new ResultsPage(driver);
            Assert.IsTrue(resultsPage.noResultsDiv.Displayed);
            Assert.AreEqual(0, resultsPage.NumberOfResults);
        }

        [Then("(?:Recipe Puppy |)couldn't understand the ingredient(?:s|)")]
        public void VerifyResultsCouldntUnderstandIngredient()
        {
            resultsPage = new ResultsPage(driver);
            Assert.IsTrue(resultsPage.errorDiv.Displayed);
            Assert.IsTrue(resultsPage.errorDiv.Text.Contains("Sorry Recipe Puppy couldn't understand the ingredient"));
        }

        [Then("(?:Recipe Puppy |)searched for value '(.*)' as a keyword instead")]
        public void VerifyResultsSearchedAsKeywordInstead(String value)
        {
            resultsPage = new ResultsPage(driver);
            Assert.IsTrue(resultsPage.errorDiv.Displayed);
            Assert.IsTrue(resultsPage.errorDiv.Text.Contains("searching as a keyword instead"));
            Assert.IsTrue(resultsPage.ResultsContainValue(value));
            Assert.IsFalse(resultsPage.IngredientsContainValue(value));
        }

        [Then("the results should contain (?:a value of |)'(.*)'")]
        public void VerifyResultsContainValue(String value)
        {
            resultsPage = new ResultsPage(driver);
            Assert.IsTrue(resultsPage.ResultsContainValue(value));
        }

        [Then("the results should not contain (?:a value of |)'(.*)'")]
        public void VerifyResultsDoNotContainValue(String value)
        {
            resultsPage = new ResultsPage(driver);
            Assert.IsFalse(resultsPage.ResultsContainValue(value));
        }
    }
}
