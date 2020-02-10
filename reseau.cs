using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AERGO
{
    class Reseau
    {
        Matrice input;
        Couche[] couches;
        Matrice output;

        public Reseau(int nb_i, int[] nb_c, int nb_o)
            //nb_i : Nombre d'entrées
            //nb_c : Nombre de neurones cachés
            //nb_o : Nombre de sorties
        {
            input = new Matrice(nb_i, 1); // On initialise la matrice d'entrée

            couches = new Couche[nb_c.Length + 1]; // On initialise le tableau de couches
            couches[0] = new Couche(nb_c[0], nb_i); // La première couche a autant d'entrées que la matrice input
            for(int i = 1; i < nb_c.Length; i++) // Pour chaque autre couche spécifiée, la remplir de neurones
            {
                couches[i] = new Couche(nb_c[i], nb_c[i-1]); // Autant d'entrées que le nombre de neurones précédents
            }
            couches[couches.Length-1] = new Couche(nb_o, nb_c[nb_c.Length-1]);
        }

        public Matrice Feed(Matrice val)
        {
            input = val.Copy();
            Matrice buffer = couches[0].Feed(input); // Première couche
            Console.WriteLine("Couche 1 ");
            Console.WriteLine("-----------------------------------------------------");
            for (int i = 1; i < couches.Length; i++)
            {
                buffer = couches[i].Feed(buffer);
                Console.WriteLine("Couche " + Convert.ToString(i+1)); // On donne la sortie des couches aux couches suivantes
                Console.WriteLine("-----------------------------------------------------");
            }
            output = buffer.Copy();
            return buffer;
        }
    }
}
