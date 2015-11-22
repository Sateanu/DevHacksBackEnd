using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Net.Mail;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace DevHacksService
{
    public partial class Service1 : ServiceBase
    {
        Timer timer;
        public Service1()
        {
            InitializeComponent();
            timer = new Timer(1000 * 60);
            timer.Elapsed += Timer_Elapsed;
            timer.Start();
        }

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            DoVoodooMagic();
        }

        private void DoVoodooMagic()
        {
            using (Entities db = new Entities())
            {
                double dateNow = DateTime.Now.Ticks;
                var orders = db.Orders.Where(x => x.Done == 0 && x.Time <= dateNow).ToList();
                foreach (var order in orders)
                {
                    PullTheLeverKronk(order);
                }
            }
        }

        private void PullTheLeverKronk(Orders order)
        {
            MailMessage
        }

        protected override void OnStart(string[] args)
        {

        }

        protected override void OnStop()
        {
        }
    }
}
