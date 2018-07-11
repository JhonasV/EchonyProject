using Microsoft.EntityFrameworkCore;
using Model;
using Model.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Service
{
    public interface IUsuarioService
    {
        Usuario GetUsuario(Usuario u);
        Usuario GetUsuarioById(Usuario u);
        List<Usuario> Busqueda(string r);
    }

    public class UsuarioService : IUsuarioService
    {
        private readonly EchonyDbContext _context;
        public UsuarioService(EchonyDbContext context)
        {
            _context = context;
        }

        public Usuario GetUsuario(Usuario u)
        {

            Usuario user = new Usuario();
           
                try
                {
                    //_dbmsParentSections.ForEach(x => x.Children = x.Children.OrderBy(y => y.Order).ToList());
                    Publicaciones p = new Publicaciones();

                    user = _context.Usuario.Where(x => x.NickName == u.NickName).Include(x => x.Foto).Include(x => x.Publicaciones).Include(x => x.Comentarios).Include(x => x.Likes).FirstOrDefault();
                }
                catch (Exception e)
                {
                    e.ToString();
                }

         
            return user;
        }

        public Usuario GetUsuarioById(Usuario u)
        {
            Usuario user = new Usuario();
           
                try
                {
                    user = _context.Usuario.Where(x => x.Id == u.Id).Include(x => x.Foto).Include(x => x.Publicaciones).Include(x => x.Comentarios).Include(x => x.Foto).FirstOrDefault();
                }
                catch (Exception e)
                {
                    e.ToString();
                }

            
            return user;
        }

        public List<Usuario> Busqueda(string r)
        {
            List<Usuario> lista = new List<Usuario>();          
                try
                {
                    lista = (from u in _context.Usuario where u.Nombre.Contains(r) || u.Apellido.Contains(r) select u).Include(x => x.Foto).ToList();
                }
                catch (Exception e)
                {
                    Console.Write(e);
                }          
            return lista;
        }
    }
}
