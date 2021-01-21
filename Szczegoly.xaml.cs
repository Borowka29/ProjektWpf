using ListaZadan.DAL;
using ListaZadan.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ListaZadan
{
    /// <summary>
    /// Interaction logic for Szczegoly.xaml
    /// </summary>
    public partial class Szczegoly : Window
    {
        private ListaZadanContext db { get; set; }
        private Zadanie PodgladaneZadanie { get; set; }

        public Szczegoly(ListaZadanContext db, Zadanie zadanie)
        {
            InitializeComponent();
            this.db = db;
            PodgladaneZadanie = zadanie;
            OdswierzZadanie();
        }
        private void OdswierzZadanie()
        {
            ListaObecnychKategorii.ItemsSource = db.Kategoria_Zadanie.Include(p => p.Zadanie).Include(p => p.Kategoria).Where(k => k.Zadanie.IdZadanie == PodgladaneZadanie.IdZadanie).ToList();
            ListaKrokow.ItemsSource = db.Podzadania.Include(i => i.Zadanie).Where(k => k.Zadanie.IdZadanie == PodgladaneZadanie.IdZadanie).OrderBy(d => d.któreNaLiscie).ToList();
            TrescZadania.Text = PodgladaneZadanie.Tresc;
            Piorytet.Value = PodgladaneZadanie.prorytet;
            DataOd.Content = PodgladaneZadanie.rozpoczecie;
            DataDo.Content = PodgladaneZadanie.zakonczenie;
            if(PodgladaneZadanie.rozpoczecie == null)
                OdKiedy.Visibility = Visibility.Collapsed;
            else
                OdKiedy.Visibility = Visibility.Visible;

        }
        private void OKButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            var EdycjaZadania = new Edytuj_Zadanie(db, PodgladaneZadanie);
            EdycjaZadania.Owner = this;
            if(true==EdycjaZadania.ShowDialog())
            {
                OdswierzZadanie();
            }
        }
    }
}
