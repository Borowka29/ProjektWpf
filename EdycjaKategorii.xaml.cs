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
    /// Logika interakcji dla klasy EdycjaKategorii.xaml
    /// </summary>
    public partial class EdycjaKategorii : Window
    {
        private readonly ListaZadanContext db;
        public List<Kategoria> ListaKategorii { get; set; }
        public EdycjaKategorii(ListaZadanContext db)
        {
            this.db = db;
            db.Kategorie.Include(k => k.Kategora_Zadanie).Load();
            ListaKategorii = db.Kategorie.Local.ToList();
            InitializeComponent();

            KategorieListView.ItemsSource = ListaKategorii;
        }

        private void DodajButton_Click(object sender, RoutedEventArgs e)
        {
            Tworzenie_Kategorii w = new Tworzenie_Kategorii(db);
            
            if(w.ShowDialog() == true)
            {
                Refresh();
            }
        }
        private void Refresh()
        {
            db.Kategorie.Include(k => k.Kategora_Zadanie).Load();
            ListaKategorii = db.Kategorie.Local.ToList();
            KategorieListView.ItemsSource = ListaKategorii;
        }

        private void EdytujButton_Click(object sender, RoutedEventArgs e)
        {
            Kategoria k = KategorieListView.SelectedItem as Kategoria;
            Edycja_Kategorii w = new Edycja_Kategorii(k);
            if(w.ShowDialog() == true)
            {
                db.SaveChanges();
                Refresh();
            }
        }

        private void UsuńButton_Click(object sender, RoutedEventArgs e)
        {
            Kategoria k = KategorieListView.SelectedItem as Kategoria;
            if(k != null && k.Kategora_Zadanie.Count == 0)
            {
                db.Remove(k);
                Refresh();
            }
            else
            {
                MessageBox.Show("Nie można usunąć kategorii, która jest przypisana do zadania.", "Nieprawidłowe dane", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void KategorieListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(KategorieListView.SelectedItem != null)
            {
                EdytujButton.IsEnabled = true;
                UsuńButton.IsEnabled = true;
            }
            else
            {
                EdytujButton.IsEnabled = false;
                UsuńButton.IsEnabled = false;
            }
        }
    }
}
