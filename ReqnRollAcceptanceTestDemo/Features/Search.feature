Feature: feature

#Background Step
#Given I navigate to the GLA website

@tag1
Scenario Outline: User is able to search with relevant keyword
	Given I am on the GLA website 
	When I click on the search icon
	And I search for the text <Search> in the search box
	Then the search results returned contains <Search>

	Examples: 
	| Search |
	| Budget |