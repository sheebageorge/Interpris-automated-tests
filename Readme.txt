The Automation.UI project is built on .NET Framework 4.6.1.

The output is a DLL file which implemented NUnit 3 framework.

The Test Project can be executed by the NUnit Console Runner which should be ported with the Automation.UI project itself.

=================================================
===== How to configuration Test Environment =====
=================================================
In the run bat file, we can configure the environment by:

SET TestEnv=Test

Currently, only supporting "QA", or "Test"

=================================================
===== How to select the test cases to run   =====
=================================================
Detailed instruction can be found at https://github.com/nunit/docs/wiki/Test-Selection-Language

Custom "Tags" to select test cases are:
 + TestID
 + StoryID
 + Priority

Here are the samples to run test suites by collection (UluruAutoUITestRun.bat):

1. To run the whole test project, please remove the "where" statement from the CLI:

call .\packages\NUnit.ConsoleRunner.3.8.0\tools\nunit3-console.exe^
 .\Functionality\bin\Debug\Functionality.dll^
 --work=%ReportDir%^
 --params=Browser=chrome;ScreenSize=max

2. To run a test case by ID, add the "where" statement to the CLI:

call .\packages\NUnit.ConsoleRunner.3.8.0\tools\nunit3-console.exe^
 .\Functionality\bin\Debug\Functionality.dll^
 --work=%ReportDir%^
 --params=Browser=chrome;ScreenSize=max^
 --where "TestID == UL-378"
 
or:

call .\packages\NUnit.ConsoleRunner.3.8.0\tools\nunit3-console.exe^
 .\Functionality\bin\Debug\Functionality.dll^
 --work=%ReportDir%^
 --params=Browser=chrome;ScreenSize=max^
 --where "StoryID == UL-105"

Note, the TestID, StoryID can be found on Jira, for example, the test cases of Log Out story:
https://qsrinternational.atlassian.net/browse/UL-489?jql=project%20%3D%20UL%20AND%20issuetype%20%3D%20Test%20AND%20issue%20in%20linkedIssues(UL-103)%20ORDER%20BY%20key%20ASC

3. To run the test suite, we declare the test suite/class name as the following:
call .\packages\NUnit.ConsoleRunner.3.8.0\tools\nunit3-console.exe^
 .\Functionality\bin\Debug\Functionality.dll^
 --work=%ReportDir%^
 --params=Browser=chrome;ScreenSize=max^
 --where "class == Automation.UI.Functionality.Test.ViewAllFilesTest"
 
Note, the test suites/classes can be found under folder "Automation.UI.Functionality.Test"

Besides, we can apply the Test-Selection-Language to customize the test execution as needed.

=================================================
===== How to run Automation.UI test project =====
=================================================
1. Go to folder "uluru/src/Qsr.Uluru.Core/ui/qa/Automation.UI/"
2. Run the file "UluruAutoUITestRun.bat" or "UluruAutoUITestRun.ps1"

=================================================
===== Configure UluruAutoUITestRun.bat      =====
=================================================
1. Edit --params==Browser=chrome;ScreenSize=max
  + <Browser>: browser name, such as "chrome", "firefox", "ie", or "edge"
  + <ScreenSize>: <number>x<number>, for examples, "1024x768", "800x600", or "max", ...

2. Edit --where "TestID == UL-378"
  + This is a Test Selection Language from NUnit
  + The following conditions are being supported:
    TestID == <Test ID on Jira> such as UL-378
    StoryID == <Story ID on Jira> such as UL-2
    Priority == <Priority on Jira> such as Highest, High, Medium, ...
  + We can use AND or OR for the where condition

3. Edit --work=TestResults
  + This is the output folder for test results
  + In this .bat file, the folder is generated automatically each execution time

4. Edit --result=ResultFileName
  + This is the name of the result file
  + It can be empty or the file name to write the output to

=================================================
===== Configure UluruAutoUITestRun.ps1      =====
=================================================
The parameters should be the same as the .bat file

Only different at the tag names:
-output as --work
-outputfile as --result
-params as --params
-condition as --where

=================================================
===== How to add a new [Test] item          =====
=================================================
1. Add a new test id in class "Automation.UI\Functionality\Libraries\TestAttributes\TestIDAttribute.cs":
  public const string TC_ID_0001 = "TC_ID_0001"; // UL-375
  public const string TC_ID_0002 = "TC_ID_0002"; // UL-376
  ...

2. Add a new mapping between test id of the project and test id on Jira in file "Automation.UI\Functionality\Data\TestCaseDataMappings.csv":
  test_id,test_jira_id
  TC_ID_0001,UL-375
  TC_ID_0002,UL-376
  ...

3. Add a new story id (if needed) in class "Automation.UI\Functionality\Libraries\TestAttributes\StoryIDAttribute.cs":
  public const string SR_ID_001 = "SR_ID_001"; // UL-2
  public const string SR_ID_002 = "SR_ID_002"; // UL-202
  ...

4. Add a new mapping (if needed) between the story id of the project and story id on Jira in file "Automation.UI\Functionality\Data\StoryDataMappings.csv":
  story_id,story_jira_id
  SR_ID_001,UL-2
  SR_ID_002,UL-202
  ...

5. Add a new [Test] item in a [TestFixture] with the added ID(s):
  [Test]
  [TestID(TestID.TC_ID_0001), StoryID(StoryID.SR_ID_001)]
  [Priority(PriorityLevel.Highest)]
  [TestCaseSource(typeof(DataProvider), "PrepareTestCases", new object[] { TestID.TC_ID_0001 })]
  public void TC_SIGN_UP_VerifyUserCanSignUpSuccessfullyWithValidEmailAndPassword(Dictionary<string, string> Data) {}
  
Note, all [TestFixture] items must be stored under folder "Automation.UI\Functionality\Test".

6. For the Data Mapping, add a new mapping between the test case with the data-driven file in Data Mapping File "Automation.UI\Functionality\Data\DataMappings.csv":
  test_id,data_type,data_file_path,excel_data_sheet_name
  TC_ID_0001,CSV,SigninData.csv,
  TC_ID_0002,CSV,SigninData.csv
  ...

Note, the "SigninData.csv" must be existing under the "Data" folder. Otherwise, the test case cannot run.

If the test case doesn't have data-driven, just remove the [TestCaseSource] line at the test case header:
  [Test]
  [TestID(TestID.TC_ID_0001), StoryID(StoryID.SR_ID_001)]
  [Priority(PriorityLevel.Highest)]
  public void TC_SIGN_UP_VerifyUserCanSignUpSuccessfullyWithValidEmailAndPassword() {}

=================================================
===== How to add a new Page Object item     =====
=================================================
All Page Object items must be stored under folder "Automation.UI\Functionality\Libraries\PageObjects".

All Page Object items must inherit the base object "Automation.UI\Core\Selenium\BasePage".

=================================================
===== Others                                =====
=================================================
- Turn off pop-up blocker of all browsers

- Configure IE 11:
Registry entries for 32 and 64 bit.
create a DWORD value with the name "iexplore.exe" and the value of 0 in the following key:
for 32-bit Windows :- HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Internet Explorer\Main\FeatureControl\FEATURE_BFCACHE
for 64-bit Windows :- HKEY_LOCAL_MACHINE\SOFTWARE\Wow6432Node\Microsoft\Internet Explorer\Main\FeatureControl\FEATURE_BFCACHE

Open IE > Go to Option > Internet options > Security Tab > Turn on Enable Protected Mode for all zone (Internet, Local intranet, Trusted sites, Restricted sites)