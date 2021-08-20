using System;
using System.Collections.Generic;
using System.Windows.Forms;
using PCSC;
using PCSC.Monitoring;

namespace SCReader
{
    /// <summary>
    /// https://github.com/danm-de/pcsc-sharp
    /// </summary>
    public partial class Form1 : Form
    {
        public string selectedCard = string.Empty;
        public string readercontent = string.Empty;
        public bool IsStart { get; set; }


        IMonitorFactory monitorFactory;
        ISCardMonitor monitor;
        public Form1()
        {
            InitializeComponent();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAllDevices_Click(object sender, EventArgs e)
        {
          
        }

        private void ListAllDevices()
        {
            List<string> devices = new List<string>();
            string devicelist = string.Empty;
             
            var contextFactory = ContextFactory.Instance;
            using (var context = contextFactory.Establish(SCardScope.System))
            {
                Console.WriteLine("Currently connected readers: ");
                var readerNames = context.GetReaders();
                foreach (var readerName in readerNames)
                {
                    devicelist = devicelist + "\t" + readerName;
                    devices.Add(readerName.Trim());
                }
            }
            comboBox1.DataSource = new BindingSource(devices, null);
            textBox1.Text = devicelist;
        }

        private void btnConnectReader_Click(object sender, EventArgs e)
        {




            //readcard();
            if (IsStart)
            {
                IsStart = false;
                btnConnectReader.Text = "Start";
                monitor.Cancel();
                monitor.Dispose();
            }
            else
            {
                if (string.IsNullOrEmpty(comboBox1.Text))
                {
                    MessageBox.Show("Please select card.");

                    return;
                }
                selectedCard = comboBox1.Text;

                IsStart = true; btnConnectReader.Text = "Stop";
                StartMonitoring();
            }
            

        }
        delegate void SetTextCallback(string text);
        delegate void SetStartCallback(bool v);
        private void SetText(string text)
        {
            // InvokeRequired required compares the thread ID of the
            // calling thread to the thread ID of the creating thread.
            // If these threads are different, it returns true.
            if (this.textBox1.InvokeRequired)
            {
                SetTextCallback d = new SetTextCallback(SetText);
                this.Invoke(d, new object[] { text });
            }
            else
            {
                this.textBox1.Text = text;
            }
        }
        private void SetStart(bool v)
        {
            // InvokeRequired required compares the thread ID of the
            // calling thread to the thread ID of the creating thread.
            // If these threads are different, it returns true.
            if (this.btnDetectcard.InvokeRequired)
            {
                SetStartCallback d = new SetStartCallback(SetStart);
                this.Invoke(d, new object[] { v });
            }
            else
            {
                this.btnDetectcard.Enabled= v;
            }
        }
        private void readcard()
        {
            try
            {
                using (var ctx = ContextFactory.Instance.Establish(SCardScope.System))
                {
                    //"HID Global OMNIKEY 3x21 Smart Card Reader 0"

                    using (var reader = ctx.ConnectReader(selectedCard, SCardShareMode.Shared, SCardProtocol.Any))
                    {
                        var cardAtr = reader.GetAttrib(SCardAttribute.AtrString);
                        //Console.WriteLine("ATR: {0}", BitConverter.ToString(cardAtr));
                        //Console.ReadKey();

                        //textBox1.Text += BitConverter.ToString(cardAtr);
                        SetText("connected;" + BitConverter.ToString(cardAtr));
                    }


                }
            }
            catch (Exception ex)
            {
                SetText("Error:" + ex.Message);

            }
        }

        private void btnDetectcard_Click(object sender, EventArgs e)
        {
             



        }

        private void Monitor_CardRemoved(object sender, CardStatusEventArgs e)
        {
            SetStart(false);
            //btnDetectcard.Enabled = false;
            SetText("Card removed.. Please insert");
        }

        private void Monitor_CardInserted(object sender, CardStatusEventArgs e)
        {
            SetStart(true);
            //btnDetectcard.Enabled = true;
            readcard();

        }
        private void Monitor_StatusChanged(object sender, StatusChangeEventArgs e)
        {
            //textBox1.Text = $"New state: {e.NewState}";
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            //StartMonitoring();
            btnDetectcard.Enabled = false;
            try
            {
                ListAllDevices();
            }
            catch (Exception ex)
            {

                textBox1.Text = ex.Message;
            }
        }

        private void StartMonitoring()
        {
             monitorFactory = MonitorFactory.Instance;
             monitor = monitorFactory.Create(SCardScope.System);
            monitor.CardInserted += Monitor_CardInserted;
            monitor.CardRemoved += Monitor_CardRemoved;
            monitor.Start(selectedCard);//"HID Global OMNIKEY 3x21 Smart Card Reader 0"
            readcard();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }
    }
}
