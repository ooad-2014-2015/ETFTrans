using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ETFTrans.Model;
using System.Windows;
using System.Threading;

namespace ETFTrans.DataAcces
{
    public class BazaFunkcije
    {
        public static void registrujLogInUposlenika(string UserName)
        {
            using (ETFTransBaza db = new ETFTransBaza())
            {

                    Log noviLog = new Log() { datum = DateTime.Now.Date.Date, tip = "Prijava", vrijeme = DateTime.Now.ToShortTimeString() };

                    db.uposlenici.First(i => i.userName == UserName).logoviUposlenika.Add(noviLog);
                    db.SaveChanges();          
            }
           
        }
        public static void upisiAutobusUBazu(Autobus x)
        {
            try
            {

                using (ETFTransBaza db = new ETFTransBaza())
                {
                   // MessageBox.Show(x.brojRaspolozivihMjesta.ToString() + x.datumProizvodnje + x.datumRegistracije + x.marka);
                   db.Autobusi.Add(x);
                   db.SaveChanges();
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
        }
        public static List<Autobus> GetAutobuse()
        {
            using(ETFTransBaza db = new ETFTransBaza())
            {
                return db.Autobusi.Include("linija").ToList<Autobus>();
            }
        }
        public static List<Autobus> GetAutobusiSaVozacem()
        {
            using(ETFTransBaza db = new ETFTransBaza())
            {
                return db.Autobusi.Include("Vozac").Distinct().ToList<Autobus>();
            }
        }
        public static void izmjeniAutobus(Autobus x)
        {
            using(ETFTransBaza db = new ETFTransBaza())
            {
                        db.Autobusi.Find(x.AutobusID).brojRaspolozivihMjesta = x.brojRaspolozivihMjesta;
                        db.Autobusi.Find(x.AutobusID).datumProizvodnje = x.datumProizvodnje;
                        db.Autobusi.Find(x.AutobusID).datumRegistracije = x.datumRegistracije;
                        db.Autobusi.Find(x.AutobusID).marka = x.marka;
                        db.Autobusi.Find(x.AutobusID).registracija = x.registracija;
                        db.SaveChanges();
            }
        }

        public static void obrisiAutobus(Autobus x)
        {
            try
            {
                using (ETFTransBaza db = new ETFTransBaza())
                {
                    Autobus a = db.Autobusi.First(i => i.AutobusID == x.AutobusID);
                    db.Autobusi.Remove(a);
                    db.SaveChanges();
                }
            }
            catch(Exception e)
            {
                MessageBox.Show(e.ToString());
            }
        }

        public static List<Stanica> dajStanice()
        {
            try
            {
                using (ETFTransBaza db = new ETFTransBaza())
                {
                    return db.stanice.GroupBy(x => x.nazivGrada).Select(x => x.FirstOrDefault()).ToList<Stanica>();
                }
            }
            catch(Exception e)
            {
                MessageBox.Show(e.ToString());
                
            }
            return new List<Stanica>();
        }

        internal static void UpisiStanicuUBazu(Stanica stanica)
        {
            if (stanica == null) return;
            using(ETFTransBaza db = new ETFTransBaza())
            {
                db.stanice.Add(stanica);
                db.SaveChanges();
            }
        }

        internal static void dodajLinijuUBazu(Linija novaLinija, List<Autobus> autobusiZaLiniju, List<Stanica> stanice)
        {
            using(ETFTransBaza db = new ETFTransBaza())
            {
                foreach(Autobus x in db.Autobusi.ToList<Autobus>())
                {
                    for(int i=0; i<autobusiZaLiniju.Count; i++)
                    {
                        if(x.AutobusID == autobusiZaLiniju[i].AutobusID)
                        {
                            x.linija = novaLinija;
                        }
                    }
                }
                foreach(Stanica x in db.stanice.ToList<Stanica>())
                {
                    for (int i = 0; i < stanice.Count; i++)
                    {
                        if (x.StanicaId == stanice[i].StanicaId)
                        {
                            x.stanicaZaLinije.Add(novaLinija);
                        }
                    }

                }
                //db.Linije.Add(novaLinija);
                db.SaveChanges();
            }
        }

        internal static List<Autobus> GetAutobuseSaLinijama()
        {
            using (ETFTransBaza db = new ETFTransBaza())
            {
               return db.Autobusi.Include("Linija").ToList<Autobus>();    
            }
        }

        internal static List<Linija> dajLinije()
        {
            try
            {
                using(ETFTransBaza db = new ETFTransBaza())
                {
                    return db.Linije.Include("staniceLinije").ToList<Linija>();
                }

            }
            catch(Exception e)
            {
                MessageBox.Show(e.ToString());
                return new List<Linija>();
            }
        }

        internal static void izbrisiLiniju(Linija x)
        {
            try
            {
                using (ETFTransBaza db = new ETFTransBaza())
                {
                    
                    Linija a = db.Linije.First(i => i.LinijaID == x.LinijaID);
                    foreach(Autobus bus in db.Autobusi.Where(i => i.linija.LinijaID == a.LinijaID))
                    {
                        bus.linija = null;
                    }
                    foreach(dolazakOdlazakAutobusa d in db.otpremljeniAutobusi.Where(i=>i.linija.LinijaID == x.LinijaID))
                    {
                        d.linija = null;
                    }
                    foreach(Karta k in db.Karte.Where(i=>i.kartaZaLiniju.LinijaID == x.LinijaID))
                    {
                        k.kartaZaLiniju = null;
                    }
                    List<DatumPolaskaLinije> datumi = db.Linije.First(i=>i.LinijaID == a.LinijaID).datumiPolaskaLinije;
                    if(datumi != null)
                    db.datumiPolaskaLinija.RemoveRange(datumi);
                    DaniVoznjeLinije dani = a.daniVoznje;
                    db.daniVoznjeZaLinije.Remove(db.daniVoznjeZaLinije.First(i => i.DaniVoznjeLinijeId == dani.DaniVoznjeLinijeId));
                    db.Linije.Remove(a);
                    db.SaveChanges();
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
        }

        internal static void spremiIzmjenuLinijeZaStanice(Linija x, List<Stanica> izbrisaneStanice, List<Stanica> dodaneStanice)
        {
            try
            {
                using (ETFTransBaza db = new ETFTransBaza())
                {
                   
                    db.Linije.First(i => i.LinijaID == x.LinijaID).odredisteLinije = x.odredisteLinije;
                    db.Linije.First(i => i.LinijaID == x.LinijaID).cijenaZaGlavnoOdrediste = x.cijenaZaGlavnoOdrediste;
                    Linija temp;
                    if(izbrisaneStanice.Count > 0)
                    foreach(Stanica st in db.stanice.ToList<Stanica>())
                    {
                        foreach(Stanica izs in izbrisaneStanice)
                        {
                            if(st.StanicaId == izs.StanicaId)
                            {
                                for (int i = 0; i < st.stanicaZaLinije.Count; i++)
                                {
                                    if (st.stanicaZaLinije[i].LinijaID == x.LinijaID)
                                    {
                                        temp = st.stanicaZaLinije[i];
                                        st.stanicaZaLinije.Remove(temp);
                                    }
                                }
                                        
                            }
                        }
                    }
                    if(dodaneStanice.Count > 0)
                    foreach (Stanica st in db.stanice.ToList<Stanica>())
                    {
                        foreach (Stanica izs in dodaneStanice)
                        {
                            if (st.StanicaId == izs.StanicaId)
                            {    
                                        st.stanicaZaLinije.Add(db.Linije.First(i=>i.LinijaID == x.LinijaID));
                            }
                        }
                    }

                    db.SaveChanges();
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
        }
        internal static void spremiIzmjenuStanice(Stanica a)
        {
            try
            {
                using(ETFTransBaza db = new ETFTransBaza())
                {
                    db.stanice.First(x => x.StanicaId == a.StanicaId).cijenaVoznje = a.cijenaVoznje;
                    db.stanice.First(x => x.StanicaId == a.StanicaId).nazivGrada = a.nazivGrada;
                    db.SaveChanges();
                }
            }
            catch(Exception e)
            {
                MessageBox.Show(e.ToString());
            }
        }

        internal static void obrisiStanicu(Stanica x)
        {
            try
            {
                using (ETFTransBaza db = new ETFTransBaza())
                {
                    Stanica a = db.stanice.First(i => i.StanicaId == x.StanicaId);
                    db.stanice.Remove(a);
                    db.SaveChanges();
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
        }

        internal static void UpdateOdredistaSvihLinijaKojeSuSadrzavaleStanicu(List<Linija> listaLinija)
        {
            try
            {
                using (ETFTransBaza db = new ETFTransBaza())
                {
                    foreach(Linija x in listaLinija)
                    {
                        db.Linije.First(a => a.LinijaID == x.LinijaID).staniceLinije = sortirajStanice(db.Linije.First(a => a.LinijaID == x.LinijaID).staniceLinije);
                        db.Linije.First(a => a.LinijaID == x.LinijaID).odredisteLinije = db.Linije.First(a => a.LinijaID == x.LinijaID).staniceLinije[db.Linije.First(a => a.LinijaID == x.LinijaID).staniceLinije.Count - 1].nazivGrada;
                        db.Linije.First(a => a.LinijaID == x.LinijaID).cijenaZaGlavnoOdrediste = db.Linije.First(a => a.LinijaID == x.LinijaID).staniceLinije[db.Linije.First(a => a.LinijaID == x.LinijaID).staniceLinije.Count - 1].cijenaVoznje;
                        db.SaveChanges();
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
        }
        private static List<Stanica> sortirajStanice(List<Stanica> stanice)
        {
            Stanica temp;
            for (int i = 0; i < stanice.Count - 1; i++)
                for (int j = i + 1; j < stanice.Count; j++)
                {
                    if (stanice[i].cijenaVoznje > stanice[j].cijenaVoznje)
                    {
                        temp = stanice[i];
                        stanice[i] = stanice[j];
                        stanice[j] = temp;
                    }
                }
            return stanice;
        }

        internal static List<Linija> dajLinijeZaStanicu(Stanica stanica)
        {
            try
            {
                using (ETFTransBaza db = new ETFTransBaza())
                {
                    return db.stanice.First(x => x.StanicaId == stanica.StanicaId).stanicaZaLinije.ToList<Linija>();
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
                return new List<Linija>();
            }
        }

        internal static void upisiUposlenikaUbazu(Uposlenik noviUposlenik)
        {
            
            try
            {
                using (ETFTransBaza db = new ETFTransBaza())
                {
                    db.uposlenici.Add(noviUposlenik);
                    db.SaveChanges();
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
        }

        internal static void ObrisiUposlenika(Uposlenik a)
        {
            Uposlenik x;
            try
            {
                using (ETFTransBaza db = new ETFTransBaza())
                {
                    Log b;
                    x = db.uposlenici.First(i => i.UposlenikId == a.UposlenikId);
                    foreach(Log z in db.logs.Where(y => y.uposlenik.UposlenikId == a.UposlenikId))
                    {
                        db.logs.Remove(z);
                    }
                    db.uposlenici.Remove(x);
                    db.SaveChanges();
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
        }

        internal static void izmjeniUposlenika(Uposlenik x)
        {
            try
            {
                using (ETFTransBaza db = new ETFTransBaza())
                {


                    db.uposlenici.First(i => i.UposlenikId == x.UposlenikId).ime = x.ime;
                    db.uposlenici.First(i => i.UposlenikId == x.UposlenikId).prezime = x.prezime;
                    db.uposlenici.First(i => i.UposlenikId == x.UposlenikId).ugovorDo = x.ugovorDo;
                    db.uposlenici.First(i => i.UposlenikId == x.UposlenikId).datumZaposlenja = x.datumZaposlenja;
                    db.uposlenici.First(i => i.UposlenikId == x.UposlenikId).plata = x.plata;
                    db.SaveChanges();
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
        }

        internal static List<Uposlenik> dajUposlenike()
        {
            try
            {
                using (ETFTransBaza db = new ETFTransBaza())
                {
                    return db.uposlenici.ToList<Uposlenik>();
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
                return new List<Uposlenik>();
            }
        }

        internal static List<Linija> dajLinijeZaOdrediste(Stanica odrediste)
        {
            try
            {
                using (ETFTransBaza db = new ETFTransBaza())
                {
                    List<Linija> linije = db.stanice.First(x => x.StanicaId == odrediste.StanicaId).stanicaZaLinije.ToList<Linija>();
                    foreach(Linija z in linije)
                    {
                        z.daniVoznje = db.daniVoznjeZaLinije.First(i=>i.DaniVoznjeLinijeId == z.LinijaID);
                        List<DatumPolaskaLinije> dani = db.datumiPolaskaLinija.Where(i => i.datumPolaskaZaLiniju.LinijaID == z.LinijaID).ToList<DatumPolaskaLinije>();
                        
                        z.datumiPolaskaLinije = new List<DatumPolaskaLinije>();
                        
                        foreach (DatumPolaskaLinije d in dani)
                            z.datumiPolaskaLinije.Add(d);
                        if (z.datumiPolaskaLinije.Count == 0)
                            z.datumiPolaskaLinije.Add(new DatumPolaskaLinije());
                    } 
                    return linije;
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
                return new List<Linija>();
            }
        }
        internal static Korisnik dajKorisnika(string IDKorisnika)
        {
            try
            {
                using (ETFTransBaza db = new ETFTransBaza())
                {
                    return db.korisnici.First(i => i.userId == IDKorisnika);
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Korisnik ne postoji!");
                return new Korisnik();
            }
        }

        internal static List<Korisnik> dajKorisnike()
        {
            try
            {
                using (ETFTransBaza db = new ETFTransBaza())
                {
                    return db.korisnici.Include("karteKorisnika").ToList<Korisnik>();
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
                return new List<Korisnik>();
            }
        }

        internal static void spremiKartuZaLinijuIKorisnika(Karta novaKarta, Linija p, Korisnik korisnik, bool postojiDatum, int datumID)
        {
            try
            {
                using (ETFTransBaza db = new ETFTransBaza())
                {
                    db.Linije.First(i => i.LinijaID == p.LinijaID).karteZaLiniju.Add(novaKarta);

                    if(postojiDatum)
                    {
                        db.datumiPolaskaLinija.First(x => x.DatumPolaskaLinijeId == datumID).brojSlobodnihMjesta--;
                    }
                    else
                    {
                        DateTime datumPolaska1 = DateTime.Parse(novaKarta.kartaZaDatum);
                        DatumPolaskaLinije noviDatumPolaska = new DatumPolaskaLinije()
                        {
                            datumPolaska = datumPolaska1.Date,
                            brojSlobodnihMjesta = p.brojRaspolozivihMjesta - 1
                        };
                        db.Linije.First(i => i.LinijaID == p.LinijaID).datumiPolaskaLinije.Add(noviDatumPolaska);
                    }
                    
                    if(korisnik != null)
                    {
                        db.korisnici.First(x => x.KorisnikId == korisnik.KorisnikId).karteKorisnika.Add(novaKarta);
                    }
                    db.SaveChanges();
                }
            }
            catch (Exception e)
            {         
                MessageBox.Show(e.ToString());
            }
        }

        internal static void upisiKorisnikaUBazu(Korisnik korisnik)
        {
            try
            {
                using (ETFTransBaza db = new ETFTransBaza())
                {
                    db.korisnici.Add(korisnik);
                    db.SaveChanges();
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
           
            }
        }

        internal static List<RezervisanaKarta> dajRezervisaneKarte()
        {
            try
            {
                using (ETFTransBaza db = new ETFTransBaza())
                {
                    List<RezervisanaKarta> lista = db.rezervisaneKarte.Include("korisnik").Include("kartaZaLiniju").ToList<RezervisanaKarta>();
                    return lista;
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
                return new List<RezervisanaKarta>();

            }
        }

        internal static void naplatiRezervisanuKartu(ProdanaKarta novaKarta, Karta x)
        {
            
                using (ETFTransBaza db = new ETFTransBaza())
                {
                    
                    db.Linije.First(i => i.LinijaID == x.kartaZaLiniju.LinijaID).karteZaLiniju.Add(novaKarta);

                   /* if(x.korisnik != null)
                    db.korisnici.First(i => i.KorisnikId == x.korisnik.KorisnikId).karteKorisnika.Add(novaKarta);
                    */
                    bool nasao=false;
                    foreach (Korisnik k in db.korisnici.Include("karteKorisnika").ToList<Korisnik>())
                    {
                        if(k.karteKorisnika != null)
                        foreach (Karta karta in k.karteKorisnika)
                        {
                            if (x.KartaId == karta.KartaId)
                            {
                                k.karteKorisnika.Add(novaKarta);
                                nasao = true;
                                break;
                            }

                        }
                        if (nasao) break;
                    }



                    db.rezervisaneKarte.Remove(db.rezervisaneKarte.Where(i => i.KartaId == x.KartaId).FirstOrDefault());
                    db.SaveChanges();
                    
                }
          
        }



        internal static void spremiOtpremu(dolazakOdlazakAutobusa novaOtprema, Autobus bus, Linija linija, Otpremnik otpremnik, bool domaci)
        {
            try
            {
                using (ETFTransBaza db = new ETFTransBaza())
                {
                    if (domaci)
                    {
                        db.otpremljeniAutobusi.Add(novaOtprema);
                        db.SaveChanges();
                        db.otpremljeniAutobusi.First(i => i.dolazakOdlazakAutobusaId == novaOtprema.dolazakOdlazakAutobusaId).autobus = db.Autobusi.First(j => j.AutobusID == bus.AutobusID);
                       if(linija != null)
                        db.otpremljeniAutobusi.First(i => i.dolazakOdlazakAutobusaId == novaOtprema.dolazakOdlazakAutobusaId).linija = db.Linije.First(j => j.LinijaID == linija.LinijaID);
                    }
                    else
                        db.otpremljeniAutobusi.Add(novaOtprema);
                    db.SaveChanges();
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());

            }
        }

        internal static void registruLogOutUposlenika(string UserName)
        {
            using (ETFTransBaza db = new ETFTransBaza())
            {

                Log noviLog = new Log() { datum = DateTime.Now.Date.Date, tip = "Odjava", vrijeme = DateTime.Now.ToShortTimeString() };

                db.uposlenici.First(i => i.userName == UserName).logoviUposlenika.Add(noviLog);
                db.SaveChanges();

            }
        }

        internal static Uposlenik dajUposlenika(string UserName, string Password)
        {
            try
            {
                using (ETFTransBaza db = new ETFTransBaza())
                {
                    return db.uposlenici.First(i => i.userName == UserName && i.password == Password);
                }
            }
            catch (Exception e)
            {     
                return null;
            }
        }

        internal static List<Log> generisiIzvjestajIzmeduDatuma(DateTime pocetniDatum, DateTime krajnjiDatum)
        {
            try
            {
                using (ETFTransBaza db = new ETFTransBaza())
                {
                    List<Log> logovi = db.logs.Include("uposlenik").OrderBy(x => x.uposlenik.UposlenikId).ToList<Log>();
                    List<Log> zaVratit = new List<Log>();
                    foreach(Log x in logovi)
                    {
                        if (x.datum >= pocetniDatum && x.datum <= krajnjiDatum)
                            zaVratit.Add(x);
                    }
                    return zaVratit;
                    
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
                return null;
            }
        }

        internal static List<Log> generisiIzvjestajNaDatum(DateTime SelectedDatum)
        {
            try
            {
                using (ETFTransBaza db = new ETFTransBaza())
                {
                    List<Log> logovi = db.logs.Include("uposlenik").OrderBy(x => x.uposlenik.UposlenikId).ToList<Log>();
                    List<Log> zaVratit = new List<Log>();
                    foreach (Log x in logovi)
                    {
                        if (x.datum.Date == SelectedDatum.Date)
                            zaVratit.Add(x);
                    }
                    return zaVratit;                    
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
                return null;
            }
        }

     
    }
}
