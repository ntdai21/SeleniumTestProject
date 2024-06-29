using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeleniumTestProject.Pages
{
    class SalesTaxCalculatorPage
    {
        IWebDriver _driver;

        public SalesTaxCalculatorPage(IWebDriver driver)
        {
            _driver = driver;
        }

        string Link => "https://www.calculator.net/sales-tax-calculator.html";
        float BeforeTaxPriceCalculated
        {
            get
            {
                IWebElement element;
                try
                {
                    element = _driver.FindElement(By.CssSelector("#content > div:nth-child(6) > div:nth-child(1) > font > b"));
                }
                catch
                {
                    element = _driver.FindElement(By.CssSelector("#content > div:nth-child(6) > div:nth-child(1)"));
                }
                return float.Parse(element.GetAttribute("innerHTML").Split("$")[2]);
            }
        }
        float SalesTaxRateCalculated
        {
            get
            {
                IWebElement element;
                try
                {
                    element = _driver.FindElement(By.CssSelector("#content > div:nth-child(6) > div:nth-child(2) > font > b"));
                }
                catch
                {
                    element = _driver.FindElement(By.CssSelector("#content > div:nth-child(6) > div:nth-child(2)"));
                }
                return float.Parse(element.GetAttribute("innerHTML").Split("$")[2]);
            }
        }
        float AfterTaxPriceCalculated
        {
            get
            {
                IWebElement element;
                try
                {
                    element = _driver.FindElement(By.CssSelector("#content > div:nth-child(6) > div:nth-child(3) > font > b"));
                }
                catch
                {
                    element = _driver.FindElement(By.CssSelector("#content > div:nth-child(6) > div:nth-child(3)"));
                }
                return float.Parse(element.GetAttribute("innerHTML").Split("$")[2]);
            }
        }
        IWebElement BeforeTaxPriceBox => _driver.FindElement(By.Name("beforetax"));
        IWebElement SalesTaxRateBox => _driver.FindElement(By.Name("taxrate"));
        IWebElement AfterTaxPriceBox => _driver.FindElement(By.Name("finalprice"));
        IWebElement CalculateButton => _driver.FindElement(By.Name("x"));
        IWebElement ClearButton => _driver.FindElement(By.CssSelector("#content > form > table > tbody > tr > td > div > table > tbody > tr:nth-child(4) > td > input[type=button]:nth-child(2)"));
        IWebElement ErrorLabel => _driver.FindElement(By.CssSelector("#content > font"));

        public void Load()
        {
            _driver.Navigate().GoToUrl(Link);
        }

        public void InsertFields(string beforeTaxPrice, string salesTaxRate, string afterTaxPrice)
        {
            BeforeTaxPriceBox.Clear();
            SalesTaxRateBox.Clear();
            AfterTaxPriceBox.Clear();
            BeforeTaxPriceBox.SendKeys(beforeTaxPrice);
            SalesTaxRateBox.SendKeys(salesTaxRate);
            AfterTaxPriceBox.SendKeys(afterTaxPrice);
        }

        public void InsertFieldsWithCorrectValues()
        {
            InsertFields("10", "10", "11");
        }

        public void Calculate()
        {
            CalculateButton.Submit();
        }

        public bool HasError()
        {
            try {
                return ErrorLabel.Displayed;
            }
            catch
            {
                return false;
            }
        }
    }
}
