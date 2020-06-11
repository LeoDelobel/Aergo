using System;
using System.IO;

namespace AERGO
{
    class Program
    {
        static void Main(string[] args)
        {
            if (!File.Exists(@"../network_config.ini")) // Si network_config.ini n'existe pas
            {
                Console.WriteLine("Erreur : quelqu'un a mal fait son travail (moi)");
                Environment.Exit(0);
            } else // S'il existe
            {
                Reseau network = Reseau.Load(); // On le charge
                // network.Save(); Console.Read();

                network.Feed(new double[] {
                Convert.ToDouble(args[0]) / 255.0,
                Convert.ToDouble(args[1]) / 255.0,
                Convert.ToDouble(args[2]) / 255.0
                });
                double[] guess = network.output.ToArrayVector();

                network.input.Debug("Entrée");
                network.output.Debug("Sortie");

                Console.WriteLine(guess[0] * 1.0); // On donne la note devinée
            }
        }
    }
}
