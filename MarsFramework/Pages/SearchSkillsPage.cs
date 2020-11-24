using MarsFramework.Global;
using OpenQA.Selenium;
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
    class SearchSkillsPage
    {
        //Search Added Skill
        [FindsBy(How = How.CssSelector, Using = "input[placeholder='Search skills']")]
        private IWebElement SearchSkills { get; set; }

        //All Categories count
        [FindsBy(How = How.XPath, Using = "//b[text()='All Categories']/following-sibling::span")]
        private IWebElement AllCategoriesCount { get; set; }

        //Get Categories List
        [FindsBy(How = How.XPath, Using = "//a[@role='listitem']")]
        private IList<IWebElement> CategoriesList { get; set; }

        public SearchSkillsPage()
        {
            PageFactory.InitElements(Global.GlobalDefinitions.Driver, this);
        }

        //Enter the title in search skill field and press enter
        internal void EnterSkillIntoSearchBox()
        {
            Extension.WaitForElementDisplayed(Driver, By.CssSelector("input[placeholder='Search skills']"), 2);

            SearchSkills.SendKeys(GlobalDefinitions.ExcelLib.ReadData(2, "Title") + "\n");
        }

        
        internal void ClickOnCategory()
        {
            foreach (IWebElement Category in CategoriesList)
            {
                Thread.Sleep(2000);
                string CategoryValue = Category.Text.Replace(Category.FindElement(By.XPath("./*")).Text, " ").TrimEnd();

                if (CategoryValue.ToLower() == GlobalDefinitions.ExcelLib.ReadData(2, "Category").ToLower())
                {
                    Category.Click();
                    Base.Image = SaveScreenShotClass.SaveScreenshot(Driver, "Report");
                    break;
                }
            }
        }

        internal void ClickOnSubCategory()
        {
            ClickOnCategory();
            IList<IWebElement> SubCategoryList = Driver.FindElements(By.XPath("//a[@role='listitem'][@class='item subcategory']"));
            foreach (IWebElement SubCategory in SubCategoryList)
            {
                string SubCategoryValue = SubCategory.Text.Replace(SubCategory.FindElement(By.XPath("./*")).Text, "").TrimEnd();
                if (SubCategoryValue.ToLower() == GlobalDefinitions.ExcelLib.ReadData(2, "SubCategory").ToLower())
                {
                    SubCategory.Click();
                    Base.Image = SaveScreenShotClass.SaveScreenshot(Driver, "Report");
                    break;
                }

            }
        }
        internal void ValidateEnteredSkillBySubCategory()
        {

            //Verify the Sub Category Count is greater than 0
            bool isSubCategoryCount = int.Parse(Driver.FindElement(By.CssSelector(".active.item.subcategory > span")).Text) > 0;
            GlobalDefinitions.ValidateBoolean(isSubCategoryCount, "Sub Category Count is greater than 0-");

            ValidateRecordData();

        }

        internal void ValidateEnteredSkillByAllCategories()
        {
            Extension.WaitForElementDisplayed(Driver, By.XPath("//b[text()='All Categories']/following-sibling::span"), 2);
            Thread.Sleep(2000);

            Base.Image = SaveScreenShotClass.SaveScreenshot(Driver, "Report");

            //Validate if All categories count is greater than 0
            GlobalDefinitions.ValidateBoolean(int.Parse(AllCategoriesCount.Text) > 0, "All Categories shown");

            ValidateRecordData();
        }

        internal void ValidateRecordData()
        {
            //Click the First Searched Element to view the details
            Driver.FindElement(By.ClassName("service-info")).Click();

            Base.Image = SaveScreenShotClass.SaveScreenshot(Driver, "Report");

            //Verify the Clicked record contains the searched Skill
            bool isRecordFound = Driver.FindElement(By.CssSelector(".ten.wide.column > .ui.fluid.card > .content")).Text.Contains(GlobalDefinitions.ExcelLib.ReadData(2, "Title"));
            GlobalDefinitions.ValidateBoolean(isRecordFound, "Searched Text Exists in Record Found-");

        }
    }
}

