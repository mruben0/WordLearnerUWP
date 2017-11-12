using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordLearner.Models
{
    public class DirectoryManager
    {       
        public string myFolder = "";       
        
        public string CreateAppdata(string ProgramFolder)
        {
            string appData = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            myFolder = Path.Combine(appData, ProgramFolder);
            return myFolder;
        }

        public void CopyFiles(string path, string destination)
        {           

           
            if (!Directory.Exists(destination))
            {
                Directory.CreateDirectory(destination);
            }

            string fileName = Path.GetFileName(path);
            string destFile = Path.Combine(destination, fileName);
            File.Copy(path, destFile, true);

            //if (fileFullNames.Length > 0)
            //{
            //    foreach (var fileFullName in fileFullNames)
            //    {
            //        string fileName = path.Substring(path.Length + 1);

            //        string dest = Path.Combine(destination, fileName);

            //        File.Copy(fileFullName, dest, true);                

            //    }

            //}          
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