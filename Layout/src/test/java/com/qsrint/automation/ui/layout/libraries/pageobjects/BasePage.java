package com.qsrint.automation.ui.layout.libraries.pageobjects;

import java.net.MalformedURLException;
import java.net.URL;

import org.openqa.selenium.By;
import org.openqa.selenium.NotFoundException;
import org.openqa.selenium.StaleElementReferenceException;
import org.openqa.selenium.WebDriver;
import org.openqa.selenium.support.ui.ExpectedConditions;
import org.openqa.selenium.support.ui.WebDriverWait;
import org.testng.log4testng.Logger;

import com.qsrint.automation.ui.layout.contants.ConfigContants;

/**
 * <h1>Base Page class</h1> This class contains some common basic actions
 * interact with page
 * 
 * @author linhptran
 *
 */
public class BasePage {
	/**
	 * Options that can be searched
	 *
	 */
	public enum SearchBy {
		/**
		 * By ID
		 */
		ID,

		/**
		 * By Xpath
		 */
		XPath,

		/**
		 * By Name
		 */
		Name,

		/**
		 * By Class Name
		 */
		ClassName,

		/**
		 * By Tag Name
		 */
		TagName,

		/**
		 * By Css Selector
		 */
		CssSelector
	}

	protected static final Logger LOGGER = Logger.getLogger(BasePage.class);

	public final WebDriver webDriverObj;
	public final String baseURL;
	public final String pageURL;

	public final long MAX_TIME_OUT_UI = Integer.parseInt(ConfigContants.getConfigParamValue(ConfigContants.CONF_KEY_PAGE_LOAD_TIMEOUT));
	public final long SLEEP_SHORT_TIME = Integer.parseInt(ConfigContants.getConfigParamValue(ConfigContants.CONF_KEY_SLEEP_SHORT_TIME));

	/**
	 * This is the constructor of Base Page Object
	 * 
	 * @param driver
	 *            current driver for testing
	 * @param baseURL
	 *            Base URL get from pom.xml
	 * @param pageURL
	 *            URL that page navigate to
	 */
	public BasePage(WebDriver driver, String baseURL, String pageURL) {
		this.webDriverObj = driver;
		this.baseURL = baseURL;
		this.pageURL = pageURL;
	}

	/**
	 * This method is get Web Element By Object by its locator
	 * 
	 * @param locator
	 *            Locator is a string contains the locator of its web element
	 * @return Web element by object
	 */
	public By getWebElementByObject(String locator) {
		By byObj = null;

		SearchBy searchType = SearchBy.ID;
		String searchStr = locator;

		if (locator.startsWith("//")) {
			searchType = SearchBy.XPath;
		} else {
			int idx = locator.indexOf('=');

			if (idx > 0) {
				try {
					searchType = SearchBy.valueOf(locator.substring(0, idx));
					searchStr = locator.substring(idx + 1);
				} catch (IllegalArgumentException e) {
					searchType = null;
				}
			}
		}

		switch (searchType) {
			case ID:
				byObj = By.id(searchStr);
				break;
			case XPath:
				byObj = By.xpath(searchStr);
				break;
			case Name:
				byObj = By.name(searchStr);
				break;
			case ClassName:
				byObj = By.className(searchStr);
				break;
			case TagName:
				byObj = By.tagName(searchStr);
				break;
			case CssSelector:
				byObj = By.cssSelector(searchStr);
				break;
			default:
				break;
		}

		return byObj;
	}

	/**
	 * This method will find the correct web element by knowing locator
	 * 
	 * @param locator
	 *            Locator is a string contains the locator of its web element
	 * @param waitForElementVisible
	 *            This param will check if wait for element visible
	 * @return its base web Object
	 * @throws InterruptedException 
	 */
	public BaseWebObject FindWebElement(String locator, boolean waitForElementVisible) throws InterruptedException {
		By byObj = getWebElementByObject(locator);
		BaseWebObject findObject = null;

		while (findObject == null) {
			try {
				if (byObj != null) {

					if (!waitForElementVisible) {
						findObject = new BaseWebObject(webDriverObj.findElement(byObj));
					} else {
						WebDriverWait wait = new WebDriverWait(webDriverObj, MAX_TIME_OUT_UI);
						findObject = new BaseWebObject(wait.until(ExpectedConditions.presenceOfElementLocated(byObj)));
					}
				}
			} catch (StaleElementReferenceException ex) {
				LOGGER.error(ex.getMessage());
			} catch (NotFoundException nex) {
				LOGGER.error(nex.getMessage());
			}
		}
		
		Thread.sleep(SLEEP_SHORT_TIME);
		return findObject;
	}

	/**
	 * This method will get the page URL
	 * 
	 * @return Base URL URL of page
	 * @throws MalformedURLException
	 */
	public String getURL() throws MalformedURLException {
		if (pageURL != null) {
			URL home = new URL(baseURL);
			URL url = new URL(home, pageURL);
			
			return url.toString();
		}

		return baseURL;
	}

	/**
	 * This method use for page navigation
	 */
	public void Navigate() {
		try {
			webDriverObj.navigate().to(getURL());
		} catch (MalformedURLException e) {
			e.printStackTrace();
		}
	}

	/**
	 * This method use for refresh page
	 */
	public void Refresh() {
		webDriverObj.navigate().refresh();
	}
}
