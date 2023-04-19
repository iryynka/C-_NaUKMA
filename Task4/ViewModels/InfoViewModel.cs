using Task4.Navigations;
using Task4.Repository;
using Task4.Tools;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Task4.ViewModels
{
    internal class InfoViewModel : INavigatable, INotifyPropertyChanged
    {
        #region Fields
        public NavigationTypes ViewType => NavigationTypes.Info;
        private Action gotoLogin;
        private Action gotoInfo;
        private string textToFilter;
        private Action<EditViewModel> gotoPerson;
        private RelayCommand<object>? gotoLoginCommand;
        private RelayCommand<object>? changeSelectedCommand;
        private RelayCommand<object>? exitCommand;
        private RelayCommand<object>? removePersonCommand;
        public event PropertyChangedEventHandler? PropertyChanged;
        private static PersonRepository personFileRepository;
        private static ObservableCollection<EditViewModel> people;
        private static ObservableCollection<EditViewModel> gridPeople;
        #endregion

        #region Properties

        public ObservableCollection<EditViewModel> People
        {
            get
            {
                return people;
            }
            set
            {
                people = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<EditViewModel> GridPeople
        {
            get
            {
                return gridPeople;
            }
            set
            {
                gridPeople = value;
                OnPropertyChanged();
            }
        }
        public EditViewModel SelectedPerson
        {
            get;
            set;
        }
        public RelayCommand<object> GotoLoginCommand
        {
            get
            {
                return gotoLoginCommand ??= new RelayCommand<object>(_ => GotoLogin());
            }
        }
        public RelayCommand<object> ChangeSelectedCommand
        {
            get
            {
                return changeSelectedCommand ??= new RelayCommand<object>(_ => GoToChangingWindow(), CanExecuteEditOrRemoveSelected);
            }
        }
        public RelayCommand<object> ExitCommand
        {
            get
            {
                return exitCommand ??= new RelayCommand<object>(o => Close());
            }
        }

        public RelayCommand<object> RemovePersonCommand
        {
            get
            {
                return removePersonCommand ??= new RelayCommand<object>(o => RemovePerson(), CanExecuteEditOrRemoveSelected);
            }
        }
        private void GotoLogin()
        {
            gotoLogin.Invoke();
        }

        public InfoViewModel(Action gotoLogin, Action<EditViewModel> gotoPerson, Action gotoInfo)
        {
            this.gotoLogin = gotoLogin;
            this.gotoPerson = gotoPerson;
            this.gotoInfo = gotoInfo;
            personFileRepository = new PersonRepository();
            people = new ObservableCollection<EditViewModel>(personFileRepository.GetAllPersons(gotoInfo));
            gridPeople = new ObservableCollection<EditViewModel>(personFileRepository.GetAllPersons(gotoInfo));
        }
        #endregion
        public static void AddOnePerson(EditViewModel person)
        {
            if (people != null)
            {
                people.Add(person);
                gridPeople.Add(person);
            }
        }
        public void GoToChangingWindow()
        {
            gotoPerson.Invoke(SelectedPerson);
        }

        private void Close()
        {
            Environment.Exit(0);
        }
        private async Task RemovePerson()
        {
            if (SelectedPerson != null)
            {
                await Task.Run(() => personFileRepository.RemoveFromRepository(SelectedPerson.Person));
                People.Remove(SelectedPerson);
                OnPropertyChanged(nameof(people));
                GridPeople.Remove(SelectedPerson);
                OnPropertyChanged(nameof(gridPeople));
            }
        }
        private bool CanExecuteEditOrRemoveSelected(object o)
        {
            return SelectedPerson != null;
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public string TextToFilter
        {
            get { return textToFilter; }
            set
            {
                if (textToFilter != value)
                {
                    textToFilter = value;
                    OnPropertyChanged();

                    if (string.IsNullOrWhiteSpace(textToFilter))
                    {
                        // If the filter text is empty or whitespace, show all people
                        GridPeople = new ObservableCollection<EditViewModel>(People);
                    }
                    else
                    {
                        // Otherwise, filter the people by surname
                        GridPeople = new ObservableCollection<EditViewModel>(
                            from person in People
                            where person.LastName.Contains(textToFilter, StringComparison.OrdinalIgnoreCase)
                            select person);
                    }
                }
            }
        }
    }
}
