The Automation.UI.Layout project is built on Java 1.8

========= VIEW SOURCE =========
1. Install Visual Studio Code
2. Open the folder "uluru/src/Qsr.Uluru.Core/ui/qa/Automation.UI/Layout"

=========== HOW TO RUN PROJECT ===============
1. Go to folder "uluru/src/Qsr.Uluru.Core/ui/qa/Automation.UI/Layout"
2. Run the file "runTestLayout.bat"

======= CONFIGURE runTestLayout.bat ==========
1. Edit -Dbaseurl=<BaseURL>
 <BaseURL>: URL of target site for testing, for examples, https://uluru-transcription-dev.azurewebsites.net, or
https://uluru-transcription-qa.azurewebsites.net

2. Edit -Dbrowser=<Browser>
 <Browser>: browser name, such as "chrome", "firefox", "ie", or "edge"
 (Only Support Chrome currently)

3. Edit -Dusername=<Username>
 <Username>: username for login or signup

4. Edit -Dpassword=<Password>
 <Password>: password for login or signup

5. Edit -Dplatform=<TestDeviceName>
 <TestDeviceName>: name of the devices which is iphoneX, ipadAir and monitor_1

6. Edit -Dtest=<TestSuitClassName>#*<TestCaseMethodName>*^
 + This is the Test Selection Language from TestNG
 + The follow conditions are being supported:
    <TestSuitClassName>: Class name of this test suit, for examples, 
    "UploadPageTest" : we want to run the test suit about test upload page layout
    <TestCaseMethodName>: Method name of this test case which wants to excute, for examples,
    "tc_ul_792_" : we want to run the test case has ul-792

7. Edit -Dgroups=<test priority>
 <test priority>: <priority on Jira> with the following values:
 highest, high, medium, low, lowest. Reference to TestPriorityContants.java

============= CONFIG DEFAULT VALUE ===========
The following configuration will be applied if the parameters not inputted from the command line:

Change config.xml:
 + <entry key="TestDevices">: path of test devices csv file
 + <entry key="PageLoadTimeout">: page load timeout
 + <entry key="SleepShortTime">: time for waiting element appear
 + <entry key="BaseURL">: URL of target site for testing, for examples, https://uluru-transcription-dev.azurewebsites.net, or
https://uluru-transcription-qa.azurewebsites.net
 + <entry key="Browser">: browser name, such as "chrome", "firefox", "ie", or "edge"
 + <entry key="Username">: username for login or signup
 + <entry key="Password">: password for login or signup
 + <entry key="ChromeDriver">: path of chrome driver
