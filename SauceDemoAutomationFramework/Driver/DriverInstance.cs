using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;

namespace SauceDemoAutomationFramework.Driver
{
	public class DriverInstance
	{
		private static IWebDriver _webDriver;
		public static void SetWebDriver(string browserType)
		{
			if (BrowserTypes.BROWSER_CHROME == browserType)
			{
				_webDriver = new ChromeDriver();
			}
			else if (BrowserTypes.BROWSER_EDGE == browserType)
			{
				_webDriver = new EdgeDriver();
			}
			else
			{
				throw new Exception("Driver setting failed...");
			}
		}
		public static IWebDriver GetInstance()
		{
			if (_webDriver is null)
			{
				SetWebDriver(BrowserTypes.BROWSER_CHROME);
			}

			return _webDriver;
		}

		private DriverInstance() { }

		public static void CloseBrowser()
		{
			_webDriver.Quit();
			_webDriver.Dispose();
			_webDriver = null;
		}
	}
}
