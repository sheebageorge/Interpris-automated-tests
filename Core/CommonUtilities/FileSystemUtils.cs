using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Automation.UI.Core.CommonUtilities
{
    public class FileSystemUtils
    {
        /// <summary>
        /// Get list of names of all files in folder
        /// </summary>
        /// <param name="folderPath">Path to the folder</param>
        /// <returns>List of file names</returns>
        public static IList<string> GetFileNameList(string folderPath)
        {
            IList<string> fileNameList = new List<string>();

            string[] fileNames = Directory.GetFiles(folderPath);

            foreach(string fileName in fileNames)
            {
                fileNameList.Add(Path.GetFileName(fileName));
            }

            return fileNameList;
        }

        /// <summary>
        /// Delete all files which matched the template name
        /// </summary>
        /// <param name="folderPath"></param>
        /// <param name="fileNameTemplate"></param>
        public static void DeleteFilesByNameContained(string folderPath, string fileNameTemplate)
        {
            string[] fileNames = Directory.GetFiles(folderPath);

            foreach (string fileName in fileNames)
            {
                if (fileName.Contains(fileNameTemplate))
                {
                    File.Delete(fileName);
                }
            }
        }

        /// <summary>
        /// Wait until the expecting file exists
        /// </summary>
        /// <param name="folderPath"></param>
        /// <param name="fileName"></param>
        /// <param name="MAX_WAIT"></param>
        /// <returns>True if the find found; otherwise, False</returns>
        public static bool WaitUntilFileExists(string folderPath, string fileName, int MAX_WAIT = 60)
        {
            int i = 0;
            while(i < MAX_WAIT)
            {
                i++;

                if (File.Exists(Path.Combine(folderPath, fileName)))
                {
                    return true;
                }

                ThreadUtils.SleepShortTime();
            }

            return false;
        }

        /// <summary>
        /// Get list of names of all files in folder by pattern
        /// </summary>
        /// <param name="folderPath">Path to the folder</param>
        /// <param name="searchPattern">Pattern to get files</param>
        /// <returns>List of file names</returns>
        public static IList<string> GetFileNameListByPattern(string folderPath, string searchPattern)
        {
            IList<string> fileNameList = new List<string>();

            string[] fileNames = Directory.GetFiles(folderPath, searchPattern);

            foreach (string fileName in fileNames)
            {
                fileNameList.Add(Path.GetFileName(fileName));
            }

            return fileNameList;
        }

        /// <summary>
        /// Build full path for input file
        /// </summary>
        /// <param name="folderPath"></param>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static string GetFullFilePath(string folderPath, string fileName)
        {
            // build the file path
            string filePath = Path.Combine(folderPath, fileName);

            if (!Path.IsPathRooted(filePath))
            {
                filePath = Path.GetFullPath(filePath);
            }

            return filePath;
        }

        /// <summary>
        /// Get random file name based on the original file name
        /// </summary>
        /// <param name="sourceFileName"></param>
        /// <returns></returns>
        public static string GetRandomFileName(string sourceFileName)
        {
            int dotPos = sourceFileName.LastIndexOf('.');

            if (dotPos > 0)
            {
                return sourceFileName.Substring(0, dotPos) + "_" + RandomUtils.GetMilisecondString() +
                    sourceFileName.Substring(dotPos);
            }

            return sourceFileName + "_" + RandomUtils.GetMilisecondString();
        }

        /// <summary>
        /// Copy a file to a new folder with new file name
        /// </summary>
        /// <param name="sourceFolder">Source folder</param>
        /// <param name="sourceFileName">Source file name</param>
        /// <param name="desFolder">Destination folder</param>
        /// <param name="desFileName">Destination file name</param>
        /// <returns>True if file copying successfully; otherwise, False</returns>
        public static bool CopyFileTo(string sourceFolder, string sourceFileName, string desFolder, string desFileName)
        {
            string sourceFile = Path.Combine(sourceFolder, sourceFileName);
            string destFile = Path.Combine(desFolder, desFileName);

            if (!Directory.Exists(desFolder))
            {
                Directory.CreateDirectory(desFolder);
            }

            File.Copy(sourceFile, destFile, true);

            return File.Exists(destFile);
        }

        /// <summary>
        /// Try to clone from the original file to a new one with new name
        /// </summary>
        /// <param name="folderPath">File folder</param>
        /// <param name="sourceFileName">Source file name</param>
        /// <param name="tailTagValue">Tail tag to make new file</param>
        /// <returns>Name of the new file</returns>
        public static string CloneFileByTailTag(string folderPath, string sourceFileName, string tailTagValue)
        {
            string newFileName;

            int dotPos = sourceFileName.LastIndexOf('.');

            if (dotPos > 0)
            {
                newFileName = sourceFileName.Substring(0, dotPos) + "_" + tailTagValue +
                    sourceFileName.Substring(dotPos);
            }
            else
            {
                newFileName = sourceFileName + "_" + tailTagValue;
            }

            CopyFileTo(folderPath, sourceFileName, folderPath, newFileName);

            return newFileName;
        }

        /// <summary>
        /// Create a list of files
        /// </summary>
        /// <param name="parenFolder">Parent folder</param>
        /// <param name="fileNameList">List of file names</param>
        public static void CreateFiles(string parenFolder, IList<string> fileNameList, string fileSize, bool rewrite = false)
        {
            // don't create the file if it is existing
            foreach (string fileName in fileNameList)
            {
                string fullFilePath = Path.Combine(parenFolder, fileName);

                if (rewrite || !File.Exists(fullFilePath))
                {
                    ProcessUtils.RunProcessAndWaitFinish(string.Format("fsutil file createnew {0} {1}", fullFilePath, fileSize));
                }
            }
        }

        /// <summary>
        /// Create Folder and Files which are not existed in current machine
        /// </summary>
        /// <param name="folderPath">folder path want to create</param>
        /// <param name="fileNames"> files want to create</param>
        /// <param name="fileSize">file sizes</param>
        public static void CreateFilesNotExistedInFolder(string folderPath, List<string> fileNames, string fileSize)
        {
            //Create folder if not existing
            Directory.CreateDirectory(folderPath);

            // create the file if not existing
            List<string> fileNamesInfolder = FileSystemUtils.GetFileNameList(folderPath).ToList();
            if (!fileNamesInfolder.SequenceEqual(fileNames))
            {
                IList<string> generateTestFiles = fileNames.Except(fileNamesInfolder).ToList();
                FileSystemUtils.CreateFiles(folderPath, generateTestFiles, fileSize);
            }
        }
    }
}
