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
    public class LearnWordsViewModel : BaseModel
    {
        private IOManager manager;
        private DirectoryManager DM;
        private string appdata;
        private int index;

        public LearnWordsViewModel()
        {
            this.DM = new DirectoryManager();
            this.manager = new IOManager();
            this.appdata = DM.GetAppDataPath("WordLearner");
            this.AnswLetter = "B";
            this.AskLetter = "A";
            this.Result = "Result";
            this.WrongCount = 0;
            List<string> filecollection = DM.GetFileList(appdata, "xlsx");

            for (int i = 0; i < filecollection.Count; i++)
            {
                Collection.Add(new File() { Name = filecollection.ElementAt(i) });
            }
        }

        public List<string> AskList;
        public List<string> AnswList;

        private string askLetter;

        public string AskLetter
        {
            get => askLetter;
            set => SetProperty(ref askLetter, value);
        }

        private string answLetter;

        public string AnswLetter
        {
            get => answLetter;
            set => SetProperty(ref answLetter, value);
        }

        private ObservableCollection<File> _collection = new ObservableCollection<File>();

        public ObservableCollection<File> Collection
        {
            get => _collection;
            set => SetProperty(ref _collection, value);
        }

        public ICommand Play
        {
            get => new RelayCommand(async () =>
            {
                if (SelectedFile == null)
                {
                    var dialog = new Windows.UI.Popups.MessageDialog("You should select dictionary. \nClick to + button");
                    await dialog.ShowAsync();
                }
                else
                {
                    string path = appdata + "\\" + SelectedFile.Name;
                    AskList = manager.AddToList(path, Start, End, askLetter);
                    AnswList = manager.AddToList(path, Start, End, answLetter);
                    Random rand = new Random();
                    this.index = rand.Next(0, AskList.Count - 1);
                    this.Question = AskList.ElementAt(index);
                    this.Right = AnswList.ElementAt(index);
                }
            });
        }

        public ICommand Check
        {
            get => new RelayCommand(() =>
            {
                if (Question != null && Answer != null)
                {
                    try
                    {
                        if (Answer == Right)
                        {
                            learned.Add(Question);
                            Result = $"Right: {Right}";
                            Random rand = new Random();
                            this.index = rand.Next(0, AskList.Count - 1);
                            this.Question = AskList.ElementAt(index);
                            this.Right = AnswList.ElementAt(index);
                            OnPropertyChaned(nameof(LearnedCount));
                        }
                        else
                        {
                            WrongList.Add(Question);
                            AskList.Add(Question);
                            AnswList.Add(Right);
                            Result = $"Wrong: {Right}";
                            Random rand = new Random();
                            this.index = rand.Next(0, AskList.Count - 1);
                            this.Question = AskList.ElementAt(index);
                            this.Right = AnswList.ElementAt(index);
                            OnPropertyChaned(nameof(WrongCount));
                        }
                    }
                    catch (Exception l)
                    {
                        var e = l;
                    }
                }
            });
        }

        public ICommand Stop
        {
            get => new RelayCommand(() =>
           {
               AskList = new List<string>();
               AnswList = new List<string>();
               System.Diagnostics.Debug.WriteLine("List Count: " + AnswList.Count);
           });
        }

        public ICommand SaveChanges
        {
            get => new RelayCommand(async () =>
            {
                if (Learned != null)
                {
                    string SavePlace = Path.Combine(appdata, "Saves");
                    string learnedataFileName = "LearnedData.wl";
                    string MistakedDataFileName = "MistakeData.wl";
                    if (!Directory.Exists(SavePlace))
                    {
                        Directory.CreateDirectory(SavePlace);
                    }
                    StorageFolder sf = await StorageFolder.GetFolderFromPathAsync(SavePlace);
                    var tempLearned = await sf.TryGetItemAsync(learnedataFileName);
                    var tempMistaked = await sf.TryGetItemAsync(MistakedDataFileName);
                    await DM.SaveDataAsync(tempLearned, sf, learnedataFileName, Learned.ToList());
                    await DM.SaveDataAsync(tempMistaked, sf, MistakedDataFileName, WrongList.ToList());
                    OnPropertyChaned(nameof(LearnedCount));
                    OnPropertyChaned(nameof(WrongCount));
                }
            });
        }

        private string question;

        public string Question
        {
            get => question;
            set => SetProperty(ref question, value);
        }

        private string answer;

        public string Answer
        {
            get => answer;
            set => SetProperty(ref answer, value);
        }

        private string right;

        public string Right
        {
            get => right;
            set => SetProperty(ref right, value);
        }

        private string result;

        public string Result
        {
            get => result;
            set => SetProperty(ref result, value);
        }

        private ObservableCollection<string> learned = new ObservableCollection<string>();

        public ObservableCollection<string> Learned
        {
            get => learned;
            set => SetProperty(ref learned, value);
        }

        private int learnedCount;

        public int LearnedCount
        {
            get
            {
                if (Learned == null)
                {
                    return 0;
                }
                else return Learned.Count;
            }
            set => SetProperty(ref learnedCount, value);
        }

        private ObservableCollection<string> wrongList = new ObservableCollection<string>();

        public ObservableCollection<string> WrongList
        {
            get => wrongList;
            set => SetProperty(ref wrongList, value);
        }

        private int wrongCount;

        public int WrongCount
        {
            get
            {
                if (WrongList == null)
                {
                    return 0;
                }
                else return WrongList.Count;
            }
            set => SetProperty(ref wrongCount, value);
        }

        private File selectedFile;

        public File SelectedFile
        {
            get => selectedFile;
            set => SetProperty(ref selectedFile, value);
        }

        private int start = 1;

        public int Start
        {
            get => start;
            set => SetProperty(ref start, value);
        }

        private int end = 10;

        public int End
        {
            get => end;
            set => SetProperty(ref end, value);
        }

        public class File
        {
            public string Name { get; set; }
        }
    }
}