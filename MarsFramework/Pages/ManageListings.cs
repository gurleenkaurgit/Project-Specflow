using MarsFramework.Global;
using NUnit.Framework;
using OpenQA.Selenium;
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
    internal class ManageListings
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

        public ManageListings()
        {
            PageFactory.InitElements(Global.GlobalDefinitions.Driver, this);
        }

        //Navigate to Manage Listing page
        internal void NavigateToManageListing()
        {
            //Wait till Manage Listing is visible
            Extension.WaitForElementDisplayed(GlobalDefinitions.Driver, By.LinkText("Manage Listings"), 5);

            //Click Manage Listing Link
            ManageListingsLink.Click();
        }

        //Check if existing Share SKill is present
        internal void CheckExistingSkillPresent()
        {
            //Navigate to Manage Listings
            NavigateToManageListing();

            //Creating Share Skill Object
            ShareSkill shareSkill = new ShareSkill();
            Thread.Sleep(1000);

            if (TableRows.Count < 1)
            {
                //Add a Share Skill
                shareSkill.AddShareSkill();
            }
        }
        internal void EditShareSkill()
        {
            //Call CheckExistingSkillPresent method to check if any Share Skill is present, if not add a Share Skill
            CheckExistingSkillPresent();

            //Creating Share Skill Object
            ShareSkill shareSkill = new ShareSkill();

            //Populate the excel data
            GlobalDefinitions.ExcelLib.PopulateInCollection(Base.ExcelPathManageListing, "EditShareSkill");

            //Call SearchListings Method to get count for existing records with same category,title and description as we are going to update
            int MatchingRecordsBeforeEdit = SearchListings(GlobalDefinitions.ExcelLib.ReadData(2, "Category"), GlobalDefinitions.ExcelLib.ReadData(2, "Title"), GlobalDefinitions.ExcelLib.ReadData(2, "Description"));

            //Navigate to Manage Listing
            NavigateToManageListing();

            Thread.Sleep(2000);

            //Click the Edit icon 
            Edit.Click();

            //Call EditShareSkillData method to add Edit data
            shareSkill.EditShareSkillData();

            //Call SaveShareSkill Method to save the Share Skill
            shareSkill.SaveShareSkill();

            //Call SearchListings Method to get count for records with same category,title and description as we edited
            int MatchingRecordsAfterEdit = SearchListings(GlobalDefinitions.ExcelLib.ReadData(2, "Category"), GlobalDefinitions.ExcelLib.ReadData(2, "Title"), GlobalDefinitions.ExcelLib.ReadData(2, "Description"));

            //checking if number of records with same category,title and description is 1 more than it has before
            int ExpectedRecords = MatchingRecordsBeforeEdit + 1;
            GlobalDefinitions.ValidateBoolean(ExpectedRecords == MatchingRecordsAfterEdit, "Share Skill Edited");


        }

        //Delete the Share skill
        internal void DeleteShareSkill()
        {
            //Populate the excel data
            GlobalDefinitions.ExcelLib.PopulateInCollection(Base.ExcelPathManageListing, "ManageListings");

            //Get the Category, Title, Description and Action for Deletion
            string CategoryToDelete = GlobalDefinitions.ExcelLib.ReadData(2, "Category");
            string TitleToDelete = GlobalDefinitions.ExcelLib.ReadData(2, "Title");
            string DescriptionToDelete = GlobalDefinitions.ExcelLib.ReadData(2, "Description");
            string Action = GlobalDefinitions.ExcelLib.ReadData(2, "Deleteaction");

            //Navigate to Manage Listing
            NavigateToManageListing();

            //Call SearchListings Method to get count for existing records with same category,title and description as we are going to Delete
            int MatchingRecordsBeforeDelete = SearchListings(CategoryToDelete, TitleToDelete, DescriptionToDelete);
            //Console.WriteLine("MatchingRecordsBeforeDelete== {0}", MatchingRecordsBeforeDelete);
            if (MatchingRecordsBeforeDelete < 1)
            {
                ShareSkill shareSkill = new ShareSkill();
                shareSkill.AddShareSkill();
                MatchingRecordsBeforeDelete = 1;
                //MatchingRecordsBeforeDelete = SearchListings(CategoryToDelete, TitleToDelete, DescriptionToDelete);
            }

            //Navigate to Manage Listing
            NavigateToManageListing();

            //Calling DeleteRecord to delete Share Skill
            DeleteRecord(CategoryToDelete, TitleToDelete, DescriptionToDelete, Action);

            //Call SearchListings Method to get count for existing records with same category,title and description as we have Deleted
            int MatchingRecordsAfterDelete = SearchListings(CategoryToDelete, TitleToDelete, DescriptionToDelete);
            int ExpectedRecords = MatchingRecordsBeforeDelete - 1;

            //checking if number of records with same category,title and description is 1 less than it has before
            GlobalDefinitions.ValidateBoolean(ExpectedRecords == MatchingRecordsAfterDelete, "Share Skill Deleted");

        }

        //Delete the Share SKill 
        internal void DeleteRecord(string CategoryToDelete, string TitleToDelete, string DescriptionToDelete, string Action)
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

                        Extension.WaitForElementDisplayed(GlobalDefinitions.Driver, By.ClassName("ns-box-inner"), 5);

                        Base.Image = SaveScreenShotClass.SaveScreenshot(Driver, "Report");

                        string text = Driver.FindElement(By.ClassName("ns-box-inner")).Text;

                        GlobalDefinitions.ValidateBoolean(text.Contains("has been deleted"), text);

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



        internal int SearchListings(string CategoryToSearch, string TitleToSearch, string DescriptionToSearch)
        {
            //Initialize the Record count to 0
            int RecordFound = 0;
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
                        RecordFound++;
                    }

                }

                //If Next button is enable click it to Navigate to next page
                if (NextButton.Enabled == true)
                {
                    NextButton.Click();
                }
            }

            //Returning the records found
            return RecordFound;

        }
    }
}
