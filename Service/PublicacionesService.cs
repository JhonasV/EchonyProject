using Microsoft.EntityFrameworkCore;
using Model;
using Model.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Service
{

    public interface IPublicacionesService
    {
        void AddPublicacion(Publicaciones p);
        List<Publicaciones> GetPublicaciones(int userId);
        List<Publicaciones> GetPublicacionesPrueba(Usuario u);
        bool AgregarPublicacion(Publicaciones p);
    }
    public class PublicacionesService : IPublicacionesService
    {
        private readonly EchonyDbContext _context;
        public PublicacionesService
            (EchonyDbContext context)
        {
            _context = context;
        }

        public void AddPublicacion(Publicaciones p)
        {
            
                try
                {
                    _context.Publicaciones.Add(p);
                    _context.SaveChanges();
                }
                catch (Exception e)
                {

                    Console.WriteLine(e);
                }

            }

        

        public List<Publicaciones> GetPublicaciones(int userId)
        {
            List<Publicaciones> detalles = new List<Publicaciones>();
          
                try
                {
                    detalles = _context.Publicaciones.Where(x => x.Usuario.Id == userId).Include(x => x.Usuario).Include(x => x.Usuario.Foto).Include(x => x.Like).Include(x => x.Comentarios).OrderByDescending(x => x.Id).ToList();
                //_context.Publicaciones.Where(x => x.UsuarioId == userId).Include(x => x.Usuario.Foto).OrderByDescending(x => x.Fecha).ToList();
            }
                catch (Exception e)
                {

                    e.ToString();
                }
            
            return detalles;
        }

        public List<Publicaciones> GetPublicacionesPrueba(Usuario u)
        {
            List<Publicaciones> lista = new List<Publicaciones>();
           
                try
                {
                    lista = _context.Publicaciones.Where(x => x.Usuario.Id == u.Id).Include(x => x.Usuario).Include(x => x.Usuario.Foto).Include(x => x.Like).Include(x => x.Comentarios).OrderByDescending(x => x.Id).ToList();
                }
                catch (Exception)
                {

                    throw;
                }
            
            return lista;
        }

        public bool AgregarPublicacion(Publicaciones p)
        {
           
            try
            {
                _context.Publicaciones.Add(p);
                _context.SaveChanges();
                return true;
               
            }
            catch (Exception e)
            {

                Console.Write(e);
            }
            return false;
        }
    }
}
