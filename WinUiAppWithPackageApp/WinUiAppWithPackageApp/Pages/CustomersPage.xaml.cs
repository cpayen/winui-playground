using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml.Controls;
using System.Windows.Input;
using WinUiAppWithPackageApp.ViewModels;

namespace WinUiAppWithPackageApp.Pages
{
    public sealed partial class CustomersPage : Page
    {
        private readonly CustomersViewModel _vm;

        public CustomersPage()
        {
            InitializeComponent();
            _vm = App.Current.Services.GetService<CustomersViewModel>();
            DataContext = _vm;
        }

        public ICommand EditCustomerCommand { get => _vm.EditCustomerCommand; }
    }
}
