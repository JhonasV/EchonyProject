﻿// <auto-generated />
using EchonyCore.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using System;

namespace EchonyCore.Migrations
{
    [DbContext(typeof(EchonyEntityContext))]
    [Migration("20180403035752_amigos")]
    partial class amigos
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.2-rtm-10011")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("EchonyCore.Models.Amigos", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("EmisorId");

                    b.Property<DateTime>("Fecha");

                    b.Property<int?>("ReceptorId");

                    b.HasKey("Id");

                    b.HasIndex("EmisorId");

                    b.HasIndex("ReceptorId");

                    b.ToTable("Amigos");
                });

            modelBuilder.Entity("EchonyCore.Models.Comentarios", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Contenido_comentario");

                    b.Property<DateTime>("Fecha_Publicacion");

                    b.Property<string>("Foto");

                    b.Property<int>("PublicacionesId");

                    b.Property<int?>("UsuarioId");

                    b.HasKey("Id");

                    b.HasIndex("PublicacionesId");

                    b.HasIndex("UsuarioId");

                    b.ToTable("Comentarios");
                });

            modelBuilder.Entity("EchonyCore.Models.Detalles", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Descripcion");

                    b.Property<DateTime>("Fecha_Nacimiento");

                    b.Property<string>("Pais");

                    b.Property<string>("Sexo");

                    b.HasKey("Id");

                    b.ToTable("Detalles");
                });

            modelBuilder.Entity("EchonyCore.Models.Emisor", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("SolicitudAmistadId");

                    b.Property<int>("UsuarioId");

                    b.HasKey("Id");

                    b.HasIndex("SolicitudAmistadId")
                        .IsUnique();

                    b.HasIndex("UsuarioId");

                    b.ToTable("Emisor");
                });

            modelBuilder.Entity("EchonyCore.Models.Foto", b =>
                {
                    b.Property<int>("Id");

                    b.Property<string>("RutaFoto");

                    b.HasKey("Id");

                    b.ToTable("Fotos");
                });

            modelBuilder.Entity("EchonyCore.Models.Publicaciones", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Contenido");

                    b.Property<DateTime>("Fecha");

                    b.Property<string>("Foto");

                    b.Property<int>("UsuarioId");

                    b.HasKey("Id");

                    b.HasIndex("UsuarioId");

                    b.ToTable("Publicaciones");
                });

            modelBuilder.Entity("EchonyCore.Models.Receptor", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("SolicitudAmistadId");

                    b.Property<int>("UsuarioId");

                    b.HasKey("Id");

                    b.HasIndex("SolicitudAmistadId")
                        .IsUnique();

                    b.HasIndex("UsuarioId");

                    b.ToTable("Receptor");
                });

            modelBuilder.Entity("EchonyCore.Models.SolicitudAmistad", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("Estado");

                    b.Property<DateTime>("Fecha");

                    b.HasKey("Id");

                    b.ToTable("SolicitudAmistad");
                });

            modelBuilder.Entity("EchonyCore.Models.Usuario", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Apellido")
                        .IsRequired()
                        .HasMaxLength(30);

                    b.Property<string>("Clave")
                        .IsRequired()
                        .HasMaxLength(30);

                    b.Property<string>("Codigo");

                    b.Property<string>("ConfirmacionClave")
                        .IsRequired();

                    b.Property<int?>("DetallesId");

                    b.Property<string>("Email")
                        .IsRequired();

                    b.Property<string>("Estado");

                    b.Property<string>("Mensaje");

                    b.Property<string>("NickName")
                        .IsRequired()
                        .HasMaxLength(30);

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasMaxLength(30);

                    b.Property<int>("Privada");

                    b.HasKey("Id");

                    b.HasIndex("DetallesId");

                    b.ToTable("Usuario");
                });

            modelBuilder.Entity("EchonyCore.Models.Amigos", b =>
                {
                    b.HasOne("EchonyCore.Models.Emisor", "Emisor")
                        .WithMany()
                        .HasForeignKey("EmisorId");

                    b.HasOne("EchonyCore.Models.Receptor", "Receptor")
                        .WithMany()
                        .HasForeignKey("ReceptorId");
                });

            modelBuilder.Entity("EchonyCore.Models.Comentarios", b =>
                {
                    b.HasOne("EchonyCore.Models.Publicaciones")
                        .WithMany("Comentarios")
                        .HasForeignKey("PublicacionesId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("EchonyCore.Models.Usuario", "Usuario")
                        .WithMany("Comentarios")
                        .HasForeignKey("UsuarioId");
                });

            modelBuilder.Entity("EchonyCore.Models.Emisor", b =>
                {
                    b.HasOne("EchonyCore.Models.SolicitudAmistad")
                        .WithOne("Emisor")
                        .HasForeignKey("EchonyCore.Models.Emisor", "SolicitudAmistadId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("EchonyCore.Models.Usuario", "Usuario")
                        .WithMany()
                        .HasForeignKey("UsuarioId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("EchonyCore.Models.Foto", b =>
                {
                    b.HasOne("EchonyCore.Models.Usuario", "Usuario")
                        .WithOne("Foto")
                        .HasForeignKey("EchonyCore.Models.Foto", "Id")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("EchonyCore.Models.Publicaciones", b =>
                {
                    b.HasOne("EchonyCore.Models.Usuario", "Usuario")
                        .WithMany("Publicaciones")
                        .HasForeignKey("UsuarioId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("EchonyCore.Models.Receptor", b =>
                {
                    b.HasOne("EchonyCore.Models.SolicitudAmistad")
                        .WithOne("Receptor")
                        .HasForeignKey("EchonyCore.Models.Receptor", "SolicitudAmistadId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("EchonyCore.Models.Usuario", "Usuario")
                        .WithMany()
                        .HasForeignKey("UsuarioId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("EchonyCore.Models.Usuario", b =>
                {
                    b.HasOne("EchonyCore.Models.Detalles", "Detalles")
                        .WithMany()
                        .HasForeignKey("DetallesId");
                });
#pragma warning restore 612, 618
        }
    }
}
