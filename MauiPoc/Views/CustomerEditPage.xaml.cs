using MauiPoc.ViewModel;

namespace MauiPoc.Views;

public partial class CustomerEditPage : ContentPage
{
    public CustomerEditPage()
    {
        InitializeComponent();

        BindingContext = new CustomerEditViewModel();
    }
}
