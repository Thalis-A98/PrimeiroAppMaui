using MauiApp1.Repositories;

namespace MauiApp1.Views;

public partial class TransactionList : ContentPage
{
	public TransactionList()
	{
        ITransactionRepository transactionRepository = this.Handler.MauiContext.Services.GetService<ITransactionRepository>();
        var repository = transactionRepository;
        InitializeComponent();

        ListTransactions.ItemsSource = repository.GetAll();
	}

	private void OnButtonClicked_To_TransactionAdd(object sender, EventArgs e)
	{
		Navigation.PushModalAsync(new TransactionAdd());
	}
}