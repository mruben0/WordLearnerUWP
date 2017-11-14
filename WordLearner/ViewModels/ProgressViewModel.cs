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
        StorageFolder sf;
        StorageFile LearnedDataFile;
        StorageFile MistakedFile;

        public ProgressViewModel()
        {
            this.DM = new DirectoryManager();
            this.managerIO = new IOManager();
            appData = DM.GetAppDataPath("WordLearner");
            savedPlace = Path.Combine(appData, "Saves");
        }

        //public override async Task InitAsync()
        //{   
        //    sf = await StorageFolder.GetFolderFromPathAsync(savedPlace);
        //    LearnedDataFile = await sf.GetFileAsync("LearnedData.wl");
        //    MistakedFile = await sf.GetFileAsync("MistakeData");
        //    var l = await Windows.Storage.FileIO.ReadLinesAsync(LearnedDataFile);
        //    var m = await Windows.Storage.FileIO.ReadLinesAsync(MistakedFile);

        //    Learned = (ObservableCollection<string>)l;
        //    Mistaked = (ObservableCollection<string>)m;
        //}
        //public override async Task init()
        //{
        //        sf = await StorageFolder.GetFolderFromPathAsync(savedPlace);
        //        LearnedDataFile = await sf.GetFileAsync("LearnedData.wl");
        //        MistakedFile = await sf.GetFileAsync("MistakeData");
        //        var l = await Windows.Storage.FileIO.ReadLinesAsync(LearnedDataFile);
        //        var m = await Windows.Storage.FileIO.ReadLinesAsync(MistakedFile);

        //        Learned = (ObservableCollection<string>)l;
        //        Mistaked = (ObservableCollection<string>)m;

        //}


        public ICommand getCollection
        {
            get => new RelayCommand(async () =>
            {
                sf = await StorageFolder.GetFolderFromPathAsync(savedPlace);
                LearnedDataFile = await sf.GetFileAsync("LearnedData.wl");
                MistakedFile = await sf.GetFileAsync("MistakeData");
                var l = await Windows.Storage.FileIO.ReadLinesAsync(LearnedDataFile);
                var m = await Windows.Storage.FileIO.ReadLinesAsync(MistakedFile);

                Learned = (ObservableCollection<string>)l;
                Mistaked = (ObservableCollection<string>)m;
            });
        }

        private ObservableCollection<string> learned;
        public ObservableCollection<string> Learned
        {
            get => learned;
            set => SetProperty(ref learned, value); 
        }

        private ObservableCollection<string> mistaked;
        public ObservableCollection<string> Mistaked
        {
            get => mistaked;
            set => SetProperty(ref mistaked, value);
        }
    }
}