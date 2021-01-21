using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Text;

namespace ListaZadan.Models
{
    [DataContract]
    public class Kategoria
    {
        [Key]
        [DataMember]
        public int IdKategoria { get; set; }
        [DataMember]
        public string Typ { get; set; }
        public virtual ICollection<Kategora_Zadanie> Kategora_Zadanie { get; set; }
    }
}
