using System;
using System.Collections.Generic;
using System.Linq;

namespace LoreLayer
{
    public class CurrencyManager
    {
        private Dictionary<string, Currency> currencies;
        private List<CurrencyTransaction> transactionHistory;

        public CurrencyManager()
        {
            currencies = new Dictionary<string, Currency>();
            transactionHistory = new List<CurrencyTransaction>();
        }

        public bool CreateCurrency(string id, string name, decimal initialAmount = 0)
        {
            if (string.IsNullOrWhiteSpace(id) || currencies.ContainsKey(id))
                return false;

            currencies[id] = new Currency(id, name, initialAmount);
            return true;
        }

        public bool ModifyCurrencyAmount(string id, decimal amount, string reason = "")
        {
            if (!currencies.TryGetValue(id, out var currency))
                return false;

            currency.Amount = amount;
            RecordTransaction(id, amount, reason, TransactionType.Set);
            return true;
        }

        public bool AddToCurrency(string id, decimal amountToAdd, string reason = "")
        {
            if (!currencies.TryGetValue(id, out var currency))
                return false;

            currency.Amount += amountToAdd;
            RecordTransaction(id, amountToAdd, reason, TransactionType.Addition);
            return true;
        }

        public bool SubtractFromCurrency(string id, decimal amountToSubtract, string reason = "")
        {
            if (!currencies.TryGetValue(id, out var currency) || amountToSubtract > currency.Amount)
                return false;

            currency.Amount -= amountToSubtract;
            RecordTransaction(id, -amountToSubtract, reason, TransactionType.Subtraction);
            return true;
        }

        public decimal GetCurrencyAmount(string id)
        {
            return currencies.TryGetValue(id, out var currency) ? currency.Amount : 0;
        }

        public IEnumerable<string> GetAllCurrencyIds()
        {
            return currencies.Keys.ToList();
        }

        public IEnumerable<Currency> GetAllCurrencies()
        {
            return currencies.Values.ToList();
        }

        public IEnumerable<CurrencyTransaction> GetTransactionHistory()
        {
            return transactionHistory;
        }

        private void RecordTransaction(string currencyId, decimal amount, string reason, TransactionType type)
        {
            var transaction = new CurrencyTransaction(currencyId, amount, DateTime.UtcNow, reason, type);
            transactionHistory.Add(transaction);
        }

        public class Currency
        {
            public string Id { get; }
            public string Name { get; }
            public decimal Amount { get; set; }

            public Currency(string id, string name, decimal initialAmount)
            {
                Id = id;
                Name = name;
                Amount = initialAmount;
            }

            public override string ToString()
            {
                return $"{Name} (ID: {Id}): {Amount}";
            }
        }

        public class CurrencyTransaction
        {
            public string CurrencyId { get; }
            public decimal Amount { get; }
            public DateTime TransactionTime { get; }
            public string Reason { get; }
            public TransactionType Type { get; }

            public CurrencyTransaction(string currencyId, decimal amount, DateTime transactionTime, string reason, TransactionType type)
            {
                CurrencyId = currencyId;
                Amount = amount;
                TransactionTime = transactionTime;
                Reason = reason;
                Type = type;
            }

            public override string ToString()
            {
                return $"{TransactionTime}: {Type} {Amount} in {CurrencyId}. Reason: {Reason}";
            }
        }

        public enum TransactionType
        {
            Addition,
            Subtraction,
            Set
        }
    }
}
