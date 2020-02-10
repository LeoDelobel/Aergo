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
            Reseau test = new Reseau(10, new int[] { 2 }, 10);
            Matrice i = new Matrice(10, 1);
            i.Fill(2);
            test.Feed(i);

            //          -----------
            Console.Read();
        }
    }
}
