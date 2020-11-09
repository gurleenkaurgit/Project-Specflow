using MarsFramework.Global;
using MarsFramework.Pages;
using NUnit.Framework.Interfaces;
using RelevantCodes.ExtentReports;
using System;
using TechTalk.SpecFlow;

namespace MarsFramework.HookUp
{
    [Binding]
    public class LanguageSteps
    {
        LanguagePage language;
        [Given(@"I clicked on the Language tab")]
        public void GivenIClickedOnTheLanguageTab()
        {
            language = new LanguagePage();
            language.NavigateToLanguageTab();

        }

        [Given(@"I have an existing Langugae")]
        public void GivenIHaveAnExistingLangugae()
        {
            language.CheckLanguageExists();
        }


        [When(@"I add a new language")]
        public void WhenIAddANewLanguage()
        {
            
            // Start Add test. (Reports)
            Base.test = Base.extent.StartTest("Add Language");

            //Populate the excel data
            GlobalDefinitions.ExcelLib.PopulateInCollection(Base.ExcelPathProfile, "Language");

            //Call AddNewLanguage Method to add a New Language
            language = new LanguagePage();
            language.AddNewLanguage();
        }
        
        [When(@"I update Language")]
        public void WhenIUpdateLanguage()
        {
            // Start Add test. (Reports)
            Base.test = Base.extent.StartTest("Update Language");

            //Populate the excel data
            GlobalDefinitions.ExcelLib.PopulateInCollection(Base.ExcelPathProfile, "Language");

            //Call UpdateLanguage Method to Update an existing Language
            language.UpdateLanguage();

        }
        
        [When(@"I delete Language")]
        public void WhenIDeleteLanguage()
        {
            // Start Add test. (Reports)
            Base.test = Base.extent.StartTest("Delete Language");

            //Populate the excel data
            GlobalDefinitions.ExcelLib.PopulateInCollection(Base.ExcelPathProfile, "Language");

            //Call DeleteLanguage Method to Delete an existing Language
            language.DeleteLanguage();
        }
        
        [Then(@"that language should be displayed on my listings")]
        public void ThenThatLanguageShouldBeDisplayedOnMyListings()
        {
            language.ValidateAddedLanguage();

           
        }
        
        [Then(@"that updated language should be displayed on my listings")]
        public void ThenThatUpdatedLanguageShouldBeDisplayedOnMyListings()
        {
            language.ValidateUpdateLanguage();

            
        }
        
        [Then(@"that deleted language should not be displayed on my listings")]
        public void ThenThatDeletedLanguageShouldNotBeDisplayedOnMyListings()
        {
            language.ValidateDeleteLanguage();

            
        }
    }
}
