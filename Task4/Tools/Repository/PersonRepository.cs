using Task4.Models;
using Task4.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Task4.Repository
{
    internal class PersonRepository
    {
        private static readonly string MainFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "MyC#Storage", "MyUsers");
        public PersonRepository()
        {

            if (!Directory.Exists(MainFolder))
            {
                Directory.CreateDirectory(MainFolder);
                for (int i = 0; i < 50; i++)
                {
                    DateTime birthday = new DateTime(1970 + i, (i % 12) + 1, (i % 12) + 1);
                    int age = Person.getAge(birthday);
                    string firstName = i < 10 ? "CustomUser0" + i : "CustomUser" + i;
                    string lastName = i < 10 ? "LastName0" + i : "LastName" + i;
                    _ = AddToRepositoryOrUpdateAsync(new Person(firstName, lastName, i + "@gmail.com",
                        birthday, Person.CalculateIsAdult(age), Person.CalculateSunSign(birthday), Person.CalculateChineseSign(birthday), Person.CalculateIsBirthday(birthday), age));
                }
            }
        }

        public async Task AddToRepositoryOrUpdateAsync(Person person)
        {
            var personInString = JsonSerializer.Serialize(person);
            using (var writer = new StreamWriter(Path.Combine(MainFolder, person.Email), false))
            {
                await writer.WriteAsync(personInString);
            }

        }

        public async Task<Person> GetPersonAsync(string email)
        {
            string personInString = null;
            string path = Path.Combine(MainFolder, email);
            if (!File.Exists(path))
            {
                return null;
            }
            using (var reader = new StreamReader(path))
            {
                personInString = await reader.ReadToEndAsync();
            }

            return JsonSerializer.Deserialize<Person>(personInString);
        }

        public void RemoveFromRepository(Person person)
        {
            File.Delete(Path.Combine(MainFolder, person.Email));
        }

        public List<EditViewModel> GetAllPersons(Action gotoInfo)
        {
            List<EditViewModel> persons = new List<EditViewModel>();
            foreach (var file in Directory.EnumerateFiles(MainFolder))
            {
                string personInString = null;
                using (var reader = new StreamReader(file))
                {
                    personInString = reader.ReadToEnd();
                }
                persons.Add(new EditViewModel(JsonSerializer.Deserialize<Person>(personInString), gotoInfo));
            }
            return persons;
        }

    }
}
