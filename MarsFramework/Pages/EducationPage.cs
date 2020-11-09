using MarsFramework.Global;
using MarsFramework.HookUp;
using NUnit.Framework;
using OpenQA.Selenium;
using RelevantCodes.ExtentReports;
using SeleniumExtras.PageObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static MarsFramework.Global.GlobalDefinitions;

namespace MarsFramework.Pages
{
    internal class EducationPage
    {
        //Click on Education Tab
        [FindsBy(How = How.LinkText, Using = "Education")]
        private IWebElement EducationTab { get; set; }

        //Click on Add New Education Button
        [FindsBy(How = How.XPath, Using = "//h3[contains(text(),'Education')]/../..//div[text()='Add New']")]
        private IWebElement AddNewEducationButton { get; set; }

        //Enter the College/University Name
        [FindsBy(How = How.Name, Using = "instituteName")]
        private IWebElement CollegeUniversityName { get; set; }

        //Select Country of College/University
        [FindsBy(How = How.Name, Using = "country")]
        private IWebElement Country { get; set; }

        //Select the Title
        [FindsBy(How = How.Name, Using = "title")]
        private IWebElement Title { get; set; }

        //Enter the Degree
        [FindsBy(How = How.Name, Using = "degree")]
        private IWebElement Degree { get; set; }

        //Select Year of graduation
        [FindsBy(How = How.Name, Using = "yearOfGraduation")]
        private IWebElement YearOfGraduation { get; set; }

        //Click Add Education Button
        [FindsBy(How = How.XPath, Using = "//input[@value='Add']")]
        private IWebElement AddEducationButton { get; set; }

        //Click Update Education Button
        [FindsBy(How = How.XPath, Using = "//input[@value='Update']")]
        private IWebElement UpdateEducationButton { get; set; }

        private static bool isEducationFound;
        public EducationPage()
        {
            PageFactory.InitElements(Driver, this);
        }

        //Navigate to Education tab
        internal void NavigateToEducationTab()
        {
            Extension.WaitForElementDisplayed(Driver, By.LinkText("Education"), 5);

            EducationTab.Click();
        }

        internal void CheckEducationExists()
        {
            SearchEducation(ExcelLib.ReadData(2, "InstituteName"), ExcelLib.ReadData(2, "Title"));
            

            if (!isEducationFound)
            {
                EducationSteps educationSteps = new EducationSteps();
                educationSteps.WhenIAddANewEducation();
            }
        }


        //Add new Education
        internal void AddNewEducation()
        {
            Extension.WaitForElementDisplayed(Driver, By.XPath("//h3[contains(text(),'Education')]/../..//div[text()='Add New']"), 5);

            //Click Add New Education 
            AddNewEducationButton.Click();

            //Enter the Education details
            Extension.WaitForElementDisplayed(Driver, By.Name("instituteName"), 2);
            CollegeUniversityName.SendKeys(ExcelLib.ReadData(2, "InstituteName"));
            SelectDropDown(Country, "SelectByText", ExcelLib.ReadData(2, "Country"));
            SelectDropDown(Title, "SelectByText", ExcelLib.ReadData(2, "Title"));
            Degree.SendKeys(ExcelLib.ReadData(2, "Degree"));
            SelectDropDown(YearOfGraduation, "SelectByValue", ExcelLib.ReadData(2, "YearOfGraduation"));

            //Click Add button
            AddEducationButton.Click();

            Base.Image = SaveScreenShotClass.SaveScreenshot(Driver, "Report");

            //Validate message
            Extension.MessageValidation("Education has been added");

        }
        internal void UpdateEducation()
        {
            //Get the Education value needs to be updated
            string expectedInstituteName = ExcelLib.ReadData(2, "InstituteName");
            string expectedTitle = ExcelLib.ReadData(2, "Title");

            //Get the rows count in Education table
            IList<IWebElement> Tablerows = Driver.FindElements(By.XPath("//h3[contains(text(),'Education')]/../..//table/tbody/tr"));
            var rowCount = Tablerows.Count;

            //Get the actual Education value and compare with Education needs to be updated, if matches update the record
            for (int i = 1; i <= rowCount; i++)
            {
                string actualInstituteName = Driver.FindElement(By.XPath("//h3[contains(text(),'Education')]/../..//table/tbody[" + i + "]/tr/td[2]")).Text;
                string actualTitle = Driver.FindElement(By.XPath("//h3[contains(text(),'Education')]/../..//table/tbody[" + i + "]/tr/td[3]")).Text;

                if (expectedInstituteName.ToLower() == actualInstituteName.ToLower() && expectedTitle.ToLower() == actualTitle.ToLower())
                {
                    //Click on Edit icon
                    Driver.FindElement(By.XPath("//h3[contains(text(),'Education')]/../..//table/tbody[" + i + "]/tr/td[6]/span[1]/i")).Click();

                    //Clear the existing values and add new value 
                    Extension.WaitForElementDisplayed(Driver, By.Name("instituteName"), 2);
                    CollegeUniversityName.Clear();
                    CollegeUniversityName.SendKeys(ExcelLib.ReadData(2, "UpdateInstituteName"));
                    SelectDropDown(Country, "SelectByText", ExcelLib.ReadData(2, "UpdateCountry"));
                    SelectDropDown(Title, "SelectByText", ExcelLib.ReadData(2, "UpdateTitle"));
                    Degree.Clear();
                    Degree.SendKeys(ExcelLib.ReadData(2, "UpdateDegree"));
                    SelectDropDown(YearOfGraduation, "SelectByValue", ExcelLib.ReadData(2, "UpdateYearOfGraduation"));

                    //Click update button
                    UpdateEducationButton.Click();

                    Base.Image = SaveScreenShotClass.SaveScreenshot(Driver, "Report");

                    break;

                }

            }
            //Validate message
            Extension.MessageValidation("Education as been updated");
        }
        internal void DeleteEducation()
        {

            //Get the Education needs to be Deleted
            String expectedInstituteName = ExcelLib.ReadData(2, "InstituteName");
            String expectedTitle = ExcelLib.ReadData(2, "Title");

            //Get the rows count in Education table
            IList<IWebElement> Tablerows = Driver.FindElements(By.XPath("//h3[contains(text(),'Education')]/../..//table/tbody/tr"));
            var rowCount = Tablerows.Count;

            //Get the actual Education value and compare with Education needs to be updated, if matches delete the record
            for (int i = 1; i <= rowCount; i++)
            {
                String actualInstituteName = Driver.FindElement(By.XPath("//h3[contains(text(),'Education')]/../..//table/tbody[" + i + "]/tr/td[2]")).Text;
                String actualTitle = Driver.FindElement(By.XPath("//h3[contains(text(),'Education')]/../..//table/tbody[" + i + "]/tr/td[3]")).Text;
                if (expectedInstituteName == actualInstituteName && expectedTitle == actualTitle)
                {
                    //CliCk on Delete icon
                    Driver.FindElement(By.XPath("//h3[contains(text(),'Education')]/../..//table/tbody[" + i + "]/tr/td[6]/span[2]/i")).Click();

                    Base.Image = SaveScreenShotClass.SaveScreenshot(Driver, "Report");

                    break;

                }
            }
            //Validate message
            Extension.MessageValidation("Education entry successfully removed");
        }

        //Search the Education in the listing
        internal void SearchEducation(String expectedInstituteName, String expectedTitle)
        {
            //Setting the isEducationFound variable to false
            isEducationFound = false;

            Thread.Sleep(2000);
            //Get all the Education records
            IList<IWebElement> EducationRecords = Driver.FindElements(By.XPath("//h3[contains(text(),'Education')]/../..//table/tbody/tr"));

            //if the expected and actual Education matches, set the isEducationFound to true
            for (int j = 1; j <= EducationRecords.Count; j++)
            {
                string actualInstituteName = Driver.FindElement(By.XPath("//h3[contains(text(),'Education')]/../..//table/tbody[" + j + "]/tr/td[2]")).Text;
                string actualTitle = Driver.FindElement(By.XPath("//h3[contains(text(),'Education')]/../..//table/tbody[" + j + "]/tr/td[3]")).Text;
                Console.WriteLine("actualInstituteName {0}", actualInstituteName);
                Console.WriteLine("actualTitle {0}", actualTitle);
                Console.WriteLine("expectedInstituteName {0}", expectedInstituteName);
                Console.WriteLine("expectedTitle {0}", expectedTitle);
                if (expectedInstituteName == actualInstituteName && expectedTitle == actualTitle)
                {
                    isEducationFound = true;
                    break;
                }
            }

        }

        //Validate the Education added is dispalyed in the listing
        internal void ValidateAddedEducation()
        {
            SearchEducation(ExcelLib.ReadData(2, "InstituteName"), ExcelLib.ReadData(2, "Title"));

            GlobalDefinitions.ValidateBoolean(isEducationFound, "Education Added");


        }

        //Validate the Education Updated is dispalyed in the listing
        internal void ValidateUpdateEducation()
        {
            SearchEducation(ExcelLib.ReadData(2, "UpdateInstituteName"), ExcelLib.ReadData(2, "UpdateTitle"));

            GlobalDefinitions.ValidateBoolean(isEducationFound, "Education Updated");

        }

        //Validate the Education Deleted is not dispalyed in the listing
        internal void ValidateDeleteEducation()
        {
            SearchEducation(ExcelLib.ReadData(2, "InstituteName"), ExcelLib.ReadData(2, "Title"));

            GlobalDefinitions.ValidateBoolean(!(isEducationFound), "Education Deleted");

        }
    }
}







