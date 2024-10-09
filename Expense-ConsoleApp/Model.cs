using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Expense_ConsoleApp
{
    public class Model
    {
        public enum Category
        {
            FoodBeverage,
            Transport,
            Bill,
            Fun,
            Shopping,
            Education
        }
        public Model()
        {
            
        }

        public double Amount { get; set; }
        public DateTime Date { get; set; }
        public Category category { get; set; }
        public string Description { get; set; }
    }
}
