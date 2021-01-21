using ListaZadan.Models;
using System;
using System.Collections.Generic;
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
    /// Logika interakcji dla klasy Edycja_Kategorii.xaml
    /// </summary>
    public partial class Edycja_Kategorii : Window
    {
        public Kategoria Kategoria { get; set; }
        public Edycja_Kategorii(Kategoria kategoria)
        {
            InitializeComponent();
            Kategoria = kategoria;
            trescKategoriiTextBox.Text = Kategoria.Typ;
        }

        private void ZapiszButton_Click(object sender, RoutedEventArgs e)
        {
            if(!string.IsNullOrEmpty(trescKategoriiTextBox.Text))
            {
                Kategoria.Typ = trescKategoriiTextBox.Text;
                DialogResult = true;
                Close();
            }
        }

        private void AnulujButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
