using System.Threading;
using Everlight.Core;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace Everlight.Applications.ComputerObjects.Pages.Computer
{
    public class Computer : PageObjectBase
    {

        public Computer(IWebDriver driver) : base(driver)
        {

        }

        #region Computer Database Homepage Elements
        public IWebElement ComputerDatabase => Driver.FindElement(By.XPath("//a[contains(text(),'Computer database')]"));
        public IWebElement SearchBox => Driver.FindElement(By.Id("searchbox"));
        public IWebElement Search => Driver.FindElement(By.Id("searchsubmit"));
        public IWebElement AddNewComputer => Driver.FindElement(By.Id("add"));
        public IWebElement Next => Driver.FindElement(By.XPath("//a[contains(text(),'Next →')]"));
        public IWebElement Previous => Driver.FindElement(By.XPath("//a[contains(text(),'← Previous')]"));
        public IWebElement Number => Driver.FindElement(By.CssSelector("section[id='main'] > h1"));
        #endregion

        #region Computer Database Homepage Functions
        public void ClickComputerDatabase()
        {
            ComputerDatabase.Click();
            Thread.Sleep(3000);
            ComputerDatabase.IsElementVisible();
        }
        public void SearchComputerName(string search)
        {
            SearchBox.EnterText(search);
            Thread.Sleep(1000);
            Search.Click();
        }
        public void ClickEntry(string searchValue)
        {
            Thread.Sleep(2000);
            Driver.FindElement(By.XPath("(//a[contains(text(),'" + searchValue + "')])[1]")).Click();
        }
        public void ClickAddNewComputer()
        {
            AddNewComputer.Click();
        }
        public void ClickNext()
        {
            Next.Click();
            Thread.Sleep(1000);
        }
        public void ClickPrevious()
        {
            Previous.Click();
            Thread.Sleep(1000);
        }
        #endregion


        #region Edit/Add Computer Page Elements
        public IWebElement ComputerNameField => Driver.FindElement(By.Id("name"));
        public IWebElement IntroducedField => Driver.FindElement(By.Id("introduced"));
        public IWebElement DiscontinuedField => Driver.FindElement(By.Id("discontinued"));
        public IWebElement Company => Driver.FindElement(By.Id("company"));
        public SelectElement CompanyDropdown => new SelectElement(Company);
        public IWebElement Save => Driver.FindElement(By.CssSelector("input[class*='primary']"));
        public IWebElement Cancel => Driver.FindElement(By.XPath("//a[contains(text(),'Cancel')]"));
        public IWebElement Delete => Driver.FindElement(By.CssSelector("input[class*='danger']"));
        public IWebElement Alert => Driver.FindElement(By.CssSelector("div[class*='alert-message']"));
        #endregion

        #region Edit/Add Computer Page Functions
        public void EnterComputerName(string computerNameValue)
        {
            Thread.Sleep(2000);
            ComputerNameField.EnterText(computerNameValue);
        }
        public void EnterIntroduced(string introducedValue)
        {
            IntroducedField.EnterText(introducedValue);
        }
        public void EnterDiscontinued(string discontinuedValue)
        {
            DiscontinuedField.EnterText(discontinuedValue);
        }
        public void SelectCompany(string companyValue)
        {
            if (companyValue.Equals(""))
            {

            }
            else
            {
                Company.IsElementVisible();
                CompanyDropdown.SelectByText(companyValue);
            }
        }
        public void ClickSave()
        {
            Save.Click();
        }
        public void ClickCancel()
        {
            Cancel.Click();
            ComputerDatabase.IsElementVisible();
        }
        public void ClickDelete()
        {
            Delete.Click();
        }
        #endregion
    }
}