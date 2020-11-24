Feature: SearchSkills
	As a Skill Trader
      I want to be able to Search Skills 
      In order to perform actions on Skills

	    Background: 
	  Given I clicked on the Manage Listings tab
	  And I have an existing Share Skill

@mytag
Scenario: Search Shared Skill By All Categories
	When I Searched for an existing Share Skill
	Then the result for searched Skill is displayed

Scenario: Search Shared Skill By SubCategory
	When I Searched for an existing Share Skill by SubCategory
	Then the result for searched Skill by SubCategory is displayed