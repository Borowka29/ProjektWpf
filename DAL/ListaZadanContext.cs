using ListaZadan.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace ListaZadan.DAL
{
    public class ListaZadanContext:DbContext
    {
        public DbSet<Zadanie> Zadania { get; set; }
        public DbSet<Podzadania> Podzadania { get; set; }
        public DbSet<Kategoria> Kategorie { get; set; }
        public DbSet<Kategora_Zadanie> Kategoria_Zadanie { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source = (localdb)\MSSQLLocalDB; Initial Catalog = ListaZadan; Integrated Security = True; Connect Timeout = 30; Encrypt = False; TrustServerCertificate = False; ApplicationIntent = ReadWrite; MultiSubnetFailover = False");
            //optionsBuilder.UseLazyLoadingProxies();
            base.OnConfiguring(optionsBuilder);
        }
    }
}
