package com.qsrint.automation.ui.layout.contants;

import java.io.File;
import java.io.FileInputStream;
import java.io.IOException;
import java.util.Properties;

/**
 * <h1>Config Constants class</h1> This class contains config constants and read
 * file config.xml.
 * 
 * <p>
 *
 * @author linhptran
 *
 */
public interface ConfigContants {

	/**
	 * This is the method of reading from config.xml file
	 * 
	 * @param configureParamName
	 *            Param Name represent with each element's tag from config file
	 * @return corresponding value
	 */
	public static String getConfigParamValue(String configureParamName) {
		File rootPath = new File(CONF_KEY_FILE_CONFIG_NAME);

		String configPath = rootPath.getPath();
		Properties configProps = new Properties();

		try {
			configProps.loadFromXML(new FileInputStream(configPath));
		} catch (IOException e) {
			e.printStackTrace();
		}

		return configProps.getProperty(configureParamName);
	}

	public String CONF_KEY_FILE_CONFIG_NAME = "config.xml";
	public String CONF_KEY_TEST_DEVICES = "TestDevices";

	public String CONF_KEY_BASEURL = "BaseURL";
	public String CONF_KEY_BROWSER = "Browser";
	
	public String CONF_KEY_USERNAME = "Username";
	public String CONF_KEY_PASSWORD = "Password";
	
	public String CONF_KEY_PAGE_LOAD_TIMEOUT = "PageLoadTimeout";
	public String CONF_KEY_SLEEP_SHORT_TIME = "SleepShortTime";

	public String CONF_KEY_GECKO_DRIVER = "GeckoDriver";
	public String CONF_KEY_CHROME_DRIVER = "ChromeDriver";
}
