using Microsoft.VisualStudio.TestTools.UnitTesting;
using Everlight.TestCases.Runner;
using Everlight.Applications.ComputerObjects.Workflow;
using Everlight.Core;
using System.Threading;

namespace Everlight.TestCases.Everlight
{
    [TestClass]
    public class CompDatabaseTestCases : TestRunner
    {

        [DataSource(dataSourceSettingName: "ComputerDatabase")]
        [TestCategory("Simple"), TestMethod]
        public void CreateComputerEntry() => 
        
            Test(() =>
            {
                ComputerData createData = new ComputerData();

                //Navigate to URL
                Driver.Url = GetApplicationUrl("Computer_Names");
                Thread.Sleep(2000);

                //Add a Computer into Database
                var addComputer = new ComputerWorkflow(Driver, extentTest);
                addComputer.ClickAddButton();
                addComputer.EnterComputerFieldsInfo(createData);
                addComputer.CheckAddAlertInfo(createData);
            });

        [DataSource(dataSourceSettingName: "ComputerDatabase")]
        [TestCategory("Simple"), TestMethod]
        public void EditComputerEntry() =>

            Test(() =>
            {
                ComputerData editData = new ComputerData();

                //Navigate to URL
                Driver.Url = GetApplicationUrl("Computer_Names");
                Thread.Sleep(2000);

                //Edit a Computer in the Database
                var editComputer = new ComputerWorkflow(Driver, extentTest);
                editComputer.SearchComputerDatabaseInfo(editData);
                editComputer.EnterComputerFieldsInfo(editData);
                editComputer.CheckEditAlertInfo(editData);
            });

        [DataSource(dataSourceSettingName: "ComputerDatabase")]
        [TestCategory("Simple"), TestMethod]
        public void DeleteComputerEntry() =>

            Test(() =>
            {
                ComputerData deleteData = new ComputerData();

                //Navigate to URL
                Driver.Url = GetApplicationUrl("Computer_Names");
                Thread.Sleep(2000);

                //Delete computer entry from database
                var deleteComputer = new ComputerWorkflow(Driver, extentTest);
                deleteComputer.SearchComputerDatabaseInfo(deleteData);
                deleteComputer.CheckDeleteAlertInfo(deleteData);
            });

        [DataSource(dataSourceSettingName: "ComputerDatabase")]
        [TestCategory("Simple"), TestMethod]
        public void NavigateComputerDatabase() =>

            Test(() =>
            {
                ComputerData navigateData = new ComputerData();

                //Navigate to URL
                Driver.Url = GetApplicationUrl("Computer_Names");
                Thread.Sleep(2000);

                //Delete computer entry from database
                var naviagteDatabase = new ComputerWorkflow(Driver, extentTest);
                naviagteDatabase.SearchComputerDatabaseInfo(navigateData);
                naviagteDatabase.ClickCancelButton();
                naviagteDatabase.NavigateDatabaseInfo(navigateData);
            });

    }
}
