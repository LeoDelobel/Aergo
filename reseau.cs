using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AERGO
{
    class Reseau
    {
        bool DEBUG = false;
        Matrice input;
        Couche[] couches;
        Matrice output;
        private double learning_rate = 0.5;

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

        public void SetLearningRate(double val)
        {
            learning_rate = val;
        }

        public Matrice Feed(Matrice val)
        {
            input = val.Copy();
            Matrice buffer = couches[0].Feed(input); // Première couche
            if (DEBUG)
            {
                Console.WriteLine("Couche 1 ");
                Console.WriteLine("-----------------------------------------------------");
            }
            for (int i = 1; i < couches.Length; i++)
            {
                buffer = couches[i].Feed(buffer);
                if (DEBUG)
                {
                    Console.WriteLine("Couche " + Convert.ToString(i + 1)); // On donne la sortie des couches aux couches suivantes
                    Console.WriteLine("-----------------------------------------------------");
                }
            }
            output = buffer.Copy();
            return buffer;
        }

        private double Diviser(double val)
        {
            return val / 2;
        }

        public static double Sigmoid(double val)
        {
            return (float)(1.0 / (1.0 + Math.Pow(Math.E, -val)));
        }

        public static double Sigmoid_Prime(double val)
        {
            return (float)Sigmoid(val) * (1 - Sigmoid(val));
        }

        public void Train(Matrice val, Matrice Yattendu)
        {
            Matrice Ysortie = Feed(val);

            // Selon la formule E = Ysortie - Yattendu :

            // Ysortie - Yattendu :
            Matrice E = Ysortie.Copy().Substract(Yattendu); // VERT

            // Selon la formule E_X = d/dx f(x) E :
            Matrice E_X = E.Copy();
            E_X = E_X.Function(Sigmoid_Prime); // JAUNE

            // Selon la formule E_W = y * E_X :
            Matrice E_W = E_X.Copy();
            double[] E_W_d = E_X.ToArrayVector();

            // Selon le procédé Delta :
            Matrice Y = couches[couches.Length - 2].output;
            double[] Y_d = Y.ToArrayVector();

            Matrice E_D = couches[couches.Length - 1].weights.Copy(); E_D.Fill(0); // Matrice vide pour accueillir les dérivées d'erreurs
            double[,] E_D_d = E_D.ToArray();

            E.Debug("Erreur");

            for (int i = 0; i < couches[couches.Length - 1].nombre_neurones; i++) // Pour chaque erreur calculée
            {
                for (int n = 0; n < couches[couches.Length - 2].nombre_neurones; n++)
                {
                    E_D_d[i, n] = E_W_d[i] * Y_d[n];
                    // Voir procédé Delta sur feuille
                }
            }

            E_D.FromArray(E_D_d);

            E_D = E_D.Scalar(learning_rate);

            Matrice activation = E.Activate().Inverse();

            E_D = E_D.MultiplyHorizontalVector(activation);

            couches[couches.Length - 1].buffer = couches[couches.Length - 1].weights.Copy().Add(E_D);

            // Selon le procédé Delta 2 :
            Matrice new_E_X = couches[couches.Length - 1].weights.Copy().MultiplyHorizontalVector(E_X).Transpose().Sum();
            //                                                                                                                  "La boucle est bouclée"
            //                                                                                                                  "La boucle est bouclée"
            //                                                                                                                  "La boucle est bouclée"
            //                                                                                                                  "La boucle est bouclée"
            //                                                                                                                  "La boucle est bouclée"
            //                                                                                                                  "La boucle est bouclée"
            //                                                                                                                  "La boucle est bouclée"

            couches[couches.Length - 1].UpdateWeights();

            Console.Read();
        }
    }
}
