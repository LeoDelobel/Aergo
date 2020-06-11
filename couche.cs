using System;
using Newtonsoft.Json;

namespace AERGO
{
    public class Couche
    {
        public Matrice weights;
        public int nombre_neurones;

        [JsonIgnore]
        public Matrice buffer;
        [JsonIgnore]
        public Matrice inputs;
        [JsonIgnore]
        public Matrice output;
        [JsonIgnore]
        private bool DEBUG = true;

        public Couche(int n, int i)
        // i définit le nombre d'entrées par neurone
        // n définit le nombre de neurones
        {
            inputs = new Matrice(n, i);
            weights = new Matrice(n, i);
            weights.Randomize();
            output = new Matrice(1, n);
            nombre_neurones = n;
            buffer = weights;
        }

        public Couche()
        {
            
        }

        public void UpdateWeights()
        {
            this.weights = this.weights.Add(buffer);
        }

        public Matrice Feed(Matrice i) // i est le vecteur d'entrées
        {
            inputs = i.Copy();
            if (DEBUG)
            {
                inputs.Debug("Entrées");
            }
            Matrice buffer = weights.Multiply(inputs); // On multiplie les entrées par les poids
            if (DEBUG)
            {
                buffer.Debug("Sortie Nette");
            }
            buffer = buffer.Sum(); // On en fait la somme
            if (DEBUG)
            {
                buffer.Debug("Sortie sommée");
            }
            buffer = buffer.Function(Sigmoid); // On fait passer le buffer dans la fonction d'activation (sigmoid)
            if (DEBUG)
            {
                buffer.Debug("Sortie sigmoid");
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
