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

        private async void FilePicker_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            FileOpenPicker openPicker = new FileOpenPicker();
            openPicker.ViewMode = PickerViewMode.Thumbnail;
            openPicker.SuggestedStartLocation = PickerLocationId.Desktop;
            openPicker.FileTypeFilter.Add(".xlsx");
            openPicker.FileTypeFilter.Add(".xls");
            StorageFile file = await openPicker.PickSingleFileAsync();
            if ( file != null)
            {
                PathBox.Text = file.Path;
            } else
            {
                var dialog = new Windows.UI.Popups.MessageDialog("Please Choose File");
                await dialog.ShowAsync();
            }
        }

        private void PathBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (PathBox.Text.Length >= 0)
            {
                AddToList.IsEnabled = true;
            }
            else AddToList.IsEnabled = false;
        }
    }
}
