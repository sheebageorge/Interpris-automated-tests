package com.qsrint.automation.ui.layout.tests;

import java.io.IOException;

import org.testng.annotations.Test;

import com.qsrint.automation.ui.layout.contants.GalenFileContants;
import com.qsrint.automation.ui.layout.contants.TestPriorityContants;
import com.qsrint.automation.ui.layout.libraries.GalenTestBase;
import com.qsrint.automation.ui.layout.libraries.pageobjects.LandingPage;
import com.qsrint.automation.ui.layout.libraries.pageobjects.LoginPage;

/**
 * <h1>All Page Test class</h1> This class contain 3 test cases. First test
 * is Header in All page, second test is All content, third test is select
 * language in All page
 * 
 * <p>
 * 
 * @author linhptran
 *
 */
public class AllPageTest extends GalenTestBase {
	/**
	 * This is test All Page layout
	 * 
	 * @param device
	 *            devices for testing and its value get from data provider
	 * @throws IOException
	 * @throws InterruptedException
	 */
	@Test(groups = TestPriorityContants.HIGHEST, dataProvider = "devices")
	public void tc_ul_793_layout_uploadContentOnDevice(TestDevice device) throws IOException, InterruptedException {
		LoginPage loginPage = new LoginPage(this.getDriver(), baseURL);
		LandingPage landingPage = new LandingPage(this.getDriver(), baseURL);
	
		loginPage.Navigate();
		loginPage.inputLoginInfo(getUsername(),getPassword());

		landingPage.Navigate();
		landingPage.allTab().click();

		checkLayout(GalenFileContants.ALL_CONTENT_ALLPAGE, device.getTags());
	}
	
	/**
	 * This is test header and avatar menu dropdown
	 * 
	 * @param device
	 *            device devices for testing and its value get from data provider
	 * @throws IOException
	 * @throws InterruptedException
	 */
	@Test(groups = TestPriorityContants.HIGHEST, dataProvider = "devices")
	public void tc_ul_1070_layout_headerOnDevice(TestDevice device) throws IOException, InterruptedException {
		LoginPage loginPage = new LoginPage(this.getDriver(), baseURL);
		LandingPage landingPage = new LandingPage(this.getDriver(), baseURL);

		loginPage.Navigate();
		loginPage.inputLoginInfo(getUsername(),getPassword());

		landingPage.Navigate();
		
		landingPage.allTab().click();
		landingPage.spanAvatar().click();

		checkLayout(GalenFileContants.HEADER, device.getTags());
	}

	/**
	 * This is test select language
	 * 
	 * @param device
	 *            device devices for testing and its value get from data provider
	 * @throws IOException
	 * @throws InterruptedException
	 */
	@Test(groups = TestPriorityContants.HIGHEST, dataProvider = "devices")
	public void tc_ul_1071_layout_selectLanguageDropdownOnDevice(TestDevice device) throws IOException, InterruptedException {
		LoginPage loginPage = new LoginPage(this.getDriver(), baseURL);
		LandingPage landingPage = new LandingPage(this.getDriver(), baseURL);

		loginPage.Navigate();
		loginPage.inputLoginInfo(getUsername(),getPassword());

		landingPage.Navigate();
		
		landingPage.allTab().click();
		landingPage.spanSelectLang().click();
		
		checkLayout(GalenFileContants.SELECT_LANGUAGE_DROPDOWN_UPLOADPAGE, device.getTags());
	}
}
