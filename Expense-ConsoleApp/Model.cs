using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Expense_ConsoleApp
{
    public class Model
    {
        private static int countId = 0;
        public Model(string name, double amount, Category category, DateTime? date = null)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("Nama Pengeluaran tidak boleh Kosong", nameof(name));
            }

            countId++;
            Id = countId;
            expenseName = name;
            Amount = amount;
            expenseCategory = category;
            Date = date ?? DateTime.Now;
        }

        public enum Category
        {
            FoodBeverage,
            Transport,
            Bill,
            Fun,
            Shopping,
            Education
        }

        public int Id { get; private set; }
        public string expenseName { get; set; }
        public double Amount { get; set; }
        public DateTime Date { get; set; }
        public Category expenseCategory { get; set; }
    }
}
