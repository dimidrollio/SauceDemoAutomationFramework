using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SauceDemoAutomationFramework.Models
{
	public class User : IEquatable<User>
	{
        public string Username { get; set; }
		public string Password { get; set; }

		public bool Equals(User? other)
		{
			return this.Username == other?.Username && this.Password == other?.Password;
		}
	}
}
