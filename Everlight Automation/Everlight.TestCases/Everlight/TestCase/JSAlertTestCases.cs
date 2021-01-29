using Microsoft.VisualStudio.TestTools.UnitTesting;
using Everlight.TestCases.Runner;
using Everlight.Applications.JavascriptAlerts.Workflow;
using Everlight.Core;
using System.Threading;

namespace Everlight.TestCases.Everlight
{
    [TestClass]
    public class JSAlertTestCases : TestRunner
    {

        [DataSource(dataSourceSettingName: "JSAlerts")]
        [TestCategory("Simple"), TestMethod]
        public void ClickForJSAlert() =>

            Test(() =>
            {
                JSAlertsData JSAlertData = new JSAlertsData();

                //Navigate to URL
                Driver.Url = GetApplicationUrl("Javascript_Alerts");
                Thread.Sleep(2000);

                //Click for JS Alert
                var JSAlert = new JSAlertsWorkflow(Driver, extentTest);
                JSAlert.JSAlertsFlowInfo(JSAlertData);
            });

        [DataSource(dataSourceSettingName: "JSAlerts")]
        [TestCategory("Simple"), TestMethod]
        public void ClickForJSConfirm() =>

            Test(() =>
            {
                JSAlertsData JSAlertData = new JSAlertsData();

                //Navigate to URL
                Driver.Url = GetApplicationUrl("Javascript_Alerts");
                Thread.Sleep(2000);

                //Click for JS Alert
                var JSAlert = new JSAlertsWorkflow(Driver, extentTest);
                JSAlert.JSConfirmInfo(JSAlertData);
            });

        [DataSource(dataSourceSettingName: "JSAlerts")]
        [TestCategory("Simple"), TestMethod]
        public void ClickForJSConfirmCancel() =>

            Test(() =>
            {
                JSAlertsData JSAlertData = new JSAlertsData();

                //Navigate to URL
                Driver.Url = GetApplicationUrl("Javascript_Alerts");
                Thread.Sleep(2000);

                //Click for JS Alert
                var JSAlert = new JSAlertsWorkflow(Driver, extentTest);
                JSAlert.JSConfirmCancelInfo(JSAlertData);
            });

        [DataSource(dataSourceSettingName: "JSAlerts")]
        [TestCategory("Simple"), TestMethod]
        public void ClickForJSPrompt() =>

            Test(() =>
            {
                JSAlertsData JSAlertData = new JSAlertsData();

                //Navigate to URL
                Driver.Url = GetApplicationUrl("Javascript_Alerts");
                Thread.Sleep(2000);

                //Click for JS Alert
                var JSAlert = new JSAlertsWorkflow(Driver, extentTest);
                JSAlert.JSPromptInfo(JSAlertData);
            });

        [DataSource(dataSourceSettingName: "JSAlerts")]
        [TestCategory("Simple"), TestMethod]
        public void ClickForJSPromptCancel() =>

            Test(() =>
            {
                JSAlertsData JSAlertData = new JSAlertsData();

                //Navigate to URL
                Driver.Url = GetApplicationUrl("Javascript_Alerts");
                Thread.Sleep(2000);

                //Click for JS Alert
                var JSAlert = new JSAlertsWorkflow(Driver, extentTest);
                JSAlert.JSPromptCancelInfo(JSAlertData);
            });
    }
}
