using Microsoft.Win32;
using System.Collections.ObjectModel;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace _0804
{
    public class Person
    {

        public string? strName { get; set; }
        public string? strSname { get; set; }
        public string? strScname { get; set; }
        public string? strPESEL { get; set; }
        public string? strBirthday { get; set; }
        public string? nPhoneNum { get; set; }
        public string? strAddress { get; set; }
        public string? strPlace { get; set; }
        public string? strZipCode { get; set; }
    }

    public partial class MainWindow : Window
    {

        public ObservableCollection<Person> People { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            People = new ObservableCollection<Person>();
            mainList.ItemsSource = People;
        }
        private void New_Click(object sender, RoutedEventArgs e)
        {

        }
        private void Open_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Pliki CSV z separatorem (,) |*.csv|Pliki CSV z separatorem (;) |*.csv";
            openFileDialog.Title = "Otwórz plik CSV";
            if (openFileDialog.ShowDialog() == true)
            {
                People.Clear();
                string filePath = openFileDialog.FileName;
                int selectedFilterIndex = openFileDialog.FilterIndex;
                string delimiter = ";";
                if (selectedFilterIndex == 1)
                {
                    delimiter = ",";
                }
                Encoding encoding = Encoding.UTF8;
                if (File.Exists(filePath))
                {
                    var lines = File.ReadAllLines(filePath, encoding);
                    foreach (var line in lines)
                    {
                        string[] columns = line.Split(delimiter);
                        if (columns != null)
                        {
                            Person uczen = new();
                            uczen.strPESEL = columns.ElementAtOrDefault(0);
                            uczen.strName = columns.ElementAtOrDefault(1);
                            uczen.strScname = columns.ElementAtOrDefault(2);
                            uczen.strSname = columns.ElementAtOrDefault(3);
                            uczen.nPhoneNum = columns.ElementAtOrDefault(4);
                            uczen.strAddress = columns.ElementAtOrDefault(5);
                            uczen.strPlace = columns.ElementAtOrDefault(6);
                            uczen.strZipCode = columns.ElementAtOrDefault(7);
                            People.Add(uczen);
                        }
                    }
                }
            }

        }
        private void Save_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Pliki CSV z separatorem (,) |*.csv|Pliki CSV z separatorem (;) |*.csv";
            saveFileDialog.Title = "Zapisz jako plik CSV";
            if (saveFileDialog.ShowDialog() == true)
            {
                string filePath = saveFileDialog.FileName;
                string delimiter = ";";
                if (saveFileDialog.FilterIndex == 1)
                {
                    delimiter = ",";
                }
                using (StreamWriter writer = new StreamWriter(filePath))
                {
                    foreach (Person item in mainList.Items)
                    {
                        var row = $"{item.strPESEL}{delimiter}{item.strName}" +
                        $"{delimiter}{item.strScname}{delimiter}{item.strSname}" +
                        $"{delimiter}{item.nPhoneNum}{delimiter}{item.strAddress}" +
                        $"{delimiter}{item.strPlace}{delimiter}{item.strZipCode}";
                        writer.WriteLine(row);
                    }
                }
            }
        }
        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void NewRecord_Click(object sender, RoutedEventArgs e)
        {
            Window1 window1 = new Window1(People);
            window1.Show();
        }
        private void RemoveSel_Click(object sender, RoutedEventArgs e)
        {
            while (mainList.SelectedItems.Count > 0)
            {
                mainList.Items.Remove(mainList.SelectedItems[0]);
            }
            //var selected = mainList.SelectedItems.Cast<Person>().ToList();

            //foreach (var person in selected)
            //{
            //    People.Remove(person);
            //}
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {

            //Random random = new Random();
            //Person newPerson = new Person
            //{
            //    strName = "Imie" + random.Next(1, 100),
            //    strScname = "Drugie" + random.Next(1, 100),
            //    strSname = "Nazwisko" + random.Next(1, 100),
            //    strPESEL = random.Next(1000000, 99999999).ToString(),
            //    strBirthday = random.Next(1, 31) + "." + random.Next(1, 12) + "." + random.Next(2000, 2025),
            //    nPhoneNum = random.Next(111111111, 999999999),


            //};
            //People.Add(newPerson);

            Window1 window1 = new Window1(People);
            window1.Show();



        }
    }
}