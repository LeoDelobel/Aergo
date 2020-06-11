using System;
using System.IO;
using System.Linq;
using Newtonsoft.Json;

namespace AERGO
{
    public class Reseau
    {
        public double learning_rate = 0.1;
        public TimeSpan time_trained;
        public Couche[] couches;

        [JsonIgnore]
        public Matrice input;
        [JsonIgnore]
        public Matrice output;
        [JsonIgnore]
        public double mean_error = 0;
        [JsonIgnore]
        bool DEBUG = false;

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
            couches[couches.Length-1] = new Couche(nb_o, nb_c[nb_c.Length-1]); // Couche de sortie
        }

        public Reseau()
        {

        }

        public void SetLearningRate(double val)
        {
            learning_rate = val;
        }

        public Matrice Calculate_Error(double[] entree, double[] voulu)
        {
            Matrice Yattendu = new Matrice(voulu.Length, 1).FromArrayVector(voulu);

            return Feed(entree).Substract(Yattendu).Transpose().Sum();
        }

        public Matrice Feed(double[] entree)
        {
            Matrice val = new Matrice(entree.Length, 1).FromArrayVector(entree);

            input = val.Copy();
            Matrice buffer = couches[0].Feed(input); // Première couche
            if (DEBUG)
            {
                Console.WriteLine("Couche 1 ");
                Console.WriteLine("-----------------------------------------------------");
                couches[0].inputs.Debug("ENTREE");
                couches[0].weights.Debug("POIDS");
            }
            for (int i = 1; i < couches.Length; i++)
            {
                buffer = couches[i].Feed(buffer);
                if (DEBUG)
                {
                    Console.WriteLine("Couche " + Convert.ToString(i + 1)); // On donne la sortie des couches aux couches suivantes
                    Console.WriteLine("-----------------------------------------------------");
                    couches[i].output.Debug("");
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

        public static double DeSigmoid(double val)
        {
            return (val * (1 - val));
        }

        public void Train(double[] entree, double[] attendu)
        {
            Matrice val = new Matrice(entree.Length, 1).FromArrayVector(entree);

            Matrice Yattendu = new Matrice(attendu.Length, 1).FromArrayVector(attendu);

            this.input = val;
            Matrice Ysortie = Feed(entree);

            int nombre_h = this.couches.Length - 1; // Nombre de couches cachées (donc, sans compter la couche de sortie)

            // Selon la formule E = Yattendu - Ysortie :
            // Calculons l'erreur

            Matrice erreur_sortie = Yattendu.Substract(Ysortie);
            double[] buffer = erreur_sortie.ToArrayVector();
            this.mean_error = buffer.Average();

            // Calcul du gradient

            Matrice gradient = Ysortie.Copy();
            gradient = gradient.Function(DeSigmoid);
            gradient = gradient.MultiplyExact(erreur_sortie);
            gradient = gradient.Scalar(this.learning_rate);

            // Sortie de la couche précédente :
            // this.couches[this.couches.Length - 2].output

            // Calcul du Delta

            Matrice buff = this.couches[this.couches.Length - 2].output.Copy().Transpose();

            Matrice deltas = gradient.MultiplyVectors(buff);

            this.couches[this.couches.Length - 1].buffer = deltas.Copy();
            this.couches[this.couches.Length - 1].UpdateWeights();

            // ------------------------------ COUCHE MILIEU ---------------------
            // ------------------------------ LA BOUCLE COMMENCE ----------------

            Matrice erreur_prec = erreur_sortie.Copy();

            for (int i = 1; i <= nombre_h - 1; i++)
            {
                int index = this.couches.Length - 1 - i;

                // Poids de la couche suivante :
                // this.couches[index + 1].weights (espérons)

                Matrice poids_T = this.couches[index + 1].weights.Transpose();
                erreur_prec = poids_T.Vector(erreur_prec); // Erreur de cette couche

                gradient = this.couches[index].output.Copy();
                gradient = gradient.Function(DeSigmoid);
                gradient = gradient.MultiplyExact(erreur_prec);
                gradient = gradient.Scalar(this.learning_rate);

                buff = this.couches[index - 1].output.Copy().Transpose();

                deltas = gradient.MultiplyVectors(buff);

                this.couches[index].buffer = deltas.Copy();
                this.couches[index].UpdateWeights();
            }

            // ------------------------------ PREMIERE COUCHE ----------------

            // Poids de la couche suivante :
            // this.couches[index + 1].weights (espérons)

            Matrice poids_T_ = this.couches[1].weights.Transpose();
            erreur_prec = poids_T_.Vector(erreur_prec); // Erreur de cette couche

            gradient = this.couches[0].output.Copy();
            gradient = gradient.Function(DeSigmoid);
            gradient = gradient.MultiplyExact(erreur_prec);
            gradient = gradient.Scalar(this.learning_rate);

            buff = this.input.Copy().Transpose();

            deltas = gradient.MultiplyVectors(buff);

            this.couches[0].buffer = deltas.Copy();
            this.couches[0].UpdateWeights();
        }

        public void Save()
        {
            string out_ = JsonConvert.SerializeObject(this, Formatting.Indented, new JsonSerializerSettings { MaxDepth = int.MaxValue });
            System.IO.File.WriteAllText(@"../network_config.ini", out_);
        }

        static public Reseau Load()
        {
            string in_ = File.ReadAllText(@"../network_config.ini");
            return JsonConvert.DeserializeObject<Reseau>(in_, new JsonSerializerSettings { MaxDepth = int.MaxValue });
        }
    }
}
