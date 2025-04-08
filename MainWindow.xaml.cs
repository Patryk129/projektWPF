using System.Collections.ObjectModel;
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

        public string strName { get; set; }
        public string strSname { get; set; }
        public string strScname { get; set; }
        public string strPESEL { get; set; }
        public string strBirthday { get; set; }
        public int nPhoneNum { get; set; }
        public string strAddress { get; set; }
        public string strPlace { get; set; }
        public string strZipCode { get; set; }

    }

    public partial class MainWindow : Window
    {

        public ObservableCollection<Person> People { get; set; }

        public MainWindow()
        {
            InitializeComponent();


            People = new ObservableCollection<Person>();
            listView.ItemsSource = People;
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {

            Random random = new Random();
            Person newPerson = new Person
            {
                strName = "Imie" + random.Next(1, 100),
                strScname = "Drugie" + random.Next(1, 100),
                strSname = "Nazwisko" + random.Next(1, 100),
                strPESEL = random.Next(1000000, 99999999).ToString(),
                strBirthday = random.Next(1, 31) + "." + random.Next(1, 12) + "." + random.Next(2000, 2025),
                nPhoneNum = random.Next(111111111, 999999999),


            };
            


            People.Add(newPerson);
        }
    }
}