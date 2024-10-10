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
        public Model(string notes,  Category category, double amount, DateTime? date = null)
        {

            countId++;
            Id = countId;
            expenseNotes = string.IsNullOrEmpty(notes) ? "-" : notes;
            expenseCategory = category;
            Amount = amount;
            Date = date ?? DateTime.Now;
        }

        public enum Category
        {
            FoodBeverage,
            Transport,
            Bills,
            Fun,
            Shopping,
            Education
        }

        public int Id { get; private set; }
        public string expenseNotes { get; set; }
        public double Amount { get; set; }
        public DateTime Date { get; set; }
        public Category expenseCategory { get; set; }
    }
}
