using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Mail;
using HealthPet.Models;

namespace HealthPet.Email
{
    public class EmailHelper
    {
        public bool SendEmail(Appointment appointment)
        {
            MailMessage mailMessage = new MailMessage();
            mailMessage.From = new MailAddress("healthpetclinic@gmail.com");
            mailMessage.To.Add(new MailAddress(appointment.Email));

            mailMessage.Subject = "Nueva cita agendada";
            mailMessage.IsBodyHtml = false;
            mailMessage.Body = "Fecha: " + appointment.Date + Environment.NewLine +
                                "Nombre: " + appointment.FirstName + " " + appointment.LastName + Environment.NewLine +
                                "Mascota: " + appointment.PetName;

            SmtpClient client = new SmtpClient();
            client.Credentials = new System.Net.NetworkCredential("healthpetclinic@gmail.com", "imah1525");
            client.Host = "smtp.gmail.com";
            client.Port = 587;
            client.EnableSsl = true;

            try
            {
                client.Send(mailMessage);
                return true;
            }
            catch (Exception ex)
            {
                var excep = ex;
            }
            return false;
        }
    }
}
