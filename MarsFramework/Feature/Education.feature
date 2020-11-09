Feature: Education
	As a Skill Trader
      I want to be able to Add, Update and Delete Education 
      In order to update my profile details

	  Background: 
	  Given I clicked on the profile tab under Profile page
	  And I clicked on the Education tab

@automation
Scenario: Add Education
	When I add a new Education
	Then that Education should be displayed on my listings

Scenario: Update Education
	Given I have an existing Education
	When I update Education 
	Then that updated Education should be displayed on my listings

Scenario: Delete Education
    Given I have an existing Education
	When I delete Education 
	Then that deleted Education should not be displayed on my listings