using System.Globalization;

namespace Expense_ConsoleApp
{
    public class Manage
    {
        private List<Model> modelExpense;

        public Manage()
        {
            modelExpense = expenseList();
        }

        //In-Memory ExpenseList
        private List<Model> expenseList()
        {
            return new List<Model>()
            {
                new Model("", Model.Category.Bills, 12000, DateTime.Now.AddDays(-10)),
                new Model("Makan Sate", Model.Category.FoodBeverage, 25000, DateTime.Now.AddDays(-5)),
                new Model("Makan Bakso", Model.Category.FoodBeverage, 50000, DateTime.Now.AddDays(-5)),
                new Model("Beli Boneka", Model.Category.Fun, 150000, DateTime.Now.AddDays(-3)),
                new Model("Beli Tissue Magic", Model.Category.Fun, 7000, DateTime.Now.AddDays(-1)),
            };

        }

        //re-format currency
        private string formatCurrency(double amount)
        {
            CultureInfo cultureInfo = new CultureInfo("id-ID");
            return "Rp " + amount.ToString("#,##0", cultureInfo);

        }

        #region TODO
        //TODO Add Expense
        public void AddExpense()
        {
            string catatan;
            double jumlah;
            int category;
            DateTime date = DateTime.Now;

            Console.WriteLine("Pilih Kategori Pengeluaran: ");
            Console.WriteLine($"{"ID",-3} {"Daftar Kategori"}");
            foreach (var categoryItems in Enum.GetValues(typeof(Model.Category)))
            {
                Console.WriteLine($"{(int)categoryItems,-3} {categoryItems}");
            }

            Console.Write("\nKetik Id Kategori: ");
            while (!int.TryParse(Console.ReadLine(), out category) || !Enum.IsDefined(typeof(Model.Category), category))
            {
                Console.WriteLine("Id atau Kategori tersebut tidak terdaftar.");
                Console.Write("Ketik Id Kategori: ");
            }

            Model.Category selectedCategory = (Model.Category)category;
            //Console.WriteLine(selectedCategory);

            Console.Write("Jumlah Pengeluaran: ");
            while (!double.TryParse(Console.ReadLine(), out jumlah))
            {
                Console.WriteLine("Input Invalid. Hanya menerima Angka/Number");
                Console.Write("Jumlah Pengeluaran: ");
            }

            Console.Write("Catatan tambahan(opsional): ");
            catatan = Console.ReadLine();

            var expenseBaru = new Model(catatan, selectedCategory, jumlah, date);
            modelExpense.Add(expenseBaru);
        }

        //TODO Edit Expense
        public void EditExpense(string inputId)
        {

            if (string.IsNullOrWhiteSpace(inputId))
            {
                Console.WriteLine("Input tidak boleh Kosong!");
                return;
            }

            if (int.TryParse(inputId, out int idExpense))
            {
                var modifyExpense = modelExpense.FirstOrDefault(t => t.Id == idExpense);

                if (modifyExpense != null)
                {
                    try
                    {
                        Console.WriteLine("Input berhasil dan sesuai");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Ini catch error");
                        Console.WriteLine(ex.Message);
                    }
                }
                else
                {
                    Console.WriteLine("Id tidak ditemukan");
                }
            }
            else
            {
                Console.WriteLine("Input invalid. Tidak sesuai ketentuan! Gunakan angka");
            }
        }

        //TODO Display Expenses
        public void DisplayExpenseList()
        {
            //header
            Console.WriteLine($"{"ID",-5} {"Deskripsi Pengeluaran",-25} {"Kategori Pengeluaran",-20} {"Pengeluaran",-12} {"Tanggal",-15}");
            Console.WriteLine(new string('-', 80));
            foreach (var item in modelExpense)
            {
                //call re-format currency
                string callFormattedCurrency = formatCurrency(item.Amount);
                Console.WriteLine($"{item.Id,-5} {item.expenseNotes,-25} {item.expenseCategory,-20} {callFormattedCurrency,-12} {item.Date.ToString("dd/MM/yyy"),-15}");
            }
        }
        #endregion
    }
}
