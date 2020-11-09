using MarsFramework.Global;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.PageObjects;
using System;
using System.Collections.Generic;
using AutoIt;
using System.Threading;
using NUnit.Core;
using MongoDB.Driver.Core.Authentication;
//using static MarsFramework.Program;
using static MarsFramework.Global.GlobalDefinitions;
using RelevantCodes.ExtentReports;
using System.Linq;
using System.Text;
using System.Collections.Specialized;
using System.Globalization;
using NUnit.Framework;
using System.IO;

namespace MarsFramework.Pages
{
    internal class ShareSkill
    {
        //Click on ShareSkill Button
        [FindsBy(How = How.LinkText, Using = "Share Skill")]
        private IWebElement ShareSkillButton { get; set; }

        //Enter the Title in textbox
        [FindsBy(How = How.Name, Using = "title")]
        private IWebElement Title { get; set; }

        //Enter the Description in textbox
        [FindsBy(How = How.Name, Using = "description")]
        private IWebElement Description { get; set; }

        //Click on Category Dropdown
        [FindsBy(How = How.Name, Using = "categoryId")]
        private IWebElement CategoryDropDown { get; set; }

        //Click on SubCategory Dropdown
        [FindsBy(How = How.Name, Using = "subcategoryId")]
        private IWebElement SubCategoryDropDown { get; set; }

        //Enter Tag names in textbox
        [FindsBy(How = How.XPath, Using = "//body/div/div/div[@id='service-listing-section']/div[contains(@class,'ui container')]/div[contains(@class,'listing')]/form[contains(@class,'ui form')]/div[contains(@class,'tooltip-target ui grid')]/div[contains(@class,'twelve wide column')]/div[contains(@class,'')]/div[contains(@class,'ReactTags__tags')]/div[contains(@class,'ReactTags__selected')]/div[contains(@class,'ReactTags__tagInput')]/input[1]")]
        private IWebElement Tags { get; set; }

        //Get the Entered Tag name
        [FindsBy(How = How.XPath, Using = "//*[text()='Tags']/../..//span[@Class='ReactTags__tag']")]
        private IWebElement EnteredTag { get; set; }

        //Get the Entered Skill-Exchange name
        [FindsBy(How = How.XPath, Using = "//*[text()='Skill-Exchange']/../..//span[@Class='ReactTags__tag']")]
        private IWebElement SkillExchangeTag { get; set; }

        //Select the Service type
        [FindsBy(How = How.XPath, Using = "//form/div[5]/div[@class='twelve wide column']/div/div[@class='field']")]
        private IList<IWebElement> ServiceTypeOptions { get; set; }

        //Select the Location Type
        [FindsBy(How = How.XPath, Using = "//form/div[6]/div[@class='twelve wide column']/div/div[@class = 'field']")]
        private IList<IWebElement> LocationTypeOption { get; set; }

        //Click on Start Date dropdown
        [FindsBy(How = How.Name, Using = "startDate")]
        private IWebElement StartDateDropDown { get; set; }

        //Click on End Date dropdown
        [FindsBy(How = How.Name, Using = "endDate")]
        private IWebElement EndDateDropDown { get; set; }

        //Storing the table of available days
        [FindsBy(How = How.XPath, Using = "//label[text()='Start date']/../../..")]
        private IWebElement Days { get; set; }

        //Click on Skill Trade option
        [FindsBy(How = How.XPath, Using = "//form/div[8]/div[@class='twelve wide column']/div/div[@class = 'field']")]
        private IList<IWebElement> SkillTradeOption { get; set; }

        //Enter Skill Exchange
        [FindsBy(How = How.XPath, Using = "//div[@class='form-wrapper']//input[@placeholder='Add new tag']")]
        private IWebElement SkillExchange { get; set; }

        //Enter the amount for Credit
        [FindsBy(How = How.XPath, Using = "//input[@placeholder='Amount']")]
        private IWebElement CreditAmount { get; set; }

        //Click on Work Samples
        [FindsBy(How = How.XPath, Using = "//label[@class='worksamples-file']/div/span/i")]
        private IWebElement WorkSamples { get; set; }

        //Get Entered Work Sample Files
        [FindsBy(How = How.XPath, Using = "//label[@class='worksamples-file']//a")]
        private IWebElement WorkSamplesFile { get; set; }

        //Click on Active/Hidden option
        [FindsBy(How = How.XPath, Using = "//form/div[10]/div[@class='twelve wide column']/div/div[@class = 'field']")]
        private IList<IWebElement> ActiveOption { get; set; }

        //Click on Save button
        [FindsBy(How = How.XPath, Using = "//input[@value='Save']")]
        private IWebElement Save { get; set; }

        //Remove Entered Tags button
        [FindsBy(How = How.CssSelector, Using = ".ReactTags__remove")]
        private IList<IWebElement> RemoveTags { get; set; }

        public ShareSkill()
        {
            PageFactory.InitElements(Global.GlobalDefinitions.Driver, this);
        }

        //Click Share SKill Button
        internal void ClickShareSkillButton()
        {
            //Wait for ShareSkill Button
            Extension.WaitForElementDisplayed(GlobalDefinitions.Driver, By.LinkText("Share Skill"), 5);

            //Click ShareSkill Button
            ShareSkillButton.Click();
        }

        //Enter Share Skill Data
        internal void EnterShareSkillData()
        {
            //Enter the Title
            Title.SendKeys(GlobalDefinitions.ExcelLib.ReadData(2, "Title"));

            //Enter the Description
            Description.SendKeys(GlobalDefinitions.ExcelLib.ReadData(2, "Description"));

            //Select Category
            GlobalDefinitions.SelectDropDown(CategoryDropDown, "SelectByText", GlobalDefinitions.ExcelLib.ReadData(2, "Category"));

            //Select Sub-Category
            GlobalDefinitions.SelectDropDown(SubCategoryDropDown, "SelectByText", GlobalDefinitions.ExcelLib.ReadData(2, "SubCategory"));

            //Enter Tags
            Tags.SendKeys(GlobalDefinitions.ExcelLib.ReadData(2, "Tags") + "\n");

            //Select Service Type
            GlobalDefinitions.SelectRadioButton(ServiceTypeOptions, GlobalDefinitions.ExcelLib.ReadData(2, "ServiceType"), By.Name("serviceType"));

            //Select Location Type
            GlobalDefinitions.SelectRadioButton(LocationTypeOption, GlobalDefinitions.ExcelLib.ReadData(2, "LocationType"), By.Name("locationType"));

            //Add Start Date
            StartDateDropDown.SendKeys(GlobalDefinitions.ExcelLib.ReadData(2, "Startdate"));

            //Add End Date
            EndDateDropDown.SendKeys(GlobalDefinitions.ExcelLib.ReadData(2, "Enddate"));

            //Select days and Enter start and End time
            EnterDaysAndTime();

            //Select Skill Trade
            GlobalDefinitions.SelectRadioButton(SkillTradeOption, GlobalDefinitions.ExcelLib.ReadData(2, "SkillTrade"), By.Name("skillTrades"));
            string SkillTradeValue = GlobalDefinitions.ExcelLib.ReadData(2, "SkillTrade").ToUpper();

            //Enter Skill-Exchange or Credit
            if (SkillTradeValue == "SKILL-EXCHANGE")
            {
                SkillExchange.SendKeys(GlobalDefinitions.ExcelLib.ReadData(2, "Skill-Exchange") + "\n");

            }
            else
            {
                CreditAmount.SendKeys(GlobalDefinitions.ExcelLib.ReadData(2, "Credit"));
            }

            //upload Work Samples
            WorkSamples.Click();
            AutoItX.WinWait("Open", "File Upload", 1);
            AutoItX.WinActivate("Open");
            AutoItX.ControlFocus("Open", "File Upload", "[CLASS:Edit; INSTANCE:1]");
            AutoItX.Send(Path.Combine(AppDomain.CurrentDomain.BaseDirectory + "..\\..\\ExcelData\\empty.txt"));
            AutoItX.Send("{Enter}");

            //Select Active radio
            GlobalDefinitions.SelectRadioButton(ActiveOption, GlobalDefinitions.ExcelLib.ReadData(2, "Active"), By.Name("isActive"));

        }

        //Validate the Entered Shared Skill Data
        internal void ValidateShareSkillData()
        {

            //Validate Entered Title
            GlobalDefinitions.ValidateFieldData(GlobalDefinitions.ExcelLib.ReadData(2, "Title"), Title.GetAttribute("value"), "Title");

            //Validate Entered Description
            GlobalDefinitions.ValidateFieldData(GlobalDefinitions.ExcelLib.ReadData(2, "Description"), Description.Text, "Description");

            //Validate Selected Category
            GlobalDefinitions.ValidateDropDown(CategoryDropDown, GlobalDefinitions.ExcelLib.ReadData(2, "Category"), "Category");

            //Validate Selected SubCategory
            GlobalDefinitions.ValidateDropDown(SubCategoryDropDown, GlobalDefinitions.ExcelLib.ReadData(2, "SubCategory"), "SubCategory");

            //Validate Entered Tags
            GlobalDefinitions.ValidateFieldData(GlobalDefinitions.ExcelLib.ReadData(2, "Tags"), EnteredTag.Text.Replace(EnteredTag.FindElement(By.XPath("./*")).Text, ""), "Tag");

            //Validate Service Type
            GlobalDefinitions.ValidateRadioButton(ServiceTypeOptions, By.Name("serviceType"), GlobalDefinitions.ExcelLib.ReadData(2, "ServiceType"), "ServiceType");

            //Validate Location Type
            GlobalDefinitions.ValidateRadioButton(LocationTypeOption, By.Name("locationType"), GlobalDefinitions.ExcelLib.ReadData(2, "LocationType"), "LocationType");

            //Validate Start Date
            GlobalDefinitions.ValidateFieldData(Convert.ToDateTime(GlobalDefinitions.ExcelLib.ReadData(2, "Startdate")).ToString("yyyy-MM-dd"), StartDateDropDown.GetAttribute("value"), "Start Date");

            //Validate End Date
            GlobalDefinitions.ValidateFieldData(Convert.ToDateTime(GlobalDefinitions.ExcelLib.ReadData(2, "Enddate")).ToString("yyyy-MM-dd"), EndDateDropDown.GetAttribute("value"), "End Date");

            //Validate Days and Time
            ValidateDaysAndTime();

            //Validate Skill Trade
            ValidateRadioButton(SkillTradeOption, By.Name("skillTrades"), GlobalDefinitions.ExcelLib.ReadData(2, "SkillTrade"), "SkillTrade");
            try
            {

                if (GlobalDefinitions.ExcelLib.ReadData(2, "SkillTrade").ToUpper() == "SKILL-EXCHANGE")
                {
                    //Validate Skill Exchange
                    GlobalDefinitions.ValidateFieldData(GlobalDefinitions.ExcelLib.ReadData(2, "Skill-Exchange"), SkillExchangeTag.Text.Replace(EnteredTag.FindElement(By.XPath("./*")).Text, ""), "Skill-Exchange");
                }
                else if (GlobalDefinitions.ExcelLib.ReadData(2, "SkillTrade").ToUpper() == "CREDIT")
                {
                    GlobalDefinitions.ValidateFieldData(GlobalDefinitions.ExcelLib.ReadData(2, "Credit"), CreditAmount.GetAttribute("value"), "Credit");
                }
            }
            catch (Exception e)
            {
                Base.test.Log(LogStatus.Fail, "Caught Exception For Skill Exchange/Credit ", e.Message);
            }

            //Validate Selected Sample File
            // GlobalDefinitions.WaitForElement(GlobalDefinitions.driver, By.XPath("//label[@class='worksamples-file']//a"), 5);
            GlobalDefinitions.ValidateFieldData(WorkSamplesFile.Text, "empty.txt", "Work Sample File");

            //Validate Active 
            GlobalDefinitions.ValidateRadioButton(ActiveOption, By.Name("isActive"), GlobalDefinitions.ExcelLib.ReadData(2, "Active"), "Active");

        }

        //Select Days and Enter the Time for days selected
        internal void EnterDaysAndTime()
        {
            //Getting all the values in Selectday column in a list
            IList<string> DaysList = GlobalDefinitions.ExcelLib.ReadData(2, "Selectday").Split('/');

            //Getting count for all days 
            //Check the checkbox for selectdays mentioned in excel and enter time for same
            int DaysRows = Days.FindElements(By.Name("Available")).Count;
            foreach (string AvailableDays in DaysList)
            {
                for (int i = 1; i <= DaysRows; i++)
                {
                    string DayValue = Days.FindElements(By.ClassName("fields"))[i].Text;
                    if (AvailableDays.ToLower() == DayValue.ToLower())
                    {
                        Days.FindElements(By.Name("Available"))[i - 1].Click();
                        string StartTime = DateTime.Parse(GlobalDefinitions.ExcelLib.ReadData(2, "Starttime")).ToString("hh:mmtt");
                        Days.FindElements(By.Name("StartTime"))[i - 1].SendKeys(StartTime);

                        string EndTime = DateTime.Parse(GlobalDefinitions.ExcelLib.ReadData(2, "Endtime")).ToString("hh:mmtt");
                        Days.FindElements(By.Name("EndTime"))[i - 1].SendKeys(EndTime);
                        break;
                    }
                }
            }
        }

        //Validate the selected days and entered time for days
        internal void ValidateDaysAndTime()
        {
            try
            {
                IList<string> DaysList = GlobalDefinitions.ExcelLib.ReadData(2, "Selectday").Split('/');

                //Getting count for all days 
                //Check the checkbox is  selected for days mentioned in excel and validate time for same
                int DaysRows = Days.FindElements(By.Name("Available")).Count;
                foreach (string AvailableDays in DaysList)
                {
                    for (int i = 1; i <= DaysRows; i++)
                    {
                        string DayValue = Days.FindElements(By.ClassName("fields"))[i].Text;
                        if (AvailableDays.ToLower() == DayValue.ToLower())
                        {
                            bool isTimeMatches = Days.FindElements(By.Name("Available"))[i - 1].Selected && Days.FindElements(By.Name("StartTime"))[i - 1].GetAttribute("value") == DateTime.Parse(GlobalDefinitions.ExcelLib.ReadData(2, "Starttime")).ToString("HH:mm") && Days.FindElements(By.Name("EndTime"))[i - 1].GetAttribute("value") == DateTime.Parse(GlobalDefinitions.ExcelLib.ReadData(2, "Endtime")).ToString("HH:mm");
                            GlobalDefinitions.ValidateBoolean(isTimeMatches, DayValue + " is selected and Time is Entered");
                           
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Base.test.Log(LogStatus.Fail, "Caught Exception For Day and Time", e.Message);
            }
        }

        //Save the Share skill
        internal void SaveShareSkill()
        {
            //Click Save
            Save.Click();
            Base.Image = SaveScreenShotClass.SaveScreenshot(Driver, "Report");
        }

        //Add Share skill
        internal void AddShareSkill()
        {
            //Populate the excel data
            GlobalDefinitions.ExcelLib.PopulateInCollection(Base.ExcelPathShareSkill, "ShareSkill");

            //Call NavigateToManageListing Method to navigate to Manage Listing Page
            ManageListings manageListings = new ManageListings();
            manageListings.NavigateToManageListing();

            //Call SearchListings Method to get count for existing records with same category,title and description as we are going to add
            int MatchingRecordsBeforeAdd = manageListings.SearchListings(GlobalDefinitions.ExcelLib.ReadData(2, "Category"), GlobalDefinitions.ExcelLib.ReadData(2, "Title"), GlobalDefinitions.ExcelLib.ReadData(2, "Description"));

            //Click Share Skill Button
            ClickShareSkillButton();

            //Call EnterShareSkillData Method to enter Share Skill data
            EnterShareSkillData();

            //Call ValidateShareSkillData Method to Validate entered Share Skill data
            ValidateShareSkillData();

            //Save Share Skill
            SaveShareSkill();

            //Call SearchListings Method to get count for records with same category,title and description as we added
            int MatchingRecordsAfterAdd = manageListings.SearchListings(GlobalDefinitions.ExcelLib.ReadData(2, "Category"), GlobalDefinitions.ExcelLib.ReadData(2, "Title"), GlobalDefinitions.ExcelLib.ReadData(2, "Description"));
            
            //checking if number of records with same category,title and description is 1 more than it has before
            int ExpectedRecords = MatchingRecordsBeforeAdd + 1;

            GlobalDefinitions.ValidateBoolean(ExpectedRecords == MatchingRecordsAfterAdd, "Share Skill Added");
            
        }

        //Edit the Existing share Skill
        internal void EditShareSkillData()
        {
            Extension.WaitForElementDisplayed(Driver, By.Name("title"), 5);
            //Clear the Title
            Title.Clear();

            //Enter the Title
            Title.SendKeys(GlobalDefinitions.ExcelLib.ReadData(2, "Title"));

            //Clear the Description
            Description.Clear();

            //Enter the Description
            Description.SendKeys(GlobalDefinitions.ExcelLib.ReadData(2, "Description"));

            //Select Category
            CategoryDropDown.SendKeys(GlobalDefinitions.ExcelLib.ReadData(2, "Category"));

            //Select Sub-Category
            GlobalDefinitions.SelectDropDown(SubCategoryDropDown, "SelectByText", GlobalDefinitions.ExcelLib.ReadData(2, "SubCategory"));

            //Clear the Entered tag
            foreach (IWebElement removeTag in RemoveTags)
            {
                removeTag.Click();
            }

            //Enter Tags
            Tags.SendKeys(GlobalDefinitions.ExcelLib.ReadData(2, "Tags") + "\n");

            //Select Service Type
            GlobalDefinitions.SelectRadioButton(ServiceTypeOptions, GlobalDefinitions.ExcelLib.ReadData(2, "ServiceType"), By.Name("serviceType"));

            //Select Location Type
            GlobalDefinitions.SelectRadioButton(LocationTypeOption, GlobalDefinitions.ExcelLib.ReadData(2, "LocationType"), By.Name("locationType"));

            //Add Start Date
            StartDateDropDown.SendKeys(GlobalDefinitions.ExcelLib.ReadData(2, "Startdate"));

            //Add End Date
            EndDateDropDown.SendKeys(GlobalDefinitions.ExcelLib.ReadData(2, "Enddate"));

            //Clear the Available days data

            int DaysRows = Days.FindElements(By.Name("Available")).Count;

            for (int i = 1; i <= DaysRows; i++)
            {
                if (Days.FindElements(By.Name("Available"))[i - 1].Selected)
                {
                    Days.FindElements(By.Name("Available"))[i - 1].Click();
                    Days.FindElements(By.Name("StartTime"))[i - 1].SendKeys(Keys.Delete);
                    Days.FindElements(By.Name("EndTime"))[i - 1].SendKeys(Keys.Delete);

                }

            }

            //Select days and Enter start and End time
            EnterDaysAndTime();

            //Select Skill Trade
            GlobalDefinitions.SelectRadioButton(SkillTradeOption, GlobalDefinitions.ExcelLib.ReadData(2, "SkillTrade"), By.Name("skillTrades"));
            string SkillTradeValue = GlobalDefinitions.ExcelLib.ReadData(2, "SkillTrade").ToUpper();

            //Enter Skill-Exchange or Credit
            if (SkillTradeValue == "SKILL-EXCHANGE")
            {

                SkillExchange.SendKeys(GlobalDefinitions.ExcelLib.ReadData(2, "Skill-Exchange") + "\n");

            }
            else
            {
                CreditAmount.Clear();
                CreditAmount.SendKeys(GlobalDefinitions.ExcelLib.ReadData(2, "Credit"));
            }


            //Select Active radio
            GlobalDefinitions.SelectRadioButton(ActiveOption, GlobalDefinitions.ExcelLib.ReadData(2, "Active"), By.Name("isActive"));

        }

    }
}
