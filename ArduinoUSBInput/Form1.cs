using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ArduinoUSBInput
{
    public partial class Form1 : Form
    {
        
        Arduino arduino = new Arduino();
        System.Threading.Thread ArduinoThread;

        ~Form1()
        {
            arduino = null;
            ArduinoThread.Abort();
        }
        public Form1()
        {
            InitializeComponent();
            arduino.ButtonChanged += arduino_ButtonChanged;
            Control.CheckForIllegalCrossThreadCalls = false;
        }

        void arduino_ButtonChanged(object sender, ButtonStateEventArgs e)
        {
            //MessageBox.Show(e.ButtonState);
            
            lblStateChanges.Items.Add(DateTime.Now.ToShortDateString() + " - " + lblStateChanges.Items.Count.ToString() + ") " + e.ButtonState);


            
            DoListen();
        }

        private void DoListen()
        {
            ArduinoThread = new System.Threading.Thread(arduino.Listen);
            ArduinoThread.Start();
            //Application.DoEvents();
            //arduino.Listen();
            //Application.DoEvents();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DoListen();
        }

        private void lblStateChanges_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }
    }
}
