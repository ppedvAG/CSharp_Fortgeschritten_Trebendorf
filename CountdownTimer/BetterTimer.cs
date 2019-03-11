using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace CountdownTimer
{
    public class BetterTimer
    {
        //Pflicht
        /// <summary>
        /// Interval in Millisekunden
        /// </summary>
        public int Interval { get; private set; }
        /// <summary>
        /// Auszuführende Aktion pro Interval
        /// </summary>
        public Action IntervalAction { get; private set; }

        //Bonus
        /// <summary>
        /// Soll der Thread im Hintergrund laufen?
        /// </summary>
        public bool IsBackground { get; private set; }
        /// <summary>
        /// Sollen alle Befehle in der Action automatisch in den Hauptthread invoked werden?
        /// </summary>
        public bool InvokeAction { get; private set; }
        /// <summary>
        /// Wieviele Durchläufe soll es maximal geben?
        /// </summary>
        public int? MaxSteps { get; private set; }
        /// <summary>
        /// Auszuführende Aktion nachdem die maximale Anzahl an Schritten durchlaufen wurde
        /// </summary>
        public Action FinishAction { get; set; }

        Thread _timerThread;
        CancellationTokenSource _cts;
        int _currentSteps = 0;
        Dispatcher _currentThread;

        public BetterTimer(Action action, int interval = 1000, bool isBackground = false, int? maxSteps = null, bool invokeAction = false, Action finishAction = null)
        {
            IntervalAction = action;
            Interval = interval;
            MaxSteps = maxSteps;
            IsBackground = isBackground;
            InvokeAction = invokeAction;
            FinishAction = finishAction;
        }

        /// <summary>
        /// Startet den Timer
        /// </summary>
        public void Start()
        {
            //TODO: Verhindern dass der Timer mehrmals hintereinander gestartet wird!

             _currentThread = Dispatcher.CurrentDispatcher;

            if(_timerThread == null || _timerThread.ThreadState != ThreadState.Running)
            {
                _timerThread = new Thread(() =>
                {
                    while (MaxSteps == null || (_currentSteps < MaxSteps))
                    {
                        try
                        {

                            _cts?.Token.ThrowIfCancellationRequested();
                            Thread.Sleep(Interval);
                            _cts?.Token.ThrowIfCancellationRequested();


                            if(!InvokeAction)
                            {
                                IntervalAction?.Invoke();
                            }
                            else
                            {
                                _currentThread.Invoke(new Action(() => IntervalAction?.Invoke()));
                                
                            }


                            _currentSteps++;
                        }
                        catch (OperationCanceledException)
                        {
                            return;
                        }
                    }

                    if (!InvokeAction)
                    {
                        FinishAction?.Invoke();
                    }
                    else
                    {
                        _currentThread.Invoke(new Action(() => FinishAction?.Invoke()));
                    }
                    
                });

                _cts = new CancellationTokenSource();

                //Threads laufen standardmäßig im Vordergrund, d.h. sie werden nicht automatisch beendet 
                //wenn der Main-Thread fertig ist
                _timerThread.IsBackground = IsBackground;

                _currentSteps = 0;

                _timerThread.Start();
            }

        }

        /// <summary>
        /// Beendet den Timer
        /// </summary>
        public void Stop()
        {
            //TODO: Sonderfall: Timer ist schon beendet oder noch nicht gestartet
            _cts?.Cancel();
        }
    }
}
