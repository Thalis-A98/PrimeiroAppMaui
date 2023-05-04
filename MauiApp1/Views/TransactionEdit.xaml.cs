using CommunityToolkit.Mvvm.Messaging;
using MauiApp1.Models;
using MauiApp1.Repositories;
using System.Text;

namespace MauiApp1.Views;

public partial class TransactionEdit : ContentPage
{
	private Transaction _transaction;
    private ITransactionRepository _repository;
	public TransactionEdit(ITransactionRepository repository)
	{
		InitializeComponent();
        _repository = repository;
	}
	public void SetTransactionToEdit(Transaction transaction)
	{
        _transaction = transaction;

		if(transaction.Type == TransactionType.Income)
			RBEditIncome.IsChecked= true;
		else 
			RBEditIncome.IsChecked= false;

		EntryNameEdit.Text = transaction.Name;
		DPDateEdit.Date = transaction.Date.Date;
        EntryValueEdit.Text = transaction.Value.ToString("C");

    }

    private void TapGestureRecognizer_Tapped(object sender, TappedEventArgs e)
    {
		Navigation.PopModalAsync();
    }

    private void btnSalvaEdicao(object sender, EventArgs e)
    {
        if (Validation() == false)
            return;

        SalvarDados();

        Navigation.PopModalAsync();

        WeakReferenceMessenger.Default.Send<string>(string.Empty);

    }

    private void SalvarDados()
    {
        Transaction transaction = new Transaction()
        {
            Id = _transaction.Id,
            Type = RBEditIncome.IsChecked ? TransactionType.Income : TransactionType.Expense,
            Name = EntryNameEdit.Text,
            Date = DPDateEdit.Date,
            Value = double.Parse(EntryValueEdit.Text)
        };

        _repository.Update(transaction);
    }

    private bool Validation()
    {
        bool valid = true;
        double result;
        StringBuilder sb = new StringBuilder();

        if (string.IsNullOrEmpty(EntryNameEdit.Text) || string.IsNullOrWhiteSpace(EntryNameEdit.Text))
        {
            sb.AppendLine("O campo 'Nome' deve ser preenchido.");
            valid = false;
        }
        if (string.IsNullOrEmpty(EntryValueEdit.Text) || string.IsNullOrWhiteSpace(EntryValueEdit.Text))
        {
            sb.AppendLine("O campo 'Valor' deve ser preenchido.");
            valid = false;
        }
        if (!string.IsNullOrEmpty(EntryValueEdit.Text) && !double.TryParse(EntryValueEdit.Text, out result))
        {
            sb.AppendLine("O campo 'Valor' é inválido.");
            valid = false;
        }

        if (valid == false)
        {
            LblError.Text = sb.ToString();
            LblError.IsVisible = true;
        }
        return valid;
    }
}