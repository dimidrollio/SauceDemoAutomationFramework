using OpenQA.Selenium;
using OpenQA.Selenium.DevTools.V126;
using OpenQA.Selenium.DevTools.V126.Animation;
using OpenQA.Selenium.Support.UI;
using System.Security.Cryptography;

namespace SauceDemoAutomationFramework.WebElements
{
	public class ActionsElements
	{
		public static void WaitForPageLoad(IWebDriver driver, int timeout = 5)
		{
			IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
			WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeout));
			wait.Until(_ => js.ExecuteScript("return document.readyState").ToString() == "complete");
		}

		public static IWebElement WaitForElementToDisplay(IWebDriver driver, By by, int timeout = 5)
		{
			DefaultWait<IWebDriver> fluentWait = new DefaultWait<IWebDriver>(driver);
			fluentWait.Timeout = TimeSpan.FromSeconds(timeout);
			fluentWait.PollingInterval = TimeSpan.FromSeconds(1);
			fluentWait.IgnoreExceptionTypes(typeof(StaleElementReferenceException));
			fluentWait.Message = "Element not found";

			IWebElement element = driver.FindElement(by);
			ScrollIntoView(driver, element);
			return element;
		}

		public static IWebElement FindElement(IWebDriver driver, By by, int timeout = 5)
		{
			IWebElement element = null;

			try
			{
				element = WaitForElementToDisplay(driver, by, timeout);
			}
			catch (StaleElementReferenceException)
			{
				try
				{
					Console.WriteLine("Stalement Element exception occurred, re-trying...");
					WaitForPageLoad(driver, timeout);
					element = WaitForElementToDisplay(driver, by, timeout);
				}
				catch
				{
					throw new ActionException("Exception during FindElement operation...");
				}
			}
			catch (NoSuchElementException)
			{
				throw new ActionException("No Such Element Exception during FindElement operation...");
			}
			catch (Exception)
			{
				throw new ActionException("Exception during FindElement operation...");
			}
			return element;
		}
		public static void ScrollIntoView(IWebDriver driver, IWebElement element)
		{
			IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
			js.ExecuteScript("arguments[0].scrollIntoViewIfNeeded()", element);
		}
		public static void Click(IWebDriver driver, By by, int timeout = 5)
		{
			try
			{
				IWebElement element = WaitForElementToDisplay(driver, by, timeout);
				if (element != null)
				{
					ScrollIntoView(driver, element);
					element.Click();
				}
			}
			catch (StaleElementReferenceException)
			{
				try
				{
					Console.WriteLine("Stale Element Exception occured, re-trying to perform Click action");
					IWebElement element = WaitForElementToDisplay(driver, by, timeout);
					ScrollIntoView(driver, element);
					element.Click();
				}
				catch
				{
					throw new ActionException("Exception during click operation...");
				}
			}
			catch
			{
				throw new ActionException("Exception during click operation");
			}
		}

		public static void Navigate(IWebDriver driver, string url)
		{
			driver.Navigate().GoToUrl(url);
		}

		public static void ClearInput(IWebDriver driver, By by)
		{
			var element = WaitForElementToDisplay(driver, by);
			Click(driver, by);
			element.SendKeys(Keys.Control + "a");
			element.SendKeys(Keys.Delete);
		}
	}
}
