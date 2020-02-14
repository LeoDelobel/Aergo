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

        public void Train(Matrice val, Matrice ideal)
        {
            Matrice actual = Feed(val).Copy();

            // INUTILE :
            double total_error = actual.Copy().Substract(ideal).Transpose().Multiply(actual).Function(Diviser).Sum().ToArrayVector()[0];
            // On calcule l'erreur totale (Une belle ligne)

            Matrice relative_error = actual.Copy().Substract(ideal); // L'erreur relative
            Matrice derivative = actual.Copy().Function(Sigmoid_Prime); // La dérivative
            if (DEBUG)
            {
                Console.WriteLine(total_error);
                relative_error.Print();
                derivative.Print();
                couches[couches.Length - 1].weights.Print();
            }
            Matrice respect_input = couches[couches.Length - 2].output; // L'entrée respective, sortie de la couche précédente

            // Exemple : Les signes correspondants sont reliés ( W -= n * (E1 * E2 * I))

            /**
             *  W : [ O B .. ] E1:   [ O et B ] E2:     [ O et B ] I:       [ O et C ]
             *      [ C..    ]       [ C      ]         [ C    ]            [ B      ]
             *                                                              [ .      ]
             */

            Matrice buff = couches[couches.Length - 1].weights.Copy();
            buff.Fill(1);
            buff = buff.MultiplyHorizontalVector(relative_error).MultiplyHorizontalVector(derivative).Vector(respect_input).Scalar(learning_rate);
            couches[couches.Length - 1].buffer = couches[couches.Length - 1].weights.Copy().Substract(buff); // On remplace les poids

            if (DEBUG)
            {
                Console.WriteLine("------------------------ :");
                couches[couches.Length - 1].weights.Print();
            }

            couches[couches.Length - 1].UpdateWeights();

            // ----------------------COUCHE H-----------------

            /**
            relative_error = relative_error.MultiplyHorizontalVector(derivative);

            total_error = relative_error.Copy().Sum().ToArrayVector()[0];
            derivative = couches[couches.Length - 2].output.Copy().Function(Sigmoid_Prime);
            respect_input = input.Copy();

            buff = couches[couches.Length - 2].weights.Copy();
            buff.Fill(1);
            buff = buff.MultiplyHorizontalVector(relative_error).MultiplyHorizontalVector(derivative).Vector(respect_input);
            couches[couches.Length - 2].weights = couches[couches.Length - 2].weights.Scalar(learning_rate).Substract(buff); // On remplace les poids
            **/

            // -----------------------------------------------

            
        }
    }
}
