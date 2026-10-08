namespace RestExercise1.Models
{
    public class Person
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; } 
        public int Height { get; set; }
        override public string ToString()
        {
            return $"Name: {Name}, Age: {Age}, Height: {Height}";
        }

    }

} 
