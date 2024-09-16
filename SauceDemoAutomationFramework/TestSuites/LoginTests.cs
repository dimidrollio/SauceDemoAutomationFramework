using NUnit.Framework;
using RazorEngine.Compilation;
using SauceDemoAutomationFramework.Driver;
using SauceDemoAutomationFramework.Models;
using SauceDemoAutomationFramework.Pages;
using SauceDemoAutomationFramework.Services;

namespace SauceDemoAutomationFramework.TestSuites
{
    public class LoginTests : CommonConditions
    {
        [Test]
        public void TC001_TestAvailableUsers()
        {
            LoginPage page = new(DriverInstance.GetInstance());
            page.OpenPage();
            var expectedUsers = page.GetAvailableUsers();

            TestDataConverter.UsersToJSON(expectedUsers);
            var actualUsers = TestDataConverter.GetUsersFromJSON();

            Assert.That(actualUsers, Is.EqualTo(expectedUsers));
        }

        [Test]
        [TestCaseSource(typeof(TestDataConverter), nameof(TestDataConverter.GetLoginTestData))]
        public void TC002_TestLoginWithNoPassword(User user, string browser)
        {
            DriverInstance.SetWebDriver(browser);
			LoginPage page = new(DriverInstance.GetInstance());

			page.OpenPage().LoginWithNoPassword(user);
            string actualErrorMessage = page.GetErrorMessageBoxText();
            string expectedErrorMessage = "Epic sadface: Password is required";

			Assert.That(actualErrorMessage, Is.EqualTo(expectedErrorMessage));
		}

		[Test]
		[TestCaseSource(typeof(TestDataConverter), nameof(TestDataConverter.GetLoginTestData))]
		public void TC002_TestLoginWithValidCredentials(User user, string browser)
		{
			DriverInstance.SetWebDriver(browser);
			LoginPage page = new(DriverInstance.GetInstance());

			page.OpenPage().LoginWithValidCredentials(user);
			string actualErrorMessage = page.GetErrorMessageBoxText();
			string expectedErrorMessage = "Epic sadface: Password is required";

			Assert.That(actualErrorMessage, Is.EqualTo(expectedErrorMessage));
		}
	}
}
