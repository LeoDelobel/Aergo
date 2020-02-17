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
            int nb_i = 3; // Nombre d'entrées (et de sorties)

            Reseau test = new Reseau(nb_i, new int[] { 15 }, nb_i);

            Console.WriteLine();
            while (true)
            {
                Matrice i = new Matrice(nb_i, 1); // Matrice d'entrée
                i = i.FromArrayVector(new double[] { 1, 1, 1 });
                Matrice o = new Matrice(nb_i, 1);
                o = o.FromArrayVector(new double[] { 1, 0, 1 }); // Matrice de sortie

                test.Train(i, o);

                Console.WriteLine();
                Console.WriteLine("-------------------------------- :");
                Console.ReadLine();
            } 
                            // ------------------------------------------------------------
        }
    }
}
