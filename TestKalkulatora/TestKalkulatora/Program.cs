using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestKalkulatora
{
    class Program
    {

        static void Main(string[] args)
        {
            Console.WriteLine("Press: 1 - dodawanie, 2 -odejmowanie, 3 - mnozenie,  4 - dzielenie" );
            var operationType = int.Parse(Console.ReadLine());

            var x = int.Parse(Console.ReadLine());
            var y = int.Parse(Console.ReadLine());

            var calc = new ExampleCalculator();

            double result = 0;

            if (operationType == 1)
            {
                result = calc.Add(x, y);
            }
            
            if (operationType == 2)
            {
                result = calc.Subtract(x, y);
            }
            
            if (operationType == 3)
            {
                 result = calc.Multiply(x, y);
            }
            
            if (operationType == 4)
            {
                 result = calc.Divide(x, y);
            }
                        
            Console.WriteLine("-------");
            Console.WriteLine(result);
            Console.ReadLine();
        }
    }
    

    public class ExampleCalculator
    {
        public ExampleCalculator()
        {
        }

        public double Add(double x, double y)
        {
            return x + y;
        }

        public double Subtract(double x, double y)
        {
            return x - y;
        }

        public double Multiply(double x, double y)
        {
            return x * y;
        }

        public double Divide(double x, double y)
        {
            return x / y;
        }

    }

}
