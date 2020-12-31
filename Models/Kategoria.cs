using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ListaZadan.Models
{
    public class Kategoria
    {
        [Key]
        public int IdKategoria { get; set; }
        public string Typ { get; set; }

        public virtual ICollection<Kategora_Zadanie> Kategora_Zadanie { get; private set; } = new ObservableCollection<Kategora_Zadanie>();
    }
}
