using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TestJunk
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            timer1.Interval = 1000 * 60;
            progressBar1.Style = ProgressBarStyle.Marquee;
            timer1.Start();
            r = new Random((int)DateTime.Now.Ticks);
        }
        Random r;
        private void timer1_Tick_1(object sender, EventArgs e)
        {
            using (WebClient client = new WebClient())
            {
                progressBar1.MarqueeAnimationSpeed = r.Next(60, 400);
                var json = client.DownloadString(@"http://192.168.2.172:8008/api/OrdersApi/GetMagic");
            }

        }
    }
}
