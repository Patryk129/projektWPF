using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
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
using System.Xml.Linq;
using static System.Net.Mime.MediaTypeNames;

namespace _0804
{
    public partial class Window1 : Window
    {
        private ObservableCollection<Person> _people;

        public Window1(ObservableCollection<Person> people)
        {
            InitializeComponent();
            _people = people;
        }
        //public class Person
        //{

        //    public string? strName { get; set; }
        //    public string? strSname { get; set; }
        //    public string? strScname { get; set; }
        //    public string? strPESEL { get; set; }
        //    public string? strBirthday { get; set; }
        //    public string? nPhoneNum { get; set; }
        //    public string? strAddress { get; set; }
        //    public string? strPlace { get; set; }
        //    public string? strZipCode { get; set; }

        //}


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string name = Imie.Text;
            string name2 = Drugie_imie.Text;
            string surname = Nazwisko.Text;
            string pesel = Pesel.Text;
            string birthDate = Data_ur.Text;
            string phoneNumber = Telefon.Text;
            string address = Adres.Text;
            string city = Miejscowosc.Text;
            string postalCode = Kod_pocztowy.Text;

            if (string.IsNullOrEmpty(name) && string.IsNullOrEmpty(name2) && string.IsNullOrEmpty(surname) &&
                string.IsNullOrEmpty(pesel) && string.IsNullOrEmpty(birthDate) && string.IsNullOrEmpty(phoneNumber) &&
                string.IsNullOrEmpty(address) && string.IsNullOrEmpty(city) && string.IsNullOrEmpty(postalCode))
            {
                this.Close();
            }
            else
            {
                MessageBoxResult result = MessageBox.Show("Czy chcesz zamknąć okno?", "Potwierdzenie", MessageBoxButton.YesNo);
                if (result == MessageBoxResult.Yes)
                {
                    this.Close();
                }
            }


        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            string name = Imie.Text;
            string name2 = Drugie_imie.Text;
            string surname = Nazwisko.Text;
            string pesel = Pesel.Text;
            string birthDate = Data_ur.Text;
            string phoneNumber = Telefon.Text;
            string address = Adres.Text;
            string city = Miejscowosc.Text;
            string postalCode = Kod_pocztowy.Text;
            if (IsNotNull(name, name2, surname, pesel, birthDate, phoneNumber, address, city, postalCode))
            {
                if (DateTime.TryParse(birthDate, out DateTime birthDate2))
                {

                    if (CheckPesel(pesel, birthDate2))
                    {
                        Pesel.Background = Brushes.White;

                        name = name.Trim();
                        name = char.ToUpper(name[0]) + name.Substring(1).ToLower();
                        if(string.IsNullOrEmpty(name2))
                        {
                            name2 = name2.Trim();
                            name2 = char.ToUpper(name2[0]) + name2.Substring(1).ToLower();
                        }

                        surname = surname.Trim();
                        if(surname.Contains("-"))
                        {
                            int index = surname.IndexOf("-");
                            surname = char.ToUpper(surname[0]) + surname.Substring(1, index - 1).ToLower() + char.ToUpper(surname[index + 1]) + surname.Substring(index + 2).ToLower();
                        }
                        else
                        {
                            surname = char.ToUpper(surname[0]) + surname.Substring(1).ToLower();
                        }

                        address = address.Trim();
                        address = char.ToUpper(address[0]) + address.Substring(1).ToLower();
                        if (address.StartsWith("ul."))
                        {
                            address = address.Substring(3).Trim();
                        }
                        address = address.Replace(" ", "");
                        if (!string.IsNullOrEmpty(address))
                        {
                            address = char.ToUpper(address[0]) + address.Substring(1);
                        }

                        phoneNumber = phoneNumber.Trim();
                        phoneNumber = phoneNumber.Replace("-", "");
                        phoneNumber = phoneNumber.Replace(" ", "");
                        if (!phoneNumber.StartsWith("+48"))
                        {
                            phoneNumber = "+48" + phoneNumber.Substring(1);
                        }
                        else if (!phoneNumber.StartsWith("+"))
                        {
                            phoneNumber = "+48" + phoneNumber;
                        }

                        //DODAJ SPRAWDZENIE ADRESU!!! I NUMERU TELEFONU
                        Person newPerson = new Person
                        {
                            strName = name,
                            strScname = name2,
                            strSname = surname,
                            strPESEL = pesel,
                            strBirthday = birthDate,
                            nPhoneNum = phoneNumber,
                            strAddress = address,
                            strPlace = city,
                            strZipCode = postalCode
                        };
                        _people.Add(newPerson);
                        this.Close();

                    }
                    else
                    {
                        Pesel.Background = Brushes.Red;
                    }
                }
            }
        }
        private bool IsNotNull(string name, string name2, string surname, string pesel,
                       string birthDateStr, string phoneNumber, string address,
                       string city, string postalCode)
        {
            bool flag = true;

            if (string.IsNullOrWhiteSpace(name))
            {
                flag = false;
                Imie.Background = Brushes.Red;
            }
            else
            {
                Imie.Background = Brushes.White;
            }

            if (string.IsNullOrWhiteSpace(name2))
            {
                flag = false;
                Drugie_imie.Background = Brushes.Red;
            }
            else
            {
                Drugie_imie.Background = Brushes.White;
            }

            if (string.IsNullOrWhiteSpace(surname))
            {
                flag = false;
                Nazwisko.Background = Brushes.Red;
            }
            else
            {
                Nazwisko.Background = Brushes.White;
            }

            if (string.IsNullOrWhiteSpace(pesel))
            {
                flag = false;
                Pesel.Background = Brushes.Red;
            }
            else
            {
                Pesel.Background = Brushes.White;
            }

            if (string.IsNullOrWhiteSpace(birthDateStr))
            {
                flag = false;
                Data_ur.Background = Brushes.Red;
            }
            else
            {
                Data_ur.Background = Brushes.White;
            }

            if (string.IsNullOrWhiteSpace(phoneNumber))
            {
                flag = false;
                Telefon.Background = Brushes.Red;
            }
            else
            {
                Telefon.Background = Brushes.White;
            }

            if (string.IsNullOrWhiteSpace(address))
            {
                flag = false;
                Adres.Background = Brushes.Red;
            }
            else
            {
                Adres.Background = Brushes.White;
            }

            if (string.IsNullOrWhiteSpace(city))
            {
                flag = false;
                Miejscowosc.Background = Brushes.Red;
            }
            else
            {
                Miejscowosc.Background = Brushes.White;
            }

            if (string.IsNullOrWhiteSpace(postalCode))
            {
                flag = false;
                Kod_pocztowy.Background = Brushes.Red;
            }
            else
            {
                Kod_pocztowy.Background = Brushes.White;
            }

            return flag;
        }

        private bool CheckPesel(string pesel, DateTime birthDate)
        {
            if (pesel.Length != 11 || !pesel.All(char.IsDigit))
                return false;

            int[] wagi = { 1, 3, 7, 9, 1, 3, 7, 9, 1, 3 };
            int suma = 0;

            for (int i = 0; i < 10; i++)
                suma += (pesel[i] - '0') * wagi[i];

            if ((10 - suma % 10) % 10 != (pesel[10] - '0'))
                return false;

            int rok = int.Parse(pesel.Substring(0, 2));
            int miesiac = int.Parse(pesel.Substring(2, 2));
            int dzien = int.Parse(pesel.Substring(4, 2));

            int wiek = miesiac / 20;
            miesiac %= 20;

            int pelnyRok = (wiek switch
            {
                0 => 1900,
                1 => 2000,
                2 => 2100,
                3 => 2200,
                4 => 1800,
                _ => 0
            }) + rok;

            if (wiek > 4 || miesiac < 1 || miesiac > 12)
                return false;

            if (!DateTime.TryParse($"{pelnyRok}-{miesiac:D2}-{dzien:D2}", out DateTime dataZPesel))
                return false;

            return dataZPesel.Date == birthDate.Date;
        }


    }


}

