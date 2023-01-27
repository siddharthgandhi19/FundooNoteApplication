using Experimental.System.Messaging;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace CommonLayer.ModelClass
{
    public class MSMQModel
    {
        MessageQueue mQ = new MessageQueue();
        public void sendData2Queue(string token)
        {
            mQ.Path = @".\private$\FundooNote";
            if(!MessageQueue.Exists(mQ.Path))
            {
                MessageQueue.Create(mQ.Path);
            }
            mQ.Formatter = new XmlMessageFormatter(new Type[] { typeof(string) });
            mQ.ReceiveCompleted += MQ_ReceiveCompleted;   //tab try to achieve delegate here
            mQ.Send(token); // delegate achieve
            mQ.BeginReceive();
            mQ.Close();
        }

        private void MQ_ReceiveCompleted(object sender, ReceiveCompletedEventArgs e) //event
        {
           var message = mQ.EndReceive(e.AsyncResult);
            string token = message.Body.ToString();
            string subject = "Test Mail";
            string body = $"Fundoo Notes Reset Password: <a href=http://localhost:4200/resetPassword/{token}> Click Here</a>";
            var smtp = new SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                Credentials = new NetworkCredential("siddharth.bridgelabz@gmail.com", "eyuviursgrvaojfl"),
                EnableSsl = true,
            };

            smtp.Send("siddharth.bridgelabz@gmail.com", "siddharth.bridgelabz@gmail.com", subject, body);
            mQ.BeginReceive();
        }
    }
}
