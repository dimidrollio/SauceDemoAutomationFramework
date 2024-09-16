using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SauceDemoAutomationFramework.WebElements
{
	public class ActionException : Exception
	{
		public ActionException(string message) : base(message) 
		{
		}
	}
}
