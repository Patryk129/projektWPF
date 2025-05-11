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
       
        private bool Check(string name, string name2, string surname, string pesel,
                       string birthDateStr, string phoneNumber, string address,
                       string city, string postalCode)
        {
            bool flag = true;
            bool poprawnaData;
            if (string.IsNullOrWhiteSpace(name))
            {
                flag = false;
                Imie.Background = Brushes.IndianRed;
                Imie.Foreground = Brushes.White;
            }
            else
            {
                Imie.Background = Brushes.White;
                Imie.Foreground = Brushes.Black;
            }
            if (string.IsNullOrWhiteSpace(surname))
            {
                flag = false;
                Nazwisko.Background = Brushes.IndianRed;
                Nazwisko.Foreground = Brushes.White;
            }
            else
            {
                Nazwisko.Background = Brushes.White;
                Nazwisko.Foreground = Brushes.Black;

            }
            if (string.IsNullOrWhiteSpace(birthDateStr))
            {
                flag = false;
                Data_ur.Background = Brushes.IndianRed;
            }
            else
            {
                poprawnaData = DateTime.TryParse(birthDateStr, out DateTime birthDate);
                if(!poprawnaData)
                {
                    flag = false;
                    Data_ur.Background = Brushes.IndianRed;
                }
                else
                {
                    Data_ur.Background = Brushes.White;

                }
            }
            if (string.IsNullOrWhiteSpace(pesel))
            {
                Pesel.Background = Brushes.IndianRed;
                Pesel.Foreground = Brushes.White;
                flag = false;
            }
            else if (!DateTime.TryParse(birthDateStr, out DateTime birthDate) || !CheckPesel(pesel, birthDate))
            {
                flag = false;
                Pesel.Background = Brushes.IndianRed;
                Pesel.Foreground = Brushes.White;
            }
            else
            {
                Pesel.Background = Brushes.White;
                Pesel.Foreground = Brushes.Black;
            }

            if (string.IsNullOrWhiteSpace(address))
            {
                flag = false;
                Adres.Background = Brushes.IndianRed;
                Adres.Foreground = Brushes.White;
            }
            else
            {
                Adres.Background = Brushes.White;
                Adres.Foreground = Brushes.Black;
            }
            if (string.IsNullOrWhiteSpace(city))
            {
                flag = false;
                Miejscowosc.Background = Brushes.IndianRed;
                Miejscowosc.Foreground = Brushes.White;
            }
            else
            {
                Miejscowosc.Background = Brushes.White;
                Miejscowosc.Foreground = Brushes.Black;
            }
            if (string.IsNullOrWhiteSpace(postalCode))
            {
                flag = false;
                Kod_pocztowy.Background = Brushes.IndianRed;
                Kod_pocztowy.Foreground = Brushes.White;
            }
            else
            {
                Kod_pocztowy.Background = Brushes.White;
                Kod_pocztowy.Foreground = Brushes.Black;
            }
            return flag;
        }

        private bool CheckPesel(string pesel, DateTime birthDate)
        {
            if (pesel.Length != 11 || !pesel.All(char.IsDigit))
            {
                return false;
            }    

            int[] wagi = { 1, 3, 7, 9, 1, 3, 7, 9, 1, 3 };
            int suma = 0;
            int k;

            for (int i = 0; i < 10; i++)
            {
                suma += int.Parse(pesel[i].ToString()) * wagi[i];
            }    
           
            if(suma%10 != 0)
            {
                k = 10 - suma % 10;
            }    
            else
            {
                k = 0;
            }
                
            if (k != int.Parse(pesel[10].ToString()))
            {
                return false;
            }  

            int rok = int.Parse(pesel.Substring(0, 2));
            int miesiac = int.Parse(pesel.Substring(2, 2));
            int dzien = int.Parse(pesel.Substring(4, 2));

            if(birthDate.Year >= 2000)
            {
                miesiac -= 20;
            }
            else if(birthDate.Year >=1800 &&  birthDate.Year <= 1899)
            {
                miesiac -= 80;
            }
            if (birthDate.Year % 100 != rok || birthDate.Month != miesiac || birthDate.Day != dzien)
            {
                return false;
            }
            return true;
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
            if (Check(name, name2, surname, pesel, birthDate, phoneNumber, address, city, postalCode))
            {
                bool exists = false;
                foreach (var person in _people)
                {
                    if (person.strPESEL == pesel)
                    {
                        exists = true;
                        break;
                    }
                }

                if (exists)
                {
                    MessageBox.Show("Osoba o podanym numerze PESEL już istnieje.", "Błąd", MessageBoxButton.OK);
                    Pesel.Background = Brushes.IndianRed;
                    Pesel.Foreground = Brushes.White;
                    return;
                }
                
                if (postalCode.Length == 5 && postalCode.All(char.IsDigit))
                    postalCode = postalCode.Insert(2, "-");

                name = name.Trim();
                name = char.ToUpper(name[0]) + name.Substring(1).ToLower();
                if (!string.IsNullOrEmpty(name2))
                {
                    name2 = name2.Trim();
                    name2 = char.ToUpper(name2[0]) + name2.Substring(1).ToLower();
                }

                surname = surname.Trim();
                if (surname.Contains("-"))
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
                address = char.ToUpper(address[0]) + address.Substring(1).ToLower();
                if (!string.IsNullOrEmpty(phoneNumber))
                {
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
                }

                Person newPerson = new Person
                {
                    strName = name,
                    strScname = name2,
                    strSname = surname,
                    strPESEL = pesel,
                    strBirthday = birthDate,
                    strPhoneNum = phoneNumber,
                    strAddress = address,
                    strPlace = city,
                    strZipCode = postalCode
                };
                _people.Add(newPerson);
                this.Close();
            }
   
        }
    }

}
