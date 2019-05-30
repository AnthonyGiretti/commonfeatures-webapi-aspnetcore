using System;
using System.Collections.Generic;
using WebApiDemo.Models;

namespace WebApiDemo.Services
{
    public interface ITransactionService
    {
        List<Transaction> GetTransactionsByYear(int year);
    }
}
