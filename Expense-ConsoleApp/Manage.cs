using System.Globalization;
using static Expense_ConsoleApp.Model;

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

        #region utilitas
        //re-format currency
        private string FormatCurrency(double amount)
        {
            CultureInfo cultureInfo = new CultureInfo("id-ID");
            return "Rp " + amount.ToString("#,##0", cultureInfo);

        }

        public void BriefDisplayExpenses()
        {

            Console.WriteLine($"{"No. ", -5} {"Tanggal",-12} {"Kategori Expense",-25} {"Jumlah Pengeluaran"}");
            Console.WriteLine(new string('-', 58));

            foreach (var briefSum in modelExpense)
            {
                string callFormattedCurrency = FormatCurrency(briefSum.Amount);

                Console.WriteLine($"{briefSum.Id, -5} {briefSum.Date.ToString("dd/MM/yyy"),-12} {briefSum.expenseCategory,-25} {callFormattedCurrency} ");

            }

        }
        private void ShowDataCategories()
        {
            Console.WriteLine($"{"ID",-3} {"Daftar Kategori"}");
            foreach (var categoryItems in Enum.GetValues(typeof(Model.Category)))
            {
                Console.WriteLine($"{(int)categoryItems,-3} {categoryItems}");
            }
        }
        private void ShowDatafromInputId(int id)
        {
            var checkSpesifikasi = modelExpense.Where(t => t.Id == id).SingleOrDefault();

            //header
            Console.WriteLine($"{"Deskripsi Pengeluaran",-23} {"Kategori Pengeluaran",-20} {"Pengeluaran",-12} {"Tanggal",-15}");
            Console.WriteLine(new string('-', 70));

            //call re-format currency
            string callFormattedCurrency = FormatCurrency(checkSpesifikasi.Amount);

            Console.WriteLine($"{checkSpesifikasi.expenseNotes,-23} {checkSpesifikasi.expenseCategory,-20} {callFormattedCurrency,-12} {checkSpesifikasi.Date.ToString("dd/MM/yyy"),-15}");


        }
        #endregion

        #region TODO:
        //TODO Add Expense
        public void AddExpense()
        {
            string catatan;
            double jumlah;
            int category;
            DateTime date = DateTime.Now;

            Console.WriteLine("Pilih Kategori Pengeluaran: ");
            ShowDataCategories();

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
        public void EditExpense(string inputId, int? opsiUser = null)
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
                    Console.Clear();

                    bool onRun= true;
                    int categoryEdited = 0;
                    double jumlah = 0;
                    while (onRun)
                    {

                        try
                        {
                            ShowDatafromInputId(idExpense);


                            Console.WriteLine("\nPilih opsi: ");
                            Console.Write("1. Edit Deskripsi Pengeluaran\n" +
                                "2. Edit Kategori\n" +
                                "3. Edit Pengeluaran\n" +
                                "4. Edit Tanggal\n" +
                                "0. Selesai\n\n");

                            if (int.TryParse(Console.ReadKey(true).KeyChar.ToString(), out int inputUser))
                            {
                                opsiUser = inputUser;

                                switch (opsiUser)
                                {
                                    case 1:
                                        Console.Write("Masukkan Jumlah Pengeluaran baru: ");
                                        string inputDeskripsi = Console.ReadLine();


                                        modifyExpense.expenseNotes = string.IsNullOrEmpty(inputDeskripsi) ? "-" : inputDeskripsi;

                                        Console.Write("Data telah diperbaharui. Tekan Enter untuk refresh");
                                        Console.ReadLine();

                                        Console.Clear();
                                        break;


                                    case 2:
                                        ShowDataCategories();

                                        Console.Write("Pilih Id Kategori baru(0-6): ");
                                        while (!int.TryParse(Console.ReadLine(), out categoryEdited) || !Enum.IsDefined(typeof(Model.Category), categoryEdited))
                                        {
                                            Console.WriteLine("Invalid Input atau Id tidak tersedia.");
                                            Console.Write("Ketik Id Kategori: ");
                                        }

                                        modifyExpense.expenseCategory = (Model.Category)categoryEdited;
                                        Console.Write("Data telah diperbaharui. Tekan Enter untuk refresh");
                                        Console.ReadLine();
                                        Console.Clear();
                                        break;


                                    case 3:
                                        Console.Write("Masukkan Jumlah Pengeluaran Baru: ");
                                        while (!double.TryParse(Console.ReadLine(), out jumlah))
                                        {
                                            Console.WriteLine("Input Invalid. Hanya menerima Angka/Number");
                                            Console.Write("Jumlah Pengeluaran: ");
                                        }

                                        modifyExpense.Amount = jumlah;

                                        Console.Write("Data telah diperbaharui. Tekan Enter untuk refresh");
                                        Console.ReadLine();

                                        Console.Clear();
                                        break;


                                    case 4:

                                        break;
                                    case 0:
                                        onRun = false;
                                        break;
                                    default:
                                        Console.WriteLine("Invalid Input atau Opsi tidak tersedia");
                                        Console.ReadLine();

                                        Console.Clear();
                                        break;
                                }
                            }
                            else
                            {
                                Console.WriteLine("Input Invalid. Gunakan Angka");
                                Console.ReadLine();

                                Console.Clear();
                            }
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                        }

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
            Console.WriteLine($"{"ID",-5} {"Deskripsi Pengeluaran",-25} {"Kategori",-15} {"Pengeluaran",-15} {"Tanggal",-15}");
            Console.WriteLine(new string('-', 80));
            foreach (var item in modelExpense)
            {
                //call re-format currency
                string callFormattedCurrency = FormatCurrency(item.Amount);
                Console.WriteLine($"{item.Id,-5} {item.expenseNotes,-25} {item.expenseCategory,-15} {callFormattedCurrency,-15} {item.Date.ToString("dd/MM/yyy"),-15}");
            }
        }
        #endregion
    }
}
