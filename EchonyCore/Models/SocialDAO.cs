using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EchonyCore.Models
{
    public class SocialDAO
    {
        public void AceptarSolicitud(SolicitudAmistad s, Receptor r, Emisor em)
        {
            using(EchonyEntityContext db = new EchonyEntityContext())
            {
                try
                {
                   /* Amigos amigos = new Amigos()
                    {
                       
                        Emisor = em,
                        Receptor = r,
                        Fecha = DateTime.Now
                    };
                    db.Amigos.Add(amigos);
                    db.SaveChanges();*/
                  //  SolicitudAmistad solicitud = db.SolicitudAmistad.Where(x=> x.Emisor.UsuarioId)

                    s.Estado = 1;
                    db.SolicitudAmistad.Attach(s);
                    var entrada = db.Entry(s);
                    entrada.Property(e => e.Estado).IsModified = true;
                    db.SaveChanges();
                }
                catch (Exception e)
                {

                    e.ToString();
                }
            }
        }

        public List<Amigos> GetAmigos(Emisor em)
        {
            List<Amigos> lista = new List<Amigos>();
            using(EchonyEntityContext db = new EchonyEntityContext())
            {
                try
                {
                    lista = db.Amigos.Where(x => x.Emisor.UsuarioId == em.UsuarioId || x.Receptor.UsuarioId == em.UsuarioId).Include(x => x.Receptor.Usuario).ToList();
                   
                }
                catch (Exception e)
                {

                    e.ToString();
                }
            }
            return lista;
        }
    }
}
