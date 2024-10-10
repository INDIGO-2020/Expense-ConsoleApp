using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Expense_ConsoleApp
{
    public class Manage
    {
        private List<Model> modelExpense;

        public Manage()
        {
            modelExpense = expenseList();
        }

        private List<Model> expenseList()
        {
            return new List<Model>()
            {
                new Model("", Model.Category.Bills, 12000, DateTime.Now.AddDays(-10)),
                new Model("Makan Sate", Model.Category.FoodBeverage, 25000, DateTime.Now.AddDays(-5)),
                new Model("Makan Bakso", Model.Category.FoodBeverage, 50000, DateTime.Now.AddDays(-5)),
                new Model("Beli Boneka", Model.Category.Fun, 150000, DateTime.Now.AddDays(-3)),
                new Model("Beli Tissue Magic", Model.Category.Fun, 7000, DateTime.Now),
            };

        }

        //re-format currency
        private string formatCurrency(double amount)
        {
            CultureInfo cultureInfo = new CultureInfo("id-ID");
            return "Rp " + amount.ToString("#,##0", cultureInfo);

        }

        //TODO Display Expenses
        public void DisplayExpenseList()
        {
            //header
            Console.WriteLine($"{"ID",-5} {"Deskripsi Pengeluaran",-25} {"Kategori Pengeluaran",-20} {"Pengeluaran",-12} {"Tanggal",-15}");
            Console.WriteLine(new string('-', 80));
            foreach (var item in modelExpense)
            {

                string callFormattedCurrency = formatCurrency(item.Amount);
                Console.WriteLine($"{item.Id,-5} {item.expenseNotes,-25} {item.expenseCategory,-20} {callFormattedCurrency,-12} {item.Date.ToString("dd/MM/yyy"),-15}");
            }
        }
    }
}
