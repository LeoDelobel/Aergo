using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AERGO
{
    class Program
    {
        static void Main(string[] args)
        {
            Matrice test = new Matrice(2, 3);
            test.Randomize();
            test.Print();
            Console.WriteLine();
            test.Transpose().Print();

            Console.Read();
        }
    }
}
