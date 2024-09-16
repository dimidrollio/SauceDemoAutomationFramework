using OpenQA.Selenium.Chrome;

namespace SauceDemoAutomationFramework.Driver
{
	public static class BrowserTypes
	{
		public static IEnumerable<string> browsers => [BROWSER_CHROME, BROWSER_EDGE];
		public const string BROWSER_CHROME = "Chrome";
		public const string BROWSER_EDGE = "Edge";
	}
}
