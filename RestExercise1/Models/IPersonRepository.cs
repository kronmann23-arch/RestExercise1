using static System.Net.Mime.MediaTypeNames;

namespace RestExercise1.Models
{
    public interface IPersonRepository
    {
        Person AddPerson(Person person);
        IEnumerable<Person> GetAllPersons(string? nameFilter, int? minAge, int? maxAge, int? minHeight, int? maxHeight);
        Person? GetPersonById(int id);
        Person? RemovePerson(int id);
        Person? UpdatePerson(int id, Person updatedPerson);
        
    }
}