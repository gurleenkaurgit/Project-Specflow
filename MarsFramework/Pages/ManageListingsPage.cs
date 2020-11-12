using MarsFramework.Global;
using MarsFramework.HookUp;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using RelevantCodes.ExtentReports;
using SeleniumExtras.PageObjects;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Web.Razor.Text;
using static MarsFramework.Global.GlobalDefinitions;

namespace MarsFramework.Pages
{
    internal class ManageListingsPage
    {
        //Click on Manage Listings Link
        [FindsBy(How = How.LinkText, Using = "Manage Listings")]
        private IWebElement ManageListingsLink { get; set; }

        //Get Pagination Buttons
        [FindsBy(How = How.XPath, Using = "//div[@class='ui buttons semantic-ui-react-button-pagination']/button")]
        private IList<IWebElement> PaginationButtons { get; set; }

        //Click Next Button
        [FindsBy(How = How.XPath, Using = "//button[contains(text(),'>')]")]
        private IWebElement NextButton { get; set; }

        //Get Listing rows
        [FindsBy(How = How.XPath, Using = "//div[@id='listing-management-section']//table//tbody//tr")]
        private IList<IWebElement> TableRows { get; set; }

        //Edit the listing
        [FindsBy(How = How.XPath, Using = "(//i[@class='outline write icon'])[1]")]
        private IWebElement Edit { get; set; }

        //Click on Yes or No
        [FindsBy(How = How.XPath, Using = "//div[@class='actions']")]
        private IWebElement ClickActionsButton { get; set; }

        private static bool isRecordFound;

        public ManageListingsPage()
        {
            PageFactory.InitElements(Global.GlobalDefinitions.Driver, this);
        }

        //Navigate to Manage Listing page
        internal void NavigateToManageListing()
        {
            //Wait till Manage Listing is visible
            Extension.WaitForElementDisplayed(GlobalDefinitions.Driver, By.LinkText("Manage Listings"), 5);

            //Click Manage Listing Link
            Actions action = new Actions(Driver);
            action.MoveToElement(ManageListingsLink).Click(ManageListingsLink).Build().Perform();

        }
        internal void EditShareSkill()
        {
            string CategoryToSearch = GlobalDefinitions.ExcelLib.ReadData(2, "Category");
            string TitleToSearch = GlobalDefinitions.ExcelLib.ReadData(2, "Title");
            string DescriptionToSearch = GlobalDefinitions.ExcelLib.ReadData(2, "Description");

            //loop through the pages 
            for (int i = 0; i < PaginationButtons.Count - 2; i++)
            {
                Thread.Sleep(2000);

                //Check the Category, Title and Description values for each row in a page, if matches increment the record found variable
                foreach (IWebElement listingRecord in TableRows)
                {
                    string Category = listingRecord.FindElement(By.XPath("td[2]")).Text;
                    string Title = listingRecord.FindElement(By.XPath("td[3]")).Text;
                    string Description = listingRecord.FindElement(By.XPath("td[4]")).Text;
                    if (Category == CategoryToSearch && Title == TitleToSearch && Description == DescriptionToSearch)
                    {
                        listingRecord.FindElement(By.CssSelector(".outline.write.icon")).Click();
                        //Populate the excel data
                        GlobalDefinitions.ExcelLib.PopulateInCollection(Base.ExcelPathManageListing, "EditShareSkill");

                        ShareSkillPage shareSkill = new ShareSkillPage();

                        //Call EditShareSkillData method to add Edit data
                        shareSkill.EditShareSkillData();

                        //Call SaveShareSkill Method to save the Share Skill
                        shareSkill.SaveShareSkill();
                        return;
                    }

                }

                //If Next button is enable click it to Navigate to next page
                if (NextButton.Enabled == true)
                {
                    NextButton.Click();
                }
            }


        }

        internal void CheckShareSkillExists()
        {
            SearchListings(GlobalDefinitions.ExcelLib.ReadData(2, "Category"), GlobalDefinitions.ExcelLib.ReadData(2, "Title"), GlobalDefinitions.ExcelLib.ReadData(2, "Description"));

            if (!isRecordFound)
            {
                ShareSkillSteps shareSkillSteps = new ShareSkillSteps();
                shareSkillSteps.GivenIClickOnShareSkillTab();
                shareSkillSteps.WhenIAddShareSkill();

            }
        }

        //Delete the Share SKill 
        internal void DeleteShareSkill(string CategoryToDelete, string TitleToDelete, string DescriptionToDelete, string Action)
        {
            Thread.Sleep(2000);

            //loop through the pages
            for (int i = 0; i < PaginationButtons.Count - 2; i++)
            {
                Thread.Sleep(2000);

                //Check the Category, Title and Description values for each row in a page, if matches delete the record
                foreach (IWebElement listingRecord in TableRows)
                {
                    string Category = listingRecord.FindElement(By.XPath("td[2]")).Text;
                    string Title = listingRecord.FindElement(By.XPath("td[3]")).Text;
                    string Description = listingRecord.FindElement(By.XPath("td[4]")).Text;
                    if (Category == CategoryToDelete && Title == TitleToDelete && Description == DescriptionToDelete)
                    {
                        listingRecord.FindElement(By.CssSelector("i.remove.icon")).Click();
                        ClickActionsButton.FindElement(By.XPath("//*[text()='" + Action + "']")).Click();

                        //Extension.WaitForElementDisplayed(GlobalDefinitions.Driver, By.ClassName("ns-box-inner"), 5);

                        Base.Image = SaveScreenShotClass.SaveScreenshot(Driver, "Report");

                        //string text = Driver.FindElement(By.ClassName("ns-box-inner")).Text;

                        // GlobalDefinitions.ValidateBoolean(text.Contains("has been deleted"), text);

                        return;

                    }
                    //If Next button is enable click it to Navigate to next page

                }
                if (NextButton.Enabled == true)
                {
                    NextButton.Click();
                }
            }
            Base.test.Log(LogStatus.Fail, "Did not deleted any record ");

        }



        internal bool SearchListings(string CategoryToSearch, string TitleToSearch, string DescriptionToSearch)
        {
            //Initialize the isRecordFound value to false
            isRecordFound = false;
            Thread.Sleep(2000);

            //loop through the pages 
            for (int i = 0; i < PaginationButtons.Count - 2; i++)
            {
                Thread.Sleep(2000);

                //Check the Category, Title and Description values for each row in a page, if matches increment the record found variable
                foreach (IWebElement listingRecord in TableRows)
                {
                    string Category = listingRecord.FindElement(By.XPath("td[2]")).Text;
                    string Title = listingRecord.FindElement(By.XPath("td[3]")).Text;
                    string Description = listingRecord.FindElement(By.XPath("td[4]")).Text;
                    if (Category == CategoryToSearch && Title == TitleToSearch && Description == DescriptionToSearch)
                    {
                        isRecordFound = true;
                        return isRecordFound;
                    }

                }

                //If Next button is enable click it to Navigate to next page
                if (NextButton.Enabled == true)
                {
                    NextButton.Click();
                }
            }

            return isRecordFound;

        }
    }
}
