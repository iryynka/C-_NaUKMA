using System;
using System.Collections.Generic;
using System.Linq;
using System.Printing;
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

namespace BirthdayDate
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private string ZodiacSign(DateTime date)
        {
            int month = date.Month;
            int day = date.Day;
            switch (month)
            {
                case 1:
                    if (day < 21)
                    {
                        return "Capricorn";
                    }
                    else
                    {
                        return "Aquarius";
                    }
                case 2:
                    if (day < 21)
                    {
                        return "Aquarius";
                    }
                    else
                    {
                        return "Pisces";
                    }
                case 3:
                    if (day < 21)
                    {
                        return "Pisces";
                    }
                    else
                    {
                        return "Aries";
                    }
                case 4:
                    if (day < 21)
                    {
                        return "Aries";
                    }
                    else
                    {
                        return "Taurus";
                    }
                case 5:
                    if (day < 22)
                    {
                        return "Taurus";
                    }
                    else
                    {
                        return "Gemini";
                    }
                case 6:
                    if (day < 22)
                    {
                        return "Gemini";
                    }
                    else
                    {
                        return "Cancer";
                    }
                case 7:
                    if (day < 23)
                    {
                        return "Cancer";
                    }
                    else
                    {
                        return "Leo";
                    }
                case 8:
                    if (day < 24)
                    {
                        return "Leo";
                    }
                    else
                    {
                        return "Virgo";
                    }
                case 9:
                    if (day < 23)
                    {
                        return "Virgo";
                    }
                    else
                    {
                        return "Libra";
                    }
                case 10:
                    if (day < 24)
                    {
                        return "Libra";
                    }
                    else
                    {
                        return "Scorpio";
                    }
                case 11:
                    if (day < 23)
                    {
                        return "Scorpio";
                    }
                    else
                    {
                        return "Sagittarius";
                    }
                case 12:
                    if (day < 22)
                    {
                        return "Sagittarius";
                    }
                    else
                    {
                        return "Capricorn";
                    }
            }
            return "Not detected";
        }

        private string ChineseZodiac(DateTime date)
        {
            int year = date.Year;
            switch (year % 12)
            {
                case 0: return "Monkey";
                case 1: return "Rooster";
                case 2: return "Dog";
                case 3: return "Pig";
                case 4: return "Rat";
                case 5: return "Ox";
                case 6: return "Tiger";
                case 7: return "Rabbit";
                case 8: return "Dragon";
                case 9: return "Snake";
                case 10: return "Horse";
                case 11: return "Goat";
            }
            return "Not detected";
        }

        private bool CompareDates(DateTime date1, DateTime date2)
        {
            if (date1.Day == date2.Day && date1.Month == date2.Month)
                return true;
            return false;

        }

        private void BCalculate_Click(object sender, RoutedEventArgs e)
        {
            DateTime dateNow = DateTime.Today;
            DateTime? selectedDate = DpBirthday.SelectedDate;
            if (selectedDate.HasValue)
            {
                if (selectedDate > dateNow)
                {
                    MessageBox.Show("Invalid selected date.");
                }
                else
                {
                    int years = new DateTime(dateNow.Subtract((System.DateTime)selectedDate).Ticks).Year - 1;
                    if (years <= 135)
                    {
                        TbAge.Text = Convert.ToString(years) + " y. o.";
                        if (CompareDates(dateNow, (System.DateTime)selectedDate))
                        {
                            TbWishes.Text = "Happy birthday!";
                        } else
                        {
                            TbWishes.Text = "";
                        }
                        TbChineseSign.Text = ChineseZodiac((System.DateTime)selectedDate);
                        TbZodiacSign.Text = ZodiacSign((System.DateTime)selectedDate);
                        return;
                    }
                    else
                    {
                        MessageBox.Show("Are you still alive?");
                    }

                }
                return;
            }
            else
            {
                MessageBox.Show("Enter your birthday date.");
                return;
            }
        }

        private void BExit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
