using CommunityToolkit.Mvvm.Messaging;
using MauiApp1.Models;
using MauiApp1.Repositories;
using System.Text;

namespace MauiApp1.Views;

public partial class TransactionAdd : ContentPage
{
    private ITransactionRepository _repository; 
	public TransactionAdd(ITransactionRepository repository)
	{
		InitializeComponent();
        _repository= repository;

	}

    private void TapGestureRecognizer_Tapped(object sender, TappedEventArgs e)
    {
		Navigation.PopModalAsync();
    }

    private void BtnSalvar(object sender, EventArgs e)
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
            Type = RBIncome.IsChecked ? TransactionType.Income : TransactionType.Expense,
            Name = EntryName.Text,
            Date = DPDate.Date,
            Value = double.Parse(EntryValor.Text)
        };

        _repository.Add(transaction);
    }

    private bool Validation()
    {
        bool valid = true;
        double result;
        StringBuilder sb = new StringBuilder();

        if (string.IsNullOrEmpty(EntryName.Text) || string.IsNullOrWhiteSpace(EntryName.Text))
        {
            sb.AppendLine("O campo 'Nome' deve ser preenchido.");
            valid= false;
        }
        if (string.IsNullOrEmpty(EntryValor.Text) || string.IsNullOrWhiteSpace(EntryValor.Text))
        {
            sb.AppendLine("O campo 'Valor' deve ser preenchido.");
            valid = false;
        }
        if(!string.IsNullOrEmpty(EntryValor.Text) && !double.TryParse(EntryValor.Text, out result))
        {
            sb.AppendLine("O campo 'Valor' é inválido.");
            valid = false;
        }

        if(valid == false)
        {
            LblError.Text = sb.ToString();
            LblError.IsVisible = true;  
        }
        return valid;
    }
}