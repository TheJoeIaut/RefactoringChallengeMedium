using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LegacyApp
{
    public static class PersonRepository
    {
        public static List<Person> InMemoryPersons { get; set; } = new List<Person>();

        public static void SavePerson(Person person)
        {
            InMemoryPersons.Add(person);
        }
    }

    public class PersonRepositoryAccess: IPersonRepository
    {
        public void SavePerson(Person person)
        {
            PersonRepository.SavePerson(person);
        }
    }

    public interface IPersonRepository
    {
        void SavePerson(Person person);
    }
}
