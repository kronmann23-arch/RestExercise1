namespace RestExercise1.Models
{
    public class PersonRepository:IPersonRepository 
    {
        private readonly List<Person> persons = new();
        private int nextId = 1;
        public PersonRepository()
        {
           
            {
                // Add some default data
                AddPerson(new Person {  Name = "Alice", Age = 30, Height = 170 });
                AddPerson(new Person {  Name = "Bob", Age = 25, Height = 180 });
                AddPerson(new Person {  Name = "Charlie", Age = 35, Height = 175 }); 
            }
        }
        public Person AddPerson(Person person)
        {
            if (person == null)
            {
                throw new ArgumentNullException(nameof(person));
            }
            person.Id = nextId++;
            persons.Add(person);
            return person;
        }

        public IEnumerable<Person> GetAllPersons(string? nameFilter, int? minAge, int? maxAge, int? minHeight, int? maxHeight)
        {
            IEnumerable<Person> items = persons.AsEnumerable();
          
            if (!string.IsNullOrWhiteSpace(nameFilter))
            {
                items = items.Where(p => p.Name != null && p.Name.Contains(nameFilter, StringComparison.OrdinalIgnoreCase));
            }

            if (minAge.HasValue)
            {
                items = items.Where(p => p.Age >= minAge.Value);
            }

            if (maxAge.HasValue)
            {
                items = items.Where(p => p.Age <= maxAge.Value);
            }
            if (minHeight.HasValue)
            {
                items = items.Where(p => p.Height >= minHeight.Value);
            }

            if (maxHeight.HasValue)
            {
                items = items.Where(p => p.Height <= maxHeight.Value);
            }
            return new List<Person>(items);
        }

        public Person? GetPersonById(int id)
        {
            return persons.FirstOrDefault(p => p.Id == id);
        }

        public Person? RemovePerson(int id)
        {
            Person? existingPerson = GetPersonById(id);
            if (existingPerson == null) return null;

            persons.Remove(existingPerson);
            return existingPerson;
        }

        public Person? UpdatePerson(int id, Person updatedPerson)
        {
            Person? existingPerson = GetPersonById(id);
            if (existingPerson == null) return null;

            existingPerson.Name = updatedPerson.Name;
            existingPerson.Age = updatedPerson.Age;
            existingPerson.Height = updatedPerson.Height;


            return existingPerson;
        }
    }
}

    
        
    

