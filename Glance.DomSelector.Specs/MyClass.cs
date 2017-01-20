using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Remote;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Threading;

namespace Glance.DomSelector.Specs
{
	[TestFixture]
	public class When_trying_to_see_if_it_runs
	{
		[Test]
		public void lets_try_something()
		{
			ChromeOptions options = new ChromeOptions();
			options.AddArguments("start-maximized");

			using (var driver = new ChromeDriver("/Users/corywheeler/Documents/projects/chromestuff", options))
			{
				CustomSeleniumLocator(driver);
			}


		}

		static void CustomSeleniumLocator(ChromeDriver driver)
		{
			driver.Navigate().GoToUrl("http://quasimatic.org/take-a-glance/?level=2");

			// Pause to allow time for glance to get loaded into the DOM.
			Thread.Sleep(3000);

			IWebElement theResult = (IWebElement) driver.FindElement(new GlanceSelector("square"));
			theResult.Click();
		}

		public class GlanceSelector : By
		{
			private string _glanceReference;

			public GlanceSelector(string glanceReference)
			{
				_glanceReference = glanceReference;

			}

			public override IWebElement FindElement(ISearchContext context)
			{
				var driver = (RemoteWebDriver)context;
				string executeGlance = "return glanceSelector('" + _glanceReference + "');";
				var element = driver.ExecuteScript(executeGlance);
				return (IWebElement) element;
			}

			public override ReadOnlyCollection<IWebElement> FindElements(ISearchContext context)
			{
				return base.FindElements(context);
			}
		}

		static void SimplerExampleCodeToExecuteGlance(ChromeDriver driver)
		{
			driver.Navigate().GoToUrl("http://quasimatic.org/take-a-glance/?level=2");
			string getTheSquare = "return glanceSelector('square');";
			IWebElement theResult = (IWebElement) driver.ExecuteScript(getTheSquare);
			theResult.Click();
			Console.WriteLine(theResult + " cory");
		}

		static void ExampleCodeToExecuteGlance(ChromeDriver driver)
		{
			try
			{
				driver.Url = "http://quasimatic.org/take-a-glance/?level=2";
				driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(20));
				driver.Manage().Timeouts().SetPageLoadTimeout(TimeSpan.FromSeconds(50));
				driver.Manage().Window.Maximize();
				driver.Navigate();

				string Javascript = "return glanceSelector('square').className;";
				string t = ((IJavaScriptExecutor)driver).ExecuteScript(Javascript).ToString();

				//Above code will return the html source of the page 
				Console.WriteLine(t);

			}
			catch (Exception e)
			{
				Console.WriteLine("Exception ....*********" + e.ToString());
			}
			finally
			{
				Thread.Sleep(2000);
				driver.Quit();
				Console.ReadLine();
			}
		}

		static void ExampleCodeToExecuteJavaScript(ChromeDriver driver)
		{
			try
			{
				driver.Url = "http://register.rediff.com/register/register.php";
				driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(20));
				driver.Manage().Timeouts().SetPageLoadTimeout(TimeSpan.FromSeconds(50));
				driver.Manage().Window.Maximize();
				driver.Navigate();

				string Javascript = "return document.documentElement.innerText;";
				string t = ((IJavaScriptExecutor)driver).ExecuteScript(Javascript).ToString();

				//Above code will return the html source of the page 
				Console.WriteLine(t);

			}
			catch (Exception e)
			{
				Console.WriteLine("Exception ....*********" + e.ToString());
			}
			finally
			{
				Thread.Sleep(2000);
				driver.Quit();
				Console.ReadLine();
			}
		}
	}
}