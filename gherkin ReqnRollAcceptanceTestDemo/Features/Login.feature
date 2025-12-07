Feature: Login

Validate login to the-internet.herokuapp.com

@webtest
Scenario: Successful login
	Given I navigate to "https://the-internet.herokuapp.com/login"
	When I login with username "tomsmith" and password "SuperSecretPassword!"
	Then I should see "You logged into a secure area!"