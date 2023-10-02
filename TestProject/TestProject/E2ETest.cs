using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.DevTools.V115.DOM;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support;
using OpenQA.Selenium.Support.Extensions;
using OpenQA.Selenium.Support.UI;
using System.ComponentModel;
using System.Drawing;
using System.Xml.Linq;

namespace TestProject
{
    [TestClass]
    public class E2ETest
    {
        private IWebDriver webDriver;
        

        [TestInitialize]
        public void TestInitialize()
        {
            webDriver = CreateDriver("CHROME");
            webDriver.Manage().Window.Size = new Size(1000, 800);
        }

        [TestCleanup]
        public void TestCleanup()
        {
            webDriver.Quit();
        }

        private IWebDriver CreateDriver(String browserName)
        {
            switch (browserName.ToUpperInvariant())
            {
                case "CHROME":
                    return new ChromeDriver();
                case "FIREFOX":
                    return new FirefoxDriver();
                case "EDGE":
                    return new EdgeDriver();
                default:
                    throw new Exception("Requested Browser is not supported.");
            }
        }

        [TestMethod]
        public void SendkeysTest()
        {

            webDriver.Navigate().GoToUrl("https://testpages.eviltester.com/styled/basic-html-form-test.html");

            var nameTB = webDriver.FindElement(By.Name("username"));
            nameTB.SendKeys("Mark Kalal");

            var passwordTB = webDriver.FindElement(By.Name("password"));
            passwordTB.SendKeys("password123");

            var submitBtn = webDriver.FindElement(By.CssSelector("input[value='submit']"));

            submitBtn.Click();

            var eemm = webDriver.FindElement(By.XPath("/html/body/div/h1"));

            Assert.AreEqual("Processed Form Details", eemm.Text);
        }


        [TestMethod]
        public void CheckboxTest()
        {

            webDriver.Navigate().GoToUrl("https://testpages.eviltester.com/styled/basic-html-form-test.html");

            var itemOneCB = webDriver.FindElement(By.CssSelector("input[value='cb1']"));
            itemOneCB.Click();

            var itemThreeCB = webDriver.FindElement(By.CssSelector("input[value='cb3']"));
            if (itemThreeCB.Selected == false)
            {
                itemThreeCB.Click();
            }
            var submitBtn = webDriver.FindElement(By.CssSelector("input[value='submit']"));
            submitBtn.Click();

        }


        [TestMethod]
        public void RadioButtonTest()
        {

            webDriver.Navigate().GoToUrl("https://testpages.eviltester.com/styled/basic-html-form-test.html");

            webDriver.FindElement(By.CssSelector("input[value='rd1']")).Click();

            var itemThreeRB = webDriver.FindElement(By.CssSelector("input[value='rd3']"));
            itemThreeRB.Click();

            var submitBtn = webDriver.FindElement(By.CssSelector("input[value='submit']"));
            submitBtn.Click();

        }

        [TestMethod]
        public void MultiSelectTest()
        {

            webDriver.Navigate().GoToUrl("https://testpages.eviltester.com/styled/basic-html-form-test.html");

            var itemOneMS = webDriver.FindElement(By.CssSelector("option[value='ms1']"));
            itemOneMS.Click();

            var itemThreeMS = webDriver.FindElement(By.CssSelector("option[value='ms3']"));
            itemThreeMS.Click();


            var itemOneRB = webDriver.FindElement(By.Name("multipleselect[]"));


            var itemSelectElement = new SelectElement(itemOneRB);
            itemSelectElement.SelectByText("Selection Item 1");
            itemSelectElement.SelectByText("Selection Item 3");

            var submitBtn = webDriver.FindElement(By.CssSelector("input[value='submit']"));
            submitBtn.Click();
        }


        [TestMethod]
        public void AlertTest()
        {

            webDriver.Navigate().GoToUrl("https://testpages.eviltester.com/styled/alerts/alert-test.html");

            //var itemOneRB = driver.FindElement(By.CssSelector("option[value='ms1']"));
            var alertBtn = webDriver.FindElement(By.Id("alertexamples"));
            alertBtn.Click();

            var alertDB = webDriver.SwitchTo().Alert();
            alertDB.Accept();
        }


        [TestMethod]
        public void ConfirmationTest()
        {

            webDriver.Navigate().GoToUrl("https://testpages.eviltester.com/styled/alerts/alert-test.html");

            var confirmBtn = webDriver.FindElement(By.Id("confirmexample"));
            confirmBtn.Click();

            var alertDB = webDriver.SwitchTo().Alert();
            alertDB.Dismiss();

            var confirmMsg = webDriver.FindElement(By.Id("confirmreturn"));

            Assert.AreEqual("false", confirmMsg.Text);

            confirmBtn.Click();

            alertDB = webDriver.SwitchTo().Alert();
            alertDB.Accept();

            confirmMsg = webDriver.FindElement(By.Id("confirmreturn"));

            Assert.AreEqual("true", confirmMsg.Text);
        }

        [TestMethod]
        public void ScrollTest()
        {

            webDriver.Manage().Window.Size = new System.Drawing.Size(1500, 150);
            webDriver.Navigate().GoToUrl("https://testpages.eviltester.com/styled/find-by-playground-test.html");

            var testLnk = webDriver.FindElement(By.Id("a50"));

            /// scroll to link
            ((IJavaScriptExecutor)webDriver).ExecuteScript("arguments[0].scrollIntoView(true);", testLnk);

            testLnk.Click();

            Assert.IsTrue(webDriver.Url.Contains("pName24"));

        }

        [TestMethod]
        public void ClickWaitTest()
        {

            webDriver.Navigate().GoToUrl("https://testpages.eviltester.com/styled/dynamic-buttons-simple.html");

            var oneBtn = webDriver.FindElement(By.Id("button00"));
            oneBtn.Click();

            Thread.Sleep(1000);
            var twoBtn = webDriver.FindElement(By.Id("button01"));
            twoBtn.Click();


            WebDriverWait wait = new WebDriverWait(webDriver, TimeSpan.FromSeconds(10));
            wait.Until(d => webDriver.FindElement(By.Id("button02")).Displayed);
            //Thread.Sleep(5000);
            webDriver.FindElement(By.Id("button02")).Click();


            wait.Until(d => webDriver.FindElement(By.Id("button03")).Displayed);
            webDriver.FindElement(By.Id("button03")).Click();


            var resultLbl = webDriver.FindElement(By.Id("buttonmessage"));
            Assert.AreEqual("All Buttons Clicked", resultLbl.Text);

        }
    }
}