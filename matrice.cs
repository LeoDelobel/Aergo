using System;
using Newtonsoft.Json;

namespace AERGO
{
    public class Matrice
    {
        public int lignes;
        public int colonnes;
        public double[,] valeurs; // Les valeurs sont un tableau 2D

        [JsonIgnore]
        Random R = new Random();

        public Matrice(int l, int c) // Constructeur
        {
            lignes = l;
            colonnes = c;
            valeurs = new double[l,c]; // Initialisation du tableau 2D

            Fill(0); // On remplit à 0
        }

        public Matrice() // Constructeur
        {

        }

        public void Fill(double val)
        {
            for (int i = 0; i < lignes; i++)
            {
                for (int n = 0; n < colonnes; n++)
                {
                    valeurs[i, n] = val; // On remplit chaque case
                }
            }
        }

        public void FromArray(double[,] val)
        {
            valeurs = val; // On remplace les valeurs par le tableau donné
        }

        public void Randomize()
        {
            for (int i = 0; i < lignes; i++)
            {
                for (int n = 0; n < colonnes; n++)
                {
                    valeurs[i, n] = R.NextDouble(); // On utilise R pour remplir la matrice
                }
            }
        }

        public void Set(int l, int c, double val)
        {
            valeurs[l, c] = val; // On remplace la valeur par le paramètre donné
        }

        public double[,] ToArray()
        {
            return valeurs; // On donne le tableau de valeurs
        }

        public Matrice MultiplyVectors(Matrice val)
        {
            // Nouvelle matrice carré
            Matrice sortie = new Matrice(this.lignes, val.colonnes);
            Matrice ceci = this.Copy();
            Matrice autre = val.Copy();
            for (int i = 0; i < sortie.lignes; i++)
            {
                for (int n = 0; n < sortie.colonnes; n++)
                {
                    sortie.valeurs[i, n] = ceci.valeurs[i, 0] * autre.valeurs[0, n]; // On utilise le vecteur pour vaincre le vecteur
                }
            }
            return sortie;
        }

        public Matrice MultiplyHorizontalVector(Matrice val)
        {
            // Multiplie de droite à gauche plutot que de haut en bas
            Matrice sortie = this.Copy();
            Matrice autre = val.Copy(); // On ne transpose pas le vecteur (Plus simple)
            for (int i = 0; i < lignes; i++)
            {
                for (int n = 0; n < colonnes; n++)
                {
                    sortie.valeurs[i, n] *= autre.valeurs[i, 0]; // On multiplie par le vecteur
                }
            }
            return sortie;
        }

        public Matrice Power(double p)
        {
            Matrice sortie = this.Copy();
            for (int i = 0; i < lignes; i++)
            {
                for (int n = 0; n < colonnes; n++)
                {
                    Math.Pow(sortie.valeurs[i, n], p); // On multiplie chaque case
                }
            }
            return sortie;
        }

        public Matrice Activate()
        {
            Matrice sortie = this.Copy();
            for (int i = 0; i < lignes; i++)
            {
                for (int n = 0; n < colonnes; n++)
                {
                    if (sortie.valeurs[i, n] >= 0) sortie.valeurs[i, n] = 1;
                    if (sortie.valeurs[i, n] < 0) sortie.valeurs[i, n] = -1;
                }
            }
            return sortie;
        }

        public Matrice Inverse()
        {
            Matrice sortie = this.Copy();
            for (int i = 0; i < lignes; i++)
            {
                for (int n = 0; n < colonnes; n++)
                {
                    sortie.valeurs[i, n] *= -1;
                }
            }
            return sortie;
        }

        public Matrice Transpose()
        {
            Matrice sortie = new Matrice(colonnes, lignes); // On crée la matrice de sortie
            for (int i = 0; i < lignes; i++)
            {
                for (int n = 0; n < colonnes; n++)
                {
                    sortie.valeurs[n, i] = valeurs[i, n]; // On tourne la matrice (inversion des lignes et colonnes)
                }
            }

            return sortie; // On retourne la matrice
        }

        public Matrice Scalar(double val)
        {
            Matrice sortie = this.Copy(); // On copie cette matrice
            for (int i = 0; i < lignes; i++)
            {
                for (int n = 0; n < colonnes; n++)
                {
                    sortie.valeurs[i, n] *= val; // On multiplie une valeur à toutes les cases
                }
            }
            return sortie;
        }

        public Matrice Val_Add(double val)
        {
            Matrice sortie = this.Copy(); // On copie cette matrice
            for (int i = 0; i < lignes; i++)
            {
                for (int n = 0; n < colonnes; n++)
                {
                    sortie.valeurs[i, n] += val; // On ajoute une valeur à toutes les cases
                }
            }
            return sortie;
        }

        public Matrice Add(Matrice val)
        {
            Matrice sortie = this.Copy(); // On copie cette matrice
            for (int i = 0; i < lignes; i++)
            {
                for (int n = 0; n < colonnes; n++)
                {
                    sortie.valeurs[i, n] += val.valeurs[i, n]; // On ajoute la case de l'autre matrice à la case ici
                }
            }
            return sortie;
        }

        public Matrice Copy()
        {
            Matrice sortie = new Matrice(lignes, colonnes);

            for (int i = 0; i < lignes; i++)
            {
                for (int n = 0; n < colonnes; n++)
                {
                    sortie.valeurs[i, n] = valeurs[i, n]; // On copie les valeurs
                }
            }

            return sortie;
        }

        public Matrice FromArrayVector(double[] val)
        {
            Matrice sortie = new Matrice(val.Length, 1);
            for (int i = 0; i < lignes; i++)
            {
                sortie.valeurs[i, 0] = val[i]; // On exécute cette méthode sur chaque case
            }
            return sortie;
        }

        public double[] ToArrayVector()
        {
            double[] sortie = new double[lignes];
            for (int i = 0; i < lignes; i++)
            {
                sortie[i] = valeurs[i, 0]; // On exécute cette méthode sur chaque case
            }
            return sortie;
        }

        public Matrice Substract(Matrice val)
        {
            Matrice sortie = this.Copy();
            for (int i = 0; i < lignes; i++)
            {
                for (int n = 0; n < colonnes; n++)
                {
                    sortie.valeurs[i, n] -= val.valeurs[i, n]; // On exécute cette méthode sur chaque case
                }
            }
            return sortie;
        }

        public Matrice MultiplyExact(Matrice val)
        {
            Matrice sortie = this.Copy();
            for (int i = 0; i < lignes; i++)
            {
                for (int n = 0; n < colonnes; n++)
                {
                    sortie.valeurs[i, n] *= val.valeurs[i, n]; // On exécute cette méthode sur chaque case
                }
            }
            return sortie;
        }

        public Matrice Function(Func<double, double> val) // On prend une méthode en paramètre
        {
            Matrice sortie = this.Copy();
            for (int i = 0; i < lignes; i++)
            {
                for (int n = 0; n < colonnes; n++)
                {
                    sortie.valeurs[i, n] = val(sortie.valeurs[i, n]); // On exécute cette méthode sur chaque case
                }
            }
            return sortie;
        }

        public Matrice Sum()
        {
            Matrice sortie = new Matrice(lignes, 1);
            for (int i = 0; i < lignes; i++)
            {
                for (int n = 0; n < colonnes; n++)
                {
                    sortie.valeurs[i, 0] += valeurs[i, n]; // On multiplie chaque case par la même case de l'autre matrice
                }
            }
            return sortie;
        }

        public Matrice Multiply(Matrice autre)
        {
            Matrice sortie = this.Copy();
            for (int i = 0; i < lignes; i++)
            {
                for (int n = 0; n < colonnes; n++)
                {
                     sortie.valeurs[i, n] *= autre.valeurs[n, 0]; // On multiplie chaque case par la même case de l'autre matrice
                }
            }
            return sortie;
        }

        public Matrice Vector(Matrice val)
        {
            Matrice sortie = this.Copy();
            Matrice autre = val.Copy().Transpose(); // On transpose le vecteur (Plus simple)
            for (int i = 0; i < lignes; i++)
            {
                for (int n = 0; n < colonnes; n++)
                {
                    sortie.valeurs[i, n] *= autre.valeurs[0, n]; // On multiplie par le vecteur
                }
            }
            return sortie;
        }

        public void Print()
        {
            for(int i = 0; i < lignes; i++)
            {
                Console.Write('\n');
                for(int n = 0; n < colonnes; n++)
                {
                    Console.Write(Convert.ToString(valeurs[i, n]) + "|");
                }
            }
            Console.WriteLine('\n');
        }

        public void Debug(string val)
        {
            Console.Write("----------------- " + val + " ----------------------");
            for (int i = 0; i < lignes; i++)
            {
                Console.Write('\n');
                for (int n = 0; n < colonnes; n++)
                {
                    Console.Write(Convert.ToString(valeurs[i, n]) + "|");
                }
            }
            Console.WriteLine('\n');
        }
    }
}
