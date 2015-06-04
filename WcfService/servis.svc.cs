using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace WcfService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "servis" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select servis.svc or servis.svc.cs at the Solution Explorer and start debugging.
    public class servis : Iservis
    {
        List<tipPretraga> listaRezultata = new List<tipPretraga>();
        DataClasses1DataContext db { get; set; }



        public servis()
        {
            this.db = new DataClasses1DataContext();
 
            List<Linija> listaLinija = db.Linijas.ToList();
            List<DaniVoznjeLinije> listaDanaVoznje = db.DaniVoznjeLinijes.ToList();
            List<DatumPolaskaLinije> listaDatumaPolaska = db.DatumPolaskaLinijes.ToList();
            List<Stanica> listaStanica = db.Stanicas.ToList();
            List<StanicaLinija> listaVezeStanicaILinija = db.StanicaLinijas.ToList();
 
            foreach (Linija l in listaLinija)
            {
                tipPretraga t = new tipPretraga();
                t.Polaziste = "Sarajevo";
                t.Cijena = l.cijenaZaGlavnoOdrediste;
                foreach (StanicaLinija sl in listaVezeStanicaILinija)
                {
                    if (sl.Linija_LinijaID == l.LinijaID)
                        t.ListaStanica(sl.Stanica.nazivGrada);
                }
                t.ListaStanica(l.odredisteLinije);
                t.Odrediste = l.odredisteLinije;
                t.VrijemePolaska = l.vrijemePolaska;
                t.VrijemeDolaska = l.vrijemeDolaska;
 
                foreach (DaniVoznjeLinije dvl in listaDanaVoznje)
                {
                    if (dvl.DaniVoznjeLinijeId == l.daniVoznje_DaniVoznjeLinijeId)
                    {
                        if (dvl.pon == true) t.ListaDanaVoznje("Pon");
                        if (dvl.uto == true) t.ListaDanaVoznje("Uto");
                        if (dvl.sri == true) t.ListaDanaVoznje("Sri");
                        if (dvl.cet == true) t.ListaDanaVoznje("Cet");
                        if (dvl.pet == true) t.ListaDanaVoznje("Pet");
                        if (dvl.sub == true) t.ListaDanaVoznje("Sub");
                        if (dvl.ned == true) t.ListaDanaVoznje("Ned");
                    }
                }
                listaRezultata.Add(t);
            }
 
 
        }
 
 
 
        private bool daLiGaIma(List<string> l, string str)
        {
            foreach (string s in l)
            {
                if (s == str) return true;
            }
            return false;
        }
 
        
 
        public List<tipPretraga> pretrazi(string polaziste, string odrediste)
        {
 
            List<tipPretraga> tmp = new List<tipPretraga>();
            int velicinaListe = listaRezultata.Count;
            for (int i = 0; i < velicinaListe; i++)
            {
                if (polaziste != "Sarajevo")
                {
                    if (!daLiGaIma(listaRezultata[i].ListaStanica1, polaziste))
                        continue;
                }
 
                if (!daLiGaIma(listaRezultata[i].ListaStanica1, odrediste))
                    continue;
 
 
                tmp.Add(listaRezultata[i]);
            }
 
            return tmp;
 
        }
    }
}
