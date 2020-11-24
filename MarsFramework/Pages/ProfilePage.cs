using MarsFramework.Global;
using MarsFramework.Pages;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using RelevantCodes.ExtentReports;
using SeleniumExtras.PageObjects;
using System;
using System.Collections.Generic;
using System.Threading;
using static MarsFramework.Global.GlobalDefinitions;

namespace MarsFramework
{
    internal class ProfilePage
    {
        #region  Initialize Web Elements
        //Click on Availability Edit Button
        [FindsBy(How = How.XPath, Using = "//strong[text()='Availability']/../..//*[@class='right floated outline small write icon']")]
        private IWebElement AvailabilityEditButton { get; set; }

        //Click Availability Type
        [FindsBy(How = How.Name, Using = "availabiltyType")]
        private IWebElement AvailabilityType { get; set; }

        //Get Availability Type Options
        [FindsBy(How = How.XPath, Using = "//select[@name='availabiltyType']/option[not(@hidden)]")]
        private IList<IWebElement> AvailabilityTypeOptions { get; set; }

        //Click on Hours Edit Button
        [FindsBy(How = How.XPath, Using = "//strong[text()='Hours']/../..//*[@class='right floated outline small write icon']")]
        private IWebElement HoursEditButton { get; set; }

        //Click  Availability Hours
        [FindsBy(How = How.Name, Using = "availabiltyHour")]
        private IWebElement AvailabilityHour { get; set; }

        //Get Availability Hours Options
        [FindsBy(How = How.XPath, Using = "//select[@name='availabiltyHour']/option[not(@hidden)]")]
        private IList<IWebElement> AvailabilityHoursOptions { get; set; }

        //Click on Earn Target Edit Button
        [FindsBy(How = How.XPath, Using = "//strong[text()='Earn Target']/../..//*[@class='right floated outline small write icon']")]
        private IWebElement EarnTargetEditButton { get; set; }

        //Select  Availability Target
        [FindsBy(How = How.Name, Using = "availabiltyTarget")]
        private IWebElement AvailabilityTarget { get; set; }

        //Get Availability Target Options
        [FindsBy(How = How.XPath, Using = "//select[@name='availabiltyTarget']/option[not(@hidden)]")]
        private IList<IWebElement> AvailabilityTargetOptions { get; set; }

        //Edit Description
        [FindsBy(How = How.XPath, Using = "//h3[text()='Description']//i[@class='outline write icon']")]
        private IWebElement EditDescription { get; set; }

        //Enter Description in text area
        [FindsBy(How = How.XPath, Using = "//textarea[@name='value']")]
        private IWebElement EnterDescription { get; set; }

        //Save Description 
        [FindsBy(How = How.XPath, Using = "//h3[text()='Description']/../..//button[text()='Save']")]
        private IWebElement SaveDescription { get; set; }

        //Change Password Drop down link
        [FindsBy(How = How.XPath, Using = "//span[@class='item ui dropdown link '][contains(text(),'Hi')]")]
        private IWebElement ChangePasswordDropDownLink { get; set; }

        //Click Change password
        [FindsBy(How = How.XPath, Using = "//a[text()='Change Password']")]
        private IWebElement ChangePasswordLink { get; set; }

        //Enter Current Password
        [FindsBy(How = How.Name, Using = "oldPassword")]
        private IWebElement CurrentPassword { get; set; }

        //Enter New Password
        [FindsBy(How = How.Name, Using = "newPassword")]
        private IWebElement NewPassword { get; set; }

        //Enter Confirm Password
        [FindsBy(How = How.Name, Using = "confirmPassword")]
        private IWebElement ConfirmPassword { get; set; }

        //Save Change password
        [FindsBy(How = How.XPath, Using = "//form[@autocomplete='new-password']//button[text()='Save']")]
        private IWebElement SaveChangedPassword { get; set; }

        //Click Profile Tab
        [FindsBy(How = How.LinkText, Using = "Profile")]
        private IWebElement ProfileTab { get; set; }

        //Finding the Sign Link
        [FindsBy(How = How.XPath, Using = "//a[contains(text(),'Sign')]")]
        private IWebElement SignIntab { get; set; }

        // Finding the Email Field
        [FindsBy(How = How.Name, Using = "email")]
        private IWebElement Email { get; set; }

        //Finding the Password Field
        [FindsBy(How = How.Name, Using = "password")]
        private IWebElement Password { get; set; }

        //Finding the Login Button
        [FindsBy(How = How.XPath, Using = "//button[contains(text(),'Login')]")]
        private IWebElement LoginBtn { get; set; }

        
        public ProfilePage()
        {
            PageFactory.InitElements(Global.GlobalDefinitions.Driver, this);
        }
        #endregion

        internal void NavigateToProfileTab()
        {
            Thread.Sleep(2000);
           // Extension.WaitForElementDisplayed(Driver, By.LinkText("Profile"), 5);
            Actions action = new Actions(Driver);
            action.MoveToElement(ProfileTab).Click(ProfileTab).Build().Perform();
            
        }

        //Select the Availability in Profile Section
        internal void SelectAvailability()
        {

            Extension.WaitForElementDisplayed(GlobalDefinitions.Driver, By.XPath("//strong[text()='Availability']/../..//*[@class='right floated outline small write icon']"), 5);
            AvailabilityEditButton.Click();
            AvailabilityType.Click();
            foreach (IWebElement AvailabilityTypeOption in AvailabilityTypeOptions)
            {
                if (AvailabilityTypeOption.Text.ToLower() == ExcelLib.ReadData(2, "AvailabilityType").ToLower())
                {
                    AvailabilityTypeOption.Click();
                    Base.Image = SaveScreenShotClass.SaveScreenshot(Driver, "Report");
                    break;
                }
            }

            //Validate message
            Extension.MessageValidation("Availability updated");
        }

        //Validate the selected availablility 
        internal void ValidateAvailabilityType()
        {

            //Get the expected Availability Type value
            string expectedAvailabilityType = ExcelLib.ReadData(2, "AvailabilityType");

            //Get the Actual Availability Type value
            string actualAvailabilityType = Driver.FindElement(By.XPath("//strong[text()='Availability']/../..//div[@class='right floated content']")).Text;

            //Validate the selected Availability Type
            GlobalDefinitions.ValidateFieldData(expectedAvailabilityType, actualAvailabilityType, expectedAvailabilityType +" is Shown in Availability Type Field-");

        }

        //Select the Availability Hour in Profile Section
        internal void SelectHours()
        {
            Extension.WaitForElementDisplayed(GlobalDefinitions.Driver, By.XPath("//strong[text()='Hours']/../..//*[@class='right floated outline small write icon']"), 5);
            HoursEditButton.Click();
            AvailabilityHour.Click();
            foreach (IWebElement AvailabilityHoursOption in AvailabilityHoursOptions)
            {
                if (AvailabilityHoursOption.Text.ToLower() == ExcelLib.ReadData(2, "AvailabilityHours").ToLower())
                {
                    AvailabilityHoursOption.Click();
                    Base.Image = SaveScreenShotClass.SaveScreenshot(Driver, "Report");
                    break;
                }
            }

            //Validate message
            Extension.MessageValidation("Availability updated");
        }

        //Validate selected Availability Hour in Profile Section
        internal void ValidateAvailabilityHours()
        {

            //Get the expected Availability Hour value
            string expectedAvailabilityHours = ExcelLib.ReadData(2, "AvailabilityHours");

            //Get the Actual Availability Hour value
            string actualAvailabilityHours = Driver.FindElement(By.XPath("//strong[text()='Hours']/../..//div[@class='right floated content']")).Text;

            //Validate the selected Availability Type
            GlobalDefinitions.ValidateFieldData(expectedAvailabilityHours, actualAvailabilityHours, expectedAvailabilityHours + " is Shown in Availability Hours Field-");

        }


        //Select the Availability Target in Profile Section
        internal void SelectEarnTarget()
        {
            Extension.WaitForElementDisplayed(GlobalDefinitions.Driver, By.XPath("//strong[text()='Earn Target']/../..//*[@class='right floated outline small write icon']"), 5);
            EarnTargetEditButton.Click();
            AvailabilityTarget.Click();
            foreach (IWebElement AvailabilityTargetOption in AvailabilityTargetOptions)
            {
                if (AvailabilityTargetOption.Text.ToLower() == ExcelLib.ReadData(2, "AvailabilityTarget").ToLower())
                {
                    AvailabilityTargetOption.Click();
                    Base.Image = SaveScreenShotClass.SaveScreenshot(Driver, "Report");
                    break;
                }
            }
            //Validate message
            Extension.MessageValidation("Availability updated");
        }

        //Validated the selected Availability Target 
        internal void ValidateAvailabilityTarget()
        {

            //Get the expected Availability Target
            string expectedAvailabilityTarget = ExcelLib.ReadData(2, "AvailabilityTarget");

            //Get the Actual Availability Target
            string actualAvailabilityTarget = Driver.FindElement(By.XPath("//strong[text()='Earn Target']/../..//div[@class='right floated content']")).Text;

            //Validate the selected Availability Target
            GlobalDefinitions.ValidateFieldData(expectedAvailabilityTarget, actualAvailabilityTarget, expectedAvailabilityTarget + " is Shown in Earn Target Field-");

        }

        //Enter the Description in Profile Section
        internal void AddDescription()
        {
            Extension.WaitForElementDisplayed(GlobalDefinitions.Driver, By.XPath("//h3[text()='Description']//i[@class='outline write icon']"), 5);
            EditDescription.Click();
            Thread.Sleep(1000);
            EnterDescription.Clear();
            EnterDescription.SendKeys(ExcelLib.ReadData(2, "Description"));
            SaveDescription.Click();
            Base.Image = SaveScreenShotClass.SaveScreenshot(Driver, "Report");

            //Validate message
            Extension.MessageValidation("Description has been saved successfully");

        }

        //Validated the Description Entered
        internal void ValidateDescription()
        {
            //Get the expected Description
            string expectedDescription = ExcelLib.ReadData(2, "Description");

            //Get the Actual Description
            string actualDescription = Driver.FindElement(By.XPath("//h3[text()='Description']/../span")).Text;

            //Validate the Entered Description
            GlobalDefinitions.ValidateFieldData(expectedDescription, actualDescription, "Description Updated is Shown-");


        }

        //Change the password
        internal void ChangePassword()
        {
            Extension.WaitForElementDisplayed(GlobalDefinitions.Driver, By.XPath("//span[@class='item ui dropdown link '][contains(text(),'Hi')]"), 8);

            //Move to dropdown list and click Change password
            Actions action = new Actions(Driver);
            action.MoveToElement(ChangePasswordDropDownLink).Build().Perform();
            Extension.WaitForElementDisplayed(GlobalDefinitions.Driver, By.XPath("//a[text()='Change Password']"), 5);
            action.MoveToElement(ChangePasswordLink).Click().Build().Perform();

            //Enter te current, new and confirm password and click save
            CurrentPassword.SendKeys(GlobalDefinitions.ExcelLib.ReadData(2, "Password"));
            NewPassword.SendKeys(GlobalDefinitions.ExcelLib.ReadData(2, "New Password"));
            ConfirmPassword.SendKeys(GlobalDefinitions.ExcelLib.ReadData(2, "Confirm Password"));
            SaveChangedPassword.Click();

            Base.Image = SaveScreenShotClass.SaveScreenshot(Driver, "Report");

            //Validate message
            Extension.MessageValidation("Password Changed Successfully");

        }

        //Validate the password is changed
        internal void ValidateChangedPassword()
        {

            try
            {
                SignIn loginobj = new SignIn();
                loginobj.SignOutSteps();

                //Click on Sign In button
                SignIntab.Click();

                //Enter UserName
                Email.SendKeys(GlobalDefinitions.ExcelLib.ReadData(2, "Username"));

                //Enter the changed Password
                Password.SendKeys(GlobalDefinitions.ExcelLib.ReadData(2, "New Password"));

                //Click Login Button
                LoginBtn.Click();
                Thread.Sleep(5000);

                GlobalDefinitions.ValidateBoolean(ChangePasswordDropDownLink.Displayed, "Logged in with New Password-");

            }
            catch (Exception e)
            {
                Base.test.Log(LogStatus.Fail, "Caught Exception For Change Password", e.Message);
            }
        }

        //Resetting the password
        internal void ResettingPassword()
        {

            ChangePasswordDropDownLink.Click();
            Extension.WaitForElementDisplayed(GlobalDefinitions.Driver, By.XPath("//a[text()='Change Password']"), 5);
            ChangePasswordLink.Click();
            CurrentPassword.SendKeys(GlobalDefinitions.ExcelLib.ReadData(2, "New Password"));
            NewPassword.SendKeys(GlobalDefinitions.ExcelLib.ReadData(2, "Password"));
            ConfirmPassword.SendKeys(GlobalDefinitions.ExcelLib.ReadData(2, "Password"));
            SaveChangedPassword.Click();

            //Validate message
            Extension.MessageValidation("Password Changed Successfully");

        }
        
    }
}