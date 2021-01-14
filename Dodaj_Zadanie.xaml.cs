using ListaZadan.DAL;
using ListaZadan.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Interaction logic for Dodaj_Zadanie.xaml
    /// </summary>
    public partial class Dodaj_Zadanie : Window
    {
        ListaZadanContext db { get; set; }
        public Zadanie zadanie { get; set; }

        public Dodaj_Zadanie(ListaZadanContext db)
        {
            InitializeComponent();
            this.db = db;
            
            zadanie = new Zadanie();
            zadanie.Tresc = "";
            
            OdswiezBazeKategorii();
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            if(TrescZadania.Text.Length!=0 && DataDo.SelectedDate!=null)
            {
                if(PrzedzialCzasu.IsChecked == false)
                {

                }
                zadanie.Tresc = TrescZadania.Text.ToString(); ;
                //zadanie.rozpoczecie = (DateTime)DataOd.SelectedDate;
                zadanie.zakonczenie = (DateTime)DataDo.SelectedDate;
                zadanie.prorytet = Piorytet.Value;
                db.Zadania.Add(zadanie);
                //Zadanie ZmienianeZadanie = db.Zadania.FirstOrDefault(z => z == zadanie);

                db.Attach(zadanie).State = EntityState.Modified;
                db.SaveChanges();
                DialogResult = true;
                this.Close();
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
            foreach (Podzadania podzadania in db.Podzadania.Include(i => i.Zadanie).Where(k => k.Zadanie.IdZadanie == zadanie.IdZadanie).OrderBy(z => z.któreNaLiscie).ToList())
            {
                podzadania.któreNaLiscie = ktory;
                ktory++;
                db.Attach(podzadania).State = EntityState.Modified;

            }
            db.SaveChanges();
            ListaKrokow.ItemsSource = db.Podzadania.Include(i => i.Zadanie).Where(k => k.Zadanie.IdZadanie == zadanie.IdZadanie).OrderBy(z => z.któreNaLiscie).ToList();
        }

        private void dodajPodzadanie(object sender, RoutedEventArgs e)
        {
            var NowePodzadanie = new Dodaj_Podzadanie(db, zadanie);
            NowePodzadanie.Owner = this;
            if (NowePodzadanie.ShowDialog() == true)
            {
                ListaKrokow.ItemsSource = db.Podzadania.Include(i => i.Zadanie).Where(k => k.Zadanie.IdZadanie == zadanie.IdZadanie).OrderBy(z => z.któreNaLiscie).ToList();
            }
        }

        private void DodawanieKategorii(object sender, RoutedEventArgs e)
        {
            foreach (Kategoria zad in ListaNiedodanychKategorii.SelectedItems)
            {
                Kategora_Zadanie NowaKategoria = new Kategora_Zadanie();
                NowaKategoria.Zadanie = zadanie;
                NowaKategoria.Kategoria = zad;
                db.Kategoria_Zadanie.Add(NowaKategoria);
                db.SaveChanges();
            }
            OdswiezBazeKategorii();
        }
        private void OdswiezBazeKategorii()
        {
            ListaObecnychKategorii.ItemsSource = db.Kategoria_Zadanie.Include(p => p.Zadanie).Include(p => p.Kategoria).Where(k => k.Zadanie.IdZadanie == zadanie.IdZadanie).ToList();
            ListaNiedodanychKategorii.ItemsSource = db.Kategorie.Include(o => o.Kategora_Zadanie).Where(p => !(p.Kategora_Zadanie.Any(k => k.Zadanie.IdZadanie == zadanie.IdZadanie && k.Kategoria.IdKategoria == p.IdKategoria))).ToList();
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

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            var Zadanie=db.Zadania.FirstOrDefault(z => z == zadanie);
            db.Zadania.Remove(Zadanie);
            db.SaveChanges();
            this.Close();
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

        private void CzyMoznaUsunacKategorie(object sender, SelectionChangedEventArgs e)
        {
            if (ListaObecnychKategorii.SelectedIndex >= 0)
                UsunKategorie.IsEnabled = true;
            else
                UsunKategorie.IsEnabled = false;
        }
    }
}
