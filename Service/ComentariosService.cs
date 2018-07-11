using Microsoft.EntityFrameworkCore;
using Model;
using Model.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Service
{
    public interface IComentariosService
    {
        List<Comentarios> GetAllComentarios();
        bool AddComentario(Comentarios c);
    }

    public class ComentariosService :IComentariosService
    {

        private readonly EchonyDbContext _context;
        public ComentariosService(EchonyDbContext context)
        {
            _context = context;
        }

        public List<Comentarios> GetAllComentarios()
        {

            try
                {
                    return _context.Comentarios.Include(x => x.Usuario.Foto).ToList();
                }
                catch (Exception)
                {

                    throw;
                }
            }
        

        public bool AddComentario(Comentarios c)
        {           
                try
                {                 
                    if (c.Foto == null){ c.Foto = "";}                   
                    _context.Comentarios.Add(c);
                    _context.SaveChanges();
                    return true;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
                return false;     
        }
    }
}
