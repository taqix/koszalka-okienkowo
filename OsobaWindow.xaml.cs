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
using System.Collections.ObjectModel;
using Firma;
using System.Globalization;

namespace Firma_okienkowo
{
    /// <summary>
    /// Logika interakcji dla klasy OsobaWindow.xaml
    /// </summary>
    public partial class OsobaWindow : Window
    {
        private Osoba _osoba;
        private KierownikZespolu _kierownik;
        private CzlonekZespolu _czlonek;
        public bool cosiek;
        public OsobaWindow()
        {
            InitializeComponent();
            comboBoxPlec.Items.Add("mężczyzna");
            comboBoxPlec.Items.Add("kobieta");
        }
        public OsobaWindow(KierownikZespolu osoba):this()
        {
            cosiek = true;
            _osoba = osoba;
            _kierownik = osoba;
            lblStanowisko.Content = "Doświadczenie";
            txtPesel.Text = osoba.pesel;
            txtName.Text = osoba.Imie;
            txtLastName.Text = osoba.Nazwisko;
            txtDateBirth.Text = osoba.DataUrodzenia.ToString("d");
            comboBoxPlec.Text = ((osoba.plec) == Plcie.M) ? "mężczyzna" : "kobieta";
            txtStanowisko.Text = osoba.doswiadczenie.ToString();
            
            
            
        }
        public OsobaWindow(CzlonekZespolu osoba) : this()
        {
            cosiek = false;
            _osoba = osoba;
            _czlonek = osoba;
            lblStanowisko.Content = "Stanowisko";
            txtPesel.Text = osoba.pesel;
            txtName.Text = osoba.Imie;
            txtLastName.Text = osoba.Nazwisko;
            txtDateBirth.Text = osoba.DataUrodzenia.ToString("d");
            comboBoxPlec.Text = ((osoba.plec) == Plcie.M) ? "mężczyzna" : "kobieta";
            txtStanowisko.Text = osoba.funkcja;
            

        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void buttonAccept_Click(object sender, RoutedEventArgs e)
        {
            bool checki = false;
            if (txtPesel.Text != "" && txtName.Text != "" && txtLastName.Text != "")
            {
                _osoba.pesel = txtPesel.Text;
                _osoba.Imie = txtName.Text;
                _osoba.Nazwisko = txtLastName.Text;
                string[] fdate = { "yyyy-MM-dd", "yyyy/MM/dd", "MM/dd/yy", "dd-MM-yyyy","dd.MM.yyyy" };
                if (DateTime.TryParseExact(txtDateBirth.Text, fdate, null, DateTimeStyles.None, out DateTime
                date) == false)
                {
                    MessageBox.Show("Źle wpisana data ur.. Możliwe typy to: yyyy - MM - dd, yyyy / MM / dd,MM / dd / yy, dd - MM - yyyy,dd.MM.yyyy");
                    checki = false;
                    InitializeComponent();
                    
                }
                else
                {
                    DateTime.TryParseExact(txtDateBirth.Text, fdate, null, DateTimeStyles.None, out DateTime datesmth);
                    _osoba.DataUrodzenia = datesmth;
                    _osoba.plec = (comboBoxPlec.Text == "kobieta") ? Plcie.K : Plcie.M;
                    if(cosiek == true)
                    {
                        _kierownik.doswiadczenie = Int32.Parse(txtStanowisko.Text);
                    }
                    else if(cosiek == false)
                    {
                        _czlonek.funkcja = txtStanowisko.Text;
                    }
                    checki = true;
                }
            }
            DialogResult = checki;
        }

        private void buttonCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
    
}
