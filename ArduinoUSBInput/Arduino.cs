using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Ports;

namespace ArduinoUSBInput
{
    public class ButtonStateEventArgs:EventArgs
    {
        public string ButtonState { get; set; }
    }
    public class Arduino
    {
        string strLastState = "";
        public delegate void ButtonHandler(object sender, ButtonStateEventArgs e);
        public event ButtonHandler ButtonChanged;
        private SerialPort port;
        public int BaudRate { get; set; }
        public string PortName { get; set; }
        public Arduino()
        {
            port = new SerialPort();
            BaudRate = 9600;
            PortName = "COM5";
            port.PortName = PortName;
            port.BaudRate = BaudRate;
            port.Open();
        }
        ~Arduino()
        {
            port.Close();
        }
        public void Listen()
        {
            string strPortState = port.ReadLine().Trim();
            if(strLastState != strPortState)
            {
                strLastState = strPortState;
                RaiseEvent(strLastState);
            }
            
        }
        protected virtual void RaiseEvent(string strButtonState)
        {

            if (ButtonChanged != null)
                if (strButtonState.Trim() == "Closed")
                    ButtonChanged(this, new ButtonStateEventArgs { ButtonState = "Closed" });
                else
                    ButtonChanged(this, new ButtonStateEventArgs { ButtonState = "Open" });
     

        }
    }
}

/*
 * using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO.Ports;



namespace ArduinoWeb
{
    public enum ValueToSend
    {
        sendOFF = 0, sendRed = 1, sendGreen = 2, sendBoth = 3
    }
    public class Arduino
    {
        private SerialPort port;
        public int BaudRate { get; set; }
        public string PortName { get; set; }
        public Arduino()
        {
            port = new SerialPort();
            BaudRate = 9600;
            PortName = "COM3";
        }
        public void Send(ValueToSend value)
        {
            switch (value)
            {
                case ValueToSend.sendOFF:
                    {
                        BaudRate = BaudRate;
                        port.PortName = PortName;
                        port.Open();
                        port.WriteLine("0");
                        port.Close();
                    }
                    break;
                case ValueToSend.sendGreen:
                    {
                        BaudRate = BaudRate;
                        port.PortName = PortName;
                        port.Open();
                        port.WriteLine("2");
                        port.Close();
                    }
                    break;
                case ValueToSend.sendRed:
                    {
                        BaudRate = BaudRate;
                        port.PortName = PortName;
                        port.Open();
                        port.WriteLine("1");
                        port.Close();
                    }
                    break;
                case ValueToSend.sendBoth:
                    {
                        BaudRate = BaudRate;
                        port.PortName = PortName;
                        port.Open();
                        port.WriteLine("3");
                        port.Close();
                    }
                    break;
                default:
                    {
                        port.Open();
                        port.WriteLine("0");
                        port.Close();
                    }
                    break;
            }
        }

    }
}
*/