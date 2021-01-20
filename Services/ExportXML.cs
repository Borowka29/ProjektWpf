using ListaZadan.DAL;
using ListaZadan.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Runtime.Serialization;
using System.Xml;

namespace ListaZadan.Services
{
    public class ExportXML
    {
        public static void Export(ListaZadanContext db, string location)
        {
            AllData data = new AllData();
            db.Kategorie.Load();
            data.KategoriaList = db.Kategorie.Local.ToList();

            db.Kategoria_Zadanie.Include(k => k.Kategoria).Include(k => k.Zadanie).Load();
            data.Kategoria_ZadanieList = db.Kategoria_Zadanie.Local.ToList();

            db.Podzadania.Include(p => p.Zadanie).Load();
            data.PodzadanieList = db.Podzadania.Local.ToList();

            db.Zadania.Include(z => z.Podzadania).Include(z => z.Kategora_Zadanie).Load();
            data.ZadanieList = db.Zadania.Local.ToList();

            var ds = new DataContractSerializer(typeof(AllData), new DataContractSerializerSettings() { PreserveObjectReferences = true });

            XmlWriterSettings settings = new XmlWriterSettings() { Indent = true, ConformanceLevel = ConformanceLevel.Auto };
            using (XmlWriter w = XmlWriter.Create(location, settings))
            {
                ds.WriteObject(w, data);
            }
        }
        public static void Import(ListaZadanContext db, string location)
        {
            AllData data;
            var ds = new DataContractSerializer(typeof(AllData), new DataContractSerializerSettings() { PreserveObjectReferences = true });

            XmlReaderSettings settings = new XmlReaderSettings() { ConformanceLevel = ConformanceLevel.Auto };
            using (XmlReader r = XmlReader.Create(location, settings))
            {
                data = ds.ReadObject(r, true) as AllData;
            }

            db.Podzadania.RemoveRange(db.Podzadania);
            db.Kategoria_Zadanie.RemoveRange(db.Kategoria_Zadanie);
            db.Zadania.RemoveRange(db.Zadania);
            db.Kategorie.RemoveRange(db.Kategorie);

            db.SaveChanges();

            foreach (var kategoria in data.KategoriaList)
            {
                kategoria.IdKategoria = 0;
                db.Kategorie.Add(kategoria);
            }
            foreach (var zadanie in data.ZadanieList)
            {
                zadanie.IdZadanie = 0;
                db.Zadania.Add(zadanie);
            }
            foreach (var podzadanie in data.PodzadanieList)
            {
                podzadanie.IdPodzadania = 0;
                db.Podzadania.Add(podzadanie);
            }
            foreach (var kategoria_zadanie in data.Kategoria_ZadanieList)
            {
                kategoria_zadanie.Id = 0;
                db.Kategoria_Zadanie.Add(kategoria_zadanie);
            }
            db.SaveChanges();
        }
    }
}
