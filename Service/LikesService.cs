using Microsoft.EntityFrameworkCore;
using Model;
using Model.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Service
{
    public interface ILikesService
    {
        List<Likes> SetMeGusta(Likes like);
        List<Likes> getLikes(Likes l);
    }

    public class LikesService : ILikesService
    {
        private readonly EchonyDbContext _context;
        public LikesService(EchonyDbContext context)
        {
            _context = context;
        }

        public List<Likes> SetMeGusta(Likes like)
        {
            string mensaje = "";
            int contador = 0;
            List<Likes> lista = new List<Likes>();         
                try
                {
                    Likes detalles = _context.Likes.Where(x => x.UsuarioId == like.UsuarioId && x.PublicacionesId == like.PublicacionesId).FirstOrDefault();
                    if (detalles != null)
                    {
                        _context.Likes.Remove(detalles);
                        _context.SaveChanges();

                        lista = _context.Likes.Where(x => x.PublicacionesId == like.PublicacionesId).Include(x => x.Usuario).ToList();
                        contador = lista.Count();
                        lista.ForEach(e =>
                        {
                            e.Usuario.Likes = null;
                        });
                        mensaje = "Like eliminado satisfactoriamente";
                    }
                    else
                    {
                        _context.Likes.Add(like);
                        _context.SaveChanges();
                        lista = _context.Likes.Where(x => x.PublicacionesId == like.PublicacionesId).Include(x => x.Usuario).ToList();
                        contador = lista.Count();
                        lista.ForEach(e =>
                        {
                            e.Usuario.Likes = null;
                        });
                        mensaje = "Like agregado satisfactoriamente";
                    }
                }
                catch (Exception e)
                {
                    mensaje = String.Format("Excepcion al agregar el like, Error {0}", e.ToString());
                    e.ToString();
                }
            
            return lista;
        }

        public List<Likes> getLikes(Likes l)
        {
            List<Likes> lista = new List<Likes>();
           
                try
                {
                    lista = lista = _context.Likes.Where(x => x.PublicacionesId == l.PublicacionesId).Include(x => x.Usuario).Include(x => x.Usuario.Foto).ToList();
                    lista.ForEach(e =>
                    {
                        e.Usuario.Foto.Usuario = null;
                        e.Usuario.Likes = null;
                    });
                }
                catch (Exception e)
                {

                    e.ToString();
                }
            
            return lista;
        }
    }
}
