using Microsoft.EntityFrameworkCore;
using Model.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class EchonyDbContext : DbContext
    {

        public EchonyDbContext(DbContextOptions<EchonyDbContext> options)
            :base(options)
        {

        }

        public DbSet<Usuario> Usuario { get; set; }
        public DbSet<Publicaciones> Publicaciones { get; set; }
        public DbSet<Comentarios> Comentarios { get; set; }
        public DbSet<SolicitudAmistad> SolicitudAmistad { get; set; }
        public DbSet<Detalles> Detalles { get; set; }
        public DbSet<Emisor> Emisor { get; set; }
        public DbSet<Receptor> Receptor { get; set; }
        public DbSet<Amigos> Amigos { get; set; }
        public DbSet<Likes> Likes { get; set; }
        public DbSet<CommentReply> CommentReply { get; set; }

    }
}
