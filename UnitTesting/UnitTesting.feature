@UnitTesting
Feature: Unit Testing
# my thought process for creating test cases usually evolves from asking the following questions:
# how does the system under test handle "nothing" as an input, all possible combinations of "bad data," 
# all possible combinations of "good data," and then performance/stress testing cases of "max data"


# so we start by considering each combination of "nothing", so null for the 1st parameter
Scenario: Method should return the value of string 2 if string 1 is null
	Given I set the value of string 1 to null
		And I set the value of string 2 to '123'
	When I pass string 1 and string 2 into the method
	Then the value returned from the method should be '123'


# and also null for the 2nd parameter
Scenario: Method should return the value of string 1 if string 2 is null
	Given I set the value of string 1 to 'abc'
		And I set the value of string 2 to null
	When I pass string 1 and string 2 into the method
	Then the value returned from the method should be 'abc'


# and what if both input parameters are null
Scenario: Method should return an empty string if both string 1 and string 2 are null
	Given I set the value of string 1 to null
		And I set the value of string 2 to null
	When I pass string 1 and string 2 into the method
	Then the value returned from the method should be ''


# after null, our method restricts the data type to strings so we don't necessarily have to test other "bad data" types


# the next incremental string type might then be an empty string for the 1st parameter
Scenario: Method should return the value of string 2 if string 1 is empty
	Given I set the value of string 1 to ''
		And I set the value of string 2 to '123'
	When I pass string 1 and string 2 into the method
	Then the value returned from the method should be '123'


# and also empty string for the 2nd parameter
Scenario: Method should return the value of string 1 if string 2 is empty
	Given I set the value of string 1 to 'abc'
		And I set the value of string 2 to ''
	When I pass string 1 and string 2 into the method
	Then the value returned from the method should be 'abc'


# and what if both input parameters are empty strings
Scenario: Method should return an empty string if both string 1 and string 2 are empty
	Given I set the value of string 1 to ''
		And I set the value of string 2 to ''
	When I pass string 1 and string 2 into the method
	Then the value returned from the method should be ''


# we were given a specific example so we should explicitly include that "happy path" data point in our tests
Scenario: Method should return an interleaved string
	Given I set the value of string 1 to 'abc'
		And I set the value of string 2 to '123'
	When I pass string 1 and string 2 into the method
	Then the value returned from the method should be 'a1b2c3'


# we also need to consider what happens when we combine strings of different lengths, so what if string 1 is shorter than string 2
Scenario: Method should return an interleaved string even if string 1 is shorter than string 2
	Given I set the value of string 1 to 'a'
		And I set the value of string 2 to '123'
	When I pass string 1 and string 2 into the method
	Then the value returned from the method should be 'a123'


# and what if string 1 is longer than string 2
Scenario: Method should return an interleaved string even if string 1 is longer than string 2
	Given I set the value of string 1 to 'abc'
		And I set the value of string 2 to '1'
	When I pass string 1 and string 2 into the method
	Then the value returned from the method should be 'a1bc'


# we may want to verify that our input values are also not being modified in an unexpected way, so we can also check the case of string 1
Scenario: Method should return an interleaved string and keep case sensitivity for string 1
	Given I set the value of string 1 to 'ABC'
		And I set the value of string 2 to 'xyz'
	When I pass string 1 and string 2 into the method
	Then the value returned from the method should be 'AxByCz'


# and also check the case of string 2
Scenario: Method should return an interleaved string and keep case sensitivity for string 2
	Given I set the value of string 1 to 'abc'
		And I set the value of string 2 to 'XYZ'
	When I pass string 1 and string 2 into the method
	Then the value returned from the method should be 'aXbYcZ'


# and although it is very unlikely that the combination of input parameters are being modified if independently they are not,
# we can also test the case sensitivty of both parameters at the same time for the sake of completion
Scenario: Method should return an interleaved string and keep case sensitivity for both string 1 and string 2
	Given I set the value of string 1 to 'aBc'
		And I set the value of string 2 to 'XyZ'
	When I pass string 1 and string 2 into the method
	Then the value returned from the method should be 'aXBycZ'


# we also want to make sure that our method is able to handle special characters appropriately for string 1
Scenario: Method should return an interleaved string with special characters for string 1
	Given I set the value of string 1 to '`~!?@#$%^&*+-=_()[]{}|\/<>;:,.'"'
		And I set the value of string 2 to '12345678901234567890123456789012'
	When I pass string 1 and string 2 into the method
	Then the value returned from the method should be '`1~2!3?4@5#6$7%8^9&0*1+2-3=4_5(6)7[8]9{0}1|2\3/4<5>6;7:8,9.0'1"2'


# and also for string 2
Scenario: Method should return an interleaved string with special characters for string 2
	Given I set the value of string 1 to 'abcdefghijklmnopqrstuvwxyzABCDEF'
		And I set the value of string 2 to '`~!?@#$%^&*+-=_()[]{}|\/<>;:,.'"'
	When I pass string 1 and string 2 into the method
	Then the value returned from the method should be 'a`b~c!d?e@f#g$h%i^j&k*l+m-n=o_p(q)r[s]t{u}v|w\x/y<z>A;B:C,D.E'F"'


# and again it is very unlikely that the combination of special character input would not work if they are both working independently,
# but we can still include the test case of both parameters at the same time for the sake of completion
Scenario: Method should return an interleaved string with special characters for both string 1 and string 2
	Given I set the value of string 1 to '`~!?@#$%^&*+-=_()[]{}|\/<>;:,.'"'
		And I set the value of string 2 to '`~!?@#$%^&*+-=_()[]{}|\/<>;:,.'"'
	When I pass string 1 and string 2 into the method
	Then the value returned from the method should be '``~~!!??@@##$$%%^^&&**++--==__(())[[]]{{}}||\\//<<>>;;::,,..''""'


# as far as stress testing for "max data" scenarios, we may consider testing large String values and see how the system handles it;
# for instance, are there any performance concerns with our current method and at what threshold does that occur?
# if so, is there a more effiecient way our method could be written to improve performance and to be more scalable?
# however, that's also usually outside the scope of "unit testing" but I still wanted to at least touch upon the principle idea