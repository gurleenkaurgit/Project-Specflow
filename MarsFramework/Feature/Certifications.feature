Feature: Certifications
	As a Skill Trader
      I want to be able to Add, Update and Delete Certification 
      In order to update my profile details

	  Background: 
	  Given I clicked on the profile tab under Profile page
	  And I clicked on the Certification tab

@automation
Scenario: Add Certification
	When I add a new Certification
	Then that Certification should be displayed on my listings

Scenario: Update Certification
	Given I have an existing Certification
	When I update Certification 
	Then that updated Certification should be displayed on my listings

Scenario: Delete Certification
    Given I have an existing Certification
	When I delete Certification 
	Then that deleted Certification should not be displayed on my listings