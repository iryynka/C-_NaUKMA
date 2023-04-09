using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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

namespace ProceedUserInfo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Person _person;

        public MainWindow()
        {
            InitializeComponent();
            BProceed.IsEnabled = false;
        }

        private async void BProceed_Click(object sender, RoutedEventArgs e)
        {

            var name = TbName.Text.Trim();
            var surname = TbSurname.Text.Trim();
            var email = TbEmail.Text.Trim();
            DateTime birthDate = DpBirthday.SelectedDate.Value.Date;
           
            _person = new Person(name, surname, email, birthDate);

           
            TbYourName.Text = await Task.Run(() => _person._name.ToString());
            TbYourSurname.Text = await Task.Run(() => _person._surname.ToString());
            TbYourEmail.Text = await Task.Run(() => _person._email.ToString());
            TbYourBirthday.Text = await Task.Run(() => _person._birthDate.ToShortDateString());
            TbAdult.Text = await Task.Run(() => _person.IsAdult.ToString());
            TbChineseSign.Text = await Task.Run(() => _person.ChineseSign.ToString());
            TbZodiacSign.Text = await Task.Run(() => _person.SunSign.ToString());
            TbIsBirthday.Text = await Task.Run(() => _person.IsBirthday.ToString());
            if (birthDate.Day == DateTime.Today.Day && birthDate.Month == DateTime.Today.Month)
            {
                TbWishes.Text = "Happy birthday!";
            }
            else
            {
                TbWishes.Text = "";
            }


        }

        private void TbName_SelectionChanged(object sender, RoutedEventArgs e)
        {
            validate();
        }

        private void TbSurname_SelectionChanged(object sender, RoutedEventArgs e)
        {
            validate();
        }

        private void TbEmail_SelectionChanged(object sender, RoutedEventArgs e)
        {
            validate();
        }

        private void DpBirthday_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            validate();
        }

        private void validate()
        {
            BProceed.IsEnabled = !string.IsNullOrWhiteSpace(TbName.Text) &&
                !string.IsNullOrWhiteSpace(TbSurname.Text) &&
                !string.IsNullOrWhiteSpace(TbEmail.Text) &&
                DpBirthday.SelectedDate.HasValue;
        }
    }
}
