using NUnit.Framework;
using OpenQA.Selenium.Chrome;

namespace Glance.DomSelector.Specs
{
	[TestFixture]
	public class When_trying_to_see_if_it_runs
	{
		[Test]
		public void lets_scrape_something()
		{
			ChromeOptions options = new ChromeOptions();
			options.AddArguments("start-maximized");

			using (var driver = new ChromeDriver("/Users/corywheeler/Documents/projects/chromestuff/"))
			{
				// Go to the home page
				driver.Navigate().GoToUrl("http://quasimatic.com");
			}
		}
	}
}