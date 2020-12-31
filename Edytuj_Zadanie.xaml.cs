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
    /// Interaction logic for EditTask.xaml
    /// </summary>
    public partial class Edytuj_Zadanie : Window
    {
        ListaZadanContext db { get; set; }
        private Zadanie EdytowaneZadanie { get; set; }
        private Zadanie KopiaEdytowanegoZadania { get; set; }
        public Edytuj_Zadanie(ListaZadanContext db, Zadanie zadanie)
        {
            InitializeComponent();
            this.db = db;
            EdytowaneZadanie = zadanie;
            KopiaEdytowanegoZadania = zadanie;
            OdswiezBazeKategorii();
            ListaKrokow.ItemsSource = db.Podzadania.Include(i=>i.Zadanie).Where(k => k.Zadanie.IdZadanie == EdytowaneZadanie.IdZadanie).ToList();  
            TrescZadania.Text = EdytowaneZadanie.Tresc;
            Piorytet.Value = EdytowaneZadanie.prorytet;
            DataOd.SelectedDate = EdytowaneZadanie.rozpoczecie;
            DataDo.SelectedDate = EdytowaneZadanie.zakonczenie;

        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void OKButton_Click(object sender, RoutedEventArgs e)
        {
            if(TrescZadania.Text.Length!=0 )
            DialogResult = true;
        }

        private void WyborPrzedzialuCzasu(object sender, RoutedEventArgs e)
        {
            if (PrzedzialCzasu.IsChecked == true)
            {
                OdKiedy.Visibility = Visibility.Visible;

            }
            else
            {
                OdKiedy.Visibility = Visibility.Collapsed;
            }
        }

        private void WyborDatyUkonczenia(object sender, RoutedEventArgs e)
        {
            if (PrzedzialCzasu.IsChecked == true)
            {
                OdKiedy.Visibility = Visibility.Visible;

            }
            else
            {
                OdKiedy.Visibility = Visibility.Collapsed;
            }
        }

        private void CzyMoznaUsunacKategorie(object sender, SelectionChangedEventArgs e)
        {
            if (ListaObecnychKategorii.SelectedIndex >= 0)
                UsunKategorie.IsEnabled = true;
            else
                UsunKategorie.IsEnabled = false;
        }

        private void CyMoznaDodacKategorie(object sender, SelectionChangedEventArgs e)
        {
            if (ListaNiedodanychKategorii.SelectedIndex >= 0)
                DodajKategorie.IsEnabled = true;
            else
                DodajKategorie.IsEnabled = false;
        }

        private void CzyMoznaUsunacPodzadanie(object sender, SelectionChangedEventArgs e)
        {
            if (ListaKrokow.SelectedIndex >= 0)
                UsunPodzadanie.IsEnabled = true;
            else
               UsunPodzadanie.IsEnabled = false;
        }

        private void UsuwanieKategorii(object sender, RoutedEventArgs e)
        {
            foreach (Kategora_Zadanie zad in ListaObecnychKategorii.SelectedItems)
            {
                db.Kategoria_Zadanie.Remove(zad);
                db.SaveChanges();
            }
            OdswiezBazeKategorii();
        }
        private void DodawanieKategorii(object sender, RoutedEventArgs e)
        {
            foreach (Kategoria zad in ListaNiedodanychKategorii.SelectedItems)
            {
                Kategora_Zadanie NowaKategoria = new Kategora_Zadanie();
                NowaKategoria.Zadanie = EdytowaneZadanie;
                NowaKategoria.Kategoria = zad;
                db.Kategoria_Zadanie.Add(NowaKategoria);
                db.SaveChanges();
            }
            OdswiezBazeKategorii();
        }
        private void OdswiezBazeKategorii()
        {
            ListaObecnychKategorii.ItemsSource = db.Kategoria_Zadanie.Include(p => p.Zadanie).Include(p => p.Kategoria).Where(k => k.Zadanie.IdZadanie == EdytowaneZadanie.IdZadanie).ToList();
            ListaNiedodanychKategorii.ItemsSource = db.Kategorie.Include(o => o.Kategora_Zadanie).Where(p => !(p.Kategora_Zadanie.Any(k => k.Zadanie.IdZadanie == EdytowaneZadanie.IdZadanie && k.Kategoria.IdKategoria == p.IdKategoria))).ToList();
        }

        private void dodajPodzadanie(object sender, RoutedEventArgs e)
        {
            var NowePodzadanie = new Dodaj_Podzadanie(db, EdytowaneZadanie);
            NowePodzadanie.Owner = this;
            if(NowePodzadanie.ShowDialog()==true)
            {
                ListaKrokow.ItemsSource= db.Podzadania.Include(i => i.Zadanie).Where(k => k.Zadanie.IdZadanie == EdytowaneZadanie.IdZadanie).OrderBy(z=>z.któreNaLiscie).ToList();
            }
        }

        private void UsunPodzadania(object sender, RoutedEventArgs e)
        {
            foreach (Podzadania zad in ListaKrokow.SelectedItems)
            {
                db.Podzadania.Remove(zad);
                
                db.SaveChanges();
            }
            int ktory = 1;
            foreach (Podzadania podzadania in db.Podzadania.Include(i => i.Zadanie).Where(k => k.Zadanie.IdZadanie == EdytowaneZadanie.IdZadanie).OrderBy(z => z.któreNaLiscie).ToList())
            {
                podzadania.któreNaLiscie=ktory;
                ktory++;
                db.Attach(podzadania).State = EntityState.Modified;
                
            }db.SaveChanges();
            ListaKrokow.ItemsSource = db.Podzadania.Include(i => i.Zadanie).Where(k => k.Zadanie.IdZadanie == EdytowaneZadanie.IdZadanie).OrderBy(z => z.któreNaLiscie).ToList();
        }
    }
}
