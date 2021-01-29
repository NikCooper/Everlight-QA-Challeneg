using AventStack.ExtentReports;
using Everlight.Core;
using OpenQA.Selenium;
using Everlight.Applications.JavascriptAlerts.Pages.JSAlerts;
using System.Threading;

namespace Everlight.Applications.JavascriptAlerts.Workflow
{
    public class JSAlertsWorkflow : WorkflowBase
    {
        public JSAlertsWorkflow(IWebDriver driver, ExtentTest test) : base(driver, test)
        {
            JSAlertsInfo = new JSAlerts(Driver);
        }

        JSAlerts JSAlertsInfo;

        public void JSAlertsFlowInfo(JSAlertsData alertsData)
        {
            JSAlertsFlow(alertsData.ResultOne);
        }
        public void JSAlertsFlow(string result)
        {
            JSAlertsInfo.ClickJSAlert();
            var alert = Driver.SwitchTo().Alert();
            alert.Accept();
            VerifyElementText(JSAlertsInfo.Result, result);
            Thread.Sleep(1000);
        }

        public void JSConfirmInfo(JSAlertsData alertsData)
        {
            JSConfirmFlow(alertsData.ResultTwo);
        }
        public void JSConfirmFlow(string result)
        {
            JSAlertsInfo.ClickJSConfirm();
            var alert = Driver.SwitchTo().Alert();
            alert.Accept();
            VerifyElementText(JSAlertsInfo.Result, result);
            Thread.Sleep(1000);
        }

        public void JSConfirmCancelInfo(JSAlertsData alertsData)
        {
            JSConfirmCancelFlow(alertsData.ResultThree);
        }
        public void JSConfirmCancelFlow(string result)
        {
            JSAlertsInfo.ClickJSConfirm();
            var alert = Driver.SwitchTo().Alert();
            alert.Dismiss();
            VerifyElementText(JSAlertsInfo.Result, result);
            Thread.Sleep(1000);
        }

        public void JSPromptInfo(JSAlertsData alertsData)
        {
            JSPromptFlow(alertsData.ResultFour);
        }
        public void JSPromptFlow(string result)
        {
            JSAlertsInfo.ClickJSPrompt();
            var alert = Driver.SwitchTo().Alert();
            alert.SendKeys("Everlight");
            alert.Accept();
            VerifyElementText(JSAlertsInfo.Result, result);
            Thread.Sleep(1000);
        }

        public void JSPromptCancelInfo(JSAlertsData alertsData)
        {
            JSPromptCancelFlow(alertsData.ResultFive);
        }
        public void JSPromptCancelFlow(string result)
        {
            JSAlertsInfo.ClickJSPrompt();
            var alert = Driver.SwitchTo().Alert();
            alert.Dismiss();
            VerifyElementText(JSAlertsInfo.Result, result);
            Thread.Sleep(1000);
        }
    }
}

