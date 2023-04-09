using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Xml.Linq;

namespace ProceedUserInfo
{
    class Person
    {
        public string _name { get; }
        public string _surname { get; }
        public string _email { get; }
        public DateTime _birthDate { get; }
        public bool IsAdult => CalculateIsAdult();
        public string SunSign => CalculateSunSign();
        public string ChineseSign => CalculateChineseSign();
        public bool IsBirthday => CalculateIsBirthday();



        public Person(string name, string surname, string email, DateTime birthDate)
        {
            _name = name;
            _surname = surname;
            _email = email;
            _birthDate = birthDate;
        }

        public Person(string name, string surname, string email)
        {
            _name = name;
            _surname = surname;
            _email = email;
            _birthDate = DateTime.MinValue;
        }

        public Person(string name, string surname, DateTime birthDate)
        {
            _name = name;
            _surname = surname;
            _email = string.Empty;
            _birthDate = birthDate;
        }



        private bool CalculateIsAdult()
        {
            DateTime dateNow = DateTime.Today;
            DateTime selectedDate = _birthDate;
            if (dateNow < selectedDate)
            {
                MessageBox.Show("Invalid selected date.");
                return false;
            }
            else
            {
                int age = new DateTime(dateNow.Subtract((System.DateTime)selectedDate).Ticks).Year - 1;
                if (age <= 135)
                {
                    if (age >= 18)
                    {
                        return true;
                    }
                    return false;
                } else
                {

                    MessageBox.Show("Are you still alive?");
                    return false;
                }
            }


        }

        private string CalculateSunSign()
        {
            int month = _birthDate.Month;
            int day = _birthDate.Day;
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



        private string CalculateChineseSign()
        {
            int year = _birthDate.Year;
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


        private bool CalculateIsBirthday()
        {
            if (_birthDate.Day == DateTime.Today.Day && _birthDate.Month == DateTime.Today.Month)
                return true;
            return false;
        }


    }
}
