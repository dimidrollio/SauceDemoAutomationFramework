using Newtonsoft.Json.Linq;
using SauceDemoAutomationFramework.Constants;
using SauceDemoAutomationFramework.Resources;

namespace SauceDemoAutomationFramework.Services
{
	public class GetEnvironmentData
	{
		public static string GetLoginURL()
		{
			string? jsonString = File.ReadAllText(WebConstants.projectDirectory + "\\Resources\\Environment.json");
			JObject json = JObject.Parse(jsonString);
			string webUrl = json["URL"]["LoginURL"].ToString();
			return webUrl;
		}
	}
}
