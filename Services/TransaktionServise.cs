using ConsoleApp1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Services
{
    public class TransaktionServise
    {
        public transaction CreateTransaction(string from, string to, double amount)
        {
            var newTransaction = new transaction(from, to, amount);
            if (ValidateTransaction(newTransaction) is (true, _))
            {
                return newTransaction;
            }
            else
            {
                throw new ArgumentException("Invalid transaction data.");
            }
        }


        public(bool isValid, string errorMessage) ValidateTransaction(transaction transaction)
        {
            if (string.IsNullOrEmpty(transaction.from))
            {
                return (false, "Sender address is required.");
            }
            if (string.IsNullOrEmpty(transaction.to))
            {
                return (false, "Recipient address is required.");
            }
            if (transaction.amount <= 0)
            {
                return (false, "Amount must be greater than zero.");
            }
            return (true, string.Empty);
        }
    }
}
