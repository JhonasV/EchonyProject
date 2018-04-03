using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EchonyCore.Models
{
    public class PerfilDAO
    {
        public Usuario GetUsuario(Usuario u)
        {

            Usuario user = new Usuario();
            using (EchonyEntityContext db = new EchonyEntityContext())
            {
                try
                {
                    //_dbmsParentSections.ForEach(x => x.Children = x.Children.OrderBy(y => y.Order).ToList());
                   
                   user = db.Usuario.Where(x => x.NickName == u.NickName).Include(x => x.Foto).Include(x => x.Publicaciones ).Include(x=> x.Comentarios).FirstOrDefault();
                }
                catch (Exception e)
                {
                    e.ToString();
                }

            }
            return user;
        }

        public Usuario GetUsuarioById(Usuario u)
        {
            Usuario user = new Usuario();
            using (EchonyEntityContext db = new EchonyEntityContext())
            {
                try
                {
                    user = db.Usuario.Where(x => x.Id == u.Id).Include(x => x.Foto).Include(x => x.Publicaciones).Include(x => x.Comentarios).FirstOrDefault();
                }
                catch (Exception e)
                {
                    e.ToString();
                }

            }
            return user;
        }

        public void AddPublicacion(Publicaciones p)
        {
            using (EchonyEntityContext db = new EchonyEntityContext())
            {
                try
                {
                    db.Publicaciones.Add(p);
                    db.SaveChanges();
                }
                catch (Exception e)
                {

                    Console.WriteLine(e);
                }

            }

        }
        public string GetPublicaciones(Usuario ux)
        {
            // List<Publicaciones> lista = new List<Publicaciones>();
            using (EchonyEntityContext db = new EchonyEntityContext())
            {
                
                var listaAux = db.Publicaciones
                    .Join(db.Usuario, p => p.UsuarioId, u => u.Id, (p, u) => new { p, u }).Where(x => x.u.NickName == ux.NickName)
                   .Select(s => new
                   {
                       s.p.Contenido,

                       s.p.Id,
                       s.p.Fecha,
                       s.u.Foto.RutaFoto,
                       s.u.Nombre,
                       s.u.Apellido

                   }).ToList();
              
                string lista = JsonConvert.SerializeObject(listaAux);                
              
                return lista;
            }
            
        }

       public List<Publicaciones> GetPubicacionesDesc(Usuario u)
        {
            List<Publicaciones> lista = new List<Publicaciones>();
            using(EchonyEntityContext db = new EchonyEntityContext())
            {
                try
                {
                    lista = db.Publicaciones.OrderByDescending(x => x.Id).Where(x=>x.Usuario.NickName == u.NickName).Include(x => x.Usuario).Include(x => x.Comentarios).ToList();
                }
                catch (Exception)
                {

                    throw;
                }
            }
            return lista;
        }

        public void AddComentario(Comentarios c)
        {
            using (EchonyEntityContext db = new EchonyEntityContext())
            {
                try
                {
                    /* List<Comentarios> lista = new List<Comentarios>();
                     lista.Add(c);
                     Publicaciones p = new Publicaciones();
                     p.Id = c.Id;
                     Publicaciones detalles = db.Publicaciones.Find(p.Id);
                     detalles.Comentarios = lista;
                     db.Publicaciones.Add(detalles);
                     db.Comentarios.Add(c);
                     db.SaveChanges();*/
                     if(c.Foto == null)
                    {
                        c.Foto = "";
                    }

                    db.Comentarios.Add(c);
                    
                    
                    db.SaveChanges();
                }
                catch (Exception e)
                {

                    Console.WriteLine(e);
                }

            }
        }

        public string AddFotoPerfil(Foto f)
        {
            string mensaje = "La foto no fue actualizada";
            using (EchonyEntityContext db = new EchonyEntityContext())
            {
                try
                {
                    db.Fotos.Attach(f);
                    var entrada = db.Entry(f);
                    entrada.Property(e => e.RutaFoto).IsModified = true;
                    db.SaveChanges();


                    mensaje = "La foto fue actualizada";
                }
                catch (Exception)
                {

                    throw;
                }


            }
            return mensaje;
        }

        /*public void AddFoto(Foto f)
        {
            using(EchonyEntityContext db = new EchonyEntityContext())
            {
                
            }
        }*/

        public bool AgregarPublicacion(Publicaciones p)
        {
            bool exito = false;
            try
            {
                using (EchonyEntityContext db = new EchonyEntityContext())
                {
                    db.Publicaciones.Add(p);
                    db.SaveChanges();
                    exito = true;
                }
            }
            catch (Exception e)
            {

                Console.Write(e);
            }
            return exito;
        }
        public List<Usuario> Busqueda(string r)
        {
            List<Usuario> lista = new List<Usuario>();
            using (EchonyEntityContext db = new EchonyEntityContext())
            {
                try
                {
                    
                    lista = (from u in db.Usuario where u.Nombre.Contains(r) || u.Apellido.Contains(r) select u).Include(x=> x.Foto).ToList();
                }
                catch (Exception e)
                {

                    Console.Write(e);
                }
            }
               
            return lista;
        }

        public void Emisor(Emisor e)
        {
            
            using(EchonyEntityContext db = new EchonyEntityContext())
            {
                db.Add(e);
                
            }
        }



        public void NotificacionAmistad(SolicitudAmistad a, Emisor e, Receptor r)
        {
            using(EchonyEntityContext db = new EchonyEntityContext())
            {
                /*db.Emisor.Add(e);
                db.SaveChanges();

                db.Receptor.Add(r);
                db.SaveChanges();*/
                /* db.Emisor.Add(e);
                 db.SaveChanges();

                 db.Receptor.Add(r);
                 db.SaveChanges();*/
                a.Emisor = e;
                a.Receptor = r;
                db.SolicitudAmistad.Add(a);
                db.SaveChanges();
            }
        }
        
       public void EliminarNotificacionAmistad(SolicitudAmistad a)
        {
            using (EchonyEntityContext db = new EchonyEntityContext())
            {
                SolicitudAmistad detalles = db.SolicitudAmistad.Find(a.Id);
                db.SolicitudAmistad.Remove(detalles);
                db.SaveChanges();
            }
        }

       public SolicitudAmistad GetAmigos(int usuario1, int usuario2)
        {
            SolicitudAmistad a = new SolicitudAmistad();
            using (EchonyEntityContext db = new EchonyEntityContext())
            {
                try
                {
                    a = db.SolicitudAmistad.Where(x => x.Receptor.UsuarioId == usuario1 && x.Emisor.UsuarioId == usuario2 || x.Receptor.UsuarioId == usuario2 && x.Emisor.UsuarioId == usuario1).FirstOrDefault();
                   // a = db.Amistad.Where(x => x.Receptor == usuario1 && x.Emisor == usuario2 || x.Emisor == usuario1 && x.Receptor == usuario2).FirstOrDefault();
                    //a = db.SolicitudAmistad.Where(x => x.ReceptorId == usuario1 && x.EmisorId == usuario2 || x.EmisorId == usuario1 && x.ReceptorId == usuario2).FirstOrDefault();
                    if(a == null)
                    {
                        a = new SolicitudAmistad { Estado = 5 };
                    }
                }
                catch (Exception)
                {

                    throw;
                }
            }

            return a;
        }
        
        public List<SolicitudAmistad> GetNotificacionesAmistad(Receptor r)
        {
            List<SolicitudAmistad> lista = new List<SolicitudAmistad>();
            using (EchonyEntityContext db = new EchonyEntityContext())
            {
                try
                {
                    //lista = db.SolicitudAmistad.Where(x => x.ReceptorId == a.ReceptorId).ToList();
                    lista = db.SolicitudAmistad.Where(x => x.Receptor.UsuarioId == r.Id && x.Estado == 0).Include(x => x.Emisor.Usuario).Include(x=> x.Emisor.Usuario.Foto)
                        .Include(x => x.Receptor.Usuario).Include(x => x.Receptor.Usuario.Foto).ToList();
                }
                catch (Exception e)
                {

                    Console.Write(e);
                }
            }
            return lista;
        }

        public List<SolicitudAmistad> GetAmigos(Receptor r)
        {
            List<SolicitudAmistad> lista = new List<SolicitudAmistad>();
            using (EchonyEntityContext db = new EchonyEntityContext())
            {
                try
                {
                    //lista = db.SolicitudAmistad.Where(x => x.ReceptorId == a.ReceptorId).ToList();
                    lista = db.SolicitudAmistad.Where(x => x.Receptor.UsuarioId == r.Id || x.Emisor.UsuarioId == r.Id && x.Estado == 1).Include(x => x.Emisor.Usuario).Include(x => x.Emisor.Usuario.Foto)
                        .Include(x => x.Receptor.Usuario).Include(x => x.Receptor.Usuario.Foto).ToList();
                }
                catch (Exception e)
                {

                    Console.Write(e);
                }
            }
            return lista;
        }

    }
}
