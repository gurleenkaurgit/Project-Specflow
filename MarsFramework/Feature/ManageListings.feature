Feature: ManageListings
	As a Skill Trader
      I want to be able to Edit and Delete the Skills shared 
      In order to update and delete skills listed on manage listing page

	  Background: 
	  Given I clicked on the Manage Listings tab
	  And I have an existing Share Skill
	 

@automation
Scenario: Update Skill Listed
	When I update Share Skill 
	Then that updated Share Skill should be displayed on managelisting page

@automation
Scenario: Delete Skill Listed
	When I Delete Share Skill 
	Then that Deleted Share Skill should not be displayed on managelisting page