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

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace WordLearner
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        private void NavView_ItemInvoked(NavigationView sender, NavigationViewItemInvokedEventArgs args)
        {
            if (args.IsSettingsInvoked)
            {
                ContentFrame.Navigate(typeof(Settings));
            } else switch (args.InvokedItem)
                     {
                         case "Home":
                             ContentFrame.Navigate(typeof(HomePage));
                             break;
                         case "Main":
                             ContentFrame.Navigate(typeof(Main));
                             break;               
                         case "Learn Words":
                             ContentFrame.Navigate(typeof(LearnWords));
                             break;
                         case "Your Progress":
                             ContentFrame.Navigate(typeof(Progress));
                             break;
                         case "Help":
                             ContentFrame.Navigate(typeof(Help));
                             break;
                         case "Contact us":
                             ContentFrame.Navigate(typeof(Contacts));
                             break;                       
                         default:
                             ContentFrame.Navigate(typeof(HomePage));
                             break;
                     }
        }

        private void NavView_SelectionChanged(NavigationView sender, NavigationViewSelectionChangedEventArgs args)
        {
           
        }

        private void NavView_Loaded(object sender, RoutedEventArgs e)
        {
            ContentFrame.Navigate(typeof(HomePage));            
        }
    }
}
