using ListaZadan.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ListaZadan.Models
{
    public class Zadanie
    {
        [Key]
        public int IdZadanie { get; set; }
        public string Tresc { get; set; }
        public double prorytet { get; set; }
        public DateTime rozpoczecie { get; set; }
        public DateTime zakonczenie { get; set; }
        public virtual ICollection<Kategora_Zadanie> Kategora_Zadanie { get; private set; } = new ObservableCollection<Kategora_Zadanie>();
        public virtual ICollection<Podzadania> Podzadania { get; private set; } = new ObservableCollection<Podzadania>();

    }
}
