package com.qsrint.automation.ui.layout.libraries.pageobjects;

import org.openqa.selenium.WebDriver;

import com.qsrint.automation.ui.layout.contants.PageConstants;

/**
 * <h1>Landing Page class</h1> This class contains find web elements and base
 * action funtions. This class will work on the landing page.
 * 
 * <p>
 * 
 * @author linhptran
 *
 */
public class LandingPage extends BasePage {
	private final String uploadTab = "XPath=//div[@role='tab']/span[contains(text(),'Upload')]";
	private final String allTab = "XPath=//div[@role='tab']/span[contains(text(),'All')]";
	
	private final String spanAvatar = "XPath=//div[@class='avatar']/span";
	private final String spanSelectLang = "XPath=//*[@aria-hidden='false']//div[contains(@class,'select-language-pack')]";

	/**
	 * This is the constructor of Landing Page Object
	 * 
	 * @param driver
	 *            Current driver for testing
	 * @param baseURL
	 *            Base URL of Landing page which uses to test
	 */
	public LandingPage(WebDriver driver, String baseURL) {
		super(driver, baseURL, PageConstants.PAGE_LANDING_URL);
	}

	/**
	 * This method will get the upload tab element
	 * 
	 * @return Upload tab element
	 * @throws InterruptedException
	 */
	public BaseWebObject uploadTab() throws InterruptedException {	
		return FindWebElement(uploadTab,true);
	}

	/**
	 * This method will get the span avatar element
	 * 
	 * @return Span avatar element
	 * @throws InterruptedException
	 */
	public BaseWebObject spanAvatar() throws InterruptedException {
		return FindWebElement(spanAvatar,true);
	}
	
	/**
	 * This method will get the span select language element
	 * 
	 * @return Span select language element
	 * @throws InterruptedException
	 */
	public BaseWebObject spanSelectLang() throws InterruptedException {	
		return FindWebElement(spanSelectLang,true);
	}
	
	/**
	 * This method will get the all tab element
	 * 
	 * @return All tab element
	 * @throws InterruptedException
	 */
	public BaseWebObject allTab() throws InterruptedException {	
		return FindWebElement(allTab,true);
	}
}
