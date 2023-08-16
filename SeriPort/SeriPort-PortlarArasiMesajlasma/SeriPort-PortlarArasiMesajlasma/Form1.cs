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

namespace SeriPort_PortlarArasiMesajlasma
{
    public partial class btnGonder : Form
    {
        public btnGonder()
        {
            InitializeComponent();
        }

        private void btnGonder_Load(object sender, EventArgs e)
        {
            

            foreach (var seriPort in SerialPort.GetPortNames())
            {
                comboBoxCOMsec.Items.Add(seriPort);
            }

            
            btnDisconnect.Enabled = false;
            comboBoxCOMsec.SelectedIndex = 0;
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            serialPort1.PortName = comboBoxCOMsec.Text;
            serialPort1.BaudRate = 9600;
            serialPort1.Parity = Parity.Even;
            serialPort1.StopBits = StopBits.One;
            serialPort1.DataBits = 8;

            try
            {
                serialPort1.Open();
            }
            catch (Exception ex)
            {

                MessageBox.Show($"Seri Port bağlantısı yapılamadı \n Hata :{ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            if (serialPort1.IsOpen)
            {
                btnConnect.Enabled = false;
                btnDisconnect.Enabled = true;
                btnSend.Enabled = true;
            }


        }

        private void btnDisconnect_Click(object sender, EventArgs e)
        {
            if (serialPort1.IsOpen)
            {
                serialPort1.Close();
                btnConnect.Enabled = true;
                btnDisconnect.Enabled = false;
                btnSend.Enabled = false;
            }
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            if (serialPort1.IsOpen)
            {

                richTxtRecived.Text += Environment.NewLine + txtMessage;
                serialPort1.Write(txtMessage.Text);
                txtMessage.Clear();
                
            }
        }

        public delegate void veriGoster(String s);
        
        public void txtYaz(String s)
        {
            richTxtRecived.Text += Environment.NewLine + s;
        }

        private void serialPort1_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            string gelenVeri = serialPort1.ReadExisting();
            richTxtRecived.Invoke(new veriGoster(txtYaz), gelenVeri);
        }
    }
}
