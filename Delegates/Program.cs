using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delegates
{
    class Program
    {
        public enum Wochentage { Montag, Dienstag, Mittwoch }

        //Signatur für den Delegate-Typen
        public delegate void EinfacheMethode();
        public delegate string MethodeMit2Integers(int z1, int z2);

        public delegate void Delegate3(int z1, int z2);
        public delegate int Delegate4(int[] array);

        static void Main(string[] args)
        {
            List<Action> actions = new List<Action>()
            {
                EinfachesDelegate, KleineÜbung, GenerischenDelegates, NeueMethode
            };
            
            Action alleMethoden = EinfachesDelegate;
            alleMethoden += KleineÜbung;
            alleMethoden += GenerischenDelegates;
            alleMethoden += NeueMethode;

            foreach (var method in alleMethoden.GetInvocationList())
            {
                Console.WriteLine($"{method.Method.Name} wird ausgeführt:");
                method.DynamicInvoke();
            }
            

            foreach (var action in actions)
            {
                Console.WriteLine($"{action.Method.Name} wird ausgeführt:");
                action.Invoke();
            }

            Console.ReadKey();
        }

        private static void NeueMethode()
        {
            Console.WriteLine("Dummny");
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

        private static void KleineÜbung()
        {
            int[] array = new int[] { 2, 5, 3 };
            int[] array2 = new int[20];

            Delegate3 delegate3 = BerechneUndGebeAus;
            Delegate4 delegate4 = LängeDesArrays;

        }

        public static void BerechneUndGebeAus(int z1, int z2)
        {
            Console.WriteLine($"{z1 + z2}");
        }

        public static int LängeDesArrays(int[] zahlen)
        {
            return zahlen.Length;
        }

        private static void EinfachesDelegate()
        {
            EinfacheMethode einfacheMethode = Ausgabe;
            einfacheMethode.Invoke();

            //....

            einfacheMethode.Invoke();

            MethodeMit2Integers methodeMit2Integern = Berechne;



            Console.WriteLine($"Ergebnis: {methodeMit2Integern.Invoke(6, 10)}");
        }

        private static string Berechne(int zahl1, int zahl2)
        {
            return (zahl1 + zahl2).ToString();
        }

        private static void Ausgabe()
        {
            Console.WriteLine("Test");
        }
    }
}
