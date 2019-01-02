@WebUiTesting @SearchByIngredients
Feature: Web UI Testing
# my thought process for creating test cases usually evolves from asking the following questions:
# how does the system under test handle "nothing" as an input, all possible combinations of "bad data," 
# all possible combinations of "good data," and then performance/stress testing cases of "max data"


# we can begin by verifying how to reset the search page back to its initial state 
Scenario: Search by Ingredients should reload the page when you click on the Recipe Puppy logo
	Given I go to the Search by Ingredients page
		And I enter a value of 'apple'
	When I click the Recipe Puppy logo
	Then the search field is equal to ''


# then we want to verify how the search handles "nothing" as an input
Scenario: Search by Ingredients should not search if no ingredients are provided
	Given I go to the Search by Ingredients page
		And I enter a value of ''
	When I attempt to perform a search
	Then no search was performed


# next we can examine how the search handles examples of "bad" ingredients that aren't specific enough
Scenario: Search by Ingredients should return no results if ingredient is not broad enough
	Given I go to the Search by Ingredients page
		And I enter a value of 'x'
	When I perform a search
	Then no results were returned


# and what if a "bad" ingredient is more specific but still invalid
Scenario: Search by Ingredients should return an error message if can't understand the ingredient provided
	Given I go to the Search by Ingredients page
		And I enter a value of 'xyz'
	When I perform a search
	Then couldn't understand the ingredient


# and what if an ingredient is "bad" but still "good" in the context of a valid keyword
Scenario: Search by Ingredients should search by keyword instead if can't understand the ingredient provided
	Given I go to the Search by Ingredients page
		And I enter a value of 'test'
	When I perform a search
	Then searched for value 'test' as a keyword instead


# then we can move on to test how the search handles variations of "good data" with numerical inputs
Scenario: Search by Ingredients should return results for ingredients that contain numbers
	Given I go to the Search by Ingredients page
		And I enter a value of '1'
	When I perform a search
	Then the results should contain a value of '1'


# and verify how the search should behave in regards to case sensitivity
Scenario: Search by Ingredients should return results for ingredients that are not case sensitive
	Given I go to the Search by Ingredients page
		And I enter a value of 'APPLE'
	When I perform a search
	Then the results should contain a value of 'apple'


# should the search be able to handle valid ingredients with spaces
Scenario: Search by Ingredients should return results for a valid ingredient that contain spaces
	Given I go to the Search by Ingredients page
		And I enter a value of 'brown sugar'
	When I perform a search
	Then the results should contain a value of 'brown sugar'


# and what about invalid ingredients with spaces
Scenario: Search by Ingredients should return results for only the first ingredient for an invalid ingredient with spaces
	Given I go to the Search by Ingredients page
		And I enter a value of 'apple pie'
	When I perform a search
	Then the results should contain a value of 'apple'
		And the search field is equal to 'apple,'


# we can verify our basic "happy path" test with a single ingredient
Scenario: Search by Ingredients should return valid results for single ingredients
	Given I go to the Search by Ingredients page
		And I enter a value of 'apple'
	When I perform a search
	Then the results should contain a value of 'apple'


# and then we also need to verify the search behavior with mulitple ingredients
Scenario: Search by Ingredients should return valid results for multiple ingredients
	Given I go to the Search by Ingredients page
		And I enter a value of 'apple, sugar, flour'
	When I perform a search
	Then the ingredients should contain a value of 'apple'
		And the ingredients should contain a value of 'sugar'
		And the ingredients should contain a value of 'flour'


# since the search field also retains information between searches, we should verify its behavior
Scenario: Search by Ingredients should keep the previous searched for ingredients in the search bar
	Given I go to the Search by Ingredients page
		And I enter a value of 'apple'
	When I perform a search
	Then the search field is equal to 'apple,'


# here would be a good opportunity to possibly extend the tests to verify the search field for multiple searches


# we should also verify the behvaior of the search drop-down menu
Scenario: Search by Ingredients should include a drop-down menu for valid ingredients
	Given I go to the Search by Ingredients page
	When I enter a value of 'apple'
	Then the search drop-down contains 'apple'


# and verify how the search field gets updated from the drop-down menu
Scenario: Search by Ingredients should add ingredients selected via the drop-down menu to the search bar
	Given I go to the Search by Ingredients page
		And I enter a value of 'apple'
	When I select the drop-down value of 'apple'
	Then the search field is equal to 'apple,'


# we could extend the scenarios here to also test appending multiple ingredients via the drop-down menu behavior;
# if it is necessary to test the number of results and/or paging, we could extend the tests to include that here;
# my next suggestion would be to extend the scenarios to include all the tests for the Advanced Search page, as well;
# if/when we decide to test the Advanced Search scenarios, I would recommend also creating a separate feature file for those tests;
# as far as testing "max data," if there are any performance thresholds we could also consider extending our scenarios to include such tests as:
# are there any response timeout limits, are there a max total number of results that can be returned, is there a max page number limit, etc.