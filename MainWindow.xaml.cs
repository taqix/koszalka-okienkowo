using System;
using System.Collections.Generic;
using System.Collections;
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
using Firma;
using System.Collections.ObjectModel;

namespace Firma_okienkowo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public bool checkowanie=true;
        public ObservableCollection<CzlonekZespolu> Czlonek { get; set; }
        public Zespol zespol = new Zespol();
        public MainWindow()
        {
            
            InitializeComponent();
            zespol = (Zespol)Zespol.odczytajXML("zapisz.XML");
            listboxCzlonkowie.ItemsSource = new ObservableCollection<CzlonekZespolu>(zespol.Czlonkowie);
            txtNazwa.Text = zespol.Nazwa;
            txtKierownik.Text = zespol.Kierownik.ToString();
            
        }
       

        private void buttonAdd_Click(object sender, RoutedEventArgs e)
        {
            CzlonekZespolu cz = new CzlonekZespolu();
            OsobaWindow okno = new OsobaWindow(cz);
            
            okno.ShowDialog();
            if (okno.DialogResult == true)
            {
                zespol.dodajCzlonka(cz); //dodajemy członka
                listboxCzlonkowie.ItemsSource = new ObservableCollection<CzlonekZespolu>(zespol.Czlonkowie);
                checkowanie = false;
            }

        }

        private void txtCzlonkowieZespolu_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            checkowanie = false;
        }

        private void buttonZmien_Click(object sender, RoutedEventArgs e)
        {
            OsobaWindow okno = new OsobaWindow(zespol.Kierownik as KierownikZespolu);
            okno.ShowDialog();
            if (okno.DialogResult == true) { txtKierownik.Text = zespol.Kierownik.ToString(); checkowanie = false; }
        }

        private void buttonUsun_Click(object sender, RoutedEventArgs e)
        {
            int zaznaczony = listboxCzlonkowie.SelectedIndex;
            if (zaznaczony != -1)
            {
                zespol.Czlonkowie.RemoveAt(zaznaczony);
                listboxCzlonkowie.ItemsSource = new ObservableCollection<CzlonekZespolu>(zespol.Czlonkowie);
                checkowanie = false;
            }
            else
            {
                MessageBox.Show("Proszę zaznaczyć członka");
            }

        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {

        }
        private void MenuZapisz_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.SaveFileDialog dlg = new Microsoft.Win32.SaveFileDialog();
            Nullable<bool> result = dlg.ShowDialog();
            if (result == true)
            {
                string filename = dlg.FileName;
                zespol.Nazwa = txtNazwa.Text;
                Zespol.zapiszXML(filename,zespol);
            }
            checkowanie = true;
        }
        private void MenuOtworz_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            Nullable<bool> result = dlg.ShowDialog();

            // Process open file dialog box results
            if (result == true)
            {
                // Open document
                string filename = dlg.FileName;
                zespol = (Zespol)Zespol.odczytajXML(filename);
                listboxCzlonkowie.ItemsSource = new ObservableCollection<CzlonekZespolu>(zespol.Czlonkowie);
                txtNazwa.Text = zespol.Nazwa;
                txtKierownik.Text = zespol.Kierownik.ToString();

            }
            

        }
        private void MenuWyjdz_Click(object sender, RoutedEventArgs e)
        {
            
            if (checkowanie == false)
            {
                if(MessageBox.Show("Czy nie chciałbyś najpierw zapisać zmian?","zamknięcie",MessageBoxButton.YesNo,MessageBoxImage.Information) == MessageBoxResult.No)
                {
                    Environment.Exit(0);
                }
                else
                {
                    InitializeComponent();
                    listboxCzlonkowie.ItemsSource = new ObservableCollection<CzlonekZespolu>(zespol.Czlonkowie);
                    txtNazwa.Text = zespol.Nazwa;
                    txtKierownik.Text = zespol.Kierownik.ToString();
                }
                
            }
            else
            {
                Environment.Exit(0);
            }
            
        }

        private void buttonZmienianie_Click(object sender, RoutedEventArgs e)
        {
            int zaznaczony = listboxCzlonkowie.SelectedIndex;
            if (zaznaczony != -1){
                OsobaWindow okno = new OsobaWindow(listboxCzlonkowie.SelectedItem as CzlonekZespolu);
                okno.ShowDialog();
                if (okno.DialogResult == true)
                {
                    InitializeComponent();
                    listboxCzlonkowie.ItemsSource = new ObservableCollection<CzlonekZespolu>(zespol.Czlonkowie);
                    txtNazwa.Text = zespol.Nazwa;
                    txtKierownik.Text = zespol.Kierownik.ToString();
                    checkowanie = false;

                }
            }
            else { MessageBox.Show("Zaznacz jednego z członków!!"); }
            

        }
    }
}
