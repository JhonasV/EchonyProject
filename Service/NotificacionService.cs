using Model;
using Model.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Service
{
    public interface INotificacionService
    {
        void NotificacionAmistad(SolicitudAmistad a, Emisor e, Receptor r);
        void EliminarNotificacionAmistad(SolicitudAmistad a);
    }

    public class NotificacionService : INotificacionService
    {
        private readonly EchonyDbContext _context;
        public NotificacionService(EchonyDbContext context)
        {
            _context = context;
        }

        public void NotificacionAmistad(SolicitudAmistad a, Emisor e, Receptor r)
        {
            try
            {
                a.Emisor = e;
                a.Receptor = r;
                _context.SolicitudAmistad.Add(a);
                _context.SaveChanges();
            }
            catch (Exception)
            {

                throw;
            }


        }

        public void EliminarNotificacionAmistad(SolicitudAmistad a)
        {
            try
            {
                SolicitudAmistad detalles = _context.SolicitudAmistad.Find(a.Id);
                _context.SolicitudAmistad.Remove(detalles);
                _context.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }

        }


    }
}
