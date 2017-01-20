using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Net.Sockets;
using System.Threading;
using System.Reflection;

namespace Glance.DomSelector.Specs
{
    [TestFixture]
    public class When_trying_to_see_if_it_runs
    {
        [Test]
        public void lets_try_something()
        {
            DesiredCapabilities capabilities = DesiredCapabilities.Chrome();

            try
            {
                using (var driver = new RemoteWebDriver(capabilities))
                {
                    driver.Manage().Window.Maximize();
                    LoadGlanceAndUseGlanceSelectorToClickDisciplineOnShopToTrot(driver);
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine("Please start selenium (ex: selenium-standalone start)");
                throw exception;
            }
        }

        static void LoadGlanceAndUseGlanceSelectorToClickDisciplineOnShopToTrot(RemoteWebDriver driver)
        {
            driver.Navigate().GoToUrl("http://shoptotrot.com");

            LoadGlanceIntoDom(driver, 3000);

            var disciplineInput = driver.FindElement(new GlanceSelector("Discipline > input"));

            ClickElementAndWaitForResult(disciplineInput, 3000);
        }

        static void ClickElementAndWaitForResult(IWebElement element, int milliSecondsToWaitAfterClick)
        {
            element.Click();

            Thread.Sleep(milliSecondsToWaitAfterClick);
        }

        static void LoadGlanceIntoDom(IWebDriver driver, int millisecondsToWaitForGlanceToLoad)
        {
            var assembly = Assembly.GetExecutingAssembly();
            var resourceName = "Glance.DomSelector.Specs.Lib.Scripts.glance-selector.js";

            using (Stream stream = assembly.GetManifestResourceStream(resourceName))
            using (StreamReader reader = new StreamReader(stream))
            {
                string glance = reader.ReadToEnd();

                ((IJavaScriptExecutor) driver).ExecuteScript(glance);

                Thread.Sleep(millisecondsToWaitForGlanceToLoad);

                ((IJavaScriptExecutor) driver).ExecuteScript(glance);

                Thread.Sleep(millisecondsToWaitForGlanceToLoad);
            }
        }

        static void CustomSeleniumLocator(IWebDriver driver)
        {
            driver.Navigate().GoToUrl("http://quasimatic.org/take-a-glance/?level=2");

            int millisecondsToWaitForGlanceToLoad = 3000;
            Thread.Sleep(millisecondsToWaitForGlanceToLoad);

            var theResult = driver.FindElement(new GlanceSelector("square"));
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
                var driver = (RemoteWebDriver) context;
                string executeGlance = "return glanceSelector('" + _glanceReference + "');";
                var element = driver.ExecuteScript(executeGlance);
                return (IWebElement) element;
            }

            public override ReadOnlyCollection<IWebElement> FindElements(ISearchContext context)
            {
                return base.FindElements(context);
            }
        }

        static void SimplerExampleCodeToExecuteGlance(RemoteWebDriver driver)
        {
            driver.Navigate().GoToUrl("http://quasimatic.org/take-a-glance/?level=2");
            string getTheSquare = "return glanceSelector('square');";
            var theResult = (IWebElement) driver.ExecuteScript(getTheSquare);
            theResult.Click();
            Console.WriteLine(theResult + " cory");
        }

        static void ExampleCodeToExecuteGlance(RemoteWebDriver driver)
        {
            try
            {
                driver.Url = "http://quasimatic.org/take-a-glance/?level=2";
                driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(20));
                driver.Manage().Timeouts().SetPageLoadTimeout(TimeSpan.FromSeconds(50));
                driver.Manage().Window.Maximize();
                driver.Navigate();

                string Javascript = "return glanceSelector('square').className;";
                string t = ((IJavaScriptExecutor) driver).ExecuteScript(Javascript).ToString();

                //Above code will return the html source of the page
                Console.WriteLine(t);
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception ....*********" + e);
            }
            finally
            {
                Thread.Sleep(2000);
                driver.Quit();
                Console.ReadLine();
            }
        }

        static void ExampleCodeToExecuteJavaScript(RemoteWebDriver driver)
        {
            try
            {
                driver.Url = "http://register.rediff.com/register/register.php";
                driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(20));
                driver.Manage().Timeouts().SetPageLoadTimeout(TimeSpan.FromSeconds(50));
                driver.Manage().Window.Maximize();
                driver.Navigate();

                string Javascript = "return document.documentElement.innerText;";
                string t = ((IJavaScriptExecutor) driver).ExecuteScript(Javascript).ToString();

                //Above code will return the html source of the page
                Console.WriteLine(t);
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception ....*********" + e);
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