using ListaZadan.DAL;
using ListaZadan.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Interaction logic for Dodaj_Podzadanie.xaml
    /// </summary>

    public partial class Dodaj_Podzadanie : Window
    {
        public Podzadania Podzadanie { get; set; }
        private Zadanie zadanie { get; set; }
        private int IlePodzadan { get; set; }

        public Dodaj_Podzadanie(ListaZadanContext db, Zadanie zadanie, int ile)
        {
            InitializeComponent();
            Podzadanie = new Podzadania();
            Podzadanie.Zadanie = zadanie;
            this.zadanie = zadanie;
            ilePodzadan.Content = ile;
            IlePodzadan = ile;
        }

        private void OKButton_Click(object sender, RoutedEventArgs e)
        {
            if (TrescPodzadania.Text.Length > 0 && ktoreNaLiscie.Text != null && (Regex.IsMatch(ktoreNaLiscie.Text, @"^\d+$")) == true)
            {
                if (int.Parse(ktoreNaLiscie.Text) > 0 && (int.Parse(ktoreNaLiscie.Text) - 1) <= IlePodzadan)
                {
                    Podzadanie.Zadanie = zadanie;
                    Podzadanie.opis = TrescPodzadania.Text.ToString();
                    Podzadanie.któreNaLiscie = Convert.ToInt32(ktoreNaLiscie.Text);
                    if (zadanie.Podzadania == null)
                        zadanie.Podzadania = new List<Podzadania>();
                    zadanie.Podzadania.Add(Podzadanie);
                    DialogResult = true;
                }
                else
                {
                    MessageBox.Show("Podany element może się znajdować na pozycji od 1 do ilości elementów powiekszonej o 1", "Nieprawidłowe dane", MessageBoxButton.OK, MessageBoxImage.Information);
                    ktoreNaLiscie.Focus();
                }

            }
            else
            {
                MessageBox.Show("Podzadanie jest puste", "Nieprawidłowe dane", MessageBoxButton.OK, MessageBoxImage.Information);
                TrescPodzadania.Focus();

            }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
