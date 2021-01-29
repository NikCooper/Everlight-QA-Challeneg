using AventStack.ExtentReports;
using Everlight.Core;
using OpenQA.Selenium;
using Everlight.Applications.ComputerObjects.Pages.Computer;
using System.Threading;

namespace Everlight.Applications.ComputerObjects.Workflow
{
    public class ComputerWorkflow : WorkflowBase
    {
        public ComputerWorkflow(IWebDriver driver, ExtentTest test) : base(driver, test)
        {
            ComputerInfo = new Computer(Driver);
        }

        Computer ComputerInfo;

        #region Computer Database
        public void ClickAddButton()
        {
            ComputerInfo.ClickAddNewComputer();
        }
        public void EnterComputerFieldsInfo(ComputerData computerData)
        {
            EnterComputerFieldsFlow(computerData.ComputerNameValue, computerData.IntroducedDate, computerData.DiscontinuedDate, computerData.CompanyValue);
        }
        public void EnterComputerFieldsFlow(string computerNameValue, string introducedValue, string discontinuedValue, string company)
        {
            ComputerInfo.EnterComputerName(computerNameValue);
            ComputerInfo.EnterIntroduced(introducedValue);
            ComputerInfo.EnterDiscontinued(discontinuedValue);
            ComputerInfo.SelectCompany(company);
            ComputerInfo.ClickSave();
        }
        public void CheckAddAlertInfo(ComputerData computerData)
        {
            CheckAddAlert(computerData.ComputerNameValue);
        }
        public void CheckAddAlert(string computerNameValue)
        {
            VerifyElementText(ComputerInfo.Alert, "Done ! Computer " + computerNameValue + " has been created");
        }
        public void CheckEditAlertInfo(ComputerData computerData)
        {
            CheckEditAlert(computerData.ComputerNameValue);
        }
        public void CheckEditAlert(string computerNameValue)
        {
            VerifyElementText(ComputerInfo.Alert, "Done ! Computer " + computerNameValue + " has been updated");
        }
        public void SearchComputerDatabaseInfo(ComputerData computerData)
        {
            SearchComputerDatabase(computerData.SearchBar, computerData.SearchValue);
        }
        public void SearchComputerDatabase(string search, string searchValue)
        {
            ComputerInfo.SearchComputerName(search);
            ComputerInfo.ClickEntry(searchValue);
            Thread.Sleep(2000);
        }
        public void CheckDeleteAlertInfo(ComputerData computerData)
        {
            ClickDeleteButton(computerData.SearchValue);
        }
        public void ClickDeleteButton(string searchValue)
        {
            ComputerInfo.ClickDelete();
            VerifyElementText(ComputerInfo.Alert, "Done ! Computer " + searchValue + " has been deleted");
        }
        public void ClickCancelButton()
        {
            ComputerInfo.ClickCancel();
        }
        public void NavigateDatabaseInfo(ComputerData computerData)
        {
            NavigateDatabase(computerData.SearchBar);
        }
        public void NavigateDatabase(string search)
        {
            ComputerInfo.ClickNext();
            Thread.Sleep(3000);
            ComputerInfo.ClickPrevious();
            ComputerInfo.SearchComputerName(search);
            ComputerInfo.ClickComputerDatabase();
        }

        #endregion
    }
}

