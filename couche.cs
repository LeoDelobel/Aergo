using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AERGO
{
    class Couche
    {
        private bool DEBUG = false;
        public Matrice inputs;
        public Matrice weights;
        public Matrice output;
        public Matrice buffer;

        public Couche(int n, int i)
        // i définit le nombre d'entrées par neurone
        // n définit le nombre de neurones
        {
            inputs = new Matrice(n, i);
            weights = new Matrice(n, i);
            weights.Randomize();
            output = new Matrice(1, n);
        }

        public void UpdateWeights()
        {
            weights = buffer.Copy();
        }

        public Matrice Feed(Matrice i) // i est le vecteur d'entrées
        {
            inputs = i.Copy();
            if (DEBUG)
            {
                inputs.Print();
            }
            Matrice buffer = weights.Multiply(inputs); // On multiplie les entrées par les poids
            if (DEBUG)
            {
                buffer.Print();
            }
            buffer = buffer.Sum(); // On en fait la somme
            if (DEBUG)
            {
                buffer.Print();
            }
            buffer = buffer.Function(Sigmoid); // On fait passer le buffer dans la fonction d'activation (sigmoid)
            if (DEBUG)
            {
                buffer.Print();
            }

            output = buffer.Copy();
            return buffer;
        }

        public static double Sigmoid(double val)
        {
            return (float)(1.0 / (1.0 + Math.Pow(Math.E, -val)));
        }

        public static double Sigmoid_Prime(double val)
        {
            return (float)Sigmoid(val) * (1 - Sigmoid(val));
        }
    }
}
