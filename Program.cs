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
                int nb_i = 2; // Nombre d'entrées (et de sorties)

                Reseau test = new Reseau(nb_i, new int[] { 3 }, nb_i);

                Matrice i = new Matrice(nb_i, 1); // Matrice d'entrée
                i = i.FromArrayVector(new double[] { 1, 1 });
                test.Feed(i).Print(); // Sortie du réseau

                Matrice o = new Matrice(nb_i, 1);
                o = o.FromArrayVector(new double[] { 1, 1 });
                test.Train(i, o);

                test.Feed(i).Print(); // Sortie du réseau
            }

            //          -----------
            Console.Read();
        }
    }
}
