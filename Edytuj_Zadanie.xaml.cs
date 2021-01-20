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
    /// Interaction logic for EditTask.xaml
    /// </summary>
    public partial class Edytuj_Zadanie : Window
    {
        ListaZadanContext db { get; set; }
        private Zadanie EdytowaneZadanie { get; set; }
        private ObservableCollection<Podzadania> LokalnePodzadania { get; set; }
        public Edytuj_Zadanie(ListaZadanContext db, Zadanie zadanie)
        {
            InitializeComponent();
            this.db = db;
            db.Zadania.Where(k => k.IdZadanie == zadanie.IdZadanie).Load();

            EdytowaneZadanie = zadanie;

            OdswiezBazeKategorii();
            db.Podzadania.Include(i => i.Zadanie).Where(k => k.Zadanie.IdZadanie == EdytowaneZadanie.IdZadanie).OrderBy(z=>z.któreNaLiscie).Load();
            LokalnePodzadania = db.Podzadania.Local.ToObservableCollection();
            ListaKrokow.ItemsSource = LokalnePodzadania; 
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
            {
                if (EdytowaneZadanie.Tresc != TrescZadania.Text)
                {
                    EdytowaneZadanie.Tresc = TrescZadania.Text;
                }
                if(DataOd.SelectedDate!=EdytowaneZadanie.rozpoczecie)
                {
                    EdytowaneZadanie.rozpoczecie = (DateTime)DataOd.SelectedDate;
                }
                if(DataDo.SelectedDate!=EdytowaneZadanie.zakonczenie)
                {
                    EdytowaneZadanie.zakonczenie = (DateTime)DataDo.SelectedDate;
                }
                if(Piorytet.Value!=EdytowaneZadanie.prorytet)
                {
                    EdytowaneZadanie.prorytet = Piorytet.Value;
                }
                db.Attach(EdytowaneZadanie).State = EntityState.Modified;
                db.SaveChanges();
                DialogResult = true;
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
            List<Kategora_Zadanie> ZaznaczoneKategorie= ListaObecnychKategorii.SelectedItems.Cast<Kategora_Zadanie>().ToList(); ;
            foreach (Kategora_Zadanie zad in ZaznaczoneKategorie)
            {
               // LokalnaBazaNienalezacychKategorii.Add(zad.Kategoria);
                LokalnaListaNatezacychKategorii.Remove(zad);
                
            }
            ListaObecnychKategorii.Items.Refresh();
            ListaNiedodanychKategorii.Items.Refresh();
        }
        private void DodawanieKategorii(object sender, RoutedEventArgs e)
        {
            List<Kategoria> ZaznaczoneKategorie = ListaNiedodanychKategorii.SelectedItems.Cast<Kategoria>().ToList(); 
            foreach (Kategoria zad in ZaznaczoneKategorie)
            {
                Kategora_Zadanie NowaKategoria = new Kategora_Zadanie();
                NowaKategoria.Zadanie = EdytowaneZadanie;
                NowaKategoria.Kategoria = zad;
                LokalnaListaNatezacychKategorii.Add(NowaKategoria);
                LokalnaBazaNienalezacychKategorii.Remove(zad);
            }
            ListaObecnychKategorii.Items.Refresh();
            ListaNiedodanychKategorii.Items.Refresh();
        }
        private ObservableCollection<Kategora_Zadanie> LokalnaListaNatezacychKategorii { get; set; }
        private ObservableCollection<Kategoria> LokalnaBazaNienalezacychKategorii { get; set; }
        private void OdswiezBazeKategorii()
        {
            db.Kategoria_Zadanie.Include(p => p.Zadanie).Include(p => p.Kategoria).Where(k => k.Zadanie.IdZadanie == EdytowaneZadanie.IdZadanie).Load() ;
            LokalnaListaNatezacychKategorii= db.Kategoria_Zadanie.Local.ToObservableCollection();
            ListaObecnychKategorii.ItemsSource = LokalnaListaNatezacychKategorii;

            db.Kategorie.Include(o => o.Kategora_Zadanie).Where(p => !(p.Kategora_Zadanie.Any(k => k.Zadanie.IdZadanie == EdytowaneZadanie.IdZadanie && k.Kategoria.IdKategoria == p.IdKategoria))).Load();
            LokalnaBazaNienalezacychKategorii= db.Kategorie.Local.ToObservableCollection();
            ListaNiedodanychKategorii.ItemsSource = LokalnaBazaNienalezacychKategorii;
        }

        private void dodajPodzadanie(object sender, RoutedEventArgs e)
        {
            var NowePodzadanie = new Dodaj_Podzadanie(db, EdytowaneZadanie, LokalnePodzadania.Count());
            NowePodzadanie.Owner = this;

            if(NowePodzadanie.ShowDialog()==true)
            {
                
                if (NowePodzadanie.Podzadanie.któreNaLiscie <= LokalnePodzadania.Count())
                {
                    foreach (Podzadania podzadania in LokalnePodzadania.Where(z => z.któreNaLiscie >= NowePodzadanie.Podzadanie.któreNaLiscie))
                    {
                        podzadania.któreNaLiscie++;
                    }
                }
                LokalnePodzadania.Add(NowePodzadanie.Podzadanie);
                ListaKrokow.Items.Refresh();
            }
        }

        private void UsunPodzadania(object sender, RoutedEventArgs e)
        {
            List<Podzadania> ZaznaczonePodzadania = ListaKrokow.SelectedItems.Cast<Podzadania>().ToList();
            foreach (Podzadania zad in ZaznaczonePodzadania)
            {
                LokalnePodzadania.Remove(zad);
            }
            int ktory = 1;
            foreach (Podzadania podzadania in LokalnePodzadania)
            {
                podzadania.któreNaLiscie=ktory;
                ktory++;
                
            }
            ListaKrokow.Items.Refresh();
        }
    }
}
