using MarsFramework.Global;
using MarsFramework.Pages;
using System;
using TechTalk.SpecFlow;

namespace MarsFramework.HookUp
{
    [Binding]
    public class ShareSkillSteps
    {
        ShareSkillPage shareSkill;
        [Given(@"I click on Share Skill tab")]
        public void GivenIClickOnShareSkillTab()
        {
            shareSkill = new ShareSkillPage();
            shareSkill.ClickShareSkillButton();
        }
        
        [When(@"I Add Share Skill")]
        public void WhenIAddShareSkill()
        {
            // Start Add test. (Reports)
            Base.test = Base.extent.StartTest("Add Share Skill");

            //Populate the excel data
            GlobalDefinitions.ExcelLib.PopulateInCollection(Base.ExcelPathShareSkill, "ShareSkill");

            shareSkill.EnterShareSkillData();
            shareSkill.ValidateShareSkillData();
            shareSkill.SaveShareSkill();

            
        }
        
        [Then(@"that added Share Skill should be displayed on managelisting page")]
        public void ThenThatAddedShareSkillShouldBeDisplayedOnManagelistingPage()
        {
            ManageListingsPage manageListings = new ManageListingsPage();
            bool isRecordFound=manageListings.SearchListings(GlobalDefinitions.ExcelLib.ReadData(2, "Category"), GlobalDefinitions.ExcelLib.ReadData(2, "Title"), GlobalDefinitions.ExcelLib.ReadData(2, "Description"));
            GlobalDefinitions.ValidateBoolean(isRecordFound, "Share Skill " + GlobalDefinitions.ExcelLib.ReadData(2, "Title") + " is added");
        }
    }
}
