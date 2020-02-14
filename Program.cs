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
            while (true)
            {
                int nb_i = 1; // Nombre d'entrées (et de sorties)

                Reseau test = new Reseau(nb_i, new int[] { 5 }, nb_i);

                Matrice i = new Matrice(nb_i, 1); // Matrice d'entrée
                i = i.FromArrayVector(new double[] { 1 });
                Console.WriteLine("Avant :");
                test.Feed(i).Print(); // Sortie du réseau
                for (int n = 0; n < 30; n++)
                {
                    Matrice o = new Matrice(nb_i, 1);
                    o = o.FromArrayVector(new double[] { 1 });
                    test.Train(i, o);

                    Console.WriteLine("Après :");
                    test.Feed(i).Print(); // Sortie du réseau

                    Console.WriteLine("-------------------------------- :");
                }
                Console.ReadLine();
            } 

            //          -----------
            
        }
    }
}
