using MarsFramework.Global;
using MarsFramework.Pages;
using NUnit.Framework;
using RelevantCodes.ExtentReports;
using System;

namespace MarsFramework
{
    public class Program
    {
        [TestFixture]
        [Category("Sprint1")]
        class User : Global.Base
        {

            [Test, Order(1)]
            public void AddShareSkillTest()
            {
                // Start Add test. (Reports)
                test = extent.StartTest("Create Share Skill");

                //Call AddShareSkill Mehthod to Add the Share Skill
                ShareSkill shareSkill = new ShareSkill();
                shareSkill.AddShareSkill();
            }


            [Test, Order(2)]
            public void EditShareSkillTest()
            {
                // Start Edit test. (Reports)
                test = extent.StartTest("Edit Share Skill");

                //Call EditShareSkill Method to Edit the Share Skill 
                ManageListings manageListings = new ManageListings();
                manageListings.EditShareSkill();
            }


            [Test, Order(3)]
            public void DeleteShareSkillTest()
            {
                // Start Delete test. (Reports)
                test = extent.StartTest("Delete Share Skill");

                //Call DeleteShareSkill Method to Delete the Share Skill
                ManageListings manageListings = new ManageListings();
                manageListings.DeleteShareSkill();

            }

        }
    }
}