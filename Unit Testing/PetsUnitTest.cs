using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dao;
using Models;
using MyExceptions;
using NUnit.Framework;

namespace Unit_Testing
{
    public class PetsUnitTest
    {
        private PetPalsCRUD crud;
        [SetUp]
        public void Setup()
        {
            crud = new PetPalsCRUD();
        }
        [Test]
        public void InvalidAgeExceptionCheck(){
        Pet invalidPet = new Dog("TestDog", -2, "Bulldog", "Bulldog");

        // Act & Assert
        Assert.Throws<InvalidPetAgeException>(() =>
        {
            crud.AddPet(invalidPet);
        });
        }
        [Test]
        public void Adopt_InvalidPetId_ThrowsAdoptionException()
        {
            PetPalsCRUD crud = new PetPalsCRUD();
            Assert.Throws<AdoptionException>(() =>
            {
                crud.Adopt(9999, 1); 
            });
        }
    }
}
