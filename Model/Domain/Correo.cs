using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;

namespace Model.Domain
{
    public class Correo
    {

        public string Destino { get; set; }
        public string Remitente { get; set; }
        public string Asunto { get; set; }
        public string Mensaje { get; set; }


        public bool PalomaMensajera(Correo cor)
        {
            bool exito = false;


            using (SmtpClient servicio = new SmtpClient("smtp-mail.outlook.com", 587))
            {
                servicio.EnableSsl = true;
                servicio.Credentials = new System.Net.NetworkCredential("echony@outlook.com", "hola1234");

                MailMessage correo = new MailMessage("echony@outlook.com", cor.Destino, cor.Asunto, cor.Mensaje);

                correo.IsBodyHtml = true;
                correo.BodyEncoding = System.Text.Encoding.UTF8;
                // correo.To.Add(cor.Destino);

                //correo.From = new MailAddress("jhonas_724@hotmail.com", "Echony", System.Text.Encoding.UTF8);

                //correo.Subject = cor.Asunto;
                // correo.Body = cor.Mensaje;







                try
                {
                    servicio.Send(correo);
                    exito = true;
                }
                catch (SmtpException e)
                {
                    Console.WriteLine("Error de envio de correo: " + e.ToString());
                }

            }
            return exito;
        }
        public String GetCodigo(int longitud)
        {
            String cadenaAleatoria = "";

            //long milis = int(DateTime.Now);
            Random r = new Random();
            int i = 0;
            while (i < longitud)
            {
                char c = (char)r.Next(255);
                // System.out.println("char:"+c);
                if ((c >= '0' && c <= 9) || (c >= 'A' && c <= 'Z'))
                {
                    cadenaAleatoria += c;
                    i++;
                }
            }
            return cadenaAleatoria;
        }
    }
}
