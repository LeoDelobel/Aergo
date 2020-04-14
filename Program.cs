using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AERGO
{
    class Program
    {
        static void Main(string[] args)
        {
            bool LOG_DATA = false;

            Reseau test = new Reseau(2, new int[] { 5 }, 1);
            test.SetLearningRate(0.1);

            Console.WriteLine();

            string data = "";

            int iteration = 0;
            while (true)
            {
                for (int i = 0; i < 100; i++)
                {
                    test.Train(new double[] { 0, 0 }, new double[] { 1 });
                    test.Train(new double[] { 0, 1 }, new double[] { 1 });
                    test.Train(new double[] { 1, 0 }, new double[] { 0 });
                    test.Train(new double[] { 1, 1 }, new double[] { 0 });

                    Console.WriteLine("Sortie (Itération " + (iteration + 1) + ") :");
                    Console.WriteLine(Math.Abs(test.mean_error));
                    Console.WriteLine("-----");

                    data += (Convert.ToString(Math.Abs(test.mean_error))) + '\n';

                    iteration++;
                }

                Console.Read();
            }

            if(LOG_DATA) System.IO.File.WriteAllText(@"R:\4 - travail\Projet\aergo_data_" + test.learning_rate + ".txt", data);
            // ------------------------------------------------------------
        }
    }
}
