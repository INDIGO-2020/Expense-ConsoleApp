using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Expense_ConsoleApp
{
    public class ExpenseApp
    {
        private Manage manage;
        public ExpenseApp()
        {
            manage = new Manage();
        }


        private void MenuApp()
        {

            Console.WriteLine(" 1. Tambah Expense");
            Console.WriteLine(" 2. Edit Expense");
            Console.WriteLine(" 3. Delete Expense");
            Console.WriteLine(" 4. Display Expense");
            Console.WriteLine(" 0. Keluar");
        }

        public void Execute()
        {
            int option = 0;
            string inputUser = string.Empty;
            do
            {
                manage.DisplayHeader(manage.headerText);

                Console.WriteLine(new string(' ', 5) + "Menu Utama");
                Console.WriteLine(new string('-', 20));
                MenuApp();
                Console.WriteLine(new string('-', 20));

                Console.Write("Pilih 1-4: ");

                if (!int.TryParse(Console.ReadLine(), out option))
                {
                    Console.WriteLine("Input tidak boleh kosong");
                    return;
                }


                switch (option)
                {
                    case 1:
                        Console.WriteLine(new string('-', 20));
                        Console.WriteLine();

                        manage.AddExpense();

                        Console.Clear();
                        break;
                    case 2:
                        manage.BriefDisplayExpenses();

                        Console.Write("\nPilih No. yang akan di Edit atau (Kosongkan untuk Kembali): ");
                        inputUser = Console.ReadLine();
                        manage.EditExpense(inputUser);

                        break;
                    case 3:
                        manage.DisplayExpenseList();

                        Console.Write("\nPilih No. yang akan di Hapus atau (Kosongkan untuk Kembali): ");
                        inputUser = Console.ReadLine();
                        manage.DeleteExpense(inputUser);

                        break;
                    case 4:
                        manage.DisplayExpenseList();

                        Console.ReadLine();
                        Console.Clear();
                        break;
                    case 0:
                        Console.WriteLine("Program Berhenti");
                        break;
                    default:
                        Console.WriteLine("Opsi tidak tersedia");
                        break;
                }

            } while (option != 0);
        }
    }
}
