using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using WinUiAppWithPackageApp.Commands;
using WinUiAppWithPackageApp.Data;

namespace WinUiAppWithPackageApp.ViewModels
{
    public class CustomersViewModel : INotifyPropertyChanged
    {
        #region DI & Ctor

        private readonly CustomersService _customersService;

        public CustomersViewModel(CustomersService customersService)
        {
            _customersService = customersService;
            Customers = new ObservableCollection<Customer>(_customersService.GetAll());
        }

        #endregion

        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;
        protected void NotifyPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        #endregion

        #region Props

        public ObservableCollection<Customer> Customers { get; private set; }

        private Customer _selectedCustomer;
        public Customer SelectedCustomer
        {
            get { return _selectedCustomer; }
            set { _selectedCustomer = value; NotifyPropertyChanged(); }
        }

        #endregion

        #region Commands

        private ICommand _createCustomerCommand;
        public ICommand CreateCustomerCommand
        {
            get => _createCustomerCommand ??= new CommandHandler<Customer>((p) => CreateCustomerCommandHandler(p), true);
        }

        public void CreateCustomerCommandHandler(Customer parameter)
        {
            Customer customer = new()
            {
                Code = parameter.Code,
                Name = parameter.Name
            };
            Customers.Add(customer);
        }

        private ICommand _editCustomerCommand;

        public ICommand EditCustomerCommand
        {
            get => _editCustomerCommand ??= new CommandHandler<Customer>((p) => EditCustomerCommandHandler(p), true);
        }

        public void EditCustomerCommandHandler(Customer parameter)
        {
            var customer = Customers.First(x => x.Id == parameter.Id);
            var index = Customers.IndexOf(customer);
            Customers.RemoveAt(index);

            customer.Code = parameter.Code;
            customer.Name = parameter.Name;
            Customers.Insert(index, customer);
        }

        #endregion
    }
}
