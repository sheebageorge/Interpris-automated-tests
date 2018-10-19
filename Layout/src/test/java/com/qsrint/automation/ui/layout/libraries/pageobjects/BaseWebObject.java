package com.qsrint.automation.ui.layout.libraries.pageobjects;

import org.openqa.selenium.WebElement;

import com.qsrint.automation.ui.layout.contants.ConfigContants;

/**
 * <h1>Base Web Object class</h1> This class contains some basic actions
 * interact with web element using Selenium
 * 
 * <p>
 * 
 * @author linhptran
 *
 */
public class BaseWebObject {
	public final WebElement webElementObj;
	public final long SLEEP_SHORT_TIME = Integer.parseInt(ConfigContants.getConfigParamValue(ConfigContants.CONF_KEY_SLEEP_SHORT_TIME));

	/**
	 * This is the constructor of Base Web Object
	 * 
	 * @param webElementObj
	 *            initial web element object
	 */
	public BaseWebObject(WebElement webElementObj) {
		this.webElementObj = webElementObj;
	}

	/**
	 * This method will get the text of its element
	 * 
	 * @return Text of its element
	 */
	public String getText() {
		return webElementObj.getText();
	}

	/**
	 * This method will get the value of its element
	 * 
	 * @return Value of its element
	 */
	public String getValue() {
		return webElementObj.getAttribute("value");
	}

	/**
	 * This method uses for click its element and wait 5s for loading
	 * 
	 * @throws InterruptedException
	 */
	public void click() throws InterruptedException {
		webElementObj.click();
		Thread.sleep(SLEEP_SHORT_TIME);
	}

	/**
	 * This method uses for send input text action
	 * 
	 * @param inputText
	 *            the value of this field element
	 * @param isEnter
	 *            This param will check if user want to enter after input
	 */
	public void sendKeys(String inputText, boolean isEnter) {
		webElementObj.clear();
		webElementObj.sendKeys(inputText);

		if (isEnter) {
			webElementObj.submit();
		}
	}
}
