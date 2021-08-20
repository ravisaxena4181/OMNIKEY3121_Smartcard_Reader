using System;
using System.Collections.Generic;
using System.Windows.Forms;
using PCSC;
namespace SCReader
{
    /// <summary>
    /// https://github.com/danm-de/pcsc-sharp
    /// </summary>
    public partial class Form1 : Form
    {
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
            string devicelist = string.Empty;
            var contextFactory = ContextFactory.Instance;
            using (var context = contextFactory.Establish(SCardScope.System))
            {
                Console.WriteLine("Currently connected readers: ");
                var readerNames = context.GetReaders();
                foreach (var readerName in readerNames)
                {
                    devicelist = devicelist + "\t" + readerName;
 
                }
            }
            textBox1.Text = devicelist;
            
        }

        private void btnConnectReader_Click(object sender, EventArgs e)
        {
            using (var ctx = ContextFactory.Instance.Establish(SCardScope.System))
            {
              var cn=  ctx.Connect("HID Global OMNIKEY 3x21 Smart Card Reader 0", SCardShareMode.Direct, SCardProtocol.Any);
                if(cn.IsConnected)
                {
                    textBox1.Text = "connected"; 
                }
               
            }
        }
    }
}
