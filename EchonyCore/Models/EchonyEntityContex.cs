﻿using EchonyCore.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

using System.Linq;
using System.Web;

namespace EchonyCore.Models
{
    public class EchonyEntityContext : DbContext
    {
      /* public EchonyEntityContext(DbContextOptions<EchonyEntityContext> options)
            : base(options)
        {

        }*/
        

     

        public DbSet<Usuario> Usuario { get; set; }
        public DbSet<Publicaciones> Publicaciones { get; set; }
        public DbSet<Comentarios> Comentarios { get; set; }
        public DbSet<Foto> Fotos { get; set; }
        public DbSet<SolicitudAmistad> SolicitudAmistad { get; set; }
        public DbSet<Detalles> Detalles { get; set; }
        public DbSet<Emisor> Emisor { get; set; }
        public DbSet<Receptor> Receptor { get; set; }
        public DbSet<Amigos> Amigos { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            optionsBuilder.UseSqlServer("Data Source=JHONAS;Initial Catalog=EchonyCoreDB;Integrated Security=True;");
        }
    }
}