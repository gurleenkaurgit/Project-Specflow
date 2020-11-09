using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using RelevantCodes.ExtentReports;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static MarsFramework.Global.GlobalDefinitions;

namespace MarsFramework.Global
{
    public static class Extension
    {
        public static bool WaitForElementDisplayed(IWebDriver driver, By by, int timeOutinSeconds)
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeOutinSeconds));
            return (wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(by)).Displayed);
        }

        public static IWebElement WaitForElementClickable(IWebElement element, IWebDriver driver, int timeOutinSeconds)
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeOutinSeconds));
            return wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(element));
        }

        //MessageValidation method will matches the expected pop up message with actual pop up message
        public static void MessageValidation(string expectedMessage)
        {
            
                WaitForElementDisplayed(Driver, By.ClassName("ns-box-inner"), 2);
                IWebElement Message = Driver.FindElement(By.ClassName("ns-box-inner"));
                string actualMessage = Message.Text;
                Driver.FindElement(By.XPath("//a[@class='ns-close']")).Click();
                if (expectedMessage == actualMessage)
                {
                    Base.test.Log(LogStatus.Pass, expectedMessage);
                    Assert.AreEqual(expectedMessage, actualMessage);
                }
                else
                {
                    Base.test.Log(LogStatus.Fail, actualMessage+ " Image example: " + Base.Image);
                    Assert.AreEqual(expectedMessage, actualMessage);
                }

            

        }


    }
}
