﻿using Microsoft.EntityFrameworkCore;
using Odontología.DB.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Odontología.DB.Data
{
    public class Context : DbContext
    {
        public DbSet<Paciente> Pacientes { get; set; }
        public DbSet<Pago> Pagos { get; set; }
        public DbSet<Presupuesto> Presupuestos { get; set; }
        public DbSet<TipoTratamiento> TipoTratamientos { get; set; }
        public DbSet<TratamientoOd> TratamientosOd { get; set; }

        public Context(DbContextOptions options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var cascadeFKs = modelBuilder.Model.GetEntityTypes()
                                     .SelectMany(t => t.GetForeignKeys())
                                     .Where(fk => !fk.IsOwnership &&
                                     fk.DeleteBehavior == DeleteBehavior.Cascade);

            foreach (var fk in cascadeFKs)
            {
                fk.DeleteBehavior = DeleteBehavior.Restrict;
            }
            modelBuilder.Entity<Pago>()
                .HasOne(pago => pago.Presupuesto)
                 .WithMany(presupuestos => presupuestos.Pagos);
            modelBuilder.Entity<Presupuesto>()
                    .HasOne(ultimoPago => ultimoPago.UltimoPago);
        }
    }
}
