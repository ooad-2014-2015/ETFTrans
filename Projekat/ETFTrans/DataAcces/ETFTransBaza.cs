using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using ETFTrans.Model;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Windows;

namespace ETFTrans.DataAcces
{
    public class ETFTransBaza : DbContext
    {
        public ETFTransBaza() : base("ETFTransBaza") {}
        public DbSet<Autobus> Autobusi { get; set; }
        public DbSet<Linija> Linije { get; set; }
        public DbSet<Karta> Karte { get; set; }
        public DbSet<ProdanaKarta> prodaneKarte { get; set; }
        public DbSet<RezervisanaKarta> rezervisaneKarte { get; set; }
        public DbSet<Vozac> Vozaci { get; set; }
        public DbSet<RadnikNaSalteruPretraga> radniciNaSalteruPretraga { get; set; }
        public DbSet<RadnikNaSalteruProdaja> radniciNaSalteruProdaja { get; set; }
        public DbSet<Otpremnik> otpremnici { get; set; }
        public DbSet<Stanica> stanice { get; set; }
        public DbSet<ClanUprave> clanoviUprave { get; set; }

        public DbSet<Direktor> direktori { get; set; }
        public DbSet<DatumPolaskaLinije> datumiPolaskaLinija { get; set; }

        public DbSet<dolazakOdlazakAutobusa> otpremljeniAutobusi { get; set; }

        public DbSet<Log> logs { get; set; }

        public DbSet<Uposlenik> uposlenici { get; set; }

        public DbSet<DaniVoznjeLinije> daniVoznjeZaLinije { get; set; }
        public DbSet<Korisnik> korisnici { get; set; }

        protected override void OnModelCreating(DbModelBuilder model)
        {
            model.Conventions.Remove<PluralizingTableNameConvention>();
        }
        
    }
}
