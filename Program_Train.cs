using System;
using System.IO;
using Newtonsoft.Json;
using System.Diagnostics;

namespace AERGO
{
    class Data
    {
        public byte Note, Valeur0, Valeur1, Valeur2;
        public string Nom, Preference;

        Data()
        {

        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Stopwatch clock = new Stopwatch();
            TimeSpan session = new TimeSpan();
            Reseau network;

            if (File.Exists(@"../network_config.ini")) // Si network_config.ini existe
            {
                Console.WriteLine("Fichier de configuration trouvé : chargement...");
                network = Reseau.Load(); // On le charge
            } else // Sinon
            {
                Console.WriteLine("Fichier de configuration manquant dans : " + System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location) + @"../network_config.ini");
                Console.WriteLine("génération d'un nouveau réseau...");
                network = new Reseau(3, new int[] { 10 }, 1); // On génére un nouveau réseau
                network.SetLearningRate(Convert.ToDouble(0.1));
            }

            Data[] donnees = JsonConvert.DeserializeObject<Data[]>(File.ReadAllText(@"../data.json"), new JsonSerializerSettings { MaxDepth = int.MaxValue }); // On charge les données
            Random R = new Random();

            while (true) {
                clock.Reset();
                clock.Start();
                for (int n = 0; n < 1; n++) // Nombre de données entrainées par session
                {
                    Data d = donnees[R.Next(0, donnees.Length)];
                    for (int i = 0; i < 1000; i++) // Nombre d'entrainements par donnée
                    {
                        // Ne pas oublier de mapper les valeurs !
                        network.Train(new double[] { d.Valeur0 / 255.0, d.Valeur1 / 255.0, d.Valeur2 / 255.0 }, new double[] { d.Note / 10.0 }); // En entrée : RGB, en sortie : note
                    }
                }

                Console.Clear();
                session += clock.Elapsed;
                Console.WriteLine("Session : " + String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                    session.Hours, session.Minutes, session.Seconds,
                    session.Milliseconds / 10));

                network.time_trained += clock.Elapsed;
                Console.WriteLine("Total : " + String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                    network.time_trained.Hours, network.time_trained.Minutes, network.time_trained.Seconds,
                    network.time_trained.Milliseconds / 10));

                Console.WriteLine("Erreur moyenne : " + network.mean_error);

                network.Save();
            }
        }
    }
}
