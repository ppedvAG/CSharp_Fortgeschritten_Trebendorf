using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaßMitLambda
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Katze> katzen = new List<Katze>()
            {
                new Katze("Schabbi", 2),
                new Katze("Fufu", 12),
                new Katze("Alfons", 5),
                new Katze("KitCat", 4),
            };

            //Filtern
            var ergebnis = Filtern(katzen, AlterGrößerFünf);

            Console.WriteLine("Katzen älter als 5 Jahre: ");
            Filtern(katzen, (Katze katze) => {
                if (katze.Alter > 5) return true;
                return false;
            }).ForEach(k => Console.WriteLine(k.Name));

            Filtern(katzen, katze => {
                if (katze.Alter > 5) return true;
                return false;
            });

            Filtern(katzen, katze => {
                return katze.Alter > 5;
            });

            Filtern(katzen, katze => katze.Alter > 5);

            katzen.Where(k => k.Alter < 4);

            Console.WriteLine("Katzen alphabetisch sortiert");
            katzen.OrderBy(k => k.Name).ToList().ForEach(k => Console.WriteLine(k.Name));


            Console.ReadKey();
        }

        //Filterkriterium als extra Methode
        private static bool AlterGrößerFünf(Katze arg)
        {
            if (arg.Alter > 5) return true;
            return false;
        }

        public static List<Katze> Filtern(List<Katze> katzen, Func<Katze, bool> filterLogik)
        {
            List<Katze> gefilterteListe = new List<Katze>();
            foreach (var katze in katzen)
            {
                if(filterLogik(katze))
                {
                    gefilterteListe.Add(katze);
                }
            }
            return gefilterteListe;
        }
    }
}
