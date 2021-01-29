using System.Threading;
using Everlight.Core;
using OpenQA.Selenium;

namespace Everlight.Applications.JavascriptAlerts.Pages.JSAlerts
{
    public class JSAlerts : PageObjectBase
    {

        public JSAlerts(IWebDriver driver) : base(driver)
        {

        }

        public IWebElement JSAlert => Driver.FindElement(By.CssSelector("div[class*='example'] > ul > li:nth-of-type(1) > button"));
        public IWebElement JSConfirm => Driver.FindElement(By.CssSelector("div[class*='example'] > ul > li:nth-of-type(2) > button"));
        public IWebElement JSPrompt => Driver.FindElement(By.CssSelector("div[class*='example'] > ul > li:nth-of-type(3) > button"));
        public IWebElement Result => Driver.FindElement(By.Id("result"));

        public void ClickJSAlert()
        {
            JSAlert.Click();
            Thread.Sleep(1000);
        }
        public void ClickJSConfirm()
        {
            JSConfirm.Click();
            Thread.Sleep(1000);
        }
        public void ClickJSPrompt()
        {
            JSPrompt.Click();
            Thread.Sleep(1000);
        }
    }
}