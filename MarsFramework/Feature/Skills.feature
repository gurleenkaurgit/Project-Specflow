Feature: Skills
	As a Skill Trader
      I want to be able to Add, Update and Delete Skills 
      In order to update my profile details

	  Background: 
	  Given I clicked on the profile tab under Profile page
	  
	  
@automation
Scenario: Add Skills
    Given I clicked on the Skills tab
	When I add a new Skill
	Then that Skill should be displayed on my listings

Scenario: Update Skill
	Given I clicked on the Skills tab
	And I have an existing Skill
	When I update Skill 
	Then that updated Skill should be displayed on my listings

Scenario: Delete Skill
	Given I clicked on the Skills tab
	And I have an existing Skill
	When I delete Skill 
	Then that deleted Skill should not be displayed on my listings