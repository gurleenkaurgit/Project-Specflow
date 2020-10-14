using MarsFramework.Global;
using MarsFramework.Pages;
using NUnit.Framework;
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
    internal class Profile
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


        public Profile()
        {
            PageFactory.InitElements(Global.GlobalDefinitions.driver, this);
        }
        #endregion

        internal void NavigateToProfileTab()
        {
            Extension.WaitForElementDisplayed(driver, By.LinkText("Profile"), 2);
            ProfileTab.Click();
        }

        //Select the Availability in Profile Section
        internal void SelectAvailability()
        {
            Extension.WaitForElementDisplayed(GlobalDefinitions.driver, By.XPath("//strong[text()='Availability']/../..//*[@class='right floated outline small write icon']"), 5);
            AvailabilityEditButton.Click();
            AvailabilityType.Click();
            foreach (IWebElement AvailabilityTypeOption in AvailabilityTypeOptions)
            {
                if (AvailabilityTypeOption.Text.ToLower() == ExcelLib.ReadData(2, "AvailabilityType").ToLower())
                {
                    AvailabilityTypeOption.Click();
                    break;
                }
            }

            //Validate message
            Extension.MessageValidation("Availability updated");
        }
        internal void ValidateAvailabilityType()
        {
            try
            {
                //Get the expected Availability value
                string expectedAvailabilityType = ExcelLib.ReadData(2, "AvailabilityType");

                //Get the Actual Availability value
                string actualAvailabilityType = driver.FindElement(By.XPath("//strong[text()='Availability']/../..//div[@class='right floated content']")).Text;

                if(expectedAvailabilityType== actualAvailabilityType)
                {
                    Base.test.Log(LogStatus.Pass, "Availability Type Selected Successful");
                    SaveScreenShotClass.SaveScreenshot(driver, "Select Availability Type");
                    Assert.IsTrue(true);
                }
                else
                {
                    Base.test.Log(LogStatus.Fail, "Select Availability Type-Test Failed");
                    Assert.IsTrue(false);
                }

            }
            catch (Exception e)
            {
                Base.test.Log(LogStatus.Fail, "Caught Exception For Availability Type", e.Message);
            }


        }

        //Select the Availability Hour in Profile Section
        internal void SelectHours()
        {
            Extension.WaitForElementDisplayed(GlobalDefinitions.driver, By.XPath("//strong[text()='Hours']/../..//*[@class='right floated outline small write icon']"), 5);
            HoursEditButton.Click();
            AvailabilityHour.Click();
            foreach (IWebElement AvailabilityHoursOption in AvailabilityHoursOptions)
            {
                if (AvailabilityHoursOption.Text.ToLower() == ExcelLib.ReadData(2, "AvailabilityHours").ToLower())
                {
                    AvailabilityHoursOption.Click();
                    break;
                }
            }

            //Validate message
            Extension.MessageValidation("Availability updated");
        }

        internal void ValidateAvailabilityHours()
        {
            try
            {
                //Get the expected Availability value
                string expectedAvailabilityHours = ExcelLib.ReadData(2, "AvailabilityHours");

                //Get the Actual Availability value
                string actualAvailabilityHours = driver.FindElement(By.XPath("//strong[text()='Hours']/../..//div[@class='right floated content']")).Text;

                if (expectedAvailabilityHours == actualAvailabilityHours)
                {
                    Base.test.Log(LogStatus.Pass, "Availability Hours Selected Successful");
                    SaveScreenShotClass.SaveScreenshot(driver, "Select Availability Hours");
                    Assert.IsTrue(true);
                }
                else
                {
                    Base.test.Log(LogStatus.Fail, "Select Availability Hours-Test Failed");
                    Assert.IsTrue(false);
                }

            }
            catch (Exception e)
            {
                Base.test.Log(LogStatus.Fail, "Caught Exception For Availability Hours", e.Message);
            }


        }

        //Select the Availability Target in Profile Section
        internal void SelectEarnTarget()
        {
            Extension.WaitForElementDisplayed(GlobalDefinitions.driver, By.XPath("//strong[text()='Earn Target']/../..//*[@class='right floated outline small write icon']"), 5);
            EarnTargetEditButton.Click();
            AvailabilityTarget.Click();
            foreach (IWebElement AvailabilityTargetOption in AvailabilityTargetOptions)
            {
                if (AvailabilityTargetOption.Text.ToLower() == ExcelLib.ReadData(2, "AvailabilityTarget").ToLower())
                {
                    AvailabilityTargetOption.Click();
                    break;
                }
            }
            //Validate message
            Extension.MessageValidation("Availability updated");
        }

        internal void ValidateAvailabilityTarget()
        {
            try
            {
                //Get the expected Availability Target
                string expectedAvailabilityTarget = ExcelLib.ReadData(2, "AvailabilityTarget");

                //Get the Actual Availability Target
                string actualAvailabilityTarget = driver.FindElement(By.XPath("//strong[text()='Earn Target']/../..//div[@class='right floated content']")).Text;

                if (expectedAvailabilityTarget == actualAvailabilityTarget)
                {
                    Base.test.Log(LogStatus.Pass, "Availability Target Selected Successful");
                    SaveScreenShotClass.SaveScreenshot(driver, "Select Availability Target");
                    Assert.IsTrue(true);
                }
                else
                {
                    Base.test.Log(LogStatus.Fail, "Select Availability Target-Test Failed");
                    Assert.IsTrue(false);
                }

            }
            catch (Exception e)
            {
                Base.test.Log(LogStatus.Fail, "Caught Exception For Availability Target", e.Message);
            }


        }

        //Enter the Description in Profile Section
        internal void AddDescription()
        {
            EditDescription.Click();
            EnterDescription.Clear();
            EnterDescription.SendKeys(ExcelLib.ReadData(2, "Description"));
            SaveDescription.Click();

            //Validate message
            Extension.MessageValidation("Description has been saved successfully");

        }
        internal void ValidateDescription()
        {
            try
            {
                //Get the expected Description
                string expectedDescription = ExcelLib.ReadData(2, "Description");

                //Get the Actual Description
                string actualDescription = driver.FindElement(By.XPath("//h3[text()='Description']/../span")).Text;

                if (expectedDescription == actualDescription)
                {
                    Base.test.Log(LogStatus.Pass, "Description Entered Successful");
                    SaveScreenShotClass.SaveScreenshot(driver, "Enter Description");
                    Assert.IsTrue(true);
                }
                else
                {
                    Base.test.Log(LogStatus.Fail, "Enter Description-Test Failed");
                    Assert.IsTrue(false);
                }

            }
            catch (Exception e)
            {
                Base.test.Log(LogStatus.Fail, "Caught Exception For Enter Description", e.Message);
            }


        }

        //Change the password
        internal void ChangePassword()
        {
            Extension.WaitForElementDisplayed(GlobalDefinitions.driver, By.XPath("//span[@class='item ui dropdown link '][contains(text(),'Hi')]"), 8);
            Actions action = new Actions(driver);
            action.MoveToElement(ChangePasswordDropDownLink).Build().Perform();
            Extension.WaitForElementDisplayed(GlobalDefinitions.driver, By.XPath("//a[text()='Change Password']"), 5);
           // IWebElement ChangePasswordLink = driver.FindElement(By.XPath("//a[text()='Change Password']"));
           // ChangePasswordLink.Click();
           action.MoveToElement(ChangePasswordLink).Click().Build().Perform();
           
           //Extension.WaitForElementDisplayed(GlobalDefinitions.driver, By.Name("oldPassword"),5);

            CurrentPassword.SendKeys(GlobalDefinitions.ExcelLib.ReadData(2, "Password"));
            NewPassword.SendKeys(GlobalDefinitions.ExcelLib.ReadData(2, "New Password"));
            ConfirmPassword.SendKeys(GlobalDefinitions.ExcelLib.ReadData(2, "Confirm Password"));
            SaveChangedPassword.Click();

            //Validate message
            Extension.MessageValidation("Password Changed Successfully");


        }
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
                //Extension.WaitForElementDisplayed(GlobalDefinitions.driver, By.XPath("//span[@class='item ui dropdown link '][contains(text(),'Hi')]"), 8);
                if (ChangePasswordDropDownLink.Displayed)
                {
                    Base.test.Log(LogStatus.Pass, "Password Changed Successful");
                    SaveScreenShotClass.SaveScreenshot(driver, "Change Password");
                    Assert.IsTrue(true);
                }
                else
                {
                    Base.test.Log(LogStatus.Fail, "Change Password-Test Failed");
                    Assert.IsTrue(false);
                    //Assert.assertEquals(true, rxBtn.isDisplayed());

                }
            }
            catch (Exception e)
            {
                Base.test.Log(LogStatus.Fail, "Caught Exception For Change Password", e.Message);
            }
            ChangePasswordDropDownLink.Click();
            Extension.WaitForElementDisplayed(GlobalDefinitions.driver, By.XPath("//a[text()='Change Password']"), 5);
            ChangePasswordLink.Click();
            CurrentPassword.SendKeys(GlobalDefinitions.ExcelLib.ReadData(2, "New Password"));
            NewPassword.SendKeys(GlobalDefinitions.ExcelLib.ReadData(2, "Password"));
            ConfirmPassword.SendKeys(GlobalDefinitions.ExcelLib.ReadData(2, "Password"));
            SaveChangedPassword.Click();

        }
    }
}