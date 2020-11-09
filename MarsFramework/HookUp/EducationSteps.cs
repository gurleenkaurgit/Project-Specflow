using MarsFramework.Global;
using MarsFramework.Pages;
using System;
using TechTalk.SpecFlow;

namespace MarsFramework.HookUp
{
    [Binding]
    public class EducationSteps
    {
        EducationPage education;
        [Given(@"I clicked on the Education tab")]
        public void GivenIClickedOnTheEducationTab()
        {
            education = new EducationPage();
            education.NavigateToEducationTab();
        }
        
        [Given(@"I have an existing Education")]
        public void GivenIHaveAnExistingEducation()
        {
            education.CheckEducationExists();
        }
        
        [When(@"I add a new Education")]
        public void WhenIAddANewEducation()
        {
            // Start Add test. (Reports)
            Base.test = Base.extent.StartTest("Add Education");

            //Populate the excel data
            GlobalDefinitions.ExcelLib.PopulateInCollection(Base.ExcelPathProfile, "Education");

            //Call AddNewSkill Method to add a New Skill
            education = new EducationPage();
            education.AddNewEducation();
        }
        
        [When(@"I update Education")]
        public void WhenIUpdateEducation()
        {
            // Start Add test. (Reports)
            Base.test = Base.extent.StartTest("Update Education");

            //Populate the excel data
            GlobalDefinitions.ExcelLib.PopulateInCollection(Base.ExcelPathProfile, "Education");

            education.UpdateEducation();
        }
        
        [When(@"I delete Education")]
        public void WhenIDeleteEducation()
        {
            // Start Add test. (Reports)
            Base.test = Base.extent.StartTest("Delete Education");

            //Populate the excel data
            GlobalDefinitions.ExcelLib.PopulateInCollection(Base.ExcelPathProfile, "Education");

            education.DeleteEducation();
        }
        
        [Then(@"that Education should be displayed on my listings")]
        public void ThenThatEducationShouldBeDisplayedOnMyListings()
        {
            education.ValidateAddedEducation();
        }
        
        [Then(@"that updated Education should be displayed on my listings")]
        public void ThenThatUpdatedEducationShouldBeDisplayedOnMyListings()
        {
            education.ValidateUpdateEducation();
        }
        
        [Then(@"that deleted Education should not be displayed on my listings")]
        public void ThenThatDeletedEducationShouldNotBeDisplayedOnMyListings()
        {
            education.ValidateDeleteEducation();
        }
    }
}
