using System;
using System.Data;
using WebApiDemo.Models;

namespace WebApiDemo.Helpers
{
    public static class DataRowHelpers
    {
        public static Transaction ToTransaction(this DataRow row)
        {
            if (row != null)
            {
                return new Transaction
                {
                    TransactionId = Convert.ToInt32(row["id"]),
                    TransactionAmount = Convert.ToDecimal(row["amount"]),
                    TransactionDate = Convert.ToDateTime(row["date"])
                };
            }
            return null;
        }
    }
}
