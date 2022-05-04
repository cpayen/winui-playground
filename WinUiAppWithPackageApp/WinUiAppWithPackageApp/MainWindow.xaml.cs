using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Linq;

namespace WinUiAppWithPackageApp
{
    public sealed partial class MainWindow : Window
    {
        public MainWindow()
        {
            this.InitializeComponent();

            NavView.SelectedItem = NavView.MenuItems.OfType<NavigationViewItem>().First();
            ContentFrame.Navigate(typeof(Pages.HomePage), null, new Microsoft.UI.Xaml.Media.Animation.EntranceNavigationTransitionInfo());
        }

        public string GetAppTitleFromSystem()
        {
            return Windows.ApplicationModel.Package.Current.DisplayName;
        }

        private void NavigationViewControl_ItemInvoked(NavigationView sender, NavigationViewItemInvokedEventArgs args)
        {
            if (args.IsSettingsInvoked == true)
            {
                ContentFrame.Navigate(typeof(Pages.SettingsPage), null, args.RecommendedNavigationTransitionInfo);
            }
            else if (args.InvokedItemContainer != null && (args.InvokedItemContainer.Tag != null))
            {
                Type newPage = Type.GetType($"WinUiAppWithPackageApp.Pages.{args.InvokedItemContainer.Tag}");
                ContentFrame.Navigate(newPage, null, args.RecommendedNavigationTransitionInfo);
            }
        }

        private void NavigationViewControl_BackRequested(NavigationView sender, NavigationViewBackRequestedEventArgs args)
        {
            if (ContentFrame.CanGoBack) ContentFrame.GoBack();
        }

        private void ContentFrame_Navigated(object sender, NavigationEventArgs e)
        {
            NavView.IsBackEnabled = ContentFrame.CanGoBack;

            // Settings
            if (ContentFrame.SourcePageType == typeof(Pages.SettingsPage))
            {
                NavView.SelectedItem = (NavigationViewItem)NavView.SettingsItem;
            }

            // Pages
            else if (ContentFrame.SourcePageType != null)
            {
                NavView.SelectedItem = NavView.MenuItems
                    .OfType<NavigationViewItem>()
                    .First(n => n.Tag.Equals(ContentFrame.SourcePageType.Name.ToString()));
            }

            NavView.Header = ((NavigationViewItem)NavView.SelectedItem)?.Content?.ToString();
        }

    }
}
