using MarsFramework.Global;
using MarsFramework.Pages;
using System;
using TechTalk.SpecFlow;

namespace MarsFramework.HookUp
{
    [Binding]
    public class ManageListingsSteps
    {
        ManageListingsPage manageListings;
        

        [Given(@"I clicked on the Manage Listings tab")]
        public void GivenIClickedOnTheManageListingsTab()
        {
            manageListings = new ManageListingsPage();
            manageListings.NavigateToManageListing();
        }
        
        [Given(@"I have an existing Share Skill")]
        public void GivenIHaveAnExistingShareSkill()
        {
            //Populate the excel data
            GlobalDefinitions.ExcelLib.PopulateInCollection(Base.ExcelPathShareSkill, "ShareSkill");

            manageListings.CheckShareSkillExists();
        }
        
        [When(@"I update Share Skill")]
        public void WhenIUpdateShareSkill()
        {
            // Start Add test. (Reports)
            Base.test = Base.extent.StartTest("Edit Share Skill");

            //Populate the excel data
            GlobalDefinitions.ExcelLib.PopulateInCollection(Base.ExcelPathShareSkill, "ShareSkill");

            manageListings.EditShareSkill();

            //Validate message
            //Extension.MessageValidation("Service Listing Updated Successfully");


        }

        [When(@"I Delete Share Skill")]
        public void WhenIDeleteShareSkill()
        {
            // Start Add test. (Reports)
            Base.test = Base.extent.StartTest("Delete Share Skill");

            //Populate the excel data
            GlobalDefinitions.ExcelLib.PopulateInCollection(Base.ExcelPathManageListing, "ManageListings");

            //Get the Category, Title, Description and Action for Deletion
            string CategoryToDelete = GlobalDefinitions.ExcelLib.ReadData(2, "Category");
            string TitleToDelete = GlobalDefinitions.ExcelLib.ReadData(2, "Title");
            string DescriptionToDelete = GlobalDefinitions.ExcelLib.ReadData(2, "Description");
            string Action = GlobalDefinitions.ExcelLib.ReadData(2, "Deleteaction");

            manageListings.DeleteShareSkill(CategoryToDelete, TitleToDelete, DescriptionToDelete,Action);

            //Validate message
            Extension.MessageValidation(TitleToDelete+" has been deleted");

        }

        [Then(@"that updated Share Skill should be displayed on managelisting page")]
        public void ThenThatUpdatedShareSkillShouldBeDisplayedOnManagelistingPage()
        {
            bool isRecordFound = manageListings.SearchListings(GlobalDefinitions.ExcelLib.ReadData(2, "Category"), GlobalDefinitions.ExcelLib.ReadData(2, "Title"), GlobalDefinitions.ExcelLib.ReadData(2, "Description"));
            GlobalDefinitions.ValidateBoolean(isRecordFound, "Updated Share Skill Exists in Listing-");
        }
        
        [Then(@"that Deleted Share Skill should not be displayed on managelisting page")]
        public void ThenThatDeletedShareSkillShouldNotBeDisplayedOnManagelistingPage()
        {

            bool isRecordFound = manageListings.SearchListings(GlobalDefinitions.ExcelLib.ReadData(2, "Category"), GlobalDefinitions.ExcelLib.ReadData(2, "Title"), GlobalDefinitions.ExcelLib.ReadData(2, "Description"));
            GlobalDefinitions.ValidateBoolean(!isRecordFound, "Deleted Share Skill Removes from Listing-");
        }
    }
}
