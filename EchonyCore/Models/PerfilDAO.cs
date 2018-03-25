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
                    user = db.Usuario.Where(x => x.NickName == u.NickName).Include(x => x.Foto).Include(x => x.Publicaciones ).FirstOrDefault();
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

        public void AddComentario(Comentarios c)
        {
            using (EchonyEntityContext db = new EchonyEntityContext())
            {
                try
                {
                    List<Comentarios> lista = new List<Comentarios>();
                    lista.Add(c);
                    Publicaciones p = new Publicaciones();
                    p.Id = c.Id;
                    Publicaciones detalles = db.Publicaciones.Find(p.Id);
                    detalles.Comentarios = lista;
                    db.Publicaciones.Add(detalles);
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
                    
                    lista = (from u in db.Usuario where u.Nombre.Contains(r) || u.Apellido.Contains(r) select u).ToList();
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
