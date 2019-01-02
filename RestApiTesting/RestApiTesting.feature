@RestApiTesting
Feature: REST API Testing
# my thought process for creating test cases usually evolves from asking the following questions:
# how does the system under test handle "nothing" as an input, all possible combinations of "bad data," 
# all possible combinations of "good data," and then performance/stress testing cases of "max data"


# we should probably start testing with the bare minimum to verify that the API returns a successful response
@Elements
Scenario: API should return results even if no parameters are provided
	When I get the API response
	Then the API should return results


# then we can verifying each expected element type is returned, like the title element
@Elements
Scenario: API should return a title element
	When I get the API response
	Then the API should return a title of 'Recipe Puppy'


# and then verifying the version element
@Elements
Scenario: API should return a version element
	When I get the API response
	Then the API should return a version of '0.1'


# and also verifying the href element
@Elements
Scenario: API should return an href element
	When I get the API response
	Then the API should return an href of 'http://www.recipepuppy.com/'


# now that we know all the expected element pieces are there, we can start dissecting the possible query options;
# for instance, how does the search query handle "nothing" as an input
@Query
Scenario: API should return results even if the search query is blank
	Given I set the value of the search query to ''
	When I get the API response
	Then the API should return results


# and then does the search query work as expected in a "happy path" scenario
@Query
Scenario: API should return valid results for search queries
	Given I set the value of the search query to 'pie'
	When I get the API response
	Then the API should return results that contain 'pie'


# and then how does the search query handle "bad data" as an input
@Query
Scenario: API should return no results for invalid search queries 
	Given I set the value of the search query to 'xyz'
	When I get the API response
	Then the API should return no results


# what are the all the possible combinations of "good data" for the search query, such as number values
@Query
Scenario: API should return valid results for search queries that contain numbers
	Given I set the value of the search query to '1'
	When I get the API response
	Then the API should return results that contain '1'


# and how does the search query handle spaces
@Query
Scenario: API should return valid results for search queries that contain spaces
	Given I set the value of the search query to 'apple pie'
	When I get the API response
	Then the API should return results that contain 'apple pie'


# we could extend the scenarios here to test additional combinations of search queries with spaces and whether or not we expect
# results that contain "apple" AND "pie" (in any order), "apple" THEN "pie" (in that order), "apple" OR "pie" (at least one of the two);
# we could also extend the test scenarios here to test how the search query handles special character inputs, and also case sensitivity


# moving on to the ingredients parameter, how do the ingredients handle "nothing" as an input
@Ingredients
Scenario: API should return results even if the ingredients are blank
	Given I set the value of the ingredients to ''
	When I get the API response
	Then the API should return results


# the ingredients can also handle comma separated values, so we need to check both single input values
@Ingredients
Scenario: API should return valid results for single ingredients
	Given I set the value of the ingredients to 'apple'
	When I get the API response
	Then the API should return results that contain the ingredient 'apple'


# and also check ingredients with multiple values
@Ingredients
Scenario: API should return valid results for multiple ingredients
	Given I set the value of the ingredients to 'apple, sugar, flour'
	When I get the API response
	Then the API should return results that contain the ingredient 'apple'
		And the API should return results that contain the ingredient 'sugar'
		And the API should return results that contain the ingredient 'flour'


# what happens when we try to pass numerical inputs as ingredient values (spoiler alert, we get no results)
@Ingredients
Scenario: API should return no results for ingredients that contain numbers
	Given I set the value of the ingredients to '1'
	When I get the API response
	Then the API should return no results


# also verify that the results are not case sensitive
@Ingredients
Scenario: API should return valid results for ingredients regardless of case
	Given I set the value of the ingredients to 'APPLE'
	When I get the API response
	Then the API should return results that contain the ingredient 'apple'


# also what happens if we have an ingredient that includes spaces
@Ingredients
Scenario: API should return valid results for ingredients that contain spaces
	Given I set the value of the ingredients to 'brown sugar'
	When I get the API response
	Then the API should return results that contain the ingredient 'brown sugar'


# we could extend the scenarios here to test combinations of how the API should handle ingredients with special characters, 
# are the ingredients supposed to be returned in alphabetical order, etc.


# one of the other remaining parameters we still need to test is how the API handles paging all the results;
# to start, we need to check and see how the page input handles "nothing" as a value
@Paging
Scenario: API should return an error if the page number is blank
	Given I set the value of the page number to ''
	When I get the API response
	Then the API should return an error


# what happens if we try to pass in a "bad data" numercial value for page number
@Paging
Scenario: API should return an error if the page number is zero
	Given I set the value of the page number to '0'
	When I get the API response
	Then the API should return an error


# what happens if we try to pass in a "bad data" string value for page number
@Paging
Scenario: API should return an error if the page number is a string
	Given I set the value of the page number to 'x'
	When I get the API response
	Then the API should return an error


# and then we can finally test a "happy path" scenario for page number
@Paging
Scenario: API should return valid results for valid page numbers
	Given I set the value of the page number to '1'
	When I get the API response
	Then the API should return results


# and we also want to verify our "max data" limit per each page
@Paging
Scenario: API should return a maximum of 10 results per page
	Given I set the value of the page number to '1'
	When I get the API response
	Then the API should return 10 results


# we could extend the scenarios here to include a test where we know the exact number of results for a query
# and then verify the specific number of results we should see on each page number


# now that we have tested each parameter individually, we also want to verify that all combinations of parameters also work;
# for instance, what happens if I search by query and ingredients
@Query @Ingredients
Scenario: API should return valid results when searching by query and ingredients
	Given I set the value of the search query to 'pie'
		And I set the value of the ingredients to 'apple'
	When I get the API response
	Then the API should return results that contain 'pie'
		And the API should return results that contain the ingredient 'apple'


# and when we search by query and page number
@Query @Paging
Scenario: API should return valid results when searching by query and page number
	Given I set the value of the search query to 'pie'
		And I set the value of the page number to '1'
	When I get the API response
	Then the API should return results that contain 'pie'
		And the API should return 10 results


# and when we search by ingredients and page number
@Ingredients @Paging
Scenario: API should return valid results when searching by ingredients and page number
	Given I set the value of the ingredients to 'apple'
		And I set the value of the page number to '1'
	When I get the API response
	Then the API should return results that contain the ingredient 'apple'
		And the API should return 10 results


# and when we search by query, ingredients, and page number
@Query @Ingredients @Paging
Scenario: API should return valid results when searching by query, ingredients, and page number
	Given I set the value of the search query to 'pie'
		And I set the value of the ingredients to 'apple'
		And I set the value of the page number to '1'
	When I get the API response
	Then the API should return results that contain 'pie'
		And the API should return results that contain the ingredient 'apple'
		And the API should return 10 results


# we could also extend the scenarios here to test how the returned results are being sorted (e.g., alphabetically by href, title, etc.);
# if there are any performance thresholds we could also consider extending our scenarios to include such tests as:
# are there any response timeout limits, are there a max total number of results that can be returned, is there a max page number limit, etc.