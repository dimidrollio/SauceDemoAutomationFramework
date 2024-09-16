using Newtonsoft.Json;
using OpenQA.Selenium.DevTools.V126.Browser;
using SauceDemoAutomationFramework.Constants;
using SauceDemoAutomationFramework.Driver;
using SauceDemoAutomationFramework.Models;

namespace SauceDemoAutomationFramework.Services
{
	public class TestDataConverter
	{
		public static void UsersToJSON(IEnumerable<User> users)
		{
			var jsonString = JsonConvert.SerializeObject(users, Formatting.Indented);
			File.WriteAllText(WebConstants.projectDirectory + "\\Resources\\Users.json", jsonString);
		}

		public static IEnumerable<User> GetUsersFromJSON()
		{
			string path = WebConstants.projectDirectory + "\\Resources\\Users.json";

			string json = File.ReadAllText(path) ?? throw new Exception("File does not exist...");

			var users = JsonConvert.DeserializeObject<List<User>>(json);

			return users is null ? throw new Exception("Could not deserialize file...") : (IEnumerable<User>)users;
		}

		public static IEnumerable<object[]> GetLoginTestData()
		{
			List<object[]> testData = [];
			foreach (var user in GetUsersFromJSON())
			{
				foreach (var browser in BrowserTypes.browsers)
				{
					testData.Add([user, browser]);
				}
			}
			return testData;
		}
	}
}
