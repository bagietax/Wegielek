using System;
using System.Configuration;
using System.Linq;
using System.Web.Management;
using System.Web.Mvc;
using MvcMovie.Dal;
using MvcMovie.Models;
using SMSApi.Api;
using SMSApi.Api.Response;
using Client = MvcMovie.Models.Client;
using Exception = System.Exception;

namespace MvcMovie.Controllers
{
    public class SmsSender : Controller
    {
        private CoalCubeDbContex db = new CoalCubeDbContex();

        private SMSFactory _smsApi;
        private UserFactory _user;

        public SmsSender()
        {
            var settings = db.SmsSender.FirstOrDefault();
            SMSApi.Api.Client smsClient = new SMSApi.Api.Client(ConfigurationManager.AppSettings["Login"]);
            smsClient.SetPasswordHash(ConfigurationManager.AppSettings["Password"]);
            _smsApi = new SMSApi.Api.SMSFactory(smsClient);
            _user = new SMSApi.Api.UserFactory(smsClient);

        }

        public bool SendTo(Client client)
        {
            var sms = new Sms();
            sms.To = client.TelephoneNumber;
            sms.Text = client.SmsTemplate;
            sms.When = DateTime.Now;
            try
            {
                sms.From = ConfigurationManager.AppSettings["Sender"];

                Send(sms, _smsApi);
                sms.IsDelivred = true;
                db.Sms.Add(sms);
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                sms.ErrorMessage = ex.Message;
                db.Sms.Add(sms);
                db.SaveChanges();
                return false;
            }
        }

        private void Send(Sms sms, SMSApi.Api.SMSFactory smsApi)
        {
            var result =
                smsApi.ActionSend()
                    .SetText(sms.Text)
                    .SetTo(sms.To)
                    .SetSender(sms.From) //Pole nadawcy lub typ wiadomość 'ECO', '2Way'
                    .Execute();
        }

        public Credits GetAvaiblePln()
        {
           return _user.ActionGetCredits().Execute();          
        }


    }
}