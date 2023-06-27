using MauiPoc.ViewModel;

namespace MauiPoc.Views;

public partial class CustomerList : ContentPage
{
    public CustomerList()
    {
        InitializeComponent();

        BindingContext = new CustomerListViewModel();
    }
}
