package com.qsrint.automation.ui.layout.libraries.pageobjects;

import org.openqa.selenium.WebDriver;

import com.qsrint.automation.ui.layout.contants.ConfigContants;
import com.qsrint.automation.ui.layout.contants.PageConstants;

/**
 * <h1>Login Page class</h1> This class contains find web elements and base
 * action funtions. This class will work on the login page.
 * 
 * <p>
 * 
 * @author linhptran
 *
 */
public class LoginPage extends BasePage {
	public final String PAGE_TITLE = "LoginPage";

	private final String logInTab = "XPath=//a[.='Log In']";
	private final String signUpTab = "XPath=//a[.='Sign Up']";

	private final String inputEmail = "XPath=//*[@name='email']";
	private final String inputPassword = "XPath=//*[@name='password']";

	private final String spanLogIn = "XPath=//button[@type='submit']/span[contains(text(),'Log In')]";
	private final String spanSignUp = "XPath=//button[@type='submit']/span[contains(text(),'Sign Up')]";

	/**
	 * This is the constructor of Login Page Object
	 * 
	 * @param driver
	 *            Current driver for testing
	 * @param baseURL
	 *            Base URL of Login page which uses to test
	 * 
	 */
	public LoginPage(WebDriver driver, String baseURL) {
		super(driver, baseURL, PageConstants.PAGE_LOGIN_URL);
	}

	/**
	 * This method will get the log in tab element
	 * 
	 * @return Log in tab element
	 * @throws InterruptedException
	 * 
	 */
	public BaseWebObject logInTab() throws InterruptedException {		
		return FindWebElement(logInTab,true);
	}

	/**
	 * This method will get the sign up tab element
	 * 
	 * @return Sign up tab element
	 * @throws InterruptedException
	 * 
	 */
	public BaseWebObject signUpTab() throws InterruptedException {
		return FindWebElement(signUpTab,true);
	}

	/**
	 * This method will get the input email element
	 * 
	 * @return Input email element
	 * @throws InterruptedException
	 */
	public BaseWebObject inputEmail() throws InterruptedException {
		return FindWebElement(inputEmail,false);
	}

	/**
	 * This method will get the input password element
	 * 
	 * @return Input password element
	 * @throws InterruptedException
	 */
	public BaseWebObject inputPassword() throws InterruptedException {
		return FindWebElement(inputPassword,false);
	}

	/**
	 * This method will get the span Log In element
	 * 
	 * @return Span log in element
	 * @throws InterruptedException
	 * 
	 */
	public BaseWebObject spanLogIn() throws InterruptedException {	
		return FindWebElement(spanLogIn,false);
	}

	/**
	 * This method will get the span sign up element
	 * 
	 * @return Span sign up element
	 * @throws InterruptedException
	 * 
	 */
	public BaseWebObject spanSignUp() throws InterruptedException {
		return FindWebElement(spanSignUp,false);
	}

	/**
	 * This method uses to login an account action
	 * 
	 * @param username
	 *            Username uses for login
	 * @param password
	 *            Password uses for login
	 *            
	 * @throws InterruptedException
	 * 
	 */
	public void inputLoginInfo(String username, String password) throws InterruptedException {
		if(username.isEmpty() || password.isEmpty()){
			username = ConfigContants.getConfigParamValue(ConfigContants.CONF_KEY_USERNAME);
			password = ConfigContants.getConfigParamValue(ConfigContants.CONF_KEY_PASSWORD);
		}
		logInTab().click();

		inputEmail().sendKeys(username, false);
		inputPassword().sendKeys(password, false);

		spanLogIn().click();
	}

	/**
	 * This method uses for sign up a new account action
	 * 
	 * @param username
	 *            Username uses for login
	 * @param password
	 *            Password uses for login
	 *    
	 * @throws InterruptedException
	 * 
	 */
	public void inputSignUpInfo(String username, String password) throws InterruptedException {
		if(username.isEmpty() || password.isEmpty()){
			username = ConfigContants.getConfigParamValue(ConfigContants.CONF_KEY_USERNAME);
			password = ConfigContants.getConfigParamValue(ConfigContants.CONF_KEY_PASSWORD);
		}
		
		signUpTab().click();

		inputEmail().sendKeys(username, false);
		inputPassword().sendKeys(password, false);

		spanSignUp().click();
	}

}
