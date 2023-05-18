using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.IO.Ports;
using System.Diagnostics;
using System.IO;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Windows.Forms.VisualStyles;

namespace PCKodas
{
    public struct CompassData
    {
        public int compassHeading;
        public string time;

        public CompassData(int _compassHeading, string _time)
        {
            compassHeading = _compassHeading;
            time = _time;
        }
    }
    public partial class Form1 : Form
    {
        private String[] ports;
        private SerialPort port;

        string connectedPort;

        public bool isConnected;

        int compass_angle = 0;

        List<CompassData> compassData = new List<CompassData> { };

        void ChangeDisplayedInfo(string connectionStatusTest = null, String[] ports = null) //Changes the main label and port box items
        {
            ConnectionStatusLabel.Text = connectionStatusTest;

            if (ports != null)
            {
                comboBox1.Items.Clear();
                foreach (string port in ports)
                {
                    comboBox1.Items.Add(port);
                }

                if (ports.Length != 0)
                {
                    comboBox1.SelectedItem = ports[0];
                }
            }
            else
            {
                comboBox1.Items.Clear();
            }
        }
        private void CheckConnectivity()
        {
            ChangeDisplayedInfo("Waiting for connection");
            GetAvailableComPorts();
        }


        void GetAvailableComPorts()
        {
            try
            {
                ports = SerialPort.GetPortNames();
                if (ports.Length > 0)
                {
                    ChangeDisplayedInfo(ports: ports);
                    foreach (string portName in ports)
                    {
                        if (ConnectController(portName))
                        {
                            break;
                        }
                    }

                }
                else
                {
                    ChangeDisplayedInfo("No COM ports available");
                    isConnected = false;
                }
            }
            catch
            {
                ChangeDisplayedInfo("No COM ports available");
                isConnected = false;
            }
        }

        private bool ConnectController(string selectedPort)
        {
            if (!isConnected)
            {
                try
                {
                    port = new SerialPort(selectedPort, 115200, Parity.None, 8, StopBits.One);
                    port.Open();
                    port.DataReceived += new SerialDataReceivedEventHandler(Port_DataReceived);
                    isConnected = true;
                    connectedPort = selectedPort;
                    ChangeDisplayedInfo("Connected to port " + selectedPort);
                    return true;
                }
                catch
                {
                    isConnected = false;
                    ChangeDisplayedInfo("Failed to connect");
                    return false;
                }
            }
            return false;
        }

        private void Port_DataReceived(object sender, SerialDataReceivedEventArgs e) //Receive USB data
        {

            int count = port.BytesToRead;
            byte[] ByteArray = new byte[count];
            port.Read(ByteArray, 0, count);
            //Debug.WriteLine("Got" + ByteArray.Length + ": ");
            //string ByteArray = port.ReadExisting();
            Debug.WriteLine("Got" + " length: " + count);
            foreach (Byte b in ByteArray)
            {
                Debug.Write(b);
            }
            Debug.Write("\n");

            int skaitmuo = 0;

            if (ByteArray.Length >= 3) {
                for (int i = 0; i < ByteArray.Length; i+= 3)
                {
                    switch ((int)ByteArray[i])
                    {
                        case (3): //Gavome kampa
                            skaitmuo = ByteArray[i+1] << 8 | ByteArray[i+2];
                            Update_Compass(skaitmuo);
                            //Debug.WriteLine("Got: " + skaitmuo);

                            string timeData = DateTime.Now.ToString("h:mm:ss");
                            compassData.Add(new CompassData(skaitmuo, timeData));
                            
                            break;
                        case (4): //Gavome kalibracija
                            skaitmuo = ByteArray[i+1] << 8 | ByteArray[i+2];
                            Update_Claibration(skaitmuo);
                            break;
                    }
                }
               
            }

        }

        private void ConnectButton_Click(object sender, EventArgs e)
        {
            ConnectController(comboBox1.GetItemText(comboBox1.SelectedItem));
        }

        public Form1()
        {
            InitializeComponent();
            CheckConnectivity();
        }

        private void groupBox1_Paint(object sender, PaintEventArgs e)
        {
            int comppasLineX = (int)Math.Round(Math.Sin(((float)compass_angle+ (float)180)*Math.PI / (float)180) * 50);
            int comppasLineY = (int)Math.Round(Math.Cos(((float)compass_angle + (float)180) * Math.PI / (float)180) * 50);

            Color black = Color.FromArgb(255, 0, 0, 0);
            Pen blackPen = new Pen(black);
            blackPen.Width = 2;
            e.Graphics.DrawLine(blackPen, CompassBox.Width / 2, CompassBox.Height / 2, CompassBox.Width/2 + comppasLineX, CompassBox.Height / 2 + comppasLineY);

            Color green = Color.FromArgb(255, 0, 255, 0);
            Pen greenPen = new Pen(green);
            greenPen.Width = 5;
            e.Graphics.DrawEllipse(greenPen, CompassBox.Width / 2-25, CompassBox.Height / 2-25, 50, 50);
        }

        void Update_Compass(int newAngle)
        {
            compass_angle = newAngle;
            this.Invoke(new MethodInvoker(delegate ()
            {
                //Access controls
                CompassBox.Refresh();
                CompassHeadingLabel.Text = newAngle.ToString();
            }));
        }

        void Update_Claibration(int newCalibration)
        {
            this.Invoke(new MethodInvoker(delegate ()
            {
                //Access controls
                ExistingConfigLabel.Enabled = true;
                ExistingConfigLabel.Text = "Existing config value: \n" + newCalibration.ToString();
            }));
        }

        private void SaveToFileButton_Click(object sender, EventArgs e)
        {
            string fullDateTime = DateTime.Now.ToString("yyyy_MM_dd_h_mm_ss");
            TextWriter txt = new StreamWriter(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, (fullDateTime + "_compass_data.txt")));
            txt.Write("Heading | Time \n");
            foreach (CompassData c in compassData)
            {
                txt.Write(c.compassHeading.ToString() + " | " + c.time + "\n");
            }
            txt.Close();
        }

        private void ConfigButton_Click(object sender, EventArgs e)
        {
            if(ConfigTextBox.Text.Length > 0)
            {
                try
                {
                    int textValue = Int32.Parse(ConfigTextBox.Text);
                    byte[] intBytes = BitConverter.GetBytes(100);
                    byte[] sendByte = new byte[3];
                    sendByte[0] = 0x01;
                    sendByte[1] = (byte)((textValue >> 0) & 0xFF);
                    sendByte[2] = (byte)((textValue >> 8) & 0xFF);
                    
                    port.Write(sendByte, 0, 3);
                }
                catch (Exception ee)
                {
                    ConfigTextBox.Text = "";
                }
            }
        }
    }
}
