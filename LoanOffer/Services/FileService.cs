using LoanOffer.Interfaces;

namespace LoanOffer.Services
{
    public class FileService : IFile
    {
        private IConfiguration configuration;
        public FileService(IConfiguration _configuration)
        {
            configuration = _configuration;
        }

        public bool CheckIfFileExist(string fileName)
        {
            string fullpath = GetFullPath(fileName);

            if (File.Exists(fullpath))
                return true;
            return false;
        }

        public string ReadFromFile(string fileName)
        {
            try
            {
                string fullpath = GetFullPath(fileName);
                string json = "";

                if (File.Exists(fullpath))
                {
                    json = File.ReadAllText(fullpath);
                }
                else
                    throw new Exception($"File not exist in path {fullpath}");

                return json;
            }
            catch(Exception ex)
            {
                throw new Exception("Exception when try to read from file", ex);
            }
        }

        public void WriteToFile(string json, string fileName)
        {
            try
            {
                string folderName = GetFolderPath();
                if (!Directory.Exists(folderName))
                {
                    Directory.CreateDirectory(folderName);
                }

                string fullpath = GetFullPath(fileName);

                File.WriteAllText(fullpath, json);
            }
            catch (Exception ex)
            {
                throw new Exception("Exception when try write into file", ex);
            }
        }

        private string GetFullPath(string fileName)
        {
            string path_to_file = GetFolderPath();
            string pathvar = Environment.CurrentDirectory;
            string fullpath = $"{pathvar}{path_to_file}\\{fileName}.json";

            return fullpath;
        }

        private string GetFolderPath()
        {
            var path_to_file = configuration.GetSection("AppSettings:Path_To_File");
            return path_to_file.Value;
        }
    }
}
