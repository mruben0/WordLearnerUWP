using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WordLearner.Models;
using Windows.Storage;

namespace WordLearner.ViewModels
{
    public class ProgressViewModel : BaseModel
    {
        private DirectoryManager DM;
        private IOManager managerIO;
        private string appData;
        private string savedPlace;
        private HelperTools tools;

        public ProgressViewModel()
        {
            this.tools = new HelperTools();
            this.DM = new DirectoryManager();
            this.managerIO = new IOManager();
            appData = DM.GetAppDataPath("WordLearner");
            savedPlace = Path.Combine(appData, "Saves");
        }

        public ICommand GetCollection
        {
            get => new RelayCommand(async () =>
            {
                try
                {
                    StorageFolder sf = await StorageFolder.GetFolderFromPathAsync(savedPlace);
                    StorageFile LearnedDataFile = await sf.GetFileAsync("LearnedData.wl");
                    StorageFile MistakedFile = await sf.GetFileAsync("MistakeData.wl");
                    var learn = await Windows.Storage.FileIO.ReadLinesAsync(LearnedDataFile);
                    var mistaken = await Windows.Storage.FileIO.ReadLinesAsync(MistakedFile);

                    if (Learned.Count == 0)
                    {
                        foreach (var item in learn)
                        {
                            Learned.Add(item);
                        }
                    }

                    if (Mistaked.Count == 0)
                    {
                        foreach (var item in mistaken)
                        {
                            Mistaked.Add(item);
                        }
                    }
                }
                catch (Exception e)
                {
                    var dialog = new Windows.UI.Popups.MessageDialog("You don't have saved Data. \nPlease Learn some word and click \"Save\" button in \"Learn Word\" Page ");
                    await dialog.ShowAsync();
                }
            });
        }

        public ICommand DelCollection
        {
            get => new RelayCommand(async () =>
             {
                 try
                 {
                     tools.CleanList(Learned);
                     tools.CleanList(Mistaked);
                     StorageFolder sf = await StorageFolder.GetFolderFromPathAsync(savedPlace);
                     StorageFile LearnedDataFile = await sf.GetFileAsync("LearnedData.wl");
                     StorageFile MistakedFile = await sf.GetFileAsync("MistakeData.wl");
                     await Windows.Storage.FileIO.WriteTextAsync(LearnedDataFile, "");
                     await Windows.Storage.FileIO.WriteTextAsync(MistakedFile, "");
                 }
                 catch (Exception)
                 {
                     var dialog = new Windows.UI.Popups.MessageDialog("You don't have saved Data. \nPlease Learn some word and click \"Save\" button in \"Learn Word\" Page ");
                     await dialog.ShowAsync();
                 }
             });
        }

        private ObservableCollection<string> learned = new ObservableCollection<string>();

        public ObservableCollection<string> Learned
        {
            get => learned;
            set => SetProperty(ref learned, value);
        }

        private ObservableCollection<string> mistaked = new ObservableCollection<string>();

        public ObservableCollection<string> Mistaked
        {
            get => mistaked;
            set => SetProperty(ref mistaked, value);
        }
    }
}