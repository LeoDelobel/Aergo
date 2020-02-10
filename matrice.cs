using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AERGO
{
    class Matrice
    {
        private int lignes;
        private int colonnes;
        private double[,] valeurs; // Les valeurs sont un tableau 2D
        Random R = new Random();

        public Matrice(int l, int c) // Constructeur
        {
            lignes = l;
            colonnes = c;
            valeurs = new double[l,c]; // Initialisation du tableau 2D
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

        public void Scalar(double val)
        {
            for (int i = 0; i < lignes; i++)
            {
                for (int n = 0; n < colonnes; n++)
                {
                    valeurs[i, n] *= val;
                }
            }
        }

        public void Add(double val)
        {
            for (int i = 0; i < lignes; i++)
            {
                for (int n = 0; n < colonnes; n++)
                {
                    valeurs[i, n] += val;
                }
            }
        }

        public void Multiply(Matrice autre)
        {
            for (int i = 0; i < lignes; i++)
            {
                for (int n = 0; n < colonnes; n++)
                {
                    valeurs[i, n] *= autre.valeurs[n, 0];
                }
            }
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
        }
    }
}
