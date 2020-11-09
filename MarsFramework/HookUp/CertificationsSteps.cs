using MarsFramework.Global;
using MarsFramework.Pages;
using System;
using TechTalk.SpecFlow;

namespace MarsFramework.HookUp
{
    [Binding]
    public class CertificationsSteps
    {
        CertificationsPage certification;
        [Given(@"I clicked on the Certification tab")]
        public void GivenIClickedOnTheCertificationTab()
        {
            certification = new CertificationsPage();
            certification.NavigateToCertificationTab();
        }
        
        [Given(@"I have an existing Certification")]
        public void GivenIHaveAnExistingCertification()
        {
            certification.CheckCertificationExists();
        }
        
        [When(@"I add a new Certification")]
        public void WhenIAddANewCertification()
        {
            // Start Add test. (Reports)
            Base.test = Base.extent.StartTest("Add Certification");

            //Populate the excel data
            GlobalDefinitions.ExcelLib.PopulateInCollection(Base.ExcelPathProfile, "Certification");

            //Call AddNewCertification Method to add a New Certification
            certification = new CertificationsPage();
            certification.AddNewCertification();
        }
        
        [When(@"I update Certification")]
        public void WhenIUpdateCertification()
        {
            // Start Add test. (Reports)
            Base.test = Base.extent.StartTest("Update Certification");

            //Populate the excel data
            GlobalDefinitions.ExcelLib.PopulateInCollection(Base.ExcelPathProfile, "Certification");

            certification.UpdateCertification();
        }

        [When(@"I delete Certification")]
        public void WhenIDeleteCertification()
        {
            // Start Add test. (Reports)
            Base.test = Base.extent.StartTest("Delete Certification");

            //Populate the excel data
            GlobalDefinitions.ExcelLib.PopulateInCollection(Base.ExcelPathProfile, "Certification");

            certification.DeleteCertification();
        }

        [Then(@"that Certification should be displayed on my listings")]
        public void ThenThatCertificationShouldBeDisplayedOnMyListings()
        {
            certification.ValidateAddedCertification();
        }
        
        [Then(@"that updated Certification should be displayed on my listings")]
        public void ThenThatUpdatedCertificationShouldBeDisplayedOnMyListings()
        {
            certification.ValidateUpdateCertification();
        }
        
        [Then(@"that deleted Certification should not be displayed on my listings")]
        public void ThenThatDeletedCertificationShouldNotBeDisplayedOnMyListings()
        {
            certification.ValidateDeleteCertification();
        }
    }
}
