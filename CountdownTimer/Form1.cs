using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CountdownTimer
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        int _remainingTime = 10;
        BetterTimer _timer;
        private bool _isRunning;

        public int RemainingTime
        {
            get { return _remainingTime; }
            set
            {
                _remainingTime = value;
                if (labelRemainingTime.InvokeRequired)
                {
                    labelRemainingTime.Invoke(new Action(() => labelRemainingTime.Text = $"Verbleibende Zeit: {value}"));
                }
                else
                {
                    labelRemainingTime.Text = $"Verbleibende Zeit: {value}";
                }
            }
        }

        //Wichtige Befehle:
        //Thread t1 = new Thread(() => {});
        //Thread.Sleep(2000);
        //t1.Start();
        //t1.Join();

        private void button_countdown_ohne_label_click(object sender, EventArgs e)
        {
            //10 Sekunden im Hintergrund unterzählen und dann MessageBox anzeigen
            Thread t1 = new Thread(() =>
            {
                Thread.Sleep(5000);
            });

            Thread warteThread = new Thread(() =>
            {
                t1.IsBackground = true;
                t1.Start();
                t1.Join();
                MessageBox.Show("Countdown-Thread ist fertig!");
            });
            warteThread.IsBackground = true;
            warteThread.Start();

        }

        private void button_countdown_mit_label_click(object sender, EventArgs e)
        {
            Thread t2 = new Thread(() =>
            {
                while (_remainingTime > 0)
                {
                    Thread.Sleep(1000);

                    RemainingTime--;
                }
                MessageBox.Show("Fertig!");
            });

            RemainingTime = 10;

            t2.IsBackground = true;
            t2.Start();
        }



        private void button_mit_BetterTimer_click(object sender, EventArgs e)
        {
            if (_isRunning == false)
            {
                _isRunning = true;
                RemainingTime = 10;
                _timer = new BetterTimer(() => RemainingTime--, 1000, true, 10, false);

                _timer.Start();

                BetterTimer secondTimer = null;

                button1.Left = 10;

                secondTimer = new BetterTimer(() =>
                {
                    button1.Left += 5;
                    if(button1.Right > this.Width)
                    {
                        secondTimer.Stop();
                    }
                },10,true,null,true);
                secondTimer.Start();
            }


        }

        private void button4_Click(object sender, EventArgs e)
        {
            _timer?.Stop();
            _isRunning = false;
        }
    }
}