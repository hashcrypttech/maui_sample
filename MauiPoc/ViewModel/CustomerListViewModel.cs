using MauiPoc.Helpers;
using MauiPoc.Models;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace MauiPoc.ViewModel;

public class CustomerListViewModel : BindableBase, IQueryAttributable
{
    private ObservableCollection<CustomerModel> _Customers;
    public ObservableCollection<CustomerModel> Customers
    {
        get => _Customers;
        set => SetProperty(ref _Customers, value);
    }

    public ICommand AddCustomerCommand { get; set; }
    public ICommand EditCustomerCommand { get; set; }

    public CustomerListViewModel()
    {
        Customers = new ObservableCollection<CustomerModel>();

        AddCustomerCommand = new Command(AddCustomer);
        EditCustomerCommand = new Command<CustomerModel>(EditCustomer);

        LoadCustomers();
    }

    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        if (query.ContainsKey("Customer") && query.ContainsKey("Mode"))
        {
            if (query["Customer"] is CustomerModel customer &&
                query["Mode"] is string mode)
            {
                if (customer.Id > 0)
                {
                    var existingCustomer = Customers.FirstOrDefault(m => m.Id == customer.Id);
                    if (existingCustomer != null)
                    {
                        if (mode == "Save")
                        {
                            existingCustomer.FirstName = customer.FirstName;
                            existingCustomer.LastName = customer.LastName;
                            existingCustomer.Email = customer.Email;
                            existingCustomer.DateOfBirth = customer.DateOfBirth;
                            existingCustomer.PhoneNumber = customer.PhoneNumber;
                            existingCustomer.City = customer.City;
                            existingCustomer.State = customer.State;
                            existingCustomer.ZipCode = customer.ZipCode;
                            existingCustomer.Country = customer.Country;
                        }
                        else if (mode == "Delete")
                        {
                            Customers.Remove(existingCustomer);
                        }
                    }
                }
                else
                {
                    int highestId = 0;
                    if (Customers.Count > 0)
                        highestId = Customers.Max(m => m.Id);

                    customer.Id = highestId + 1;
                    Customers.Add(customer);
                }
            }
        }

        query.Clear();
    }

    public void LoadCustomers()
    {
        Customers.Add(new CustomerModel
        {
            Id = 1,
            FirstName = "John",
            LastName = "Doe",
            Email = "john.doe@abc.com",
            DateOfBirth = new DateTime(1990, 1, 1),
            PhoneNumber = "9123456780",
            Address = "123, Main Street",
            City = "New York",
            State = "NY",
            ZipCode = "10001",
            Country = "USA"
        });

        Customers.Add(new CustomerModel
        {
            Id = 2,
            FirstName = "Jane",
            LastName = "Doe",
            Email = "jane.doe@abc.com",
            DateOfBirth = new DateTime(1992, 2, 1),
            PhoneNumber = "9128366780",
            Address = "123, Main Street",
            City = "New York",
            State = "NY",
            ZipCode = "10001",
            Country = "USA"
        });

        Customers.Add(new CustomerModel
        {
            Id = 3,
            FirstName = "Alex",
            LastName = "Smith",
            Email = "alex.smith@brand.com",
            DateOfBirth = new DateTime(1987, 7, 23),
            PhoneNumber = "9428366940",
            Address = "123, Main Street",
            City = "New York",
            State = "NY",
            ZipCode = "10001",
            Country = "USA"
        });
    }

    private async void AddCustomer()
    {
        await Shell.Current.GoToAsync("CustomerEdit", true);
    }

    private async void EditCustomer(CustomerModel customer)
    {
        var parameters = new Dictionary<string, object>
        {
            { "Customer", customer }
        };

        await Shell.Current.GoToAsync("CustomerEdit", true, parameters);
    }
}
