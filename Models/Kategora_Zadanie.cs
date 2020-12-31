using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ListaZadan.Models
{
    public class Kategora_Zadanie
    {
        [Key]
        public int Id { get; set; }
        public virtual Zadanie Zadanie { get; set; }
        public virtual Kategoria Kategoria { get; set; }
    }
}
