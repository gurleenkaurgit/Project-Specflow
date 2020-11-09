Feature: Language
	As a Skill Trader
      I want to be able to Add, Update and Delete Language 
      In order to update my profile details

	  Background: 
	  Given I clicked on the profile tab under Profile page
	  And I clicked on the Language tab

@automation
Scenario: Add language
	When I add a new language
	Then that language should be displayed on my listings

Scenario: Update language
	Given I have an existing Langugae
	When I update Language 
	Then that updated language should be displayed on my listings

Scenario: Delete language
    Given I have an existing Langugae
	When I delete Language 
	Then that deleted language should not be displayed on my listings