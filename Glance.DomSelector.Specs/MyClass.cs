﻿using NUnit.Framework;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using System;
using System.Threading;

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

			using (var driver = new ChromeDriver("/Users/corywheeler/Documents/projects/chromestuff"))
			{
				SimplerExampleCodeToExecuteGlance(driver);
			}


		}

		static void SimplerExampleCodeToExecuteGlance(ChromeDriver driver)
		{
			driver.Navigate().GoToUrl("http://quasimatic.org/take-a-glance/?level=2");
			string getTheSquare = "return glanceSelector('square').className;";
			var theResult = driver.ExecuteScript(getTheSquare);
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