using MarsFramework.Global;
using MarsFramework.HookUp;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using RelevantCodes.ExtentReports;
using SeleniumExtras.PageObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static MarsFramework.Global.GlobalDefinitions;

namespace MarsFramework.Pages
{
    internal class CertificationsPage
    {
        //Click on Certifications Tab
        [FindsBy(How = How.LinkText, Using = "Certifications")]
        private IWebElement CertificationTab { get; set; }

        //Click on Add New Certification Button
        [FindsBy(How = How.XPath, Using = "//h3[contains(text(),'Certification')]/../..//div[text()='Add New']")]
        private IWebElement AddNewCertificationButton { get; set; }

        //Add the Certification Name
        [FindsBy(How = How.Name, Using = "certificationName")]
        private IWebElement AddCertificationName { get; set; }

        //Add Certified From 
        [FindsBy(How = How.Name, Using = "certificationFrom")]
        private IWebElement AddCertificationFrom { get; set; }

        //Choose Certification Year
        [FindsBy(How = How.Name, Using = "certificationYear")]
        private IWebElement ChooseCertificationYear { get; set; }

        //Click Add Certification Button
        [FindsBy(How = How.XPath, Using = "//input[@value='Add']")]
        private IWebElement AddCertificationButton { get; set; }

        //Click Update Certification Button
        [FindsBy(How = How.XPath, Using = "//input[@value='Update']")]
        private IWebElement UpdateCertificationButton { get; set; }

        private static bool isCertificationFound;

        public CertificationsPage()
        {
            PageFactory.InitElements(Driver, this);
        }

        //Navigate to Certification tab
        internal void NavigateToCertificationTab()
        {
            Extension.WaitForElementDisplayed(Driver, By.LinkText("Certifications"), 2);
            Actions action = new Actions(Driver);
            action.MoveToElement(CertificationTab).Click(CertificationTab).Build().Perform();
           
        }

        internal void CheckCertificationExists()
        {
            //Populate the excel data
            GlobalDefinitions.ExcelLib.PopulateInCollection(Base.ExcelPathProfile, "Certification");

            SearchCertification(ExcelLib.ReadData(2, "CertificationName"), ExcelLib.ReadData(2, "CertificationFrom"));

            if (!isCertificationFound)
            {
                CertificationsSteps certificationSteps = new CertificationsSteps();
                certificationSteps.WhenIAddANewCertification();
            }
        }

        //Add new certifiaction
        internal void AddNewCertification()
        {
            Extension.WaitForElementDisplayed(Driver, By.XPath("//h3[contains(text(),'Certification')]/../..//div[text()='Add New']"), 10);

            //Click Add New button 
            AddNewCertificationButton.Click();

            //Enter the Certification Details
            Extension.WaitForElementDisplayed(Driver, By.Name("certificationName"), 2);
            AddCertificationName.SendKeys(ExcelLib.ReadData(2, "CertificationName"));
            AddCertificationFrom.SendKeys(ExcelLib.ReadData(2, "CertificationFrom"));
            SelectDropDown(ChooseCertificationYear, "SelectByValue", ExcelLib.ReadData(2, "CertificationYear"));

            //Click Add button
            AddCertificationButton.Click();

            Base.Image = SaveScreenShotClass.SaveScreenshot(Driver, "Report");

            //Validate message
            Extension.MessageValidation(ExcelLib.ReadData(2, "CertificationName") + " has been added to your certification");

        }

        //Update the existing Certification
        internal void UpdateCertification()
        {

            //Get the Certification value needs to be updated
            String expectedValue = ExcelLib.ReadData(2, "CertificationName");

            //Get the rows count in Certification table
            IList<IWebElement> Tablerows = Driver.FindElements(By.XPath("//h3[contains(text(),'Certification')]/../..//table/tbody/tr"));
            var rowCount = Tablerows.Count;

            //Get the actual Certification value and compare with Certification needs to be updated, if matches update the record
            for (int i = 1; i <= rowCount; i++)
            {
                String actualValue = Driver.FindElement(By.XPath("//h3[contains(text(),'Certification')]/../..//tbody[" + i + "]/tr/td[1]")).Text;
                if (expectedValue == actualValue)
                {
                    //Click on Edit icon
                    Driver.FindElement(By.XPath("//h3[contains(text(),'Certification')]/../..//tbody[" + i + "]/tr/td[4]/span[1]/i")).Click();

                    //Clear the existing value and add new value 
                    Extension.WaitForElementDisplayed(Driver, By.Name("certificationName"), 2);
                    AddCertificationName.Clear();
                    AddCertificationName.SendKeys(ExcelLib.ReadData(2, "UpdateCertificationName"));
                    AddCertificationFrom.Clear();
                    AddCertificationFrom.SendKeys(ExcelLib.ReadData(2, "UpdateCertificationFrom"));
                    SelectDropDown(ChooseCertificationYear, "SelectByValue", ExcelLib.ReadData(2, "UpdateCertificationYear"));

                    //Click update button
                    UpdateCertificationButton.Click();

                    Base.Image = SaveScreenShotClass.SaveScreenshot(Driver, "Report");

                    break;
                }

            }
            //Validate message
            Extension.MessageValidation(ExcelLib.ReadData(2, "UpdateCertificationName") + " has been updated to your certification");

        }

        //Delete the existing certification
        internal void DeleteCertification()
        {

            //Get the Certification needs to be Deleted
            String expectedValue = ExcelLib.ReadData(2, "CertificationName");

            //Get the rows count in Certification table
            IList<IWebElement> Tablerows = Driver.FindElements(By.XPath("//h3[contains(text(),'Certification')]/../..//table/tbody/tr"));
            var rowCount = Tablerows.Count;

            //Get the actual Certification value and compare with Certification needs to be updated, if matches delete the record
            for (int i = 1; i <= rowCount; i++)
            {
                String actualValue = Driver.FindElement(By.XPath("//h3[contains(text(),'Certification')]/../..//tbody[" + i + "]/tr/td[1]")).Text;
                if (expectedValue == actualValue)
                {
                    //CliCk on Delete icon
                    Driver.FindElement(By.XPath("//h3[contains(text(),'Certification')]/../..//tbody[" + i + "]/tr/td[4]/span[2]/i")).Click();

                    Base.Image = SaveScreenShotClass.SaveScreenshot(Driver, "Report");

                    break;
                }
            }
            //Validate message
            Extension.MessageValidation(ExcelLib.ReadData(2, "CertificationName") + " has been deleted from your certification");

        }

        //Search the Certification in the listing
        internal void SearchCertification(String expectedCertificationName, String expectedCertificationFrom)
        {
            //Setting the isCertificationFound variable to false
            isCertificationFound = false;

            //Get all the Certification records
            IList<IWebElement> CertificationRecords = Driver.FindElements(By.XPath("//h3[contains(text(),'Certification')]/../..//table/tbody/tr"));

            //if the expected and actual Certification matches, set the isCertificationFound to true
            for (int j = 1; j <= CertificationRecords.Count; j++)
            {
                String actualCertificationName = Driver.FindElement(By.XPath("//h3[contains(text(),'Certification')]/../..//table/tbody[" + j + "]/tr/td[1]")).Text;
                String actualCertificationFrom = Driver.FindElement(By.XPath("//h3[contains(text(),'Certification')]/../..//table/tbody[" + j + "]/tr/td[2]")).Text;
                if (expectedCertificationName == actualCertificationName && expectedCertificationFrom == actualCertificationFrom)
                {
                   
                    isCertificationFound = true;
                    break;
                }
            }

        }

        //Validate the Certification added is dispalyed in the listing
        internal void ValidateAddedCertification()
        {
            SearchCertification(ExcelLib.ReadData(2, "CertificationName"), ExcelLib.ReadData(2, "CertificationFrom"));

            GlobalDefinitions.ValidateBoolean(isCertificationFound, "Added Certification Exists in Listing-");
           
        }

        //Validate the Certification Updated is dispalyed in the listing
        internal void ValidateUpdateCertification()
        {
            SearchCertification(ExcelLib.ReadData(2, "UpdateCertificationName"), ExcelLib.ReadData(2, "UpdateCertificationFrom"));

            GlobalDefinitions.ValidateBoolean(isCertificationFound, "Updated Certification Exists in Listing-");

        }

        //Validate the Certification Deleted is not dispalyed in the listing
        internal void ValidateDeleteCertification()
        {
            SearchCertification(ExcelLib.ReadData(2, "CertificationName"), ExcelLib.ReadData(2, "CertificationFrom"));

            GlobalDefinitions.ValidateBoolean(!(isCertificationFound), "Deleted Certification Removes from Listing-");
            
        }


    }
}




