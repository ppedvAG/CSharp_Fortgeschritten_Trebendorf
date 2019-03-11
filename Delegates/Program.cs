using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delegates
{
    class Program
    {

        //Signatur für den Delegate-Typen
        public delegate void NoParametersReturnVoid();
        public delegate string TwoIntegersReturnString(int z1, int z2);

        public delegate void TwoIntegersReturnVoid(int z1, int z2);
        public delegate int IntArrayReturnInteger(int[] array);

        static void Main(string[] args)
        {
            List<Action> actions = new List<Action>()
            {
                EinfachesDelegate, KleineÜbung, GenerischenDelegates, NeueMethode
            };
            
            Action alleMethoden = EinfachesDelegate;
            alleMethoden += KleineÜbung;
            alleMethoden += GenerischenDelegates;

            foreach (var method in alleMethoden.GetInvocationList())
            {
                Console.WriteLine($"\n{method.Method.Name} wird ausgeführt:");
                method.DynamicInvoke();
            }
            
            Console.ReadKey();
        }

        #region Szenarien
        private static void EinfachesDelegate()
        {
            NoParametersReturnVoid einfacheMethode = Ausgabe;
            einfacheMethode.Invoke();

            TwoIntegersReturnString methodeMit2Integern = Berechne;

            Console.WriteLine($"Ergebnis: {methodeMit2Integern.Invoke(6, 10)}");
        }

        private static void KleineÜbung()
        {
            int[] array = new int[] { 2, 5, 3 };
            int[] array2 = new int[20];

            TwoIntegersReturnVoid delegate3 = BerechneUndGebeAus;
            IntArrayReturnInteger delegate4 = LängeDesArrays;

        }

        private static void GenerischenDelegates()
        {
            Action action = null;

            action?.Invoke();

            action = Ausgabe;
            Action<int, int> actionMit2Parametern = BerechneUndGebeAus;
            Func<int, int, string> funcMit2Parametern = Berechne;


            Console.WriteLine($"Ergebnis: {funcMit2Parametern.Invoke(2, 5)}");

            action += Ausgabe;
            action -= Ausgabe;
            action = Ausgabe;

            action.Invoke();
        }
        #endregion


        #region Hilfsmethoden
        private static void NeueMethode()
        {
            Console.WriteLine("NeueMethode");
        }


        public static void BerechneUndGebeAus(int z1, int z2)
        {
            Console.WriteLine($"{z1 + z2}");
        }

        public static int LängeDesArrays(int[] zahlen)
        {
            return zahlen.Length;
        }


        private static string Berechne(int zahl1, int zahl2)
        {
            return (zahl1 + zahl2).ToString();
        }

        private static void Ausgabe()
        {
            Console.WriteLine("Ausgabe");
        }
        #endregion

    }
}