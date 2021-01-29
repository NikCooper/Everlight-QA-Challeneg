# Everlight-QA-Challenge

This challenge took roughly 8 hours to complete, including the manual test case creation, setting up the framework and automating the test cases. 

Manual Test Cases:
These can be found in here -> Manual Test Cases\Everlight QA Project - Test Cases.xlsx


How to run automation tests:
- Clone repo
- Open solution file in Visual Studio and restore neget packages
- To run tests in the IDE you will need to configure the runsettings file from Test->Configure Run Settings and select file in location -> Everlight Automation\Everlight.TestCases\RunSettings.runsettings
- As this is a data driven framework reading from an excel file you may need the following Access Database Engine package to be installed on your machine. This can be found here - https://www.microsoft.com/en-us/download/confirmation.aspx?id=13255
- Run the tests in the test explorer
- HTML report can be found at location -> Everlight Automation\Everlight.TestCases\bin\Debug\Logs\{{dateTestRan}}


Framework Description:
The framework is separated into 3 different projects.

1. Everlight.Application - This project contains the pages and workflows for each application, where a page is the basic actions for navigating the page and the workflow is a group of actions to achieve an outcome, which also contains the assertions.

2. Everlight.TestCases
This project contains the test cases for both applications. There is a test runner to control test execution, runsettings file containing settings needed to run tests in the IDE and an app.config file containing urls and other config for loggeing and data sources.

3. Everlight.Core - This project contains the core base classes and utitilities used in the framework.

