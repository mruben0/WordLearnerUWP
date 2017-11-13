using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using WordLearner.ViewModels;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace WordLearner
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class LearnWords : Page
    {
        public LearnWords()
        {
            this.InitializeComponent();
            this.viewModel = new LearnWordsViewModel();
        }

        public LearnWordsViewModel viewModel;

        private void AppBarButton_Click(object sender, RoutedEventArgs e)
        {
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            if (this.FileList.Visibility == Visibility.Collapsed)
            {
                this.FileList.Visibility = Visibility.Visible;
            }
            else
                this.FileList.Visibility = Visibility.Collapsed;
        }

        private void Settings_Click(object sender, RoutedEventArgs e)
        {
            if (this.PannelChangeLabels.Visibility == Visibility.Collapsed)
            {
                this.PannelChangeLabels.Visibility = Visibility.Visible;
            }
            else
                this.PannelChangeLabels.Visibility = Visibility.Collapsed;
        }

        private void HidePannel_Click(object sender, RoutedEventArgs e)
        {
            this.PannelChangeLabels.Visibility = Visibility.Collapsed;
        }
    }
}