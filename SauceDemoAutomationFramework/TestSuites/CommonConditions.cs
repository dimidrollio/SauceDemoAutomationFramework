using NUnit.Framework;
using OpenQA.Selenium;
using SauceDemoAutomationFramework.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SauceDemoAutomationFramework.TestSuites
{
	public class CommonConditions
	{
		protected IWebDriver _driver;

		[TearDown]
		public void Teardown()
		{
			DriverInstance.CloseBrowser();
		}
	}
}
