using MauiApp1.Views;

namespace MauiApp1;

public partial class App : Application
{
	public App(TransactionList listPage)
	{
		InitializeComponent();

		MainPage = new NavigationPage(listPage);
	}
}
