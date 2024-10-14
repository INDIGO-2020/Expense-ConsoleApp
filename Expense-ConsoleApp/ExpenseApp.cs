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

        }

        public void Execute()
        {
            int option = 0;
            string header = "Expense Management ConsoleApp";

            do
            {
                manage.DisplayHeader(header);

                Console.Write("Pilih 1-4: ");

                if (!int.TryParse(Console.ReadLine(), out option))
                {
                    Console.WriteLine("Input tidak boleh kosong");
                    return;
                }


                switch (option)
                {
                    case 1:
                        manage.AddExpense();

                        Console.Clear();
                        break;
                    case 2:
                        break;
                    case 3:
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
