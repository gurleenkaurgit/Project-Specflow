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
            [Test]
            public void AddProfileDetailsTest()
            {
                // Start Add test. (Reports)
                test = extent.StartTest("Add Profile Details Test");

                //Populate the excel data
                GlobalDefinitions.ExcelLib.PopulateInCollection(Base.ExcelPathProfile, "Profile");

                //Call SelectAvailability Method to select an availability Type
                Profile profile = new Profile();
                profile.SelectAvailability();
                profile.ValidateAvailabilityType();

                //Call SelectHours Method to select Hours
                profile.SelectHours();
                profile.ValidateAvailabilityHours();

                //Call SelectEarnTarget Method to select Earn Target
                profile.SelectEarnTarget();
                profile.ValidateAvailabilityTarget();

                //Call AddDescription Method to add description
                profile.AddDescription();
                profile.ValidateDescription();
            }

            [Test]
            public void ChangePasswordTest()
            {
                // Start Add test. (Reports)
                test = extent.StartTest("Change Password");

                //Populate the excel data
                GlobalDefinitions.ExcelLib.PopulateInCollection(Base.ExcelPathShareSkill, "SignIn");

                Profile profile = new Profile();
               // profile.NavigateToProfileTab();
                profile.ChangePassword();
                profile.ValidateChangedPassword();
            }

             [Test]
            public void LanguageTest()
            {
                // Start Add test. (Reports)
                test = extent.StartTest("Add, Update and Delete Language");

                //Populate the excel data
                GlobalDefinitions.ExcelLib.PopulateInCollection(Base.ExcelPathProfile, "Language");

                Language language = new Language();
                language.NavigateToLanguageTab();

                //Add and Validate added Language
                language.AddNewLanguage();
                language.ValidateAddedLanguage();

                //Update and validate updated Language
                language.UpdateLanguage();
                language.ValidateUpdateLanguage();

                //Delete and Validate deleted Language
                language.DeleteLanguage();
                language.ValidateDeleteLanguage();

            }
            [Test]
            public void SkillTest()
            {
                // Start Add test. (Reports)
                test = extent.StartTest("Add, Update and delete Skill");

                //Populate the excel data
                GlobalDefinitions.ExcelLib.PopulateInCollection(Base.ExcelPathProfile, "Skill");

                Skills skill = new Skills();
                skill.NavigateToSkillTab();

                //Add and Validate added Skill
                skill.AddNewSkill();
                skill.ValidateAddedSkill();

                //Update and validate updated Skill
                skill.UpdateSkill();
                skill.ValidateUpdateSkill();

                //Delete and Validate deleted Skill
                skill.DeleteSkill();
                skill.ValidateDeleteSkill();

            }
            [Test]
            public void EducationTest()
            {
                // Start Add test. (Reports)
                test = extent.StartTest("Add, Update and Delete Education");

                //Populate the excel data
                GlobalDefinitions.ExcelLib.PopulateInCollection(Base.ExcelPathProfile, "Education");

                Education education = new Education();
                education.NavigateToEducationTab();

                //Add and Validate added Education
                education.AddNewEducation();
                education.ValidateAddedEducation();

                //Update and validate updated Education
                education.UpdateEducation();
                education.ValidateUpdateEducation();

                //Delete and Validate deleted Education
                education.DeleteEducation();
                education.ValidateDeleteEducation();

            }

            [Test]
            public void CertificationsTest()
            {
                // Start Add test. (Reports)
                test = extent.StartTest("Add, Update and Delete Certification");

                //Populate the excel data
                GlobalDefinitions.ExcelLib.PopulateInCollection(Base.ExcelPathProfile, "Certification");

                Certifications certification = new Certifications();
                certification.NavigateToCertificationTab();

                //Add and Validate added Certification
                certification.AddNewCertification();
                certification.ValidateAddedCertification();


                //Update and validate updated Certification
                certification.UpdateCertification();
                certification.ValidateUpdateCertification();

                //Delete and Validate deleted Certification
                certification.DeleteCertification();
                certification.ValidateDeleteCertification();

            }

            [Test]
            public void AddShareSkillTest()
            {
                // Start Add test. (Reports)
                test = extent.StartTest("Create Share Skill");

                //Call AddShareSkill Method to Add the Share Skill
                ShareSkill shareSkill = new ShareSkill();
                shareSkill.AddShareSkill();
            }


            [Test]
            public void EditShareSkillTest()
            {
                // Start Edit test. (Reports)
                test = extent.StartTest("Edit Share Skill");

                //Call EditShareSkill Method to Edit the Share Skill 
                ManageListings manageListings = new ManageListings();
                manageListings.EditShareSkill();
            }


            [Test]
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