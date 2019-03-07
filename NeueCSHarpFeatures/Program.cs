using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeueCSHarpFeatures
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Action> actions = new List<Action>()
            {
                PatternMatching,
                Tupel,
                Default
            };

            foreach (var item in actions)
            {
                Console.WriteLine($"{item.Method.Name} wird ausgeführt:");
                item.Invoke();
            }

            Console.ReadKey();
        }

        private static void Default()
        {
            int z;
            z = 5;
            if(z == default)
            {
                Console.WriteLine("z ist noch 0!");
            }
        }

        private static void Tupel()
        {
            var result = Dividiere(20, 0);
           
            if(result.hatGeklappt)
            {
                Console.WriteLine($"Ergebnis: {result.ergebnis}");
            }

            #region Javascript-Style
            //dynamic ss = "asdsd";
            //ss = 232;
            //ss = true;
            //ss.Invoke();
            //ss.AddAsFavorite(232);
            #endregion

            (bool hatGeklappt, double ergebnis) Dividiere(double z1, double z2)
            {
                if(z2 == 0)
                {
                    return (false, 0);
                }
                return (true, z1 / z2);
            }

        }

        private static void PatternMatching()
        {
            object iregndwas = 525;

            if(iregndwas is int zahl)
            {
                Console.WriteLine(zahl * 2);
            }

            Grafik g = new Kreis() { Umfang = 30};

            if(g is Kreis kreis && kreis.Umfang > 20)
            {
                Console.WriteLine($"Umfang des Kreises: {kreis.Umfang} ");
            }

        }
    }

    public class Grafik { }
    public class Kreis : Grafik { public int Umfang { get; set; } }
}