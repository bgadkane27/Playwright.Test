using AutoIt;
using Microsoft.Playwright;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace TEST.ERP.Helpers
{
    public static class DataHelper
    {        
        public static string GetFile(string fileName)
        {
            string result;
            if (!File.Exists(fileName))
            {
                throw new Exception($"Invalid path or file:{fileName} does not exists");
            }
            using (var fileStream = new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                using (var streamReader = new StreamReader(fileStream))
                {
                    result = streamReader.ReadToEnd();
                }
            }
            return result;
        }
        public static string GetDataFile(string folder, string subFolder, string fileName)
        {
            string basePath = AppDomain.CurrentDomain.BaseDirectory;

            // Move up from /bin/Debug or /bin/Release to project root
            string projectPath = Path.GetFullPath(Path.Combine(basePath, @"..\..\..\Models"));

            // Combine folder and subfolder
            string fullFilePath = Path.Combine(projectPath, folder);

            if (!string.IsNullOrEmpty(subFolder))
            {
                fullFilePath = Path.Combine(fullFilePath, subFolder);
            }

            // Add file name with .json extension
            fullFilePath = Path.Combine(fullFilePath, $"{fileName}.json");

            return GetFile(fullFilePath);
        }
        public static T ConvertJsonDataModel<T>(string fileData)
        {
            var jsonData = JsonConvert.DeserializeObject<T>(fileData);
            if (jsonData == null)
                throw new InvalidOperationException("Failed to deserialize JSON");
            return jsonData;
        }
        public static string GetImportFile(string folder, string fileName, string extension = ".Xlsx")
        {
            try
            {
                // Base directory where the test assembly is running
                string basePath = AppDomain.CurrentDomain.BaseDirectory;

                // Go two levels up to project root and enter ImportFiles
                string projectPath = Path.GetFullPath(Path.Combine(basePath, @"..\..\..\Import"));

                // Add subfolder if provided (e.g. "Accounting")
                string fullFolderPath = Path.Combine(projectPath, folder);

                // Add the file name with .xlsx extension
                string fullFilePath = Path.Combine(fullFolderPath, $"{fileName}{extension}");

                // Check if file exists
                if (!File.Exists(fullFilePath))
                    throw new FileNotFoundException($"File not found: {fullFilePath}");

                // Return full path to be passed to UploadFile
                return fullFilePath;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error locating import file '{fileName}{extension}': {ex.Message}", ex);
            }
        }
        public static void UploadFile(string filePath)
        {
            try
            {
                // Wait up to 10 seconds for the dialog
                AutoItX.WinWaitActive("Open", "", 10);

                // Set the file path
                AutoItX.ControlSetText("Open", "", "Edit1", filePath);

                // Wait up to 10 seconds for the dialog
                AutoItX.WinWaitActive("Open", "", 2);

                // Click Open
                AutoItX.ControlClick("Open", "", "Button1");
                Thread.Sleep(2000);
            }
            catch (Exception ex)
            {
                throw new Exception($"File upload failed: {ex.Message}", ex);
            }
        }                     
    }
}
