using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace WordLearner.Models
{
    public class DirectoryManager
    {       
        public string myFolder = "";       
        
        public string GetAppDataPath(string ProgramFolder)
        {
            string appData = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            myFolder = Path.Combine(appData, ProgramFolder);
            if (!Directory.Exists(myFolder))
            {
                Directory.CreateDirectory(myFolder);
            }
            return myFolder;
        }


        public List<string> GetFileList(string path, string format)
        {
            List<string> fileList = Directory.GetFiles(path, $"*.{format}").ToList();
            List<string> result = fileList.Select(e => e.Substring(path.Length + 1)).ToList();
            return result;
        }

        public bool DeleteFile(string fileName)
        {
            try
            {
                File.Delete(fileName);
                return true;
            }
            catch (Exception)
            {
                return false;
            }     
        }
    }
}