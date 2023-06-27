using MauiPoc.Helpers;

namespace MauiPoc.Models;

public class CustomerModel : BindableBase
{
    private int _Id;
    public int Id
    {
        get => _Id;
        set => SetProperty(ref _Id, value);
    }

    private string _FirstName;
    public string FirstName
    {
        get => _FirstName;
        set => SetProperty(ref _FirstName, value);
    }

    private string _LastName;
    public string LastName
    {
        get => _LastName;
        set => SetProperty(ref _LastName, value);
    }

    private string _Email;
    public string Email
    {
        get => _Email;
        set => SetProperty(ref _Email, value);
    }

    private DateTime? _DateOfBirth;
    public DateTime? DateOfBirth
    {
        get => _DateOfBirth;
        set => SetProperty(ref _DateOfBirth, value);
    }

    private string _PhoneNumber;
    public string PhoneNumber
    {
        get => _PhoneNumber;
        set => SetProperty(ref _PhoneNumber, value);
    }

    private string _Address;
    public string Address
    {
        get => _Address;
        set => SetProperty(ref _Address, value);
    }

    private string _City;
    public string City
    {
        get => _City;
        set => SetProperty(ref _City, value);
    }

    private string _State;
    public string State
    {
        get => _State;
        set => SetProperty(ref _State, value);
    }

    private string _ZipCode;
    public string ZipCode
    {
        get => _ZipCode;
        set => SetProperty(ref _ZipCode, value);
    }

    private string _Country;
    public string Country
    {
        get => _Country;
        set => SetProperty(ref _Country, value);
    }
}
