// See https://aka.ms/new-console-template for more information
using Expense_ConsoleApp;


var app = new Manage();
app.DisplayExpenseList();
Console.WriteLine();
string inputUser = Console.ReadLine();

app.EditExpense(inputUser);
