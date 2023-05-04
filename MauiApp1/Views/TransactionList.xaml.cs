using CommunityToolkit.Mvvm.Messaging;
using MauiApp1.Models;
using MauiApp1.Repositories;
using System.Threading.Tasks;

namespace MauiApp1.Views;

public partial class TransactionList : ContentPage
{
	private TransactionAdd _trasactionAdd;
	private TransactionEdit _trasactionEdit;
	private ITransactionRepository _repository;
    public TransactionList(TransactionAdd transactionAdd , TransactionEdit transactionEdit, ITransactionRepository repository)
	{
		this._trasactionAdd = transactionAdd;
		this._trasactionEdit = transactionEdit;
		this._repository = repository;

        InitializeComponent();

		Reload();

		WeakReferenceMessenger.Default.Register<string>(this, (e, msg) =>
		{
			Reload();
        });

	}

	private void Reload()
	{
		var itens = _repository.GetAll();
        ListTransactions.ItemsSource = _repository.GetAll();

		double income = itens.Where(a => a.Type == Models.TransactionType.Income).Sum(a => a.Value);
		double expense = itens.Where(a => a.Type == Models.TransactionType.Expense).Sum(a => a.Value);

		double balance = income - expense;

		lblValueReceita.Text = income.ToString("C");
		lblValueDespesa.Text = expense.ToString("C");
		lblSaldo.Text = balance.ToString("C");
    }

	private void OnButtonClicked_To_TransactionAdd(object sender, EventArgs e)
	{

		var transactionAdd = Handler.MauiContext.Services.GetService<TransactionAdd>();
		Navigation.PushModalAsync(transactionAdd);
	}

    private void TapGestureRecognizer_TappedEdit(object sender, TappedEventArgs e)
    {
		var grid = (Grid)sender;
		var gesture = (TapGestureRecognizer)grid.GestureRecognizers[0];
		Transaction transaction = (Transaction)gesture.CommandParameter;

        var transactionEdit = Handler.MauiContext.Services.GetService<TransactionEdit>();
		transactionEdit.SetTransactionToEdit(transaction);
        Navigation.PushModalAsync(transactionEdit);
    }

    private void TapGestureRecognizer_Tapped(object sender, TappedEventArgs e)
    {

    }

    private async void TapGestureRecognizer_TappedDelete(object sender, TappedEventArgs e)
    {
		await AnimationBorder((Border)sender, true);
		bool result = await App.Current.MainPage.DisplayAlert("Excluir","Tem certeza que deseja Excluir?", "Sim", "Não");

		if (result)
		{
			Transaction transaction = (Transaction)e.Parameter;
			_repository.Delete(transaction);
			Reload();
		}
		else
		{
            await AnimationBorder((Border)sender, false);
        }
    }
	private Color _borderOriginalColor;
	private String _labelOriginal;
	private async Task AnimationBorder(Border border, bool IsDeleteAnimation)
	{
        var label = (Label)border.Content;
        if (IsDeleteAnimation)
		{
            _borderOriginalColor = border.BackgroundColor;
			_labelOriginal = label.Text;
			await border.RotateYTo(90, 1000);
			border.BackgroundColor =Colors.Red;
			label.TextColor = Colors.White;
			label.Text = "X";
            await border.RotateYTo(180, 1000);
        }
		else
		{
            await border.RotateYTo(90, 1000);
            label.TextColor = Colors.Black;
			border.BackgroundColor = _borderOriginalColor;
			label.Text = _labelOriginal ;
            await border.RotateYTo(0, 1000);
        }
	}
}