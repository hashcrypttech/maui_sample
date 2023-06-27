using MauiPoc.Helpers;
using MauiPoc.Models;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows.Input;

namespace MauiPoc.ViewModel;

public class CustomerEditViewModel : BindableBase, IQueryAttributable
{
    private CustomerModel _Customer;
    public CustomerModel Customer
    {
        get => _Customer;
        set => SetProperty(ref _Customer, value);
    }

    private bool _CanDeleteCustomer;
    public bool CanDeleteCustomer
    {
        get => _CanDeleteCustomer;
        set => SetProperty(ref _CanDeleteCustomer, value);
    }

    public ICommand CancelCommand { get; set; }
    public ICommand SaveCustomerCommand { get; set; }
    public ICommand DeleteCustomerCommand { get; set; }

    public CustomerEditViewModel()
    {
        CancelCommand = new Command(Cancel);
        SaveCustomerCommand = new Command(SaveCustomer);
        DeleteCustomerCommand = new Command(DeleteCustomer);
    }

    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        CanDeleteCustomer = false;

        if (query.ContainsKey("Customer"))
        {
            Customer = query["Customer"] as CustomerModel;

            if (Customer.Id > 0)
                CanDeleteCustomer = true;

            query.Clear();
        }
        else
        {
            Customer = new CustomerModel();
        }
    }

    private async void Cancel()
    {
        await Shell.Current.GoToAsync("..", true);
    }

    private async void SaveCustomer()
    {
        if (string.IsNullOrWhiteSpace(Customer.FirstName) ||
            string.IsNullOrWhiteSpace(Customer.LastName))
        {
            await Shell.Current.DisplayAlert("Alert", "Please enter values for required fields.\nFields marked with * are required.", "Ok");
            return;
        }

        if (!string.IsNullOrWhiteSpace(Customer.Email))
        {
            var isEmailValid = IsValidEmail(Customer.Email);
            if (!isEmailValid)
            {
                await Shell.Current.DisplayAlert("Alert", "Email is not valid", "Ok");
                return;
            }
        }

        var parameters = new Dictionary<string, object>
        {
            { "Customer", Customer },
            { "Mode", "Save" }
        };

        await Shell.Current.GoToAsync("..", true, parameters);
    }

    private async void DeleteCustomer()
    {
        if (Customer.Id == 0)
            return;

        bool shouldDelete = await Shell.Current.DisplayAlert("Confirm",
            $"Are you sure you want to delete {Customer.FirstName} {Customer.LastName}?",
            "Yes", "No");

        if (!shouldDelete)
            return;

        var parameters = new Dictionary<string, object>
        {
            { "Customer", Customer },
            { "Mode", "Delete" }
        };

        await Shell.Current.GoToAsync("..", true, parameters);
    }

    private bool IsValidEmail(string email)
    {
        if (string.IsNullOrWhiteSpace(email))
            return false;

        try
        {
            email = Regex.Replace(email, @"(@)(.+)$", DomainMapper, RegexOptions.None, TimeSpan.FromMilliseconds(200));

            string DomainMapper(Match match)
            {
                var idn = new IdnMapping();
                string domainName = idn.GetAscii(match.Groups[2].Value);
                return match.Groups[1].Value + domainName;
            }
        }
        catch (RegexMatchTimeoutException ex)
        {
            System.Diagnostics.Debug.WriteLine($"Error: {ex.Message}");
            return false;
        }
        catch (ArgumentException ex)
        {
            System.Diagnostics.Debug.WriteLine($"Error: {ex.Message}");
            return false;
        }

        try
        {
            return Regex.IsMatch(email,
                @"^[^@\s]+@[^@\s]+\.[^@\s]+$",
                RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));
        }
        catch (RegexMatchTimeoutException ex)
        {
            System.Diagnostics.Debug.WriteLine($"Error: {ex.Message}");
            return false;
        }
    }
}
