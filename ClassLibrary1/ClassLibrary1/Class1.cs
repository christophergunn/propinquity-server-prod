using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace ClassLibrary1
{
	[TestFixture]
    public class Class1
    {
		[Test]
		public void TBC()
		{
			var something = new Something();

			Assert.That(something.GetInt(), Is.EqualTo(10));
		}

		
    }

	public class Something
	{
		private Random _rnd;

		public Something()
		{
			_rnd = new Random();
		}

		public int GetInt()
		{
			return _rnd.Next(1, 10);
		}
	}
}
