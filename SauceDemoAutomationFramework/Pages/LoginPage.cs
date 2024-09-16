using AventStack.ExtentReports;
using OpenQA.Selenium;
using SauceDemoAutomationFramework.Driver;
using SauceDemoAutomationFramework.Models;
using SauceDemoAutomationFramework.Services;
using SauceDemoAutomationFramework.WebElements;

namespace SauceDemoAutomationFramework.Pages
{
	public class LoginPage
	{
		private readonly IWebDriver _driver;
        private readonly string pageUrl = GetEnvironmentData.GetLoginURL();

        public LoginPage(IWebDriver driver)
        {
            _driver = driver;
        }

        private readonly string usernameField = "//input[@id = \"user-name\"]";
        private readonly string passwordField = "//input[@id = \"password\"]";
        private readonly string errorMessage = "//div[@class=\"error-message-container error\"]/*[text()]";
        private readonly string submitButton = "//input[@id=\"login-button\"]";
        private readonly string acceptedUsernames = "//*[@id = \"login_credentials\"]";
        private readonly string acceptedPasswords = "//*[@class= \"login_password\"]";
	
        public LoginPage OpenPage()
        {
            ActionsElements.Navigate(_driver, pageUrl);
            ActionsElements.WaitForPageLoad(_driver);
            return this;
        }
        public IEnumerable<User> GetAvailableUsers()
        {
            var usernamesString = ActionsElements.WaitForElementToDisplay(_driver, By.XPath(acceptedUsernames)).Text;
            var passwordsString = ActionsElements.WaitForElementToDisplay(_driver, By.XPath(acceptedPasswords)).Text;

            List<User> result = [];

			IEnumerable<string> usernames = usernamesString.Split(separator: '\n', options: StringSplitOptions.TrimEntries).Skip(1);
            IEnumerable<string> passwords = passwordsString.Split(separator: '\n', options: StringSplitOptions.TrimEntries).Skip(1);
			foreach (string username in usernames)
            {
                foreach (string password in passwords)
                {
                    var user = new User() {
                        Username = username,
                        Password = password
                    };
                    result.Add(user);
                }
            }

            return result;
        }

        public string GetErrorMessageBoxText()
        {
			var errorMessageElement = ActionsElements.WaitForElementToDisplay(_driver, By.XPath(errorMessage));
            return errorMessageElement.Text;
		}
		public LoginPage LoginWithNoPassword(User user)
        {
            var usernameElement = ActionsElements.WaitForElementToDisplay(_driver, By.XPath(usernameField));
            var loginElement = ActionsElements.WaitForElementToDisplay(_driver, By.XPath(passwordField));
			ActionsElements.ClearInput(_driver, By.XPath(usernameField));
			ActionsElements.ClearInput(_driver, By.XPath(passwordField));

			usernameElement.SendKeys(user.Username);
            loginElement.SendKeys(user.Password);

            ActionsElements.ClearInput(_driver, By.XPath(passwordField));
            ActionsElements.Click(_driver, By.XPath(submitButton));
            return this;
        }

		public LoginPage LoginWithValidCredentials(User user)
		{
			var usernameElement = ActionsElements.WaitForElementToDisplay(_driver, By.XPath(usernameField));
			var loginElement = ActionsElements.WaitForElementToDisplay(_driver, By.XPath(passwordField));
			ActionsElements.ClearInput(_driver, By.XPath(usernameField));
			ActionsElements.ClearInput(_driver, By.XPath(passwordField));

			usernameElement.SendKeys(user.Username);
			loginElement.SendKeys(user.Password);

			ActionsElements.Click(_driver, By.XPath(submitButton));
			return this;
		}

		public LoginPage LoginWithNoCredentials(User user)
		{
			var usernameElement = ActionsElements.WaitForElementToDisplay(_driver, By.XPath(usernameField));
			var loginElement = ActionsElements.WaitForElementToDisplay(_driver, By.XPath(passwordField));
			ActionsElements.ClearInput(_driver, By.XPath(usernameField));
			ActionsElements.ClearInput(_driver, By.XPath(passwordField));

			usernameElement.SendKeys(user.Username);
			loginElement.SendKeys(user.Password);

            ActionsElements.ClearInput(_driver, By.XPath(usernameField));
			ActionsElements.ClearInput(_driver, By.XPath(passwordField));

			ActionsElements.Click(_driver, By.XPath(submitButton));
			return this;
		}
	}
}
