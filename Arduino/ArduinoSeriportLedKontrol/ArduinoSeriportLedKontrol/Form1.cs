using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace ArduinoSeriportLedKontrol
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            string[] ports = SerialPort.GetPortNames();
            foreach (string port in ports)
                comboBox1.Items.Add(port);
        }


        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (serialPort1.IsOpen) serialPort1.Close();
        }

        private void btnLedOn_Click(object sender, EventArgs e)
        {
            serialPort1.Write("1");
            lblLedStatus.Text = "ON";
            btnLedOff.Enabled = true;
            btnLedOn.Enabled = false;
        }

        private void btnLedOff_Click(object sender, EventArgs e)
        {
            serialPort1.Write("0");
            lblLedStatus.Text = "OFF";
            btnLedOn.Enabled = true;
            btnLedOff.Enabled = false;

        }

        private void comboBox1_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            serialPort1.PortName = comboBox1.SelectedItem.ToString();
            serialPort1.Open();
        }
    }
}









