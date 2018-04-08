using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EchonyCore.Models
{
    public class SocialDAO
    {
        public void AceptarSolicitud(SolicitudAmistad s)
        {
            using(EchonyEntityContext db = new EchonyEntityContext())
            {
                try
                {
                
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

        public List<Likes> SetMeGusta(Likes like)
        {
           
            string mensaje = "";
            int contador = 0;
            List<Likes> lista = new List<Likes>();
            using(EchonyEntityContext db = new EchonyEntityContext())
            {
                try
                {
                    Likes detalles = db.Likes.Where(x => x.UsuarioId == like.UsuarioId && x.PublicacionesId == like.PublicacionesId).FirstOrDefault();
                    if(detalles != null)
                    {
                        db.Likes.Remove(detalles);
                        db.SaveChanges();

                        lista = db.Likes.Where(x => x.PublicacionesId == like.PublicacionesId).Include(x => x.Usuario).ToList();
                        contador = lista.Count();
                        mensaje = "Like eliminado satisfactoriamente";
                    }
                    else
                    {
                        db.Likes.Add(like);
                        db.SaveChanges();
                        lista = db.Likes.Where(x => x.PublicacionesId == like.PublicacionesId).Include(x=> x.Usuario).ToList();
                        contador = lista.Count();
                        mensaje = "Like agregado satisfactoriamente";
                    }
                    
                }
                catch (Exception e)
                {
                    mensaje = String.Format("Excepcion al agregar el like, Error {0}", e.ToString());
                    e.ToString();
                }
            }
            return lista;
        }

        public List<Likes> getLikes(Likes l)
        {
            List<Likes> lista = new List<Likes>();
            using(EchonyEntityContext db = new EchonyEntityContext())
            {
                try
                {
                    lista = lista = db.Likes.Where(x => x.PublicacionesId == l.PublicacionesId).Include(x => x.Usuario).Include(x=> x.Usuario.Foto).ToList();

                    lista.ForEach(e =>
                    {
                        e.Usuario.Foto.Usuario = null;
                    });
                }
                catch (Exception e)
                {

                    throw;
                }
            }
            return lista;
        }
    }
}
