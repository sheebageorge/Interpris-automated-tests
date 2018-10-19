package com.qsrint.automation.ui.layout.tests;

import java.io.IOException;

import org.testng.annotations.Test;

import com.qsrint.automation.ui.layout.contants.GalenFileContants;
import com.qsrint.automation.ui.layout.contants.TestPriorityContants;
import com.qsrint.automation.ui.layout.libraries.GalenTestBase;
import com.qsrint.automation.ui.layout.libraries.pageobjects.LandingPage;
import com.qsrint.automation.ui.layout.libraries.pageobjects.LoginPage;

/**
 * <h1>Upload Page Test class</h1> This class contain 3 test cases. First test
 * is Header in upload page, second test is Upload content, third test is select
 * language in upload page
 * 
 * <p>
 * 
 * @author linhptran
 *
 */
public class UploadPageTest extends GalenTestBase {

	/**
	 * This is test Upload Page layout
	 * 
	 * @param device
	 *            devices for testing and its value get from data provider
	 * @throws IOException
	 * @throws InterruptedException
	 */
	@Test(groups = TestPriorityContants.HIGHEST, dataProvider = "devices")
	public void tc_ul_894_layout_uploadContentOnDevice(TestDevice device) throws IOException, InterruptedException {
		LoginPage loginPage = new LoginPage(this.getDriver(), baseURL);
		LandingPage landingPage = new LandingPage(this.getDriver(), baseURL);
	
		loginPage.Navigate();
		loginPage.inputLoginInfo(getUsername(),getPassword());

		landingPage.Navigate();
		landingPage.uploadTab().click();

		checkLayout(GalenFileContants.UPLOAD_CONTENT_UPLOADPAGE, device.getTags());
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
	public void tc_ul_792_layout_headerOnDevice(TestDevice device) throws IOException, InterruptedException {
		LoginPage loginPage = new LoginPage(this.getDriver(), baseURL);
		LandingPage landingPage = new LandingPage(this.getDriver(), baseURL);

		loginPage.Navigate();
		loginPage.inputLoginInfo(getUsername(),getPassword());

		landingPage.Navigate();
		landingPage.uploadTab().click();
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
	public void tc_ul_897_layout_selectLanguageDropdownOnDevice(TestDevice device) throws IOException, InterruptedException {
		LoginPage loginPage = new LoginPage(this.getDriver(), baseURL);
		LandingPage landingPage = new LandingPage(this.getDriver(), baseURL);

		loginPage.Navigate();
		loginPage.inputLoginInfo(getUsername(),getPassword());

		landingPage.Navigate();
		
		landingPage.uploadTab().click();
		landingPage.spanSelectLang().click();
		
		checkLayout(GalenFileContants.SELECT_LANGUAGE_DROPDOWN_UPLOADPAGE, device.getTags());
	}

}
