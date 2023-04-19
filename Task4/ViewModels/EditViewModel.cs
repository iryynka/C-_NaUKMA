using Task4.Exeptions;
using Task4.Models;
using Task4.Navigations;
using Task4.Repository;
using Task4.Tools;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Task4.ViewModels
{
    internal class EditViewModel : INotifyPropertyChanged, INavigatable
    {
        #region Fields
        private Person person;
        private static PersonRepository PersonFileRepository = new PersonRepository();
        private RelayCommand<object>? cancelCommand;
        private RelayCommand<object>? gotoInfoCommand;
        private Action gotoInfo;
        private DateTime changedBirthday = DateTime.Today;
        public event PropertyChangedEventHandler? PropertyChanged;

        public NavigationTypes ViewType => NavigationTypes.Redactor;
        #endregion

        #region Properties
        public Person Person
        {
            get { return person; }
        }
        public string ChangedFirstName
        {
            get;
            set;
        }
        public string ChangedLastName
        {
            get;
            set;
        }
        public DateTime ChangedBirthday
        {
            get { return changedBirthday; }
            set { changedBirthday = value; }
        }
      
        public string FirstName
        {
            get { return person.FirstName; }
        }
        public string LastName
        {
            get { return person.LastName; }
        }
        public string Email
        {
            get { return person.Email; }
        }
        public DateTime Birthday
        {
            get { return person.Birthday; }
        }
        public bool IsAdult
        {
            get { return person.IsAdult; }
        }
        public bool IsBirthday
        {
            get { return person.IsBirthday; }
        }
        public string ChineseSign
        {
            get { return person.ChineseSign; }

        }
        public string SunSign
        {
            get { return person.SunSign; }
        }
        public string BirthdayInString
        {
            get { return person.BirthdayInString; }
        }
        public EditViewModel(Person person, Action gotoInfo)
        {
            this.person = person;
            this.gotoInfo = gotoInfo;
        }
        #endregion

        private async Task Proceed()
        {
            bool isAdult;
            string sunSign;
            string chineseSign;
            bool isBirthday;
            Task<int> t = Task.Run(() => Person.getAge(ChangedBirthday));
            int age = await t;
            Task<bool> t1 = Task.Run(() => Person.CalculateIsAdult(age));
            Task<string> t2 = Task.Run(() => Person.CalculateSunSign(ChangedBirthday));
            Task<string> t3 = Task.Run(() => Person.CalculateChineseSign(ChangedBirthday));
            Task<bool> t4 = Task.Run(() => Person.CalculateIsBirthday(ChangedBirthday));
            isAdult = await t1;
            sunSign = await t2;
            chineseSign = await t3;
            isBirthday = await t4;
            try
            {
                Person changedPerson = new Person(ChangedFirstName, ChangedLastName, Email, ChangedBirthday, isAdult, sunSign, chineseSign, isBirthday, age);
                person = changedPerson;
            }
            catch (WrongEmail ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
                return;
            }
            catch (AgeInPast ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
                return;
            }
            catch (AgeInFuture ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
                return;
            }
            await PersonFileRepository.AddToRepositoryOrUpdateAsync(person);
            gotoInfo.Invoke();
        }
        public RelayCommand<object> CancelCommand
        {
            get
            {
                return cancelCommand ??= new RelayCommand<object>(o => Cancel());
            }
        }
        public RelayCommand<object> GotoInfoCommand
        {
            get
            {
                return gotoInfoCommand ??= new RelayCommand<object>(_ => Proceed(), CanExecute);
            }
        }
        private void Cancel()
        {
            gotoInfo.Invoke();
        }
        private bool CanExecute(object o)
        {
            return !String.IsNullOrWhiteSpace(ChangedFirstName) && !String.IsNullOrWhiteSpace(ChangedLastName);
        }
    }
}
