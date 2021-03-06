using System;
using System.Collections.Generic;
using System.Windows.Forms;
using PCSC;
using PCSC.Monitoring;
using PCSC.Iso7816;
using System.Text;
using System.Globalization;
using System.Linq;

namespace SCReader
{
    /// <summary>
    /// https://github.com/danm-de/pcsc-sharp
    /// https://csharp.hotexamples.com/examples/PCSC.Iso7816/IsoReader/-/php-isoreader-class-examples.html
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
        private void writetocard() {
            byte[] DATA_TO_WRITE = {
            0x0F, 0x0E, 0x0D, 0x0C, 0x0B, 0x0A, 0x09, 0x08, 0x07, 0x06, 0x05, 0x04, 0x03, 0x02, 0x01, 0x00
        };
            using (var ctx = ContextFactory.Instance.Establish(SCardScope.System))
            {

                using (var isoReader = new IsoReader(
                   context: ctx,
                   readerName: comboBox1.Text,
                   mode: SCardShareMode.Shared,
                   protocol: SCardProtocol.Any))
                {
                    // Build a GET CHALLENGE command 
                    var apdu = new CommandApdu(IsoCase.Case4Extended, SCardProtocol.T0)
                    {
                        CLA = 0x00, // Class
                        Instruction = InstructionCode.WriteBinary,
                        P1 = 0x00, // Parameter 1
                        P2 = 0x00, // Parameter 2
                        Le = DATA_TO_WRITE.Length,// 0x08, // Expected length of the returned data
                        Data    = DATA_TO_WRITE,
                    };

                    SetText(string.Format( "Send APDU with \"GET CHALLENGE\" command: {0}",
                        BitConverter.ToString(apdu.ToArray())));

                    var response = isoReader.Transmit(apdu);

                    SetText(string.Format("SW1 SW2 = {0:X2} {1:X2}",
                        response.SW1, response.SW2));

                    if (!response.HasData)
                    {
                        SetText(string.Format("No data. (Card does not understand \"GET CHALLENGE\")"));
                    }
                    else
                    {
                        var data = response.GetData();
                        SetText(string.Format("Challenge: {0}", BitConverter.ToString(data)));
                    }
                }

            }

        }

        private void btnDetectcard_Click(object sender, EventArgs e)
        {
            writetocard();
            return;
            using (var context = ContextFactory.Instance.Establish(SCardScope.System))
            {
                
                using (var rfidReader = context.ConnectReader(comboBox1.Text, SCardShareMode.Shared, SCardProtocol.Any))
                {
                    var apdu = new CommandApdu(IsoCase.Case2Short, rfidReader.Protocol)
                    {
                        CLA = 0xFF,
                        Instruction = InstructionCode.GetData,
                        P1 = 0x00,
                        P2 = 0x00,
                        Le = 0 // We don't know the ID tag size
                    };

                    using (rfidReader.Transaction(SCardReaderDisposition.Leave))
                    {
                        Console.WriteLine("Retrieving the UID .... ");

                        var sendPci = SCardPCI.GetPci(rfidReader.Protocol);
                        var receivePci = new SCardPCI(); // IO returned protocol control information.

                        var receiveBuffer = new byte[256];
                        var command = apdu.ToArray();

                        var bytesReceived = rfidReader.Transmit(
                            sendPci, // Protocol Control Information (T0, T1 or Raw)
                            command, // command APDU
                            command.Length,
                            receivePci, // returning Protocol Control Information
                            receiveBuffer,
                            receiveBuffer.Length); // data buffer

                        var responseApdu =
                            new ResponseApdu(receiveBuffer, bytesReceived, IsoCase.Case2Short, rfidReader.Protocol);
                        Console.WriteLine("SW1: {0:X2}, SW2: {1:X2}\nUid: {2}",
                            responseApdu.SW1,
                            responseApdu.SW2,
                            responseApdu.HasData ? BitConverter.ToString(responseApdu.GetData()) : "No uid received");
                    }
                }
            }
             
        }
        #region events

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
                this.textBox1.Text += Environment.NewLine + text;
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
                this.btnDetectcard.Enabled = v;
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
                        var rstatus = reader.GetStatus();

                        Console.WriteLine("Reader {0} connected with protocol {1} in state {2}",
                            rstatus.GetReaderNames().FirstOrDefault(),
                            rstatus.Protocol,
                            rstatus.State);
                        SetText(string.Format("Reader {0} connected with protocol {1} in state {2}",
                            rstatus.GetReaderNames().FirstOrDefault(),
                            rstatus.Protocol,
                            rstatus.State));
                        //Console.WriteLine("ATR: {0}", BitConverter.ToString(cardAtr));
                        //Console.ReadKey();

                        //textBox1.Text += BitConverter.ToString(cardAtr);
                        SetText(string.Format("ATR: {0}", BitConverter.ToString(cardAtr)));
                    }


                }
            }
            catch (Exception ex)
            {
                SetText("Error:" + ex.Message);

            }
        }
        private void readrecords()
        {
            try
            {
                var contextFactory = ContextFactory.Instance;
                using (var ctx = contextFactory.Establish(SCardScope.System))
                {
                    using (var isoReader = new IsoReader(ctx, comboBox1.Text, SCardShareMode.Shared, SCardProtocol.Any, false))
                    {

                        var apdu = new CommandApdu(IsoCase.Case4Extended, SCardProtocol.T0)
                        {
                            CLA = 0x00, // Class
                            Instruction = InstructionCode.WriteBinary,
                            P1 = 0x00, // Parameter 1
                            P2 = 0x00, // Parameter 2
                            Le = 0, // Expected length of the returned data
                            //Le = 0x00 // Expected length of the returned data
                            Data = Encoding.ASCII.GetBytes("4F6C6976696572204D616973FFFF07913344026414F8FFFFFFFFFFFF"),
                        };

                        var response = isoReader.Transmit(apdu);
                        SetText(string.Format("SW1 SW2 = {0:X2} {1:X2}", response.SW1, response.SW2));
                        if (response.SW1 == 0x90 && response.SW2 == 0x00)
                        {
                            if (response.HasData)
                            {
                                string dtresp = Encoding.UTF8.GetString(response.GetData());
                                SetText(string.Format("data = {0:X2},lenght ={1:X2}", dtresp, response.PciCount));
                                string outstring = Encoding.ASCII.GetString(response[0].FullApdu);

                                SetText(string.Format("data = {0:X2}", outstring));
                            }

                        }
                        else
                        {
                            SetText("No data found.");
                        }

                        //Console.WriteLine("SW1 SW2 = {0:X2} {1:X2}", response.SW1, response.SW2);
                        // ..
                    }
                }

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        private void checkcard()
        {
            try
            {
                var contextFactory = ContextFactory.Instance;
                using (var ctx = contextFactory.Establish(SCardScope.System))
                {
                    using (var isoReader = new IsoReader(ctx, comboBox1.Text,
                        SCardShareMode.Shared, SCardProtocol.Any, false))
                    {

                        var apdu = new CommandApdu(IsoCase.Case2Short, isoReader.ActiveProtocol)
                        {
                            CLA = 0x00, // Class
                            Instruction = InstructionCode.GetChallenge,
                            P1 = 0x00, // Parameter 1
                            P2 = 0x00, // Parameter 2
                            Le = 0x08 // Expected length of the returned data
                        };

                        var response = isoReader.Transmit(apdu);
                        SetText(string.Format("SW1 SW2 = {0:X2} {1:X2}", response.SW1, response.SW2));
                        if (response.SW1 == 0x90 && response.SW2 == 0x00)
                        {
                            if (response.HasData)
                            {
                                string dtresp = Encoding.UTF8.GetString(response.GetData());
                                SetText(string.Format("data = {0:X2},lenght ={1:X2}", dtresp, response.PciCount));
                                string outstring = Encoding.ASCII.GetString(response[0].FullApdu);

                                SetText(string.Format("data = {0:X2}", outstring));
                            }

                        }
                        else
                        {
                            SetText("No data found.");
                        }

                        //Console.WriteLine("SW1 SW2 = {0:X2} {1:X2}", response.SW1, response.SW2);
                        // ..
                    }
                }

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }


        private void case2()
        {
            var contextFactory = ContextFactory.Instance;
            using (var ctx = contextFactory.Establish(SCardScope.System))
            {
                using (var isoReader = new IsoReader(ctx, comboBox1.Text, SCardShareMode.Shared, SCardProtocol.Any, false))
                {
                    string author = "RAVI00000110001";
                    byte bClass = byte.Parse("A0", NumberStyles.AllowHexSpecifier);
                    // Convert a C# string to a byte array  
                    byte[] bytes = Encoding.ASCII.GetBytes(author);
                    var apdu = new CommandApdu(IsoCase.Case2Short, isoReader.ActiveProtocol)
                    {
                        CLA = bClass, // Class
                        Instruction = InstructionCode.ReadRecord,
                        P1 = 0x01, // Parameter 1
                        P2 = 0x04, // Parameter 2
                        Le = 0x00, // Expected length of the returned data
                                   //Data= bytes
                    };


                    var response = isoReader.Transmit(apdu);
                    SetText(string.Format("SW1 SW2 = {0:X2} {1:X2}", response.SW1, response.SW2));
                    if (response.SW1 == 0x90 && response.SW2 == 0x00)
                    {
                        MessageBox.Show("success");
                    }
                    else
                    {
                        if (response.HasData)
                        {
                            string dtresp = Encoding.UTF8.GetString(response.GetData());
                            MessageBox.Show(dtresp);
                        }


                    }
                    //Console.WriteLine("SW1 SW2 = {0:X2} {1:X2}", response.SW1, response.SW2);
                    // ..
                }

            }
        }

        private void Monitor_CardRemoved(object sender, CardStatusEventArgs e)
        {
            //SetStart(false);
            //btnDetectcard.Enabled = false;
            SetText("Card removed.. Please insert");
        }

        private void Monitor_CardInserted(object sender, CardStatusEventArgs e)
        {
            //SetStart(true);
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
            //btnDetectcard.Enabled = false;
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

        private void btnReadfromCard_Click(object sender, EventArgs e)
        {
            checkcard();
        } 
        #endregion
    }
}
