using System.Globalization;

namespace Expense_ConsoleApp
{
    public class Manage
    {
        private List<Model> modelExpense;
        public string headerText = "Expense Management ConsoleApp v 1.0";
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
            Console.Clear();

            DisplayHeader(headerText);
            Console.WriteLine($"{"No. ",-5} {"Tanggal",-12} {"Kategori Expense",-20} {"Jumlah Pengeluaran"}");
            Console.WriteLine(new string('-', 58));

            foreach (var briefSum in modelExpense)
            {
                string callFormattedCurrency = FormatCurrency(briefSum.Amount);

                Console.WriteLine($"{briefSum.Id,-5} {briefSum.Date.ToString("dd/MM/yyy"),-12} {briefSum.expenseCategory,-20} {callFormattedCurrency} ");

            }
        }


        private void ShowDataCategories()
        {
            Console.WriteLine($"{"ID",-3} {"Daftar Kategori"}");
            Console.WriteLine(new string('-', 17));
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

        public void DisplayHeader(string headerText)
        {
            int widthConsole = Console.WindowWidth;
            int textLength = headerText.Length;
            int totalPadding = (widthConsole - textLength) / 2;

            if (totalPadding > 0)
            {
                string padding = new string('=', totalPadding - 1);
                string longLine = new string('=', widthConsole - 1);
                Console.WriteLine($"{longLine}");
                Console.WriteLine($"{padding} {headerText} {padding}");
                Console.WriteLine();
            }
            else
            {
                Console.WriteLine(headerText);
            }
        }
        #endregion

        #region TODO:
        //TODO Add Expense
        public void AddExpense()
        {
            string catatan;
            double jumlah;
            int category;
            int countmaxInvalid = 0;

            DateTime date = DateTime.Now;
            bool onRun = true;
            string confirmAddingnewData;


            do
            {
                Console.WriteLine("Pilih Kategori Pengeluaran: ");
                ShowDataCategories();

                Console.Write("\nKetik Id Kategori: ");
                while (!int.TryParse(Console.ReadLine(), out category) || !Enum.IsDefined(typeof(Model.Category), category) || countmaxInvalid >= 3)
                {
                    countmaxInvalid++;
                    if (countmaxInvalid >= 3)
                    {
                        Console.WriteLine("Kembali ke Menu Utama. Tekan Enter");
                        Console.ReadLine();
                        return;
                    }

                    Console.WriteLine("Id atau Kategori tersebut tidak terdaftar.");
                    Console.Write("Ketik Id Kategori: ");

                }

                Model.Category selectedCategory = (Model.Category)category;
                //Console.WriteLine(selectedCategory);

                Console.Write("Jumlah Pengeluaran: ");

                while (true)
                {
                    if (!double.TryParse(Console.ReadLine(), out jumlah))
                    {
                        Console.WriteLine("Input Invalid. Hanya menerima Angka/Number");
                        Console.Write("Jumlah Pengeluaran: ");
                        continue;
                    }
                    else if (jumlah < 0)
                    {
                        Console.WriteLine("Pengeluaran Tidak bisa Negatif");
                        Console.Write("Jumlah Pengeluaran: ");
                    }
                    else
                    {
                        break;
                    }

                }


                Console.Write("Catatan tambahan(opsional): ");
                catatan = Console.ReadLine();

                var expenseBaru = new Model(catatan, selectedCategory, jumlah, date);
                modelExpense.Add(expenseBaru);
                Console.WriteLine("Data ditambahkan!");


                /* confirmasi penambahan data?
                 * confirm ? continue : break; 
                 */

                while (true)
                {
                    Console.Write("Add Data? (Y/N): ");
                    confirmAddingnewData = Console.ReadLine();

                    if (string.IsNullOrWhiteSpace(confirmAddingnewData))
                    {
                        Console.WriteLine("Input Invalid atau Kosong.");
                    }
                    else if (confirmAddingnewData.ToLower() == "y")
                    {
                        break;
                    }
                    else if (confirmAddingnewData.ToLower() == "n")
                    {
                        onRun = false;

                        Console.WriteLine("Kembali ke Menu Utama. Tekan Enter");
                        Console.ReadLine();
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Input Invalid. Pilihan hanya (Y/N)");
                    }

                    countmaxInvalid++;
                    if (countmaxInvalid >= 3)
                    {
                        onRun = false;

                        Console.WriteLine("Kembali ke Menu Utama. Tekan Enter");
                        Console.ReadLine();
                        break;
                    }
                }
            } while (onRun);
        }

        //TODO Edit Expense
        public void EditExpense(string inputId, int? opsiUser = null)
        {

            if (string.IsNullOrWhiteSpace(inputId))
            {
                Console.WriteLine("Input tidak boleh Kosong!");
            }


            if (int.TryParse(inputId, out int idExpense))
            {
                var modifyExpense = modelExpense.FirstOrDefault(t => t.Id == idExpense);

                if (modifyExpense != null)
                {
                    Console.Clear();
                    DisplayHeader(headerText);

                    DateTime dateTime;
                    bool onRun = true;
                    int categoryEdited = 0;
                    double jumlah = 0;

                    int countMaxinValid = 3;
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
                                        Console.Write("Masukkan Deskripsi baru: ");
                                        string inputDeskripsi = Console.ReadLine();

                                        inputDeskripsi = string.IsNullOrEmpty(inputDeskripsi) ? "-" : inputDeskripsi;

                                        if (inputDeskripsi == modifyExpense.expenseNotes)
                                        {
                                            Console.WriteLine("Tidak ada perubahan pada Deskripsi.");
                                        }
                                        else
                                        {
                                            modifyExpense.expenseNotes = inputDeskripsi;
                                            Console.Write("Deskripsi telah diperbaharui. Tekan Enter untuk refresh");
                                        }


                                        Console.ReadLine();
                                        Console.Clear();
                                        DisplayHeader(headerText);

                                        break;


                                    case 2:
                                        ShowDataCategories();

                                        Console.Write("Pilih Id Kategori baru(0-6): ");
                                        while (!int.TryParse(Console.ReadLine(), out categoryEdited) || !Enum.IsDefined(typeof(Model.Category), categoryEdited))
                                        {
                                            Console.WriteLine("Invalid Input atau Id tidak tersedia.");
                                            Console.Write("Ketik Id Kategori: ");
                                        }

                                        if ((Model.Category)categoryEdited == modifyExpense.expenseCategory)
                                        {
                                            Console.WriteLine("Tidak ada perubahan pada Kategori.");
                                        }
                                        else
                                        {
                                            modifyExpense.expenseCategory = (Model.Category)categoryEdited;
                                            Console.Write("Kategori telah diperbaharui. Tekan Enter untuk refresh");
                                        }

                                        Console.ReadLine();
                                        Console.Clear();
                                        DisplayHeader(headerText);

                                        break;


                                    case 3:
                                        Console.Write("Masukkan Jumlah Pengeluaran Baru: ");

                                        while (true)
                                        {
                                            if (!double.TryParse(Console.ReadLine(), out jumlah))
                                            {
                                                Console.WriteLine("Input Invalid. Hanya menerima Angka/Number");
                                                Console.Write("Jumlah Pengeluaran: ");
                                                continue;
                                            }
                                            else if (jumlah < 0)
                                            {
                                                Console.WriteLine("Pengeluaran Tidak bisa Negatif");
                                                Console.Write("Jumlah Pengeluaran: ");
                                            }
                                            else if (jumlah == modifyExpense.Amount)
                                            {
                                                Console.WriteLine("Tidak ada perubahan pada Jumlah Pengeluaran.");
                                                break;
                                            }
                                            else
                                            {
                                                modifyExpense.Amount = jumlah;
                                                Console.Write("Jumlah Pengeluaran telah diperbaharui. Tekan Enter untuk refresh");
                                                break;
                                            }

                                        }


                                        Console.ReadLine();
                                        Console.Clear();
                                        DisplayHeader(headerText);

                                        break;


                                    case 4:
                                        Console.WriteLine("format Penulisan Tanggal dd/MM/yyy (contoh: 22/08/1999)");
                                        Console.WriteLine(new string('-', 25));
                                        Console.Write("Masukkan Tanggal baru atau Kosongkan untuk Set Tanggal ke Hari ini: ");
                                        string inputTanggalBaru = Console.ReadLine();

                                        DateTime getTanggal = modifyExpense.Date;

                                        while (true)
                                        {
                                            if (string.IsNullOrEmpty(inputTanggalBaru))
                                            {
                                                if (getTanggal.Date == DateTime.Now.Date)
                                                {
                                                    Console.WriteLine("Tidak ada perubahan pada Tanggal");
                                                }
                                                else
                                                {
                                                    modifyExpense.Date = DateTime.Now;
                                                    Console.WriteLine("Tanggal di Set Hari Ini");
                                                }

                                                break;
                                            }
                                            else if (DateTime.TryParseExact(inputTanggalBaru, "dd/MM/yyyy", null, DateTimeStyles.None, out dateTime))
                                            {
                                                if (dateTime.Date == getTanggal.Date)
                                                {
                                                    Console.WriteLine("Tidak ada perubahan pada Tanggal");
                                                }
                                                else
                                                {
                                                    modifyExpense.Date = dateTime;
                                                    Console.Write("Tanggal telah diperbaharui. Tekan Enter untuk refresh");
                                                }
                                                break;

                                            }
                                            else
                                            {
                                                Console.WriteLine("Format Penulisan salah.");
                                                Console.Write("Masukkan Tanggal baru: ");
                                                inputTanggalBaru = Console.ReadLine();

                                            }
                                        }

                                        Console.ReadLine();
                                        Console.Clear();
                                        DisplayHeader(headerText);

                                        break;


                                    case 0:
                                        onRun = false;
                                        Console.Clear();
                                        break;


                                    default:
                                        Console.WriteLine("Invalid Input atau Opsi tidak tersedia");

                                        Console.ReadLine();
                                        Console.Clear();
                                        DisplayHeader(headerText);

                                        break;
                                }
                            }
                            else
                            {
                                Console.WriteLine("Input Invalid. Gunakan Angka");
                                Console.ReadLine();

                                Console.Clear();
                                DisplayHeader(headerText);

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

                    Console.ReadLine();
                    Console.Clear();
                    return;
                }
            }
            else
            {
                Console.WriteLine("Input invalid. Tidak sesuai ketentuan! Gunakan angka");
            }
        }

        public void DeleteExpense(string inputUser)
        {

            if (string.IsNullOrEmpty(inputUser))
            {
                Console.WriteLine("Input tidak boleh Kosong!");
                return;
            }

            if (int.TryParse(inputUser, out int deleteId))
            {
                var modifyExpense = modelExpense.FirstOrDefault(t => t.Id == deleteId);
                Console.Clear();

                if (modifyExpense != null)
                {
                    bool onRun = true;

                    while (onRun)
                    {
                        try
                        {
                            DisplayHeader(headerText);

                            ShowDatafromInputId(deleteId);

                            Console.Write("\nHapus Data? (Y/N): ");
                            string confirm = Console.ReadLine();

                            if (confirm.ToLower() == "y")
                            {
                                modelExpense.Remove(modifyExpense);
                                Console.WriteLine("Data berhasil dihapus. Tekan Enter untuk refresh");
                                Console.ReadLine();

                                Console.Clear();
                                break;
                            }
                            else if (confirm.ToLower() == "n")
                            {
                                Console.Clear();
                                return;
                            }
                            else
                            {
                                Console.Write("Input Invalid. ketik Y/N untuk konfirmasi");
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
                    Console.WriteLine("Invalid Input atau Id tidak ditemukan");
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
