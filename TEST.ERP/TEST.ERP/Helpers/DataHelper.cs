using AutoIt;
using Newtonsoft.Json;

namespace TEST.ERP.Helpers
{
    public static class DataHelper
    {        
        private static string GetFile(string fileName)
        {
            if (!File.Exists(fileName))
            {
                throw new Exception($"Invalid path or file:{fileName} does not exists");
            }
            
            using var fileStream = new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.Read);
            using var streamReader = new StreamReader(fileStream);
            var result = streamReader.ReadToEnd();

            return result;
        }
        public static string GetDataFile(string folder, string subFolder, string fileName)
        {
            var basePath = AppDomain.CurrentDomain.BaseDirectory;

            // Move up from /bin/Debug or /bin/Release to project root
            var projectPath = Path.GetFullPath(Path.Combine(basePath, @"..\..\..\Models"));

            // Combine folder and subfolder
            var fullFilePath = Path.Combine(projectPath, folder);

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
            return jsonData ?? throw new InvalidOperationException("Failed to deserialize JSON");
        }
        public static string GetImportFile(string folder, string fileName, string extension = ".Xlsx")
        {
            try
            {
                // Base directory where the test assembly is running
                var basePath = AppDomain.CurrentDomain.BaseDirectory;

                // Go two levels up to project root and enter ImportFiles
                var projectPath = Path.GetFullPath(Path.Combine(basePath, @"..\..\..\Import"));

                // Add subfolder if provided (e.g. "Accounting")
                var fullFolderPath = Path.Combine(projectPath, folder);

                // Add the file name with .xlsx extension
                var fullFilePath = Path.Combine(fullFolderPath, $"{fileName}{extension}");

                // Check if file exists
                return !File.Exists(fullFilePath) ? throw new FileNotFoundException($"File not found: {fullFilePath}") :
                    // Return full path to be passed to UploadFile
                    fullFilePath;
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
