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
            Couche test = new Couche(2, 1);

            Matrice input = new Matrice(1, 1);
            input.Fill(2);

            test.Feed(input);

            //          -----------
            Console.Read();
        }
    }
}
