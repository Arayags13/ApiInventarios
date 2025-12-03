using System;
using System.Collections.Generic;
using ApiInventarios.DLL.Entidades;
using Microsoft.EntityFrameworkCore;

namespace ApiInventarios.DLL;

public partial class Caso2Context : DbContext
{
    public Caso2Context()
    {
    }

    public Caso2Context(DbContextOptions<Caso2Context> options)
        : base(options)
    {
    }

    public virtual DbSet<Producto> Productos { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlite("Data Source=C:\\dev\\ApiInventarios\\ApiInventarios.DLL\\Caso2.db");

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
