using MarsFramework.Global;
using NUnit.Framework.Interfaces;
using RelevantCodes.ExtentReports;
using System;
using TechTalk.SpecFlow;

namespace MarsFramework.HookUp
{
    [Binding]
    public class ProfileSteps
    {
        ProfilePage profile;
        [Given(@"I clicked on the profile tab under Profile page")]
        public void GivenIClickedOnTheProfileTabUnderProfilePage()
        {       
            profile = new ProfilePage();
            profile.NavigateToProfileTab();
        }
        
        [When(@"I update Availability Type")]
        public void WhenIUpdateAvailabilityType()
        {
            // Start Add test. (Reports)
            Base.test = Base.extent.StartTest("Add Availability Type");

            //Populate the excel data
            GlobalDefinitions.ExcelLib.PopulateInCollection(Base.ExcelPathProfile, "Profile");

            //Call SelectAvailability Method to select an availability Type
            profile.SelectAvailability();
        }
        
        [Then(@"Updated Availability Type should be displayed")]
        public void ThenUpdatedAvailabilityTypeShouldBeDisplayed()
        {
            profile.ValidateAvailabilityType();

         
        }


        [When(@"I update Availability Hours")]
        public void WhenIUpdateAvailabilityHours()
        {
            // Start Add test. (Reports)
            Base.test = Base.extent.StartTest("Add Availability Hours");

            //Populate the excel data
            GlobalDefinitions.ExcelLib.PopulateInCollection(Base.ExcelPathProfile, "Profile");

            //Call SelectHours Method to select Hours
            profile.SelectHours();
        }

        [Then(@"Updated Availability Hours should be displayed")]
        public void ThenUpdatedAvailabilityHoursShouldBeDisplayed()
        {
            profile.ValidateAvailabilityHours();
        }

        [When(@"I update Availability Earn Target")]
        public void WhenIUpdateAvailabilityEarnTarget()
        {
            // Start Add test. (Reports)
            Base.test = Base.extent.StartTest("Add Availability Target");

            //Populate the excel data
            GlobalDefinitions.ExcelLib.PopulateInCollection(Base.ExcelPathProfile, "Profile");

            profile.SelectEarnTarget();
        }

        [Then(@"Updated Availability Earn Target should be displayed")]
        public void ThenUpdatedAvailabilityEarnTargetShouldBeDisplayed()
        {
            profile.ValidateAvailabilityTarget();
        }


        [When(@"I update Profile Description")]
        public void WhenIUpdateProfileDescription()
        {
            // Start Add test. (Reports)
            Base.test = Base.extent.StartTest("Add Description");

            //Populate the excel data
            GlobalDefinitions.ExcelLib.PopulateInCollection(Base.ExcelPathProfile, "Profile");

            profile.AddDescription();
        }

        [Then(@"Updated Description should be displayed")]
        public void ThenUpdatedDescriptionShouldBeDisplayed()
        {
            profile.ValidateDescription();

        }

        [When(@"I Change the Password to login")]
        public void WhenIChangeThePasswordToLogin()
        {
            // Start Add test. (Reports)
            Base.test = Base.extent.StartTest("Change Password");

            //Populate the excel data
            GlobalDefinitions.ExcelLib.PopulateInCollection(Base.ExcelPathShareSkill, "SignIn");

            profile.ChangePassword();
        }

        [Then(@"I should be able to change password and login with new password")]
        public void ThenIShouldBeAbleToChangePasswordAndLoginWithNewPassword()
        {
            profile.ValidateChangedPassword();
            profile.ResettingPassword();
        }

    }
}
