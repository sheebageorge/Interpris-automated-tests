
namespace Automation.UI.Core
{
    /// <summary>
    /// Project configuration constants
    /// Get configured parameters from App Config file
    /// </summary>
    public class ProjectConfigParams
    {
        /// <summary>
        /// Get config param value by param name
        /// </summary>
        /// <param name="configureParamName">Config param name</param>
        /// <returns>Config param value</returns>
        public static string GetConfigParamValue(string configureParamName)
        {
            return System.Configuration.ConfigurationManager.AppSettings[configureParamName];
        }

        #region Data Driven Infor
        public const string CONF_KEY_DATA_FOLDER_NAME = "DataFolder";
        public const string CONF_KEY_DATA_MAPPINGS_FILE_NAME = "DataMappings";
        public const string CONF_KEY_COMMON_DATA_MAPPING_FILE_NAME = "CommonDataMappings";
        public const string CONF_KEY_TC_DATA_MAPPINGS_FILE_NAME = "TestCaseDataMappings";
        public const string CONF_KEY_SR_DATA_MAPPINGS_FILE_NAME = "StoryDataMappings";
        public const string CONF_KEY_DOWNLOAD_FOLDER_NAME = "DownloadFolder";

        public const string CONF_KEY_PAGE_LOAD_TIMEOUT = "PageLoadTimeout";

        public const string CONF_KEY_DEFAULT_BROWSER = "Browser";
        public const string CONF_KEY_DEFAULT_SCREEN_SIZE = "ScreenSize";

        public const string CONF_KEY_BROWSERSTACK_ENABLED = "browserstack.enabled";
        public const string CONF_KEY_BROWSERSTACK_USER = "browserstack.user";
        public const string CONF_KEY_BROWSERSTACK_KEY = "browserstack.key";
        public const string CONF_KEY_BROWSERSTACK_OS = "os";
        public const string CONF_KEY_BROWSERSTACK_OS_VERSION = "os_version";
        public const string CONF_KEY_BROWSERSTACK_RESOLUTION = "resolution";
        #endregion

        #region Configuration by environments
        public const string SUPPORTED_ENV_NAME = "TestEnv";
        public const string SUPPORTED_ENV_QA = "QA";
        public const string SUPPORTED_ENV_TEST = "Test";

        public const string HEAD_CONF_KEY_PLATFORM_BASEURL = "PlatformBaseURL";
        public const string HEAD_CONF_KEY_TRANSCRIPTION_BASEURL = "TranscriptionBaseURL";
        public const string HEAD_CONF_KEY_INTERPRIS_BASEURL = "InterprisBaseURL";
        #endregion
    }
}
