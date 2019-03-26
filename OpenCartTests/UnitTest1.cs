using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using System.Threading;
using System.Linq;
using System.Text;
using System.Collections.Generic;


namespace OpenCartTests
{
    [TestClass]
    public class HomePageTests
    {
        IWebDriver driver;
        
        [TestInitialize]
        public void TestSetup()
        {
            driver = new ChromeDriver();
            driver.Manage().Window.Maximize();
        }

        [TestCleanup]
        public void TestTeardown()
        {
            driver.Quit();
        }

        [TestCategory("HomePageTests")]
        [TestMethod]
        public void Test01OpenCartLink()
        {
            driver.Navigate().GoToUrl("https://www.opencart.com");

            Thread.Sleep(1000);

            var ViewDemo = driver.FindElement(By.CssSelector("a.btn.btn-white.btn-xl"));

            ViewDemo.Click();

            Thread.Sleep(1000);

            IWebElement ViewAdministration = driver.FindElement(By.CssSelector("span.hidden-xs"));

            ViewAdministration.Click();

            Thread.Sleep(1000);

            IReadOnlyCollection<string> windowHandles = driver.WindowHandles;
            string firstTab = windowHandles.First();
            string lastTab = windowHandles.Last();
            driver.SwitchTo().Window(lastTab);

            Thread.Sleep(1000);

            IWebElement Username = driver.FindElement(By.Id("input-username"));

            IWebElement Password = driver.FindElement(By.Id("input-password"));

            IWebElement LoginButton = driver.FindElement(By.CssSelector("button.btn"));

            Username.Clear();
            Username.SendKeys("demo");

            Password.Clear();
            Password.SendKeys("demo");

            Thread.Sleep(1000);

            LoginButton.Click();

            Thread.Sleep(1000);

            IWebElement OpenCartLink = driver.FindElement(By.XPath("//*[@id='footer']/a"));

            OpenCartLink.Click();

            var homePageHeading = driver.FindElement(By.CssSelector("h1"));

            var expectedHeadingText = "The best FREE and open-source eCommerce platform";

            var actualHeadingText = homePageHeading.Text;

            Assert.AreEqual(expectedHeadingText, actualHeadingText);
        }

        [TestCategory("HomePageTests")]
        [TestMethod]
        public void Test02ProductFilter()
        {
            driver.Navigate().GoToUrl("https://demo.opencart.com/admin/");

            Thread.Sleep(1000);

            IWebElement Username = driver.FindElement(By.Id("input-username"));

            IWebElement Password = driver.FindElement(By.Id("input-password"));

            IWebElement LoginButton = driver.FindElement(By.CssSelector("button.btn"));

            Username.Clear();
            Username.SendKeys("demo");

            Password.Clear();
            Password.SendKeys("demo");

            Thread.Sleep(1000);

            LoginButton.Click();

            Thread.Sleep(1000);

            IWebElement Catalog = driver.FindElement(By.XPath("//*[@id='menu-catalog']/a"));

            Catalog.Click();

            Thread.Sleep(1000);

            IWebElement Products = driver.FindElement(By.XPath("//*[@id='collapse1']/li[2]/a"));

            Products.Click();

            IWebElement ProductName = driver.FindElement(By.XPath("//*[@id='input-name']"));

            ProductName.SendKeys("Apple");

            Thread.Sleep(1000);

            IWebElement Model = driver.FindElement(By.XPath("//*[@id='input-model']"));

            Model.SendKeys("Product 15");

            IWebElement FilterButton = driver.FindElement(By.XPath("//*[@id='button-filter']"));

            FilterButton.Click();

            Thread.Sleep(1000);

            IWebElement ProductNameResult = driver.FindElement(By.XPath("//*[@id='form-product']/div/table/tbody/tr/td[4]"));

            Assert.AreEqual("Product 15", ProductNameResult.Text);

            Thread.Sleep(1000);
        }

        [TestMethod]
        public void Test03EditStoreTitle()
        {
            driver.Navigate().GoToUrl("https://demo.opencart.com/admin/");

            Thread.Sleep(1000);

            IWebElement Username = driver.FindElement(By.Id("input-username"));

            IWebElement Password = driver.FindElement(By.Id("input-password"));

            IWebElement LoginButton = driver.FindElement(By.CssSelector("button.btn"));

            Username.Clear();
            Username.SendKeys("demo");

            Password.Clear();
            Password.SendKeys("demo");

            Thread.Sleep(1000);

            LoginButton.Click();

            Thread.Sleep(1000);

            IWebElement SystemButton = driver.FindElement(By.XPath("//*[@id='menu-system']/a"));

            SystemButton.Click();

            IWebElement SettingsButton = driver.FindElement(By.XPath("//*[@id='collapse42']/li[1]/a"));

            SettingsButton.Click();

            IWebElement EditButton = driver.FindElement(By.XPath("//*[@id='form-store']/div/table/tbody/tr/td[4]/a/i"));

            EditButton.Click();

            IWebElement MetaTitle = driver.FindElement(By.XPath("//*[@id='input-meta-title']"));

            MetaTitle.Clear();

            Thread.Sleep(1000);

            MetaTitle.SendKeys("Toma");

            IWebElement SaveButton = driver.FindElement(By.XPath("//*[@id='button-save']/i"));

            SaveButton.Click();

            var SettingsErrorMsg = driver.FindElement(By.XPath("//*[@id='content']/div[2]/div[1]"));

            var expectedErrorMsg = "Warning: You do not have permission to modify settings!";
            
            var actualErrorMsg = SettingsErrorMsg.Text;

            Assert.IsTrue(actualErrorMsg.Contains(expectedErrorMsg));
        }
    }
}
