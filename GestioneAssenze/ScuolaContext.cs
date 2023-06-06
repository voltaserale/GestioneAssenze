using System;
using System.Collections.Generic;
using GestioneAssenze.Models;
using Microsoft.EntityFrameworkCore;

namespace GestioneAssenze;

public partial class ScuolaContext : DbContext
{
    public ScuolaContext()
    {
    }

    public ScuolaContext(DbContextOptions<ScuolaContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Assenza> Assenzas { get; set; }

    public virtual DbSet<Classe> Classes { get; set; }

    public virtual DbSet<Docente> Docentes { get; set; }

    public virtual DbSet<Lezione> Leziones { get; set; }

    public virtual DbSet<Studente> Studentes { get; set; }

    public virtual DbSet<Svolge> Svolges { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseMySQL("server=localhost;database=scuola;uid=root;pwd=5SIA_2023");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Assenza>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("assenza");

            entity.HasIndex(e => e.Iddocente, "iddocente");

            entity.HasIndex(e => e.Matrstudente, "matrstudente");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Data)
                .HasColumnType("date")
                .HasColumnName("data");
            entity.Property(e => e.Durata)
                .HasMaxLength(100)
                .HasDefaultValueSql("'1'")
                .HasColumnName("durata");
            entity.Property(e => e.Iddocente).HasColumnName("iddocente");
            entity.Property(e => e.Matrstudente)
                .HasMaxLength(5)
                .IsFixedLength()
                .HasColumnName("matrstudente");
            entity.Property(e => e.Ora)
                .HasColumnType("time")
                .HasColumnName("ora");
            entity.Property(e => e.Tipo)
                .HasMaxLength(20)
                .HasColumnName("tipo");

            entity.HasOne(d => d.IddocenteNavigation).WithMany(p => p.Assenzas)
                .HasForeignKey(d => d.Iddocente)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("assenza_ibfk_2");

            entity.HasOne(d => d.MatrstudenteNavigation).WithMany(p => p.Assenzas)
                .HasForeignKey(d => d.Matrstudente)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("assenza_ibfk_1");
        });

        modelBuilder.Entity<Classe>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("classe");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Indirizzo)
                .HasMaxLength(100)
                .HasColumnName("indirizzo");
            entity.Property(e => e.Nome)
                .HasMaxLength(10)
                .HasColumnName("nome");
            entity.Property(e => e.Tipologia)
                .HasMaxLength(20)
                .HasColumnName("tipologia");
        });

        modelBuilder.Entity<Docente>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("docente");

            entity.HasIndex(e => e.Username, "username").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Cognome)
                .HasMaxLength(20)
                .HasColumnName("cognome");
            entity.Property(e => e.Nome)
                .HasMaxLength(10)
                .HasColumnName("nome");
            entity.Property(e => e.Password)
                .HasMaxLength(100)
                .HasColumnName("password");
            entity.Property(e => e.Username)
                .HasMaxLength(100)
                .HasColumnName("username");
        });

        modelBuilder.Entity<Lezione>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("lezione");

            entity.HasIndex(e => e.Idclasse, "idclasse");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Argomento)
                .HasMaxLength(100)
                .HasColumnName("argomento");
            entity.Property(e => e.Data)
                .HasColumnType("date")
                .HasColumnName("data");
            entity.Property(e => e.Idclasse).HasColumnName("idclasse");
            entity.Property(e => e.Materia)
                .HasMaxLength(20)
                .HasColumnName("materia");
            entity.Property(e => e.Ora)
                .HasColumnType("time")
                .HasColumnName("ora");

            entity.HasOne(d => d.IdclasseNavigation).WithMany(p => p.Leziones)
                .HasForeignKey(d => d.Idclasse)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("lezione_ibfk_1");
        });

        modelBuilder.Entity<Studente>(entity =>
        {
            entity.HasKey(e => e.Matricola).HasName("PRIMARY");

            entity.ToTable("studente");

            entity.HasIndex(e => e.Idclasse, "idclasse");

            entity.HasIndex(e => e.Username, "username").IsUnique();

            entity.Property(e => e.Matricola)
                .HasMaxLength(5)
                .IsFixedLength()
                .HasColumnName("matricola");
            entity.Property(e => e.Cognome)
                .HasMaxLength(20)
                .HasColumnName("cognome");
            entity.Property(e => e.Datanascita)
                .HasColumnType("date")
                .HasColumnName("datanascita");
            entity.Property(e => e.Idclasse).HasColumnName("idclasse");
            entity.Property(e => e.Nome)
                .HasMaxLength(10)
                .HasColumnName("nome");
            entity.Property(e => e.Password)
                .HasMaxLength(100)
                .HasColumnName("password");
            entity.Property(e => e.Username)
                .HasMaxLength(100)
                .HasColumnName("username");

            entity.HasOne(d => d.IdclasseNavigation).WithMany(p => p.Studentes)
                .HasForeignKey(d => d.Idclasse)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("studente_ibfk_1");
        });

        modelBuilder.Entity<Svolge>(entity =>
        {
            entity.HasKey(e => new { e.Iddocente, e.Idlezione }).HasName("PRIMARY");

            entity.ToTable("svolge");

            entity.HasIndex(e => e.Idlezione, "idlezione");

            entity.Property(e => e.Iddocente).HasColumnName("iddocente");
            entity.Property(e => e.Idlezione).HasColumnName("idlezione");
            entity.Property(e => e.Tipologia)
                .HasMaxLength(20)
                .HasColumnName("tipologia");

            entity.HasOne(d => d.IddocenteNavigation).WithMany(p => p.Svolges)
                .HasForeignKey(d => d.Iddocente)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("svolge_ibfk_1");

            entity.HasOne(d => d.IdlezioneNavigation).WithMany(p => p.Svolges)
                .HasForeignKey(d => d.Idlezione)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("svolge_ibfk_2");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
