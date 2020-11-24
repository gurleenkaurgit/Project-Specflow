using MarsFramework.Global;
using MarsFramework.Pages;
using System;
using TechTalk.SpecFlow;

namespace MarsFramework.HookUp
{
    [Binding]
    public class SkillsSteps
    {
        SkillsPage skills;
        [Given(@"I clicked on the Skills tab")]
        public void GivenIClickedOnTheSkillsTab()
        {
            skills = new SkillsPage();

            skills.NavigateToSkillTab();

            
        }
        
        [Given(@"I have an existing Skill")]
        public void GivenIHaveAnExistingSkill()
        {           
            skills.CheckSkillExists();
        }
        
        [When(@"I add a new Skill")]
        public void WhenIAddANewSkill()
        {
            // Start Add test. (Reports)
            Base.test = Base.extent.StartTest("Add Skill");

            //Populate the excel data
            GlobalDefinitions.ExcelLib.PopulateInCollection(Base.ExcelPathProfile, "Skill");

            //Call AddNewSkill Method to add a New Skill
            skills = new SkillsPage();
            skills.AddNewSkill();
        }
        
        [When(@"I update Skill")]
        public void WhenIUpdateSkill()
        {
            // Start Add test. (Reports)
            Base.test = Base.extent.StartTest("Update Skill");

            //Populate the excel data
            GlobalDefinitions.ExcelLib.PopulateInCollection(Base.ExcelPathProfile, "Skill");

            //Call UpdateSkill Method to Update an existing Skill
            skills.UpdateSkill();
        }
        
        [When(@"I delete Skill")]
        public void WhenIDeleteSkill()
        {
            // Start Add test. (Reports)
            Base.test = Base.extent.StartTest("Delete Skill");

            //Populate the excel data
            GlobalDefinitions.ExcelLib.PopulateInCollection(Base.ExcelPathProfile, "Skill");

            //Call DeleteSkill Method to Delete an existing Skill
            skills.DeleteSkill();
        }
        
        [Then(@"that Skill should be displayed on my listings")]
        public void ThenThatSkillShouldBeDisplayedOnMyListings()
        {
            skills.ValidateAddedSkill();
        }
        
        [Then(@"that updated Skill should be displayed on my listings")]
        public void ThenThatUpdatedSkillShouldBeDisplayedOnMyListings()
        {
            skills.ValidateUpdateSkill();
        }
        
        [Then(@"that deleted Skill should not be displayed on my listings")]
        public void ThenThatDeletedSkillShouldNotBeDisplayedOnMyListings()
        {
            skills.ValidateDeleteSkill();
        }
    }
}
