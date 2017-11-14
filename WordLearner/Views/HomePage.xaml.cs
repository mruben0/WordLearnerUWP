using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using WordLearner.ViewModels;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace WordLearner
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class HomePage : Page
    {
        public HomePage()
        {
            this.InitializeComponent();
            this.ViewModel = new HomeViewModel();
        }

        public HomeViewModel ViewModel { get; set; }

        private void HubSection_Tapped(object sender, TappedRoutedEventArgs e)
        {
            Frame.Navigate(typeof(Main));
        }

        private void LearnWords_Tapped(object sender, TappedRoutedEventArgs e)
        {
            Frame.Navigate(typeof(LearnWords));
        }

        private void SeeProgress_Tapped(object sender, TappedRoutedEventArgs e)
        {
            Frame.Navigate(typeof(Progress));
        }

        private void About_Tapped(object sender, TappedRoutedEventArgs e)
        {
            Frame.Navigate(typeof(Contacts));
        }
    }
}