using System.Collections.Generic;

namespace WinUiAppWithPackageApp.Data
{
    public static class CustomersService
    {
        public static IEnumerable<Customer> GetAll()
        {
            return new List<Customer>
            {
                new Customer { Id = 1, Code = "SONY", Name = "Sony Music Entertainment", Status = "Active" },
                new Customer { Id = 1, Code = "2TS", Name = "2ThinkSoft", Status = "Active" },
                new Customer { Id = 1, Code = "LG", Name = "LG", Status = "Active" },
                new Customer { Id = 1, Code = "DELL", Name = "Dell Inc.", Status = "Active" },
                new Customer { Id = 1, Code = "C6", Name = "Coaxys", Status = "Active" },
            };
        }
    }
}
