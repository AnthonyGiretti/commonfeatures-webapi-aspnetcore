using System;
using System.Collections.Generic;
using System.Data;
using WebApiDemo.Models;
using WebApiDemo.Repositories;

namespace WebApiDemo.Services
{
    public class TransactionService : ITransactionService
    {
        private ITransactionRepository _transactionRepository;

        public TransactionService(ITransactionRepository transactionRepository)
        {
            _transactionRepository = transactionRepository;
        }

        public List<Transaction> GetTransactionsByYear(int year)
        {
            var list = new List<Transaction>();
            var datatable = _transactionRepository.GetTransactionsByYear(year);

            if (null == datatable || datatable.Rows.Count == 0)
                return list;

            foreach (DataRow row in datatable.Rows)
            {
                list.Add(new Transaction
                {
                    TransactionId = Convert.ToInt32(row["id"]),
                    TransactionAmount = Convert.ToDecimal(row["amount"]),
                    TransactionDate = Convert.ToDateTime(row["date"])
                });
            }

            return list;
        }

        public Transaction GetTransactionById(int transactionId)
        {
            var datatable = _transactionRepository.GetTransactionById(transactionId);

            if (null == datatable || datatable.Rows.Count == 0)
                return null;

            var row = datatable.Rows[0];
            return new Transaction
            {
                TransactionId = Convert.ToInt32(row["id"]),
                TransactionAmount = Convert.ToDecimal(row["amount"]),
                TransactionDate = Convert.ToDateTime(row["date"])
            };
            
        }
    }
}