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
        public ManageListings()
        {
            PageFactory.InitElements(Global.GlobalDefinitions.driver, this);
        }

        //Click on Manage Listings Link
        [FindsBy(How = How.LinkText, Using = "Manage Listings")]
        private IWebElement manageListingsLink { get; set; }

        //Get Pagination Buttons
        [FindsBy(How = How.XPath, Using = "//div[@class='ui buttons semantic-ui-react-button-pagination']/button")]
        private IList<IWebElement> paginationButtons { get; set; }

        //Click Next Button
        [FindsBy(How = How.XPath, Using = "//button[contains(text(),'>')]")]
        private IWebElement nextButton { get; set; }

        //Get Listing rows
        [FindsBy(How = How.XPath, Using = "//div[@id='listing-management-section']//table//tbody//tr")]
        private IList<IWebElement> tableRows { get; set; }

        //View the listing
        [FindsBy(How = How.XPath, Using = "(//i[@class='eye icon'])[1]")]
        private IWebElement view { get; set; }

        //Delete the listing
        [FindsBy(How = How.XPath, Using = "//table[1]/tbody[1]")]
        private IWebElement delete { get; set; }

        //Edit the listing
        [FindsBy(How = How.XPath, Using = "(//i[@class='outline write icon'])[1]")]
        private IWebElement edit { get; set; }

        //Click on Yes or No
        [FindsBy(How = How.XPath, Using = "//div[@class='actions']")]
        private IWebElement clickActionsButton { get; set; }

        private string img { get; set; }

        internal void NavigateToManageListing()
        {
            //Wait till Manage Listing is visible
            GlobalDefinitions.WaitForElement(GlobalDefinitions.driver, By.LinkText("Manage Listings"), 5);
            
            //Click Manage Listing Link
            manageListingsLink.Click();
        }

        internal void CheckExistingSkillPresent()
        {
            //Navigate to Manage Listings
            NavigateToManageListing();

            //Creating Share Skill Object
            ShareSkill shareSkill = new ShareSkill();
            Thread.Sleep(1000);
           
            if (tableRows.Count < 1)
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
            edit.Click();

            //Call EnterShareSkillData method to add Edit data
            shareSkill.EnterShareSkillData();

            //Call ValidateShareSkillData Method to Validate entered Share Skill data
           // shareSkill.ValidateShareSkillData();

            //Call SaveShareSkill Method to save the Share Skill
            string img = shareSkill.SaveShareSkill();

            //Call SearchListings Method to get count for records with same category,title and description as we edited
            int MatchingRecordsAfterEdit = SearchListings(GlobalDefinitions.ExcelLib.ReadData(2, "Category"), GlobalDefinitions.ExcelLib.ReadData(2, "Title"), GlobalDefinitions.ExcelLib.ReadData(2, "Description"));
            Console.WriteLine("MatchingRecordsBeforeEdit== {0}", MatchingRecordsBeforeEdit);
            Console.WriteLine("MatchingRecordsBeforeEdit== {0}", MatchingRecordsAfterEdit);

            //checking if number of records with same category,title and description is 1 more than it has before
            int ExpectedRecords = MatchingRecordsBeforeEdit + 1;
            try
            {
                if (ExpectedRecords == MatchingRecordsAfterEdit)
                {
                    Base.test.Log(LogStatus.Pass, "Test Passed, Edited a Share Skill Successfully");
                    Base.test.Log(LogStatus.Pass, "Image-" + img);
                   Assert.IsTrue(true);
                }
                else
                {
                    Base.test.Log(LogStatus.Fail, "Test Failed, Edited a Share Skill Successfully" + img);

                }
            }
            catch (Exception e)
            {
                Base.test.Log(LogStatus.Fail, "Test Failed, Edited a Share Skill Successfully", e.Message);
            }

        }
        internal void DeleteShareSkill()
        {
            //Populate the excel data
            GlobalDefinitions.ExcelLib.PopulateInCollection(Base.ExcelPathManageListing, "ManageListings");
                      
            //Get the Category, Title, Description and Action for Deletion
            string CategoryToDelete = GlobalDefinitions.ExcelLib.ReadData(2, "Category");
            string TitleToDelete = GlobalDefinitions.ExcelLib.ReadData(2, "Title");
            string DescriptionToDelete = GlobalDefinitions.ExcelLib.ReadData(2, "Description");
            string Action= GlobalDefinitions.ExcelLib.ReadData(2, "Deleteaction");

            //Navigate to Manage Listing
            NavigateToManageListing();

            //Call SearchListings Method to get count for existing records with same category,title and description as we are going to Delete
            int MatchingRecordsBeforeDelete = SearchListings(CategoryToDelete, TitleToDelete, DescriptionToDelete);
            Console.WriteLine("MatchingRecordsBeforeDelete== {0}", MatchingRecordsBeforeDelete);
            if (MatchingRecordsBeforeDelete < 1)
            {
                ShareSkill shareSkill = new ShareSkill();
                shareSkill.AddShareSkill();
                MatchingRecordsBeforeDelete= SearchListings(CategoryToDelete, TitleToDelete, DescriptionToDelete);
            }

            //Navigate to Manage Listing
            NavigateToManageListing();

            //Calling DeleteRecord to delete Share Skill
            DeleteRecord(CategoryToDelete, TitleToDelete, DescriptionToDelete,Action);

            //Call SearchListings Method to get count for existing records with same category,title and description as we have Deleted
            int MatchingRecordsAfterDelete = SearchListings(CategoryToDelete, TitleToDelete, DescriptionToDelete);
            int ExpectedRecords = MatchingRecordsBeforeDelete - 1;
            Console.WriteLine("MatchingRecordsBeforeDelete== {0}", MatchingRecordsBeforeDelete);
            Console.WriteLine("MatchingRecordsAfterDelete =={0}", MatchingRecordsAfterDelete);

            //checking if number of records with same category,title and description is 1 less than it has before
            try
            {
                if (ExpectedRecords == MatchingRecordsAfterDelete)
                {
                    Base.test.Log(LogStatus.Pass, "Test Passed, Deleted a Share Skill Successfully");
                    Base.test.Log(LogStatus.Pass, "Image-" + img);
                    Assert.IsTrue(true);
                }
                else
                {
                    Base.test.Log(LogStatus.Fail, "Test Failed, Deleted a Share Skill Successfully" + img);

                }
            }
            catch (Exception e)
            {
                Base.test.Log(LogStatus.Fail, "Test Failed, Deleted a Share Skill Successfully", e.Message);
            }

        }
        internal void DeleteRecord(string CategoryToDelete, string TitleToDelete, string DescriptionToDelete,string Action)
        {
            Thread.Sleep(2000);

            //loop through the pages
            for (int i = 0; i < paginationButtons.Count - 2; i++)
            {
                Thread.Sleep(2000);

                //Check the Category, Title and Description values for each row in a page, if matches delete the record
                foreach (IWebElement listingRecord in tableRows)
                {
                    string Category = listingRecord.FindElement(By.XPath("td[2]")).Text;
                    string Title = listingRecord.FindElement(By.XPath("td[3]")).Text;
                    string Description = listingRecord.FindElement(By.XPath("td[4]")).Text;
                    if (Category == CategoryToDelete && Title == TitleToDelete && Description == DescriptionToDelete)
                    {
                        listingRecord.FindElement(By.CssSelector("i.remove.icon")).Click();
                        clickActionsButton.FindElement(By.XPath("//*[text()='" + Action+ "']")).Click();
                        img = SaveScreenShotClass.SaveScreenshot(GlobalDefinitions.driver, "Report");
                        string text = GlobalDefinitions.driver.FindElement(By.ClassName("ns-box-inner")).Text;
                        try
                        {
                            if (text.Contains("has been deleted"))
                            {
                                Base.test.Log(LogStatus.Pass, text );
                                Assert.IsTrue(true);
                            }
                            else
                            {
                                Base.test.Log(LogStatus.Fail, text);
                            }
                        }
                        catch (Exception e)
                        {
                            Base.test.Log(LogStatus.Fail, text, e.Message);
                        }
                        return;

                    }
                    //If Next button is enable click it to Navigate to next page
                    
                }
                if (nextButton.Enabled == true)
                {
                    nextButton.Click();
                }
            }
            Base.test.Log(LogStatus.Fail, "Did not delete any record ");

        }



        internal int SearchListings(string CategoryToSearch, string TitleToSearch, string DescriptionToSearch)
        {
            //Initialize the Record count to 0
            int RecordFound = 0;
            Thread.Sleep(1000);

            //loop through the pages 
            for (int i = 0; i < paginationButtons.Count - 2; i++)
            {
                Thread.Sleep(1000);

                //Check the Category, Title and Description values for each row in a page, if matches increment the record found variable
                foreach (IWebElement listingRecord in tableRows)
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
                if (nextButton.Enabled == true)
                {
                    nextButton.Click();
                }
            }

            //Returning the records found
            return RecordFound;

        }
    }
}
