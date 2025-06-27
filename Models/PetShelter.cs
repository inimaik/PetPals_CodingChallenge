using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class PetShelter
    {
        private List<Pet> availablePets = new List<Pet>();

        public void AddPet(Pet pet)
        {
            availablePets.Add(pet);
        }
        public void RemovePet(Pet pet)
        {
            availablePets.Remove(pet);
        }
        public void ListAvailablePets()
        {
            foreach (var pet in availablePets)
            {
                Console.WriteLine(pet);
            }
        }
    }
}
