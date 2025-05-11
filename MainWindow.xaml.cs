using Microsoft.Win32;
using System;
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
        public string? strPhoneNum { get; set; }
        public string? strAddress { get; set; }
        public string? strPlace { get; set; }
        public string? strZipCode { get; set; }
        public Person()
        {
            strName = "";
            strScname = "";
            strSname = "";
            strPESEL = "00000000000";
            strBirthday = "";
            strPhoneNum = "";
            strAddress = "";
            strPlace = "";
            strZipCode = "";
        }
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
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
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
                            uczen.strBirthday = columns.ElementAtOrDefault(4);
                            uczen.strPhoneNum = columns.ElementAtOrDefault(5);
                            uczen.strAddress = columns.ElementAtOrDefault(6);
                            uczen.strPlace = columns.ElementAtOrDefault(7);
                            uczen.strZipCode = columns.ElementAtOrDefault(8);
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
                        $"{delimiter}{item.strBirthday}" +
                        $"{delimiter}{item.strPhoneNum}{delimiter}{item.strAddress}" +
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
        private void EditRecord_Click(object sender, RoutedEventArgs e)
        {
            if(People.Count > 0)
            {
                Edit_student window1 = new Edit_student(People);
                window1.ShowDialog();
                RefreshList();
            }
        }
        private void RemoveSel_Click(object sender, RoutedEventArgs e)
        {
            var selected = mainList.SelectedItems.Cast<Person>().ToList();

            foreach (var person in selected)
            {
                People.Remove(person);
            }
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Window1 window1 = new Window1(People);
            window1.Show();
        }
        public void RefreshList()
        {
            mainList.ItemsSource = null;
            mainList.ItemsSource = People;
        }

        private void About(object sender, RoutedEventArgs e)
        {
            Info info = new Info();
            info.Show();
        }


    }
}