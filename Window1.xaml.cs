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
using System.Windows.Shapes;

namespace WpfApp1
{
    /// <summary>
    /// Logika interakcji dla klasy Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        public Window1()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            string name = Imie.Text;
            string name2 = Drugie_imie.Text;
            string surname = Nazwisko.Text;
            string birthDate = Data_ur.Text;
            string phoneNumber = Telefon.Text;
            string address = Adres.Text;
            string city = Miejscowosc.Text;
            string postalCode = Kod_pocztowy.Text;


        }
    }

}
