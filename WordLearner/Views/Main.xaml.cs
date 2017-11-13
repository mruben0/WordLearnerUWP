using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.UI.Xaml.Controls;
using WordLearner.ViewModels;
using System;

namespace WordLearner
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Main : Page
    {
        public Main()
        {
            this.InitializeComponent();
            this.ViewModel = new MainViewModel();
        }

        public MainViewModel ViewModel { get; set; }

        private void PathBox_TextChanged(object sender, TextChangedEventArgs e)
        {
        }
    }
}