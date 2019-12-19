using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;

namespace iBeautyWebApi.Classes
{
    public class Email
    {
        public bool SendRegistrationCode(string email, string name, int code)
        {
            //var user = "ibeautyapphn@gmail.com";
            //var pass = "ibeauty123";
            var user = "registrodev@gmail.com";
            var pass = "comidas45*";
            MailMessage mail = new MailMessage();
            mail.To.Add(new MailAddress(email));
            mail.From = new MailAddress(user);
            mail.Subject = "Confirmacion de Registro";
            mail.Body = "<p>Bienvenido " + name + "</p><p>Gracias por registrarte, tu cuenta te da acceso a todas la funcionalidades de nuestra App, utiliza el siguiente codigo para la verificacion:</p><p><h2>" + code + "</h2></p></p><p>¡Feliz dia!</p></p><p>iBeauty</p>";
            mail.IsBodyHtml = true;
            mail.BodyEncoding = System.Text.Encoding.UTF8;
            mail.SubjectEncoding = System.Text.Encoding.Default;

            SmtpClient Smtp = new SmtpClient();
            Smtp.Port = 587;
            Smtp.Host = "smtp.gmail.com";
            Smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            Smtp.EnableSsl = true;
            Smtp.UseDefaultCredentials = false;
            Smtp.Credentials = new System.Net.NetworkCredential(user, pass);
            try
            {
                Smtp.Send(mail);
            }
            catch
            {
            }
            return true;
        }

        public bool SendTemporalPassword(string email, string name, string temporalpass)
        {
            var user = "ibeautyapphn@gmail.com";
            var pass = "ibeauty123";
            MailMessage mail = new MailMessage();
            mail.To.Add(new MailAddress(email));
            mail.From = new MailAddress(user);
            mail.Subject = "Contraseña Temporal";
            mail.Body = "<p>Hola " + name + "</p><p>Se te asigno una contraseña temporal para que puedas acceder a nuestra app, la contraseña temporal es:</p><p><h3>" + temporalpass + "</h3></p></p><p>Puedes cambiarla en cualquier momento en las configuraciones de tu perfil.</p></p><p>iBeauty</p>";
            mail.IsBodyHtml = true;
            mail.BodyEncoding = System.Text.Encoding.UTF8;
            mail.SubjectEncoding = System.Text.Encoding.Default;

            SmtpClient Smtp = new SmtpClient();
            Smtp.Port = 587;
            Smtp.Host = "smtp.gmail.com";
            Smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            Smtp.EnableSsl = true;
            Smtp.UseDefaultCredentials = false;
            Smtp.Credentials = new System.Net.NetworkCredential(user, pass);
            try
            {
                Smtp.Send(mail);
            }
            catch
            {
            }
            return true;
        }
    }
}
