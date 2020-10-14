using Excel;
using OpenQA.Selenium;
using System;

using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using OpenQA.Selenium.Support.UI;
using MarsFramework.Config;
using OpenQA.Selenium.Interactions;
using System.Reflection.Emit;
using NUnit.Framework;
using NUnit.Core;
using System.Security.Permissions;
using System.Reflection;
using SeleniumExtras.PageObjects;
using RelevantCodes.ExtentReports;

namespace MarsFramework.Global
{
    class GlobalDefinitions
    {
        public GlobalDefinitions()
        {
            PageFactory.InitElements(driver, this);
        }
        
        //Initialise the browser
        public static IWebDriver driver { get; set; }

        //Navigate to URL
        public static void NavigateUrl()
        {
            driver.Navigate().GoToUrl(GlobalDefinitions.ExcelLib.ReadData(2, "Url"));
        }

        //Generic method for drop-down selection
        public static void SelectDropDown(IWebElement dropDownElement, String selectBY, String bY )
        {
            SelectElement dropDown = new SelectElement(dropDownElement);
            dropDown.SelectByIndex(0);
            if (selectBY.ToLower() == "SelectByText".ToLower())
            {
                dropDown.SelectByText(bY);
            }
            else if (selectBY.ToLower() == "SelectByValue".ToLower())
            {
                dropDown.SelectByValue(bY);
            }
            else if (selectBY.ToLower() == "SelectByIndex".ToLower())
            {
                dropDown.SelectByIndex(int.Parse(bY));
            }
        }

        //Generic method to click Radio Button
        public static void SelectRadioButton(IList<IWebElement> radioElementsList, String radioButtonToClick, By locator)
        {
            foreach (IWebElement element in radioElementsList)
            {
                if (element.Text.ToLower() == radioButtonToClick.ToLower())
                {
                    element.FindElement(locator).Click();
                    break;
                }
            }

        }
       
        //Generic method to Validate Text fields data
        public static void ValidateFieldData(String expectedValue, String actualValue, String fieldName)
        {
            try
            {
                if (expectedValue.ToUpper() == actualValue.ToUpper())
                {
                    Base.test.Log(LogStatus.Pass, fieldName + " Entered Successfully");
                    Assert.IsTrue(true);
                }
                else
                    Base.test.Log(LogStatus.Fail, fieldName + " Entered Failed, Image - " + SaveScreenShotClass.SaveScreenshot(GlobalDefinitions.driver, "Report"));
            }
            catch (Exception e)
            {
                Base.test.Log(LogStatus.Fail, "Caught Exception For " + fieldName, e.Message);
            }

        }

        //Generic method to Validate dropdown fields data
        public static void ValidateDropDown(IWebElement dropDownElement, String expectedValue, String dropDownFieldName)
        {
            try
            {
                SelectElement DropDown = new SelectElement(dropDownElement);

                if (DropDown.SelectedOption.Text.ToUpper() == expectedValue.ToUpper())
                {
                    Base.test.Log(LogStatus.Pass, dropDownFieldName + " Selected Successfully");
                    Assert.IsTrue(true);
                }

                else
                    Base.test.Log(LogStatus.Fail, dropDownFieldName + " Selection Failed, Image - " + SaveScreenShotClass.SaveScreenshot(GlobalDefinitions.driver, "Report"));
            }
            catch (Exception e)
            {
                Base.test.Log(LogStatus.Fail, "Caught Exception For " + dropDownFieldName, e.Message);
            }

        }

        //Generic method to Validate Radio Button data
        public static void ValidateRadioButton(IList<IWebElement> radioElementsList, By locator, String expectedValue, String radiobuttonsFieldName)
        {
            try
            {
                foreach (IWebElement Element in radioElementsList)
                    if (Element.FindElement(locator).Selected)
                    {
                        if (Element.Text.ToUpper() == expectedValue.ToUpper())
                        {
                            Base.test.Log(LogStatus.Pass, radiobuttonsFieldName + " Selected Successfully");
                            Assert.IsTrue(true);
                        }
                        else
                            Base.test.Log(LogStatus.Fail, radiobuttonsFieldName + " Selection Failed, Image - " + SaveScreenShotClass.SaveScreenshot(GlobalDefinitions.driver, "Report"));
                    }
            }
            catch (Exception e)
            {
                Base.test.Log(LogStatus.Fail, "Caught Exception For " + radiobuttonsFieldName, e.Message);
            }
        }

        


        #region Excel 
        public class ExcelLib
        {
            static List<Datacollection> dataCol = new List<Datacollection>();

            public class Datacollection
            {
                public int rowNumber { get; set; }
                public string colName { get; set; }
                public string colValue { get; set; }
            }


            public static void ClearData()
            {
                dataCol.Clear();
            }


            private static DataTable ExcelToDataTable(string fileName, string SheetName)
            {
                // Open file and return as Stream
                using (System.IO.FileStream stream = File.Open(fileName, FileMode.Open, FileAccess.Read))
                {
                    using (IExcelDataReader excelReader = ExcelReaderFactory.CreateOpenXmlReader(stream))
                    {
                        excelReader.IsFirstRowAsColumnNames = true;

                        //Return as dataset
                        DataSet result = excelReader.AsDataSet();
                        //Get all the tables
                        DataTableCollection table = result.Tables;

                        // store it in data table
                        DataTable resultTable = table[SheetName];

                        //excelReader.Dispose();
                        //excelReader.Close();
                        // return
                        return resultTable;
                    }
                }
            }

            public static string ReadData(int rowNumber, string columnName)
            {
                try
                {
                    //Retriving Data using LINQ to reduce much of iterations

                    rowNumber = rowNumber - 1;
                    string data = (from colData in dataCol
                                   where colData.colName == columnName && colData.rowNumber == rowNumber
                                   select colData.colValue).SingleOrDefault();

                    //var datas = dataCol.Where(x => x.colName == columnName && x.rowNumber == rowNumber).SingleOrDefault().colValue;


                    return data.ToString();
                }

                catch (Exception e)
                {
                    //Added by Kumar
                    Console.WriteLine("Exception occurred in ExcelLib Class ReadData Method!" + Environment.NewLine + e.Message.ToString());
                    return null;
                }
            }

            public static void PopulateInCollection(string fileName, string SheetName)
            {
                ExcelLib.ClearData();
                DataTable table = ExcelToDataTable(fileName, SheetName);

                //Iterate through the rows and columns of the Table
                for (int row = 1; row <= table.Rows.Count; row++)
                {
                    for (int col = 0; col < table.Columns.Count; col++)
                    {
                        Datacollection dtTable = new Datacollection()
                        {
                            rowNumber = row,
                            colName = table.Columns[col].ColumnName,
                            colValue = table.Rows[row - 1][col].ToString()
                        };


                        //Add all the details for each row
                        dataCol.Add(dtTable);

                    }
                }

            }
        }

        #endregion

        #region screenshots
        public class SaveScreenShotClass
        {
            public static string SaveScreenshot(IWebDriver driver, string ScreenShotFileName) // Definition
            {
                var folderLocation = (Base.ScreenshotPath);

                if (!System.IO.Directory.Exists(folderLocation))
                {
                    System.IO.Directory.CreateDirectory(folderLocation);
                }

                var screenShot = ((ITakesScreenshot)driver).GetScreenshot();
                var fileName = new StringBuilder(folderLocation);

                fileName.Append(ScreenShotFileName);
                fileName.Append(DateTime.Now.ToString("_dd-mm-yyyy_mss"));
                //fileName.Append(DateTime.Now.ToString("dd-mm-yyyym_ss"));
                fileName.Append(".jpeg");
                screenShot.SaveAsFile(fileName.ToString(), ScreenshotImageFormat.Jpeg);
                return fileName.ToString();
            }
        }
        #endregion
    }
}
