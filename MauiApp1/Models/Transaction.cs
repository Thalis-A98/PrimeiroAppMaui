﻿using LiteDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiApp1.Models
{
    public class Transaction
    {
        [BsonId]
        public int Id { get; set; }
        public TransactionType Type { get; set; }
        public string Name { get; set; }
        public DateTimeOffset Date { get; set; }
        public double Value { get; set; }

    }

    public enum TransactionType
    {
        Income,  //Receitas
        Expense //Despesas
    }
}