using System.Collections.ObjectModel;
using System.Windows.Input;
using Windows.Storage;
using Windows.Storage.Pickers;
using WordLearner.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace WordLearner.ViewModels
{
    public class MainViewModel : BaseModel
    {
        public MainViewModel()
        {
            var DM = new DirectoryManager();
            var appdata = DM.GetAppDataPath("WordLearner");
            List<string> filecollection = DM.GetFileList(appdata, "xlsx");

            for (int i = 0; i < filecollection.Count; i++)
            {
                collection.Add(new File() { Name = filecollection.ElementAt(i) });
            }
        }

        public ObservableCollection<File> _collection = new ObservableCollection<File>();

        public ObservableCollection<File> collection
        {
            get => _collection;
            set => SetProperty(ref _collection, value);
        }

        private string path;

        public string Path
        {
            get => path;
            set => SetProperty(ref path, value);
        }

        private File selectedFile;

        public File SelectedFile
        {
            get => selectedFile;
            set => SetProperty(ref selectedFile, value);
        }

        public ICommand TestGetFIles
        {
            get => new RelayCommand(async () =>
            {
                FileOpenPicker openPicker = new FileOpenPicker();
                openPicker.ViewMode = PickerViewMode.Thumbnail;
                openPicker.SuggestedStartLocation = PickerLocationId.Desktop;
                openPicker.FileTypeFilter.Add(".xlsx");
                openPicker.FileTypeFilter.Add(".xls");
                openPicker.FileTypeFilter.Add(".WL");
                StorageFile file = await openPicker.PickSingleFileAsync();
                if (file != null)
                {
                    var DM = new DirectoryManager();
                    var appdata = DM.GetAppDataPath("WordLearner");
                    StorageFolder dest = await StorageFolder.GetFolderFromPathAsync(appdata);

                    if (await DM.IfStorageItemExist(dest, file.Name) == false)
                    {
                        await file.CopyAsync(dest);
                        List<string> filecollection = DM.GetFileList(appdata, "xlsx");
                        collection.Add(new File() { Name = file.Name });
                    }
                    else
                    {
                        var dialog = new Windows.UI.Popups.MessageDialog("Your file already exist. \n Please rename or choose another");
                        await dialog.ShowAsync();
                    }
                }
                else
                {
                    var dialog = new Windows.UI.Popups.MessageDialog("Please Choose File");
                    await dialog.ShowAsync();
                }
            });
        }

        public ICommand DeleteSelected
        {
            get => new RelayCommand(async () =>
             {
                 if (SelectedFile != null)
                 {
                     var DM = new DirectoryManager();
                     var appdata = DM.GetAppDataPath("WordLearner");
                     StorageFolder dest = await StorageFolder.GetFolderFromPathAsync(appdata);
                     var fileForDelete = await dest.GetFileAsync(SelectedFile.Name);
                     await fileForDelete.DeleteAsync();
                     collection.Remove(collection.FirstOrDefault(e => e.Name == SelectedFile.Name));
                 }
             });
        }

        public class File
        {
            public string Name { get; set; }
        }
    }
}