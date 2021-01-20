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
        private ObservableCollection<Podzadania> LokalnePodzadania { get; set; }
        private ObservableCollection<Kategora_Zadanie> LokalnaListaNatezacychKategorii { get; set; }= new ObservableCollection<Kategora_Zadanie>();

        private ObservableCollection<Kategoria> LokalnaBazaNienalezacychKategorii { get; set; }

        public Dodaj_Zadanie(ListaZadanContext db)
        {
            InitializeComponent();
            this.db = db;
            
            zadanie = new Zadanie();
            ListaKrokow.ItemsSource = zadanie.Podzadania;
            ListaObecnychKategorii.ItemsSource =LokalnaListaNatezacychKategorii;

            db.Kategorie.Include(o => o.Kategora_Zadanie).Load();
            LokalnaBazaNienalezacychKategorii = db.Kategorie.Local.ToObservableCollection();
            ListaNiedodanychKategorii.ItemsSource = LokalnaBazaNienalezacychKategorii;
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            if(TrescZadania.Text.Length!=0 && DataDo.SelectedDate!=null)
            {
                if(PrzedzialCzasu.IsChecked == false)
                {
                    zadanie.rozpoczecie = DateTime.Today;
                }
                zadanie.Tresc = TrescZadania.Text.ToString();
                zadanie.zakonczenie = (DateTime)DataDo.SelectedDate;
                zadanie.prorytet = Piorytet.Value;
                db.Zadania.Add(zadanie);
                //Zadanie ZmienianeZadanie = db.Zadania.FirstOrDefault(z => z == zadanie);

                //db.Attach(zadanie).State = EntityState.Modified;
                db.SaveChanges();
                DialogResult = true;
                this.Close();
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
                podzadania.któreNaLiscie = ktory;
                ktory++;

            }
            ListaKrokow.Items.Refresh();
        }
        
        private void dodajPodzadanie(object sender, RoutedEventArgs e)
        {
            var NowePodzadanie = new Dodaj_Podzadanie(db, zadanie, zadanie.Podzadania.Count());
            NowePodzadanie.Owner = this;
            if (NowePodzadanie.ShowDialog() == true)
            {
                ListaKrokow.ItemsSource = db.Podzadania.Include(i => i.Zadanie).Where(k => k.Zadanie.IdZadanie == zadanie.IdZadanie).OrderBy(z => z.któreNaLiscie).ToList();
            }
        }

        private void DodawanieKategorii(object sender, RoutedEventArgs e)
        {
            List<Kategoria> ZaznaczoneKategorie = ListaNiedodanychKategorii.SelectedItems.Cast<Kategoria>().ToList();
            foreach (Kategoria zad in ZaznaczoneKategorie)
            {
                Kategora_Zadanie NowaKategoria = new Kategora_Zadanie();
                NowaKategoria.Zadanie = zadanie;
                NowaKategoria.Kategoria = zad;
                LokalnaListaNatezacychKategorii.Add(NowaKategoria);
                LokalnaBazaNienalezacychKategorii.Remove(zad);
            }
            ListaObecnychKategorii.Items.Refresh();
            ListaNiedodanychKategorii.Items.Refresh();
            
        }
        
        private void OdswiezBazeKategorii()
        {
            
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
