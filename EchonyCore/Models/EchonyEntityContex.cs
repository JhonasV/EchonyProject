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
        public DbSet<Publicaciones> Publiaciones { get; set; }
        public DbSet<Comentarios> Comentarios { get; set; }
        public DbSet<Foto> Fotos { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            optionsBuilder.UseSqlServer("Data Source=JHONAS;Initial Catalog=EchonyCoreDB;Integrated Security=True;");
        }
    }
}