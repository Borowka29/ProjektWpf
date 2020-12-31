using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ListaZadan.Models
{
    public class Podzadania
    {
        [Key]
        public int IdPodzadania { get; set; }
        public string opis { get; set; }
        public int któreNaLiscie { get; set; }
        public virtual Zadanie Zadanie { get; set; }
    }
}
