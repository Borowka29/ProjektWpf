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
        public virtual Zadanie Zadanie { get; set; }
        [DataMember]
        public virtual Kategoria Kategoria { get; set; }
    }
}
