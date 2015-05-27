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
        public static void registrujLogInUposlenika(int id)
        {
            using (ETFTransBaza db = new ETFTransBaza())
            {
        
                Log noviLog = new Log() { datum = DateTime.Now.Date.Date, tip = "Prijava", vrijeme = DateTime.Now.ToShortTimeString() };
               
                foreach (Uposlenik x in db.uposlenici.ToList<Uposlenik>())
                {
                    if(x.UposlenikId == id)
                    {
                        x.logoviUposlenika.Add(noviLog);
                        db.SaveChanges();
                        break;
                    }
                }
                
            }
           
        }
        public static bool ValidirajLogInClanUprave(string userName, string password)
        {
            using (ETFTransBaza db = new ETFTransBaza())
            {
                try
                {
                   
                        List<ClanUprave> ClanoviUprave = db.clanoviUprave.ToList<ClanUprave>();
                        foreach (ClanUprave x in ClanoviUprave)
                        {
                            if (x.userName == userName && x.password == password)
                            {
                                registrujLogInUposlenika(x.UposlenikId);
                                return true;
                            }
                        }
                        return false;
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.ToString());
                    return false;
                }

            }
        }
        public static bool ValidirajLogInRadnikProdaja(string userName, string password)
        {
            using (ETFTransBaza db = new ETFTransBaza())
            {
                try
                {
                    List<RadnikNaSalteruProdaja> radnici = db.radniciNaSalteruProdaja.ToList<RadnikNaSalteruProdaja>();

                    foreach (RadnikNaSalteruProdaja x in radnici)
                    {
                        if (x.userName == userName && x.password == password)
                        {
                            registrujLogInUposlenika(x.UposlenikId);
                            return true;
                        }
                    }
                    return false;
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.ToString());
                    return false;
                }

            }
        }
        public static bool ValidirajLogInRadnikPretraga(string userName, string password)
        {
            using (ETFTransBaza db = new ETFTransBaza())
            {
                try
                {

                    List<RadnikNaSalteruPretraga> radnici = db.radniciNaSalteruPretraga.ToList<RadnikNaSalteruPretraga>();
                    foreach (RadnikNaSalteruPretraga x in radnici)
                    {
                        if (x.userName == userName && x.password == password)
                        {
                            registrujLogInUposlenika(x.UposlenikId);
                            return true;
                        }
                    }
                    return false;
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.ToString());
                    return false;
                }

            }
        }
        public static bool ValidirajLogInOtpremnik(string userName, string password)
        {
            using (ETFTransBaza db = new ETFTransBaza())
            {
                try
                {

                    List<Otpremnik> radnici = db.otpremnici.ToList<Otpremnik>();
                    foreach (Otpremnik x in radnici)
                    {
                        if (x.userName == userName && x.password == password)
                        {
                            registrujLogInUposlenika(x.UposlenikId);
                            return true;
                        }
                    }
                    return false;
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.ToString());
                    return false;
                }

            }
        }
        public static bool ValidirajLogInDirektor(string userName, string password)
        {
            using (ETFTransBaza db = new ETFTransBaza())
            {
                try
                {

                    List<Direktor> radnici = db.direktori.ToList<Direktor>();
                    foreach (Direktor x in radnici)
                    {
                        if (x.userName == userName && x.password == password)
                        {
                            registrujLogInUposlenika(x.UposlenikId);
                            return true;
                        }
                    }
                    return false;
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.ToString());
                    return false;
                }

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
                return db.Autobusi.ToList<Autobus>();
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
                    db.Autobusi.First(i => i.linija.LinijaID == a.LinijaID).linija = null;
                    DaniVoznjeLinije dani = a.daniVoznje;
                    DaniVoznjeLinije temp = db.daniVoznjeZaLinije.First(i => i.DaniVoznjeLinijeId == dani.DaniVoznjeLinijeId);
                    db.daniVoznjeZaLinije.Remove(temp);
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
                    x = db.uposlenici.First(i => i.UposlenikId == a.UposlenikId);
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
    }
}
