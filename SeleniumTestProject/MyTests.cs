using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using SeleniumTestProject.Pages;
using System.Diagnostics;

namespace SeleniumTestProject
{
    public class MyTests
    {
        IWebDriver _driver;
        SalesTaxCalculatorPage _page;

        [SetUp]
        public void Setup()
        {
            _driver = new EdgeDriver();
            _driver.Manage().Window.Maximize();

            //Load home page as default
            _page = new SalesTaxCalculatorPage(_driver);
            _page.Load();
        }

        [TestCase("", ExpectedResult = "false")]
        [TestCase(" ", ExpectedResult = "false")]
        [TestCase("abc", ExpectedResult = "false")]
        [TestCase("-1", ExpectedResult = "false")]
        [TestCase("0", ExpectedResult = "true")]
        [TestCase(".5", ExpectedResult = "true")]
        [TestCase("0.5", ExpectedResult = "true")]
        [TestCase("+10", ExpectedResult = "true")]
        [TestCase("          10       ", ExpectedResult = "true")]
        [TestCase("10000000000000000000000000000000000000000000000", ExpectedResult = "false")]
        public bool BeforeTaxPriceTest(string price)
        {
            _page.InsertFields(price, "10", "");
            _page.Calculate();
            return !_page.HasError();
        }

        [TestCase("", ExpectedResult = "false")]
        [TestCase(" ", ExpectedResult = "false")]
        [TestCase("abc", ExpectedResult = "false")]
        [TestCase("-1", ExpectedResult = "false")]
        [TestCase("0", ExpectedResult = "true")]
        [TestCase(".5", ExpectedResult = "true")]
        [TestCase("0.5", ExpectedResult = "true")]
        [TestCase("+10", ExpectedResult = "true")]
        [TestCase("          10       ", ExpectedResult = "true")]
        [TestCase("10000000000000000000000000000000000000000000000", ExpectedResult = "false")]
        public bool SalesTaxRateTest(string price)
        {
            _page.InsertFields("10", price, "");
            _page.Calculate();
            return !_page.HasError();
        }

        [TestCase("", ExpectedResult = "false")]
        [TestCase(" ", ExpectedResult = "false")]
        [TestCase("abc", ExpectedResult = "false")]
        [TestCase("-1", ExpectedResult = "false")]
        [TestCase("0", ExpectedResult = "true")]
        [TestCase(".5", ExpectedResult = "true")]
        [TestCase("0.5", ExpectedResult = "true")]
        [TestCase("+10", ExpectedResult = "true")]
        [TestCase("          10       ", ExpectedResult = "true")]
        [TestCase("10000000000000000000000000000000000000000000000", ExpectedResult = "false")]
        public bool AfterTaxPriceTest(string price)
        {
            _page.InsertFields("", "10", price);
            _page.Calculate();
            return !_page.HasError();
        }

        [TearDown]
        public void TearDown()
        {
            _driver.Close();
        }
    }
}