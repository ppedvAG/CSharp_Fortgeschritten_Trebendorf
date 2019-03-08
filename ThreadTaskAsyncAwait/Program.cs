using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ThreadTaskAsyncAwait
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Action> actions = new List<Action>()
            {
                //EinfacherThread,
                //Wettrennen, Wettrennen, Wettrennen, Wettrennen,
                //ThreadsBeenden,
                //WettrenneMitTasks,
                AsyncAwait
            };

            foreach (var action in actions)
            {
                Console.WriteLine($"{action.Method.Name} wird ausgeführt:");
                action.Invoke();
            }

            Console.WriteLine("Beende MainThread mit Tasteneingabe");

            Console.ReadKey();

            Console.WriteLine("MainThread wurde beendet");
        }



        private static void AsyncAwait()
        {
            List<string> urls = new List<string>()
            {
                "http://www.google.de",
                "http://www.zeit.de",
                "httasdasdasp://www.asodkjasokdoaskjdoas.de",
                "http://www.twitter.com"
            };

            foreach (var item in urls)
            {
                CheckWebsite(item);
            }
        }

        static async void CheckWebsite(string url)
        {
            try
            {
                HttpClient client = new HttpClient();
                //await wartet auf einem neuen Thread
                Task<string> result =  client.GetStringAsync(url);
                Console.WriteLine($"{url} wird untersucht....");
                await Task.Delay(1000);
                string stringResult = result.Result;
                Console.WriteLine($"{url} funktioniert!");
            }
            catch (Exception exp)
            {
                Console.WriteLine($"{url} funktioniert nicht weil {exp.Message}!");
            }
        }

        private static void WettrenneMitTasks()
        {
            _cts = new CancellationTokenSource();

            Task<int> schuhmacher = new Task<int>(() =>
            {
                Stopwatch watch = new Stopwatch();
                watch.Start();
                MacheEtwas("Schuhmacher", 100);
                watch.Stop();
                return watch.Elapsed.Milliseconds;
            });

            Task<int> häkkinen = new Task<int>(() =>
            {
                Stopwatch watch = new Stopwatch();
                watch.Start();
                MacheEtwas("Häkkinen", 100);
                watch.Stop();
                return watch.Elapsed.Milliseconds;
            });

            schuhmacher.Start();
            häkkinen.Start();

            int indexDesTasksDerZuerstFertigWurde = Task.WaitAny(schuhmacher, häkkinen);



            string sieger = indexDesTasksDerZuerstFertigWurde == 0 ? "schuhmacher" : "häkkinen";

            Console.WriteLine($"Sieger ist {sieger}");

            Console.WriteLine($"Häkkinen hat {häkkinen.Result} Milli-Sekunden gebraucht");
            Console.WriteLine($"Schuhmacher hat {schuhmacher.Result} Milli-Sekunden gebraucht");

        }

        private static void ThreadsBeenden()
        {
            Thread schuhmacher = new Thread(() => MacheEtwas("Schuhmacher", 1000));

            _cts = new CancellationTokenSource();

            schuhmacher.Start();


            Console.WriteLine("Drücke C fürs Canceln oder A fürs Aborten");
            var key = Console.ReadKey();
            if (key.Key == ConsoleKey.A)
            {
                Console.WriteLine("Thread wird hart gekillt!");
                schuhmacher.Abort();
                Console.WriteLine("Thread wurde hart gekillt!");
            }
            else if (key.Key == ConsoleKey.C)
            {
                Console.WriteLine("Thread wird nach 1 Sekunde soft gekillt!");
                _cts.CancelAfter(1000);
                Console.WriteLine("Thread-Kill wurde beantragt!");
            }

            schuhmacher.Join();
            Console.WriteLine("Thread ist fertig");

        }

        static string _gewinner;
        static object _dummy = true;
        static CancellationTokenSource _cts;

        private static void Wettrennen()
        {
            Thread schuhmacher = new Thread(() => MacheEtwas("Schuhmacher", 100));
            Thread häkkinen = new Thread(() => MacheEtwas("Häkkinen", 100));

            schuhmacher.Priority = ThreadPriority.Highest;
            häkkinen.Priority = ThreadPriority.Lowest;

            _gewinner = string.Empty;

            häkkinen.Start();
            schuhmacher.Start();

            schuhmacher.Join();
            häkkinen.Join();
            //mache erst weiter, wenn beide Threads fertig sind

            Console.WriteLine($"Gewinner ist {_gewinner}");
        }

        private static void EinfacherThread()
        {
            Thread t1 = new Thread(() => MacheEtwas("t1", 300));
            //Thread läuft standardmäßig im Vordergrund
            t1.IsBackground = true;
            t1.Start();

            Console.WriteLine("T1 wurde gestartet");
        }

        private static void MacheEtwas(string name, int sleepTime)
        {
            try
            {
                for (int i = 0; i < 10; i++)
                {
                    _cts?.Token.ThrowIfCancellationRequested();
                    Thread.Sleep(sleepTime);
                    _cts?.Token.ThrowIfCancellationRequested();
                    Console.WriteLine($"{name}: {i + 1}. Aufgabe erledigt!");
                }
                _cts?.Token.ThrowIfCancellationRequested();
                Console.WriteLine($"{name} ist fertig!");
                _cts?.Token.ThrowIfCancellationRequested();
                lock (_dummy)
                {
                    if (_gewinner == string.Empty)
                    {
                        _gewinner = name;
                    }
                }
            }
            catch (OperationCanceledException exp)
            {
                Console.WriteLine("Ja ich beende mich");
                return;
            }
            catch (Exception exp)
            {
                Console.WriteLine("Fehler ist aufgetreten:");
                Console.WriteLine(exp.Message);
            }
        }
    }
}
