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

            Filtern(katzen, (Katze katze) => {
                if (katze.Alter > 5) return true;
                return false;
            });


        }

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

    public class Katze
    {
        public Katze(string name, int alter)
        {
            Name = name;
            Alter = alter;
        }

        public string Name { get; set; }
        public int Alter { get; set; }

    }
}
