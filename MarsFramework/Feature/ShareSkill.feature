Feature: ShareSkill
	As a Skill Trader
      I want to be able to Add Share Skill 
      In order to list my Skills on Manage Listing page

@mytag
Scenario: Add Share Skill
	Given I click on Share Skill tab
	When I Add Share Skill
	Then that added Share Skill should be displayed on managelisting page