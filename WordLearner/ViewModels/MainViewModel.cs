using System.Collections.ObjectModel;
using System.Windows.Input;
using WordLearner.Models;

namespace WordLearner.ViewModels
{
    public class MainViewModel : BaseModel
    {
        public MainViewModel()
        {
            test t1 = new test() { Name = "aadad" };
            test t = new test() { Name = "aadad" };
            test t2 = new test() { Name = "Gago" };
            test t3 = new test() { Name = "Gago" };

            collection.Add(t1);
            collection.Add(t);
            collection.Add(t2);
            collection.Add(t3);
        }
        public ObservableCollection<test> _collection = new ObservableCollection<test>();
        public ObservableCollection<test> collection
        {
            get => _collection;
            set => SetProperty(ref _collection, value);
        }

        private string path;
        public  string Path
        {
            get => path;
            set => SetProperty(ref path, value);
        }

        private test selectedFile;
        public test SelectedFile
        {
            get => selectedFile;
            set => SetProperty(ref selectedFile, value);
        }

        public ICommand AddSelected
        {
            get => new RelayCommand(() =>
            {
                IOManager iOManager = new IOManager();
                if (iOManager.IsValidFormat("xlsx", Path) || iOManager.IsValidFormat("xls", Path))
                {
                    DirectoryManager directoryManager = new DirectoryManager();
                    string appdata = directoryManager.CreateAppdata("WordLearner");
                    directoryManager.CopyFiles(path, appdata);
                }
            });
        }

        public ICommand DeleteSelected
        {
            get => new RelayCommand(() =>
             {
                 if (SelectedFile != null)
                 {
                     collection.Remove(SelectedFile);
                 }
                 
             });
        }

        public class test
        {
            public string Name { get; set; }
        }
    }
}
