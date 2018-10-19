package com.qsrint.automation.ui.layout.libraries;

import static java.util.Arrays.asList;

import java.io.FileReader;
import java.io.IOException;
import java.time.LocalDate;
import java.time.LocalDateTime;
import java.time.LocalTime;
import java.time.format.DateTimeFormatter;
import java.util.ArrayList;
import java.util.Collections;
import java.util.LinkedList;
import java.util.List;
import java.util.concurrent.TimeUnit;

import org.openqa.selenium.Dimension;
import org.openqa.selenium.WebDriver;
import org.openqa.selenium.chrome.ChromeDriver;
import org.openqa.selenium.chrome.ChromeOptions;
import org.testng.annotations.BeforeGroups;
import org.testng.annotations.BeforeSuite;
import org.testng.annotations.DataProvider;
import org.testng.annotations.Parameters;
import org.testng.log4testng.Logger;

import com.galenframework.api.Galen;
import com.galenframework.reports.GalenTestInfo;
import com.galenframework.reports.HtmlReportBuilder;
import com.galenframework.reports.model.LayoutReport;
import com.galenframework.speclang2.pagespec.SectionFilter;
import com.galenframework.testng.GalenTestNgTestBase;
import com.qsrint.automation.ui.layout.contants.*;
import com.qsrint.automation.ui.layout.libraries.pageobjects.BasePage;
import com.opencsv.CSVReader;

/**
 * <h1>Galen Test Base class</h1> This class contains base function of Galen
 * There are create Driver and config baseURl, Test devices
 * 
 * <p>
 *
 * @author linhptran
 */
public abstract class GalenTestBase extends GalenTestNgTestBase {
	protected static String baseURL = "";

	private String deviceModel = ConfigContants.getConfigParamValue(ConfigContants.CONF_KEY_TEST_DEVICES);
	private CSVReader reader;
	public PomConfig config = null;

	protected static final Logger LOGGER = Logger.getLogger(BasePage.class);
	protected static final long PAGELOAD_TIME_OUT = Integer
			.parseInt(ConfigContants.getConfigParamValue(ConfigContants.CONF_KEY_PAGE_LOAD_TIMEOUT));

	/**
	 * This method used to get configuration for Test run.
	 * 
	 * @param baseUrl
	 *            Base URL of the Web application under test
	 * @param browser
	 *            Target browser including Chrome, Firefox, Internet Explorer, Edge
	 * @param username
	 *            Username which use for login or signup 
	 * @param password
	 *            Password which use for login or signup 
	 * @param platform
	 *            Target device under test including Desktop, Tablet, Mobile
	 */
	@Parameters({ "BaseURL", "Browser", "Username", "Password", "Platform"})
	@BeforeSuite
	@BeforeGroups(groups = {"highest", "high", "medium", "low", "lowest"})
	public void testSuiteConfig(String baseUrl, String browser, String username, 
								String password, String platform) {
		config = new PomConfig(baseUrl, browser, username, password, platform);
	}

	/**
	 * This method used to get username from file bat
	 * 
	 * @return username from file bat
	 */
	public String getUsername() {
		return config.getUsername();
	}
	
	/**
	 * This method used to get password from file bat
	 * 
	 * @return password from file bat
	 */
	public String getPassword() {
		return config.getPassword();
	}
	
	/**
	 * This method used to create Browser Driver.
	 * 
	 * @param args
	 *            Array of the Test Devices
	 */
	public WebDriver createDriver(Object[] args) {
		if (!config.getBaseURL().isEmpty()) {
			baseURL = config.getBaseURL();
		} else {
			baseURL = ConfigContants.getConfigParamValue(ConfigContants.CONF_KEY_BASEURL);
		}

		WebDriver driver = null;

		if (!config.getBrowser().isEmpty()) {
			driver = setUpWebDriverInstance(config.getBrowser());
		} else {
			driver = setUpWebDriverInstance(ConfigContants.getConfigParamValue(ConfigContants.CONF_KEY_BROWSER));
		}

		if (args.length > 0) {
			if (args[0] != null && args[0] instanceof TestDevice) {
				TestDevice device = (TestDevice) args[0];

				if (device.getScreenSize() != null) {
					driver.manage().window().setSize(device.getScreenSize());
				}
			}
		}

		return driver;
	}

	/**
	 * This method used to set up Web Driver.
	 * 
	 * @param browser
	 *            name of browser want to set up
	 */
	private WebDriver setUpWebDriverInstance(String browser) {
		WebDriver driver = null;
		
		switch (browser) {
			case BrowserContants.BROWSER_CHROME:
				System.setProperty("webdriver.chrome.driver",
									ConfigContants.getConfigParamValue(ConfigContants.CONF_KEY_CHROME_DRIVER));
				ChromeOptions chromeOptions = new ChromeOptions();
	
				chromeOptions.addArguments("disable-infobars");
				chromeOptions.addArguments("enable-automation");
				chromeOptions.addArguments("ignore-certificate-errors");
	
				chromeOptions.setAcceptInsecureCerts(true);
				driver = new ChromeDriver(chromeOptions);
				break;
			case BrowserContants.BROWSER_FIREFOX:
				System.setProperty("webdriver.gecko.driver",
									ConfigContants.getConfigParamValue(ConfigContants.CONF_KEY_GECKO_DRIVER));
	
				break;
			case BrowserContants.BROWSER_EDGE:
				// setup Edge driver
	
				break;
			case BrowserContants.BROWSER_IE:
				// setup IE driver
	
				break;
			case BrowserContants.BROWSER_SAFARI:
				// setup Safari driver
	
				break;
			default:
				LOGGER.error("Can not find this browser");
				LOGGER.error("The browser contains chrome, firefox, edge, ie, safari");
				
				break;
		}

		driver.manage().timeouts().pageLoadTimeout(PAGELOAD_TIME_OUT, TimeUnit.SECONDS);

		return driver;
	}

	/**
	 * This method used to store the test devices into DataProvider. In this method,
	 * the test devices will get from CSV file or pom.xml file
	 * 
	 */
	@DataProvider(name = "devices")
	public Object[] devices() throws IOException {
		ArrayList<TestDevice> deviceList = new ArrayList<TestDevice>();
		
		reader = new CSVReader(new FileReader(deviceModel));
		String[] line;
		
		if (!config.getPlatform().isEmpty()) {
			while ((line = reader.readNext()) != null) {
				if(line[0].matches(config.getPlatform())) {
					deviceList.add(new TestDevice(line[0], new Dimension(Integer.parseInt(line[1]), 
							Integer.parseInt(line[2])), asList(line[0])));
				} else {
					LOGGER.error("Wrong Test devices name");
				}
			}
		} else {
			while ((line = reader.readNext()) != null) {
				try {
					if (line.length == 3)
						deviceList.add(new TestDevice(line[0], new Dimension(Integer.parseInt(line[1]), 
										Integer.parseInt(line[2])), asList(line[0])));
					else
						LOGGER.error("The length needs to be 3");
				} catch (NumberFormatException ex) {
					LOGGER.error("The size must be integer");
					LOGGER.error(ex.getMessage());
				}
			}
		}

		return deviceList.toArray();
	}

	/**
	 * This method used to create the Galen HTML Report After created, It will be
	 * stored in its TC reports folder.
	 * 
	 * @param specPath
	 *            Path of Galen gspec file
	 * @param devices
	 *            Devices use for this test case
	 * @param idTestCase
	 *            ID of this test case
	 * 
	 */
	public void createReport(String specPath, TestDevice devices, String idTestCase) throws IOException {
		LayoutReport layoutReport = Galen.checkLayout(this.getDriver(), specPath,
				new SectionFilter(devices.getTags(), Collections.<String>emptyList()), null);
		List<GalenTestInfo> tests = new LinkedList<GalenTestInfo>();

		LocalDateTime today = LocalDateTime.now();
		today = LocalDateTime.of(LocalDate.now(), LocalTime.now());
		DateTimeFormatter formatDateTime = DateTimeFormatter.ofPattern("dd-MM-yyyy HH-mm-ss");

		GalenTestInfo test = GalenTestInfo.fromString(idTestCase + " Test " + devices.toString() 
													+ " " + today.format(formatDateTime));
		test.getReport().layout(layoutReport, "check layout");
		tests.add(test);

		new HtmlReportBuilder().build(tests, "reports/" + idTestCase + "/" + today.format(formatDateTime));
	}

	/**
	 * <h1>Test Device Object</h1> This object contains Test device name, screen
	 * size and tags name of its
	 * 
	 * <p>
	 * 
	 * @author linhptran
	 *
	 */
	public static class TestDevice {
		private final String name;
		private final Dimension screenSize;
		private final List<String> tags;

		/**
		 * This is the constructor of Test Device Object
		 * 
		 * @param name
		 *            Name of test device
		 * @param screenSize
		 *            screen size of test device
		 * @param tags
		 *            Tags name of test device
		 */
		public TestDevice(String name, Dimension screenSize, List<String> tags) {
			this.name = name;
			this.screenSize = screenSize;
			this.tags = tags;
		}

		/**
		 * This method gets Test Devices name method
		 * 
		 * @return Test Devices name
		 */
		public String getName() {
			return name;
		}

		/**
		 * This method gets Screen Size method
		 * 
		 * @return Screen Size
		 */
		public Dimension getScreenSize() {
			return screenSize;
		}

		/**
		 * This method gets Tags method
		 * 
		 * @return List of Tags
		 */
		public List<String> getTags() {
			return tags;
		}

		/**
		 * This method returns a string with test devices name, screen width and screen
		 * height
		 * 
		 */
		@Override
		public String toString() {
			return String.format("%s %dx%d", name, screenSize.width, screenSize.height);
		}
	}

	/**
	 * <h1>Pom Config Object</h1> This Object contains BaseURL, browser, username,
	 * password and platform 
	 * 
	 * <p>
	 * 
	 * @author linhptran
	 *
	 */
	public static class PomConfig {
		private final String baseURL;
		private final String browser;
		
		private final String username;
		private final String password;

		private final String platform;

		/**
		 * This is the contructor of Pom Config Objects and its value get from pom.xml
		 * file
		 * 
		 * @param baseURL
		 *            Base URL which needs to test
		 * @param username
		 *            Username which needs to test
		 * @param password
		 *            Password which needs to test
		 * @param browser
		 *            Browser which needs to test
		 * @param platform
		 *            Platform which are desktop, tablet or mobile
		 */
		public PomConfig(String baseURL, String browser, String username, String password, String platform) {
			this.baseURL = baseURL;
			this.browser = browser;

			this.username = username;
			this.password = password;
			
			this.platform = platform;
		}

		/**
		 * This method gets Base URL method
		 * 
		 * @return Base URL
		 */
		public String getBaseURL() {
			return baseURL;
		}

		/**
		 * This method gets Browser method
		 * 
		 * @return Browser
		 */
		public String getBrowser() {
			return browser;
		}
		
		/**
		 * This method gets Username method
		 * 
		 * @return Username
		 */
		public String getUsername() {
			return username;
		}
		
		/**
		 * This method gets Password method
		 * 
		 * @return Password
		 */
		public String getPassword() {
			return password;
		}

		/**
		 * This method gets the Platform method
		 * 
		 * @return Platform
		 */
		public String getPlatform() {
			return platform;
		}
	}
}