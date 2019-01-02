using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using TechTalk.SpecFlow; // included so I could use Gherkin

namespace UnitTesting
{
    [Binding]
    public class UnitTestingSteps
    {
        private String string1;
        private String string2;
        private String actualResult;

        [Before]
        public void Setup()
        {
            SetString1ToNull();
            SetString2ToNull();
            actualResult = null;
        }

        [Given("I set the value of string 1 to null")]
        public void SetString1ToNull()
        {
            string1 = null;
        }

        [Given("I set the value of string 1 to '(.*)'")]
        public void SetString1Value(String value)
        {
            string1 = value;
        }

        [Given("I set the value of string 2 to null")]
        public void SetString2ToNull()
        {
            string2 = null;
        }

        [Given("I set the value of string 2 to '(.*)'")]
        public void SetString2Value(String value)
        {
            string2 = value;
        }

        [When("I pass string 1 and string 2 into the method")]
        public void DoTheThing()
        {
            actualResult = UnitTesting.InterleaveStrings(string1, string2);
        }

        [Then("the value returned from the method should be null")]
        public void VerifyNull()
        {
            Assert.IsNull(actualResult);
        }

        [Then("the value returned from the method should be '(.*)'")]
        public void VerifyValue(String expectedResult)
        {
            Assert.AreEqual(expectedResult, actualResult);
        }
    }
}