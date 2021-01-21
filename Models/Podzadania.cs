using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Text;

namespace ListaZadan.Models
{
    [DataContract]
    public class Podzadania
    {
        [Key]
        [DataMember]
        public int IdPodzadania { get; set; }
        [DataMember]
        public string opis { get; set; }
        [DataMember]
        public int któreNaLiscie { get; set; }
        [DataMember]
        [Required]
        public Zadanie Zadanie { get; set; }
    }
}
