using System;
using System.Collections.Generic;
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

namespace Piłkarze
{
    public partial class MainWindow : Window
    {

        private string plik = "pilkarze.txt";
        public MainWindow()
        {
            //labelImie.Content = "Podaj imie";
            //labelNazwisko.Content = "Podaj nazwisko";
            InitializeComponent();
        }

        #region metody
        private bool czyPuste(textBoxBorderControl tekst)
        {
            if (tekst.Text.Trim() == "")
            {
                tekst.Blad("Pole puste!");
                return false;

            }
            tekst.Blad("");
            return true;
        }
        private void Clear()
        {
            textBoxImie.Text = "";
            textBoxNazwisko.Text = "";
            sliderWaga.Value = 75;
            sliderWiek.Value = 50;

        }
        private void Zaladuj(Footballer fb)
        {
            textBoxImie.Text = fb.Imie;
            textBoxNazwisko.Text = fb.Nazwisko;
            sliderWaga.Value = fb.Waga;
            sliderWiek.Value = fb.Wiek;
        }

        private void ListBox1_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            if (listBox1.SelectedIndex > -1)
                Zaladuj((Footballer)listBox1.SelectedItem);
        }
        #endregion

        #region przyciski
        private void buttonDodaj_Click(object sender, RoutedEventArgs e)
        {

            if (czyPuste(textBoxImie)&czyPuste(textBoxNazwisko))
            {

                var bFootballer = new Footballer(textBoxImie.Text.Trim(), textBoxNazwisko.Text.Trim(), (int)sliderWiek.Value, (int)sliderWaga.Value);
                listBox1.Items.Add(bFootballer);
                Clear();
            }
        }
        private void buttonEdytuj_Click(object sender, RoutedEventArgs e)
        {
            if (czyPuste(textBoxImie) & czyPuste(textBoxNazwisko))
            {
                var bFootballer = new Footballer(textBoxImie.Text.Trim(), textBoxNazwisko.Text.Trim(), (int)sliderWiek.Value, (int)sliderWaga.Value);
                var czyJuzJest = false;
                foreach(var i in listBox1.Items)
                {
                    var footballer = i as Footballer;
                    if(footballer.czyIstnieje(bFootballer))
                    {
                        czyJuzJest = true;
                        break;
                    }
                }
                if (!czyJuzJest)
                {
                    var dialogResult = MessageBox.Show($"Czy na pewno chcesz zmienić dane  {Environment.NewLine} {listBox1.SelectedItem}?", "Edycja", MessageBoxButton.YesNo);

                    if (dialogResult == MessageBoxResult.Yes)
                    {
                        var nowy = listBox1.SelectedItem as Footballer;
                        nowy.Edit(bFootballer);
                        listBox1.Items.Refresh();
                    }
                    Clear();
                    listBox1.SelectedIndex = -1;
                }
                else
                    MessageBox.Show($"{bFootballer.ToString()} już jest na liście.");
            }
        }
        private void buttonUsun_Click(object sender, RoutedEventArgs e)
        {
            var bFootballer = new Footballer(textBoxImie.Text.Trim(), textBoxNazwisko.Text.Trim(), (int)sliderWiek.Value, (int)sliderWaga.Value);
            var dialogResult = MessageBox.Show($"Czy na pewno chcesz usunąć dane  {Environment.NewLine} {listBox1.SelectedItem}?","Usuń", MessageBoxButton.YesNo);

            if (dialogResult == MessageBoxResult.Yes)
            {
                listBox1.Items.RemoveAt(listBox1.SelectedIndex);
            }
            Clear();
            listBox1.SelectedIndex = -1;
        }

        #endregion

        #region pliki
        private void Otwarcie(object sender, RoutedEventArgs e)
        {
            var fb = Zapis.ZPliku(plik);
            if (fb != null)
                foreach (var i in fb)
                    listBox1.Items.Add(i);
        }

        private void Zamkniecie(object sender, System.ComponentModel.CancelEventArgs e)
        {
            int n = listBox1.Items.Count;
            Footballer[] fb = null;
            if (n>0)
            {
                fb = new Footballer[n];
                int i = 0;
                foreach (var p in listBox1.Items)
                    fb[i++] = p as Footballer;
                Zapis.DoPliku(plik, fb);
            }
        }
        #endregion


    }
}
