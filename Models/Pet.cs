using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Pet
    {
         public int PetId { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public string Breed { get; set; }
        public bool Adopted { get; set; } = false;

        public Pet(string name, int age, string breed)
        {
            Name = name;
            Age = age;
            Breed = breed;
        }
        //Age must be positive when adding..handle this excepion..maybe in petshelter class
        public override string ToString()
        {
            return $"Name: {Name}, Age: {Age}, Breed: {Breed}";
        }
    }
}
