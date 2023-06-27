using MauiPoc.Views;

namespace MauiPoc;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();

		Routing.RegisterRoute("CustomerEdit", typeof(CustomerEditPage));
	}
}
