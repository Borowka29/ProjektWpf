﻿using ListaZadan.DAL;
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
    /// Interaction logic for Tworzenie_Kategorii.xaml
    /// </summary>
    public partial class Tworzenie_Kategorii : Window
    {
        private readonly ListaZadanContext db;
        public Tworzenie_Kategorii(ListaZadanContext db)
        {
            this.db = db;
            InitializeComponent();
        }

        private void DodajButton_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(trescKategoriiTextBox.Text))
            {
                db.Kategorie.Add(new Kategoria() { Typ = trescKategoriiTextBox.Text });
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
