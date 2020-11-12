Feature: Skills
	As a Skill Trader
      I want to be able to Add, Update and Delete Skills 
      In order to update my profile details

	  Background: 
	  Given I clicked on the profile tab under Profile page
	  And I clicked on the Skills tab
	  
@automation
Scenario: Add Skills
	When I add a new Skill
	Then that Skill should be displayed on my listings

Scenario: Update Skill
	Given I have an existing Skill
	When I update Skill 
	Then that updated Skill should be displayed on my listings

Scenario: Delete Skill
	Given I have an existing Skill
	When I delete Skill 
	Then that deleted Skill should not be displayed on my listings