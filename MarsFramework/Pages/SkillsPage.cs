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
    internal class SkillsPage
    {
        //Click on Skills Tab
        [FindsBy(How = How.LinkText, Using = "Skills")]
        private IWebElement SkillTab { get; set; }

        //Click on Add New Skill Button
        [FindsBy(How = How.XPath, Using = "//h3[contains(text(),'Skills')]/../..//div[text()='Add New']")]
        private IWebElement AddNewSkillButton { get; set; }

        //Add the Skill
        [FindsBy(How = How.XPath, Using = "//input[@placeholder='Add Skill']")]
        private IWebElement AddSkill { get; set; }

        //Choose Skill Level
        [FindsBy(How = How.Name, Using = "level")]
        private IWebElement ChooseSkillLevel { get; set; }

        //Click Add Skill Button
        [FindsBy(How = How.XPath, Using = "//input[@value='Add']")]
        private IWebElement AddSkillButton { get; set; }

        //Click Update Skill Button
        [FindsBy(How = How.XPath, Using = "//input[@value='Update']")]
        private IWebElement UpdateSkillButton { get; set; }

        private static bool isSkillFound;

        public SkillsPage()
        {
            PageFactory.InitElements(Driver, this);
        }

        //Navigate to SKill tab
        internal void NavigateToSkillTab()
        {
            Extension.WaitForElementDisplayed(Driver, By.XPath("//a[contains(text(),'Skills')]"), 5);
            SkillTab.Click();
        }

        // Add a new Skill
        internal void AddNewSkill()
        {

            //Click Add New button 
            AddNewSkillButton.Click();

            //Enter the language and level
            Extension.WaitForElementDisplayed(Driver, By.XPath("//input[@placeholder='Add Skill']"), 2);
            AddSkill.SendKeys(ExcelLib.ReadData(2, "Skill"));
            SelectDropDown(ChooseSkillLevel, "SelectByText", ExcelLib.ReadData(2, "Level"));

            //Click Add button
            AddSkillButton.Click();

            Base.Image = SaveScreenShotClass.SaveScreenshot(Driver, "Report");

            //Validate message
            Extension.MessageValidation(ExcelLib.ReadData(2, "Skill") + " has been added to your skills");

        }

        internal void CheckSkillExists()
        {
            SearchSkill(ExcelLib.ReadData(2, "Skill"), ExcelLib.ReadData(2, "Level"));



            if (!isSkillFound)
            {
                SkillsSteps skillSteps = new SkillsSteps();
                skillSteps.WhenIAddANewSkill();
                //Populate the excel data
                //GlobalDefinitions.ExcelLib.PopulateInCollection(Base.ExcelPathProfile, "Language");

                //Call AddNewLanguage Method to add a New Language
                //AddNewLanguage();

            }
        }

        //Update a Skill
        internal void UpdateSkill()
        {

            //Get the Skill value needs to be updated
            String expectedValue = ExcelLib.ReadData(2, "Skill");

            //Get the rows count in Skill table
            IList<IWebElement> Tablerows = Driver.FindElements(By.XPath("//h3[contains(text(),'Skills')]/../..//table/tbody/tr"));
            var rowCount = Tablerows.Count;

            //Get the actual Skill value and compare with Skill needs to be updated, if matches update the record
            for (int i = 1; i <= rowCount; i++)
            {
                String actualValue = Driver.FindElement(By.XPath("//h3[contains(text(),'Skills')]/../..//table/tbody[" + i + "]/tr/td[1]")).Text;
                if (expectedValue == actualValue)
                {
                    //Click on Edit icon
                    Driver.FindElement(By.XPath("//h3[contains(text(),'Skills')]/../..//table/tbody[" + i + "]/tr/td[3]/span[1]/i")).Click();

                    //Clear the existing value and add new value 
                    Extension.WaitForElementDisplayed(Driver, By.XPath("//input[@placeholder='Add Skill']"), 2);
                    AddSkill.Clear();
                    AddSkill.SendKeys(ExcelLib.ReadData(2, "UpdateSkill"));
                    SelectDropDown(ChooseSkillLevel, "SelectByText", ExcelLib.ReadData(2, "UpdateLevel"));

                    //Click update button
                    UpdateSkillButton.Click();

                    Base.Image = SaveScreenShotClass.SaveScreenshot(Driver, "Report");

                    break;
                }

            }
            //Validate message
            Extension.MessageValidation(ExcelLib.ReadData(2, "UpdateSkill") + " has been updated to your skills");

        }

        //Delete a Skill
        internal void DeleteSkill()
        {

            //Get the Skill needs to be Deleted
            string expectedValue = ExcelLib.ReadData(2, "Skill");

            //Get the rows count in Skill table
            IList<IWebElement> Tablerows = Driver.FindElements(By.XPath("//h3[contains(text(),'Skills')]/../..//table/tbody/tr"));
            var rowCount = Tablerows.Count;

            //Get the actual Skill value and compare with Skill needs to be updated, if matches delete the record
            for (int i = 1; i <= rowCount; i++)
            {
                string actualValue = Driver.FindElement(By.XPath("//h3[contains(text(),'Skills')]/../..//table/tbody[" + i + "]/tr/td[1]")).Text;
                if (expectedValue == actualValue)
                {
                    //CliCk on Delete icon
                    Driver.FindElement(By.XPath("//h3[contains(text(),'Skills')]/../..//table/tbody[" + i + "]/tr/td[3]/span[2]/i")).Click();

                    Base.Image = SaveScreenShotClass.SaveScreenshot(Driver, "Report");

                    break;
                }
            }
            //Validate message
            Extension.MessageValidation(ExcelLib.ReadData(2, "Skill") + " has been deleted");

        }

        //Search the Skill in the listing
        internal void SearchSkill(String expectedSkill, String expectedSkillLevel)
        {
            //Setting the isSkillFound variable to false
            isSkillFound = false;

            //Get all the Skill records
            IList<IWebElement> SkillRecords = Driver.FindElements(By.XPath("//h3[contains(text(),'Skills')]/../..//table/tbody/tr"));

            //if the expected and actual Skill matches, set the isSkillFound to true
            for (int j = 1; j <= SkillRecords.Count; j++)
            {
                String actualSkill = Driver.FindElement(By.XPath("//h3[contains(text(),'Skills')]/../..//table/tbody[" + j + "]/tr/td[1]")).Text;
                String actualSkillLevel = Driver.FindElement(By.XPath("//h3[contains(text(),'Skills')]/../..//table/tbody[" + j + "]/tr/td[2]")).Text;
                if (expectedSkill == actualSkill && expectedSkillLevel == actualSkillLevel)
                {

                    isSkillFound = true;
                    break;
                }
            }

        }

        //Validate the Skill added is dispalyed in the listing
        internal void ValidateAddedSkill()
        {
            SearchSkill(ExcelLib.ReadData(2, "Skill"), ExcelLib.ReadData(2, "Level"));

            GlobalDefinitions.ValidateBoolean(isSkillFound, "Skill Added");

        }

        //Validate the Skill Updated is dispalyed in the listing
        internal void ValidateUpdateSkill()
        {
            SearchSkill(ExcelLib.ReadData(2, "UpdateSkill"), ExcelLib.ReadData(2, "UpdateLevel"));

            GlobalDefinitions.ValidateBoolean(isSkillFound, "Skill Updated");

        }

        //Validate the Skill Deleted is not dispalyed in the listing
        internal void ValidateDeleteSkill()
        {
            SearchSkill(ExcelLib.ReadData(2, "Skill"), ExcelLib.ReadData(2, "Level"));

            GlobalDefinitions.ValidateBoolean(!(isSkillFound), "Skill Deleted");

        }

    }
}
