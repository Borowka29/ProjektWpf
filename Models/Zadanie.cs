using ListaZadan.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Text;

namespace ListaZadan.Models
{
    [DataContract]
    public class Zadanie
    {
        [Key]
        [DataMember]
        public int IdZadanie { get; set; }
        [DataMember]
        public string Tresc { get; set; }
        [DataMember]
        public double prorytet { get; set; }
        [DataMember]
        public DateTime rozpoczecie { get; set; }
        [DataMember]
        public DateTime zakonczenie { get; set; }
        public virtual ICollection<Kategora_Zadanie> Kategora_Zadanie { get; set; } //= new ObservableCollection<Kategora_Zadanie>();
        public virtual ICollection<Podzadania> Podzadania { get; set; } //= new ObservableCollection<Podzadania>();
    }
}
