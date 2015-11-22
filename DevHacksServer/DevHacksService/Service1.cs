using DevHacksServer.Models;
using DevHacksService.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Net;
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
            timer = new Timer(1000 * 3);
            timer.Elapsed += Timer_Elapsed;
            eventLog1 = new System.Diagnostics.EventLog();
            if (!System.Diagnostics.EventLog.SourceExists("DevHackSource"))
            {
                System.Diagnostics.EventLog.CreateEventSource(
                    "DevHackSource", "DevHackLog");
            }
            eventLog1.Source = "DevHackSource";
            eventLog1.Log = "DevHackLog";
            eventLog1.WriteEntry("STARTIING");
        }

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {

            DoVoodooMagic();
        }

        private void DoVoodooMagic()
        {
            eventLog1.WriteEntry("Incepem vrajitoria !!");
            try
            {
                eventLog1.WriteEntry("Baza de date!");
                double dateNow = DateTime.Now.Ticks;
                eventLog1.WriteEntry(dateNow.ToString());
                try
                {

                    List<Order> orders = GetGoodOrders();
                    string url = @"http://192.168.2.172:8008/api/OrdersApi/GetGoodOrders";
                    using (WebClient wc = new WebClient())
                    {
                        var json = wc.DownloadString(url);
                        eventLog1.WriteEntry(string.Format("{0} - {1}", orders.Count, json));
                        foreach (var order in orders)
                        {
                            eventLog1.WriteEntry(string.Format("Orderu cu id-ul {0}", order.Id));
                            order.Done = 1;
                            PullTheLeverKronk(order);
                        }
                    }
                }
                catch (Exception ex)
                {
                    eventLog1.WriteEntry(ex.Message, EventLogEntryType.Error);
                }
            }
            catch (Exception ex)
            {
                eventLog1.WriteEntry(ex.Message, EventLogEntryType.Error);
            }
        }
        public List<Order> GetGoodOrders()
        {
            List<Order> orders = new List<Order>();
            return orders;
        }
        private void PullTheLeverKronk(Order order)
        {
            SendTheOwl("andrei.sateanu@gmail.com");
            SendTheOwl("alexbuicescu@gmail.com");
            
        }

        public bool SendTheOwl(string email)
        {
            MailMessage msg = new MailMessage();

            msg.From = new MailAddress("andrei.sateanu@gmail.com");
            msg.To.Add(email);
            msg.Subject = "BRRRRRRRRRRRRRRRAAAAAAAAAAAAAAAAAAAAAAAAA " + DateTime.Now.ToString();
            msg.Body = "BRRRRRRRRRRRRRRRRRRRRRRRAAAAAAAA";
            SmtpClient client = new SmtpClient();
            client.UseDefaultCredentials = true;
            client.Host = "smtp.gmail.com";
            client.Port = 587;
            client.EnableSsl = true;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.Credentials = new NetworkCredential("andrei.sateanu@gmail.com", Resources.EmailPass);
            client.Timeout = 20000;
            try
            {
                client.Send(msg);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
            finally
            {
                msg.Dispose();
            }
        }

        protected override void OnStart(string[] args)
        {
            eventLog1.WriteEntry("BRRRRRRRRRA I'm ON!");
            timer.Start();
        }

        protected override void OnStop()
        {
            eventLog1.WriteEntry("Baby don't hurt me...no more");
            timer.Stop();
        }
    }
}
