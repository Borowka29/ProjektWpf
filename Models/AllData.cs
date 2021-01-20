using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace ListaZadan.Models
{
    [DataContract]
    public class AllData
    {
        [DataMember]
        public List<Zadanie> ZadanieList { get; set; }
        [DataMember]
        public List<Kategoria> KategoriaList { get; set; }
        [DataMember]
        public List<Kategora_Zadanie> Kategoria_ZadanieList { get; set; }
        [DataMember]
        public List<Podzadania> PodzadanieList { get; set; }
    }
}
