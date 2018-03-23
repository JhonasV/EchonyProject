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
                    user = db.Usuario.Where(x => x.NickName == u.NickName).Include(x => x.Foto).FirstOrDefault();
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
                    db.Publiaciones.Add(p);
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
                /*from s in db.Services
                join sa in db.ServiceAssignments on s.Id equals sa.ServiceId
                where sa.LocationId == 1
                select s*/
                /*lista = (from p in db.Publiaciones
                         join u in db.Usuario on p.UsuarioId equals u.Id
                         where u.Id == ux.Id
                         select (p => { p})).Include(x => x.Comentario).ToList();*/

                //JavaScriptSerializer js = new JavaScriptSerializer();
                
                // lista = db.Publiaciones.Where(x => x.UsuarioId == ux.Id).Include(x => x.Comentario).ToList();
                /* var entryPoint = (from ep in dbContext.tbl_EntryPoint
                                   join e in dbContext.tbl_Entry on ep.EID equals e.EID
                                   join t in dbContext.tbl_Title on e.TID equals t.TID
                                   where e.OwnerID == user.UID
                                   select new
                                   {
                                       UID = e.OwnerID,
                                       TID = e.TID,
                                       Title = t.Title,
                                       EID = e.EID
                                   }).Take(10);*/
                /*var listaAux = (from la in db.Publiaciones
                                join u in db.Usuario on la.UsuarioId equals u.Id
                                join c in db.Comentarios on u.Id equals c.UsuarioId
                                where u.NickName == ux.NickName
                                select new {
                                    la,
                                    u,
                                    c
                                    

                                }).ToList();*/

                /*var listaAux = db.Publiaciones
                               .Join(db.Usuario, p => p.UsuarioId,
                               u => u.Id,
                               (p, u) => new { p, u }
                               )
                               .Join(db.Comentarios,
                               com => com.u.Id,
                               coment => coment.UsuarioId,
                               (com, coment) => new {com, coment})
                               .Join)*/




                //Publicacion
                /*
                                        Nickname = com.u.NickName,
                                                   Publicacion_Contenido = com.p.Contenido,
                                                   Publicacion_Fecha = com.p.Fecha,
                                                   Comentarios = com.p.Comentario,
                                        //Comentador
                                       /* comentador_foto = com.u.Foto,
                                                   comentador_comentario = coment.Comentario,
                                                   comentario_fecha = coment.Fecha_Comentario,*/


                /*  }).ToList();*/
                var listaAux = db.Publiaciones
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
                //var lista = listaAux.ToList();
                string lista = JsonConvert.SerializeObject(listaAux);                //string lista = js.Serialize(listaAux);
                //return lista;
                //return (IEnumerable<Publicaciones>)listaAux;
                return lista;
            }
            //return lista;
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
                    Publicaciones detalles = db.Publiaciones.Find(p.Id);
                    detalles.Comentarios = lista;
                    db.Publiaciones.Add(detalles);
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


    }
}
