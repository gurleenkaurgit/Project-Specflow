Feature: Profile
	As a Skill Trader
      I want to be able to Update Availability(Type, Hours and Earn Target), Description and Change Password 
      In order to update my profile details

	  Background: 
	  Given I clicked on the profile tab under Profile page

@automation
Scenario: Edit Availability Type
	When I update Availability Type
	Then Updated Availability Type should be displayed 

Scenario: Edit Availability Hours
	When I update Availability Hours
	Then Updated Availability Hours should be displayed 

Scenario: Edit Availability Earn Target
	When I update Availability Earn Target
	Then Updated Availability Earn Target should be displayed 

Scenario: Edit Profile Description
	When I update Profile Description
	Then Updated Description should be displayed 

Scenario: Change Password
	When I Change the Password to login
	Then I should be able to change password and login with new password


