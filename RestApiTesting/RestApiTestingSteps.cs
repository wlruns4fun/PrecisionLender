using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json.Linq; // included so I could use JSON
using System;
using System.Net;
using TechTalk.SpecFlow; // included so I could use Gherkin

namespace RestApiTesting
{
    [Binding]
    public class RestApiTestingSteps
    {
        private const String API = "http://www.recipepuppy.com/api/";
        private WebClient client; // using WebClient because one the simpliest examples I could find, not being familiar with C#
        private JObject response;

        private String query;
        private String ingredients;
        private String page;

        [Before]
        public void Setup()
        {
            client = new WebClient(); 
            response = null;

            query = null;
            ingredients = null;
            page = null;
        }

        [Given("I set the value of the (?:search |)query to '(.*)'")]
        public void SetSearchQueryValue(String value)
        {
            query = value;
        }

        [Given("I set the value of the ingredients to '(.*)'")]
        public void SetIngredientsValue(String value)
        {
            ingredients = value;
        }

        [Given("I set the value of the page (?:number |)to '(.*)'")] // would normally restrict this input to "(\\d+)"
        public void SetPageValue(String value) // but I also want to include "bad data" tests for string values 
        {
            page = value; 
        }

        [When("I get the (?:API |)response")]
        public void GetResponse()
        {
            String url = API;

            if (query != null || ingredients != null || page != null)
            {
                url += "?"; // order of parameters doesn't matter so we don't have to test all the permutations
            }

            if (query != null) 
            {
                url += "q=" + query;
            }

            if (ingredients != null)
            {
                if (query != null)
                {
                    url += "&";
                }

                url += "i=" + ingredients;
            }

            if (page != null)
            {
                if (query != null || ingredients != null)
                {
                    url += "&";
                }

                url += "p=" + page;
            }

            try
            {
                String downloadString = client.DownloadString(url);
                response = JObject.Parse(downloadString);
            }
            catch // need to handle exceptions and test for cases that return an error instead of results
            {
                response = null; // TODO: need to refactor this with better error handling, but should be OK for this excercise
            }
        }

        [Then("(?:the API|it) should return an error")]
        public void VerifyError() 
        {
            // this is currently a bad method because if we don't perform any actions we still have a null response but no error
            Assert.IsNull(response); // TODO: need to refactor this error verification to check response status in header, but should be OK for this exercise
        }

        [Then("(?:the API|it) should return a (.*) (?:element |)of '(.*)'")]
        public void VerifyElement(String elementName, String expectedValue)
        {
            if (response == null)
            {
                Assert.Fail();
            }
            else
            {
                String actualValue = (String)response[elementName];

                Assert.AreEqual(expectedValue, actualValue);
            }
        }

        private int GetNumberOfResults()
        {
            if (response == null)
            {
                return 0;
            }
            else
            {
                JArray results = (JArray)response["results"];
                return results.Count;
            }
        }

        [Then("(?:the API|it) should return results")]
        public void VerifyResultsExist()
        {
            if (response == null)
            {
                Assert.Fail();
            }
            else
            {
                Assert.IsTrue(GetNumberOfResults() > 0);
            }
        }

        [Then("(?:the API|it) should return (?:0|no) results")]
        public void VerifyNoResults()
        {
            if (response == null)
            {
                Assert.Fail();
            }
            else
            {
                Assert.IsTrue(GetNumberOfResults() == 0);
            }
        }

        [Then("(?:the API|it) should return (\\d+) results")]
        public void VerifyNumberOfResults(int expectedNumberOfResults)
        {
            if (response == null)
            {
                Assert.Fail();
            }
            else
            {
                int actualNumberOfResults = GetNumberOfResults();
                Assert.AreEqual(expectedNumberOfResults, actualNumberOfResults);
            }
        }

        private bool DoesRecipeTitleContainValue(JObject recipe, String value)
        {
            if (recipe == null)
            {
                return false;
            }
            else
            {
                String title = (String)recipe["title"];
                return title.ToLower().Contains(value.ToLower());
            }
        }

        private bool DoRecipeIngredientsContainValue(JObject recipe, String value)
        {
            if (recipe == null)
            {
                return false;
            }
            else
            {
                String ingredients = (String)recipe["ingredients"];
                return ingredients.ToLower().Contains(value.ToLower());
            }
        }

        [Then("(?:the API|it) should return results that contain '(.*)'")]
        public void VerifyResultsContent(String value)
        {
            if (response == null)
            {
                Assert.Fail();
            }
            else
            {
                JArray results = (JArray)response["results"];

                if (results.Count == 0)
                {
                    Assert.Fail();
                }

                foreach (JObject recipe in results)
                {
                    bool doesRecipeTitleContainValue = DoesRecipeTitleContainValue(recipe, value);
                    bool doRecipeIngredientsContainValue = DoRecipeIngredientsContainValue(recipe, value);

                    Assert.IsTrue(doesRecipeTitleContainValue || doRecipeIngredientsContainValue); // value could be in the title or ingredients
                }
            }
        }

        [Then("(?:the API|it) should return results that contain the ingredient '(.*)'")]
        public void VerifyResultsIngredients(String value)
        {
            if (response == null)
            {
                Assert.Fail();
            }
            else
            {
                JArray results = (JArray)response["results"];

                if (results.Count == 0)
                {
                    Assert.Fail();
                }

                foreach (JObject recipe in results)
                {
                    bool doRecipeIngredientsContainValue = DoRecipeIngredientsContainValue(recipe, value);

                    Assert.IsTrue(doRecipeIngredientsContainValue);
                }
            }
        }
    }
}
