using Microsoft.EntityFrameworkCore;
using Model;
using Model.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Service
{
    public interface ISolicitudAmistadService
    {
        List<SolicitudAmistad> GetAmigos(Receptor r);
        List<SolicitudAmistad> GetAmigos(Usuario r);
        List<SolicitudAmistad> GetNotificacionesAmistad(Receptor r);
        void AceptarSolicitud(SolicitudAmistad s);
        SolicitudAmistad GetAmigo(int usuario1, int usuario2);
    }

    public class SolicitudAmistadService : ISolicitudAmistadService
    {
        private readonly EchonyDbContext _context;
        public SolicitudAmistadService(EchonyDbContext context)
        {
            _context = context;
        }

        public List<SolicitudAmistad> GetNotificacionesAmistad(Receptor r)
        {
            List<SolicitudAmistad> lista = new List<SolicitudAmistad>();
            
                try
                {
                   
                    lista = _context.SolicitudAmistad.Where(x => x.Receptor.UsuarioId == r.Id && x.Estado == 0).Include(x => x.Emisor.Usuario).Include(x => x.Emisor.Usuario.Foto)
                        .Include(x => x.Receptor.Usuario).Include(x => x.Receptor.Usuario.Foto).ToList();
                }
                catch (Exception e)
                {

                    Console.Write(e);
                }
            
            return lista;
        }
        public List<SolicitudAmistad> GetAmigos(Usuario r)
        {
            List<SolicitudAmistad> lista = new List<SolicitudAmistad>();
           
                try
                {
                    //lista = db.SolicitudAmistad.Where(x => x.ReceptorId == a.ReceptorId).ToList();
                    lista = _context.SolicitudAmistad.Where(x => x.Receptor.UsuarioId == r.Id && x.Estado == 1).Include(x => x.Emisor.Usuario).Include(x => x.Emisor.Usuario.Foto)
                        .Include(x => x.Receptor.Usuario).Include(x => x.Receptor.Usuario.Foto).ToList();
                }
                catch (Exception e)
                {

                    Console.Write(e);
                }
            
            return lista;
        }

        public List<SolicitudAmistad> GetAmigos(Receptor r)
        {
            List<SolicitudAmistad> lista = new List<SolicitudAmistad>();          
            try
            {                   
                lista = _context.SolicitudAmistad.Where(x => x.Receptor.UsuarioId == r.UsuarioId || x.Emisor.UsuarioId == r.UsuarioId && x.Estado == 1)
                    .Include(x => x.Emisor.Usuario).Include(x => x.Emisor.Usuario.Foto)
                    .Include(x => x.Receptor.Usuario).Include(x => x.Receptor.Usuario.Foto).ToList();
            }
            catch (Exception e)
            {

                Console.Write(e);
            }
            
            return lista;
        }

        public void AceptarSolicitud(SolicitudAmistad s)
        {          
                try
                {
                    s.Estado = 1;
                    _context.SolicitudAmistad.Attach(s);
                    var entrada = _context.Entry(s);
                    entrada.Property(e => e.Estado).IsModified = true;
                    _context.SaveChanges();
                }
                catch (Exception e)
                {
                    e.ToString();
                }            
        }

        public SolicitudAmistad GetAmigo(int usuario1, int usuario2)
        {
            SolicitudAmistad a = new SolicitudAmistad();           
                try
                {
                    a = _context.SolicitudAmistad.Where(x => x.Receptor.UsuarioId == usuario1 && x.Emisor.UsuarioId == usuario2 || x.Receptor.UsuarioId == usuario2 && x.Emisor.UsuarioId == usuario1).FirstOrDefault();
                    // a = db.Amistad.Where(x => x.Receptor == usuario1 && x.Emisor == usuario2 || x.Emisor == usuario1 && x.Receptor == usuario2).FirstOrDefault();
                    //a = db.SolicitudAmistad.Where(x => x.ReceptorId == usuario1 && x.EmisorId == usuario2 || x.EmisorId == usuario1 && x.ReceptorId == usuario2).FirstOrDefault();
                    if (a == null)
                    {
                        a = new SolicitudAmistad { Estado = 5 };
                    }
                }
                catch (Exception)
                {

                    throw;
                }       
            return a;
        }
    }
}
