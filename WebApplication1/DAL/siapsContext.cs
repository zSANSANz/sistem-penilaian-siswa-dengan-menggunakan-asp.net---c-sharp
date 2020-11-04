using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;
using WebApplication1.Models;

namespace WebApplication1.DAL
{
    public class siapsContext : DbContext
    {
        public DbSet<person> personCt { get; set; }
        public DbSet<perAdmin> perAdminCt { get; set; }
        public DbSet<perSiswa> perSiswaCt { get; set; }
        public DbSet<perGuru> perGuruCt { get; set; }
        public DbSet<perWaliKelas> perWaliKelasCt { get; set; }
        public DbSet<nilAkhir> nilAkhirCt { get; set; }
        public DbSet<nilSikapKI1KI2> nilSikapKI1KI2Ct { get; set; }
        public DbSet<nilPengetahuanKognitifKI3> nilPengetahuanKognitifKI3Ct { get; set; }
        public DbSet<nilKetrampilanPsikomotorikKI4> nilKetrampilanPsikomotorikKI4Ct { get; set; }
        public DbSet<nilUtsUas> nilUtsUasCt { get; set; }
        public DbSet<sysMapel> sysMapelCt { get; set; }
        public DbSet<sysKelas> sysKelasCt { get; set; }
        public DbSet<sysSekolah> sysSekolahCt { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}