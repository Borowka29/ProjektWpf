using ListaZadan.DAL;
using ListaZadan.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ListaZadan
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private GridViewColumnHeader listViewSortCol = null;
        private SortAdorner listViewSortAdorner = null;
        private static ListaZadanContext db { get; set; }
        public MainWindow()
        {
            InitializeComponent();
            db = new ListaZadanContext();
            TasksListView.ItemsSource = db.Zadania.Include(z=>z.Kategora_Zadanie).ToList();
            
        }

        private void TasksListViewColumnHeader_Click(object sender, RoutedEventArgs e)
        {


            // Nie wiem czemu nie działa poniższy kod 
            //!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
            //GridViewColumnHeader column = (sender as GridViewColumnHeader);
            //string sortBy = column.Tag.ToString();
            //if (listViewSortCol != null)
            //{
            //    AdornerLayer.GetAdornerLayer(listViewSortCol).Remove(listViewSortAdorner);
            //    //TasksListView.Items.SortDescriptions.Clear();
            //}

            //ListSortDirection newDir = ListSortDirection.Ascending;
            //if (listViewSortCol == column && listViewSortAdorner.Direction == newDir)
            //    newDir = ListSortDirection.Descending;

            //listViewSortCol = column;
            //listViewSortAdorner = new SortAdorner(listViewSortCol, newDir);
            //AdornerLayer.GetAdornerLayer(listViewSortCol).Add(listViewSortAdorner);
            ////TasksListView.Items.SortDescriptions.Add(new SortDescription(sortBy, newDir));
        }

        private void PokarzZadanie_Click(object sender, RoutedEventArgs e)
        {
            Zadanie zadanie = (sender as FrameworkElement).DataContext as Zadanie;
            var PodgladZadania = new Szczegoly(db, zadanie);
            PodgladZadania.Owner = this;
            PodgladZadania.ShowDialog();

        }

        private void EdytujZadanie_Click(object sender, RoutedEventArgs e)
        {
            Zadanie zadanie = (sender as FrameworkElement).DataContext as Zadanie;
            var EdycjaZadania = new Edytuj_Zadanie(db, zadanie);
            EdycjaZadania.Owner = this;
            if(true==EdycjaZadania.ShowDialog())
            {
                TasksListView.ItemsSource = db.Zadania.ToList();
            }
        }

        private void UsunZadania_Click(object sender, RoutedEventArgs e)
        {
            foreach(Zadanie zad in TasksListView.SelectedItems)
            {
                var zadanie = db.Zadania.FirstOrDefault(z => z == zad);
                db.Zadania.Remove(zadanie);
                db.SaveChanges();


            }
            TasksListView.ItemsSource = db.Zadania.ToList();
        }

        private void ListboxSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (TasksListView.SelectedIndex >= 0)
                UsunZadanie.IsEnabled = true;
            else
                UsunZadanie.IsEnabled = false;
        }

        private void ZmianaGrupowania(object sender, SelectionChangedEventArgs e)
        {
            int ile = 0;
            if (Grupowanie.SelectedIndex == 1) TasksListView.ItemsSource = db.Zadania.OrderByDescending(p => p.prorytet).ToList();
            else if (Grupowanie.SelectedIndex == 2) TasksListView.ItemsSource = db.Zadania.OrderByDescending(p => p.zakonczenie).ToList();
            else if (ile != 0) { TasksListView.ItemsSource = db.Zadania.OrderByDescending(p => p.IdZadanie).ToList(); ile=1; }

        }

        private void Szukaj(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(SzukajZadania.Text))
            {
                TasksListView.ItemsSource = db.Zadania.Where(z => z.Tresc.ToLower().Contains(SzukajZadania.Text.ToLower())).ToList();
            }
            else TasksListView.ItemsSource = db.Zadania.ToList();
        }

        private void DodajZadania_Click(object sender, RoutedEventArgs e)
        {
            var DodawanieZadania = new Dodaj_Zadanie(db);
            DodawanieZadania.Owner = this;
            if(true==DodawanieZadania.ShowDialog())
            {
                TasksListView.ItemsSource = db.Zadania.ToList();
            }
            
        }

        //private void NewTask_Click(object sender, RoutedEventArgs e)
        //{
        //    NewTask view = new NewTask();
        //    view.ShowDialog();
        //}

        //private void EditTask_Click(object sender, RoutedEventArgs e)
        //{
        //    EditTask view = new EditTask();
        //    view.ShowDialog();
        //}
    }



    public class SortAdorner : Adorner
    {
        private static Geometry ascGeometry =
            Geometry.Parse("M 0 4 L 3.5 0 L 7 4 Z");

        private static Geometry descGeometry =
            Geometry.Parse("M 0 0 L 3.5 4 L 7 0 Z");

        public ListSortDirection Direction { get; private set; }

        public SortAdorner(UIElement element, ListSortDirection dir) : base(element)
        {
            this.Direction = dir;
        }

        protected override void OnRender(DrawingContext drawingContext)
        {
            base.OnRender(drawingContext);

            if (AdornedElement.RenderSize.Width < 20)
                return;

            TranslateTransform transform = new TranslateTransform
                (
                    AdornedElement.RenderSize.Width - 15,
                    (AdornedElement.RenderSize.Height - 5) / 2
                );
            drawingContext.PushTransform(transform);

            Geometry geometry = ascGeometry;
            if (this.Direction == ListSortDirection.Descending)
                geometry = descGeometry;
            drawingContext.DrawGeometry(Brushes.Black, null, geometry);

            drawingContext.Pop();
        }
    }
}