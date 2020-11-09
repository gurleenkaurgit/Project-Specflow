﻿using MarsFramework.Config;
using MarsFramework.Pages;

using MongoDB.Driver.Core.Authentication;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using RelevantCodes.ExtentReports;
using System;
using System.IO;
using TechTalk.SpecFlow;
using static MarsFramework.Global.GlobalDefinitions;

namespace MarsFramework.Global
{
    [Binding]
    class Base
    {
        #region To access Path from resource file
        public static string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory + "..\\..\\");
        public static int Browser = Int32.Parse(MarsResource.Browser);
        public static string ExcelPathShareSkill = path + MarsResource.ExcelPathShareSkill;
        public static string ScreenshotPath = path + MarsResource.ScreenShotPath;
        public static string ReportPath = path + MarsResource.ReportPath;
        public static string ExcelPathManageListing = path + MarsResource.ExcelPathManageListing;
        public static string ExcelPathProfile = path + MarsResource.ExcelPathProfile;
        #endregion

        #region reports
        public static ExtentTest test;
        public static ExtentReports extent;
        #endregion

        public static string Image;

        #region setup and tear down
        [BeforeTestRun]
        public static void Inititalize()
        {

            // advisasble to read this documentation before proceeding http://extentreports.relevantcodes.com/net/
            switch (Browser)
            {

                case 1:
                    GlobalDefinitions.Driver = new FirefoxDriver();
                    break;
                case 2:
                    GlobalDefinitions.Driver = new ChromeDriver();
                    GlobalDefinitions.Driver.Manage().Window.Maximize();
                    break;

            }

            #region Initialise Reports

            extent = new ExtentReports(ReportPath, false, DisplayOrder.NewestFirst);
            extent.LoadConfig(MarsResource.ReportXMLPath);

            #endregion

            if (MarsResource.IsLogin == "true")
            {
                SignIn loginobj = new SignIn();
                loginobj.LoginSteps();
            }
            else
            {
                SignUp obj = new SignUp();
                obj.Register();
            }

        }

        [AfterScenario]
        public void ImageLink()
            {
               Console.WriteLine("NUnit.Framework.TestContext.CurrentContext.Result.Outcome" + NUnit.Framework.TestContext.CurrentContext.Result.Outcome);
            Console.WriteLine("ResultState.Success" + ResultState.Success);

            if (NUnit.Framework.TestContext.CurrentContext.Result.Outcome == ResultState.Success)
            {
                Base.test.Log(LogStatus.Pass, "Image example: " + Base.Image);
            }
}

        [AfterTestRun]
        public static void TearDown()
        {
             // end test. (Reports)
            extent.EndTest(test);
           
            // calling Flush writes everything to the log file (Reports)
            extent.Flush();
            // Close the driver :)            
           // GlobalDefinitions.Driver.Close();
            GlobalDefinitions.Driver.Quit();
        }
        #endregion

    }
}