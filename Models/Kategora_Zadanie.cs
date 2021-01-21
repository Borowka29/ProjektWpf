using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Text;

namespace ListaZadan.Models
{
    [DataContract]
    public class Kategora_Zadanie
    {
        [Key]
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        [Required]
        public Zadanie Zadanie { get; set; }
        [DataMember]
        [Required]
        public Kategoria Kategoria { get; set; }
        public override string ToString()
        {
            return Kategoria.Typ;
        }
    }
}
