using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace ProyectoPrueba.Models
{
    public partial class ProyectoContext : DbContext
    {
        public ProyectoContext()
        {
        }

        public ProyectoContext(DbContextOptions<ProyectoContext> options)
            : base(options)
        {
        }

        public virtual DbSet<CategoriaEntradum> CategoriaEntrada { get; set; } 
        public virtual DbSet<Entradum> Entrada { get; set; } 
        public virtual DbSet<Evento> Eventos { get; set; } 
        public virtual DbSet<Usuario> Usuarios { get; set; } 

      

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CategoriaEntradum>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Nombre).HasMaxLength(50);
            });

            modelBuilder.Entity<Entradum>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CategoriaEntradaId).HasColumnName("CategoriaEntradaID");

                entity.Property(e => e.EventoId).HasColumnName("EventoID");

                entity.Property(e => e.UsuariosId).HasColumnName("UsuariosID");

                entity.HasOne(d => d.CategoriaEntrada)
                    .WithMany(p => p.Entrada)
                    .HasForeignKey(d => d.CategoriaEntradaId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Entrada__Categor__48CFD27E");

                entity.HasOne(d => d.Evento)
                    .WithMany(p => p.Entrada)
                    .HasForeignKey(d => d.EventoId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Entrada__EventoI__47DBAE45");

                entity.HasOne(d => d.Usuarios)
                    .WithMany(p => p.Entrada)
                    .HasForeignKey(d => d.UsuariosId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Entrada__Usuario__4AB81AF0");
            });

            modelBuilder.Entity<Evento>(entity =>
            {
                entity.ToTable("Evento");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Fecha).HasColumnType("datetime");

                entity.Property(e => e.Titulo).HasMaxLength(50);

                entity.Property(e => e.Ubicacion).HasMaxLength(50);
            });

            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.ToTable("Usuario");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Contraseña).HasMaxLength(50);

                entity.Property(e => e.CorreoElectronico)
                    .HasMaxLength(50)
                    .HasColumnName("Correo Electronico");

                entity.Property(e => e.Nombre).HasMaxLength(50);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
