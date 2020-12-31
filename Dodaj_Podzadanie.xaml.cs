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
        ListaZadanContext db { get; set; }
        private Podzadania Podzadanie { get; set; }
        private Zadanie zadanie { get; set; }

        public Dodaj_Podzadanie(ListaZadanContext db, Zadanie zadanie)
        {
            InitializeComponent();
            this.db = db;
            this.zadanie = zadanie;
            ilePodzadan.Content = zadanie.Podzadania.Count();
            
        }

        private void OKButton_Click(object sender, RoutedEventArgs e)
        {
            if(TrescPodzadania.Text.Length>0 && ktoreNaLiscie.Text!=null && (Regex.IsMatch(ktoreNaLiscie.Text, @"^\d+$")) ==true && (Regex.IsMatch(ilePodzadan.Content.ToString(), @"^\d+$") == true)) 
            {
                if(int.Parse(ktoreNaLiscie.Text)!=0 && (int.Parse(ktoreNaLiscie.Text)-1)<= int.Parse(ilePodzadan.Content.ToString()))
                {
                    Podzadania Podzadanie = new Podzadania();
                    Podzadanie.Zadanie = zadanie;
                    Podzadanie.opis = TrescPodzadania.Text.ToString();
                    Podzadanie.któreNaLiscie = Convert.ToInt32(ktoreNaLiscie.Text);
                    db.Podzadania.Add(Podzadanie);
                    if (int.Parse(ktoreNaLiscie.Text)  <= int.Parse(ilePodzadan.Content.ToString()))
                    {
                        int k = Int16.Parse(ktoreNaLiscie.Text);
                        List<Podzadania> WybranePodzadania = db.Podzadania.Where(z => z.któreNaLiscie >=k). ToList();
                        foreach (Podzadania podzadania in WybranePodzadania)
                        {
                            podzadania.któreNaLiscie++;
                            db.Attach(podzadania).State = EntityState.Modified;
                            db.SaveChanges();
                        }
                    }
                    else
                    {
                        db.SaveChanges();
                    }
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
                MessageBox.Show("Podzadanie jest puste","Nieprawidłowe dane", MessageBoxButton.OK, MessageBoxImage.Information);
                    TrescPodzadania.Focus();

            }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
