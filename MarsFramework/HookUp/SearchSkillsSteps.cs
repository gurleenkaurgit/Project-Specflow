using MarsFramework.Global;
using MarsFramework.Pages;
using System;
using TechTalk.SpecFlow;

namespace MarsFramework.HookUp
{
    [Binding]
    public class SearchSkillsSteps
    {
        SearchSkillsPage searchSkills;
        [When(@"I Searched for an existing Share Skill")]
        public void WhenISearchedForAnExistingShareSkill()
        {
            searchSkills = new SearchSkillsPage();
            // Start Add test. (Reports)
            Base.test = Base.extent.StartTest("Search Share Skill");

            //Populate the excel data
            GlobalDefinitions.ExcelLib.PopulateInCollection(Base.ExcelPathShareSkill, "ShareSkill");

            searchSkills.EnterSkillIntoSearchBox();

                    }

        [When(@"I Searched for an existing Share Skill by SubCategory")]
        public void WhenISearchedForAnExistingShareSkillBySubCategory()
        {
            searchSkills = new SearchSkillsPage();
            // Start Add test. (Reports)
            Base.test = Base.extent.StartTest("Search Share Skill By SubCategory");

            //Populate the excel data
            GlobalDefinitions.ExcelLib.PopulateInCollection(Base.ExcelPathShareSkill, "ShareSkill");

            searchSkills.EnterSkillIntoSearchBox();
            searchSkills.ClickOnSubCategory();
        }
        
        [Then(@"the result for searched Skill is displayed")]
        public void ThenTheResultForSearchedSkillIsDisplayed()
        {
            searchSkills.ValidateEnteredSkillByAllCategories();
        }
        
        [Then(@"the result for searched Skill by SubCategory is displayed")]
        public void ThenTheResultForSearchedSkillBySubCategoryIsDisplayed()
        {
            searchSkills.ValidateEnteredSkillBySubCategory();
        }
    }
}
