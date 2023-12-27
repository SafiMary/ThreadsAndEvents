using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;
//using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Runtime.InteropServices;

namespace ThreadsAndEvents
{
    public partial class Form1 : Form
    {
        public delegate void MethodContainer();
        public event MethodContainer onExit;
        public Thread thread;
        public int x = 0;
       public CancellationTokenSource cts = new CancellationTokenSource();
        public Form1()
        {
            InitializeComponent();
            onExit += Createthread;
            Createthread();
        }
        
        public void Createthread()
        {
            thread = new Thread(Job);
            thread.Start();
        }
        public void Job()
        {
            Thread.Sleep(1500);
            Random rnd = new Random();
            int value = rnd.Next((int)numericUpMin.Value, (int)numericUpMax.Value);
            Invoke((MethodInvoker)delegate
            {
                chart1.Series[0].Points.AddXY(x, value);
            });
            x++;    
            onExit.Invoke();
        }
        private void Form1_Load(object sender, EventArgs e)
        {

        }
        [Obsolete]
        private void button1_Click(object sender, EventArgs e)
        {
            if(thread.ThreadState== ThreadState.Suspended)
            {
                button1.Text = "Пауза";thread.Resume();
          
            }
            else
            {
                button1.Text = "Возобновить"; thread.Suspend();
            }
        }
        [Obsolete]
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (thread.ThreadState == ThreadState.Suspended) thread.Resume();
            thread.Abort();
            cts.Cancel();

        }

        private void numericUpMin_ValueChanged(object sender, EventArgs e)
        {

        }
        [Obsolete]
        private void numericUpMax_ValueChanged(object sender, EventArgs e)
        {
             thread.Suspend();
            if (numericUpMax.Value < numericUpMin.Value)
            {
               
                if ((sender as NumericUpDown).Name == "numericUpMax")
                {
                    numericUpMax.Value = numericUpMin.Value;
                   // if (!(thread.ThreadState == ThreadState.Running)) thread.Resume();
                }
                else
                {
                    numericUpMin.Value = numericUpMax.Value; 
                   // if (!(thread.ThreadState == ThreadState.Running)) thread.Resume();
                }
                
            }
            thread.Suspend();thread.Resume();
        }
    }
}
