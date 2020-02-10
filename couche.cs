using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AERGO
{
    class Couche
    {
        private Matrice inputs;
        private Matrice weights;
        private Matrice output;

        public Couche(int n, int i)
        // i définit le nombre d'entrées par neurone
        // n définit le nombre de neurones
        {
            inputs = new Matrice(n, i);
            weights = new Matrice(n, i);
            weights.Randomize();
            output = new Matrice(1, n);
        }

        public void Feed(Matrice i) // i est le vecteur d'entrées
        {
            inputs = i.Copy();
            inputs.Print();
            Matrice buffer = weights.Multiply(inputs); // On multiplie les entrées par les poids
            buffer.Print();
            buffer = buffer.Sum();
            buffer.Print();
            buffer = buffer.Function(Sigmoid);
            buffer.Print();
        }

        public static double Sigmoid(double val)
        {
            return (float)(1.0 / (1.0 + Math.Pow(Math.E, -val)));
        }
    }
}
