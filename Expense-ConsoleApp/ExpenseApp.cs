using System;
using System.Collections.Generic;
using System.Linq;
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

        private void DisplayHeader(string headerText)
        {
            int widthConsole = Console.WindowWidth;
            int textLength = headerText.Length;
            int totalBetweenText = (widthConsole - textLength) / 2;

            if (totalBetweenText > 0)
            {
                string padding = new string('=', totalBetweenText -1);
                Console.WriteLine($"{padding} {headerText} {padding}");
            }
            else
            {
                Console.WriteLine(headerText);
            }
        }
        public void Execute()
        {
            DisplayHeader("Expense Management ConsoleApp");
        }
    }
}
