using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestOil
{
    public class Good
    {
        string name;
        double price;
        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        public double Price
        {
            get { return price; }
            set
            {
                if (price < 0)
                {
                    throw new Exception("Цена не может быть меньше нуля");
                }
                price = value;
            }
        }
        public Good()
        {
            Name = "Unknown";
            Price = 0;
        }
        public Good(string name, double price)
        {
            Name = name;
            Price = price;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
