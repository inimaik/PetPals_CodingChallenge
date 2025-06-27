using Dao;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetPals
{
    internal class Program
    {
        static void Main(string[] args)
        {
            PetPalsCRUD petsPals=new PetPalsCRUD();
            bool exit = false;

            while (!exit)
            {
                Console.WriteLine("\n==== PetPals Adoption Platform ====");
                Console.WriteLine("1. View Upcoming Adoption Events");
                Console.WriteLine("2. Register for Adoption Event");
                Console.WriteLine("3. Add a Pet");
                Console.WriteLine("4. Display Pets available for Adoption");
                Console.WriteLine("5. Adopt Pets in the Event");
                Console.WriteLine("6. Record Cash Donations");
                Console.WriteLine("7. Read Pet Data from file");
                Console.WriteLine("8. Exit");
                Console.Write("Choose an option: ");
                string input = Console.ReadLine();

                switch (input)
                {
                    case "1":
                        Console.WriteLine("=== Upcoming Adoption Events ===");

                        var upcomingEvents = petsPals.GetUpcomingAdoptionEvents();

                        if (upcomingEvents.Any())
                        {
                            foreach (var ev in upcomingEvents)
                            {
                                Console.WriteLine($"Event ID: {ev.AdoptionEventId}, Name: {ev.EventName}, Date: {ev.EventDate.ToShortDateString()}");
                            }
                        }
                        else
                        {
                            Console.WriteLine("No upcoming events found.");
                        }
                        break;
                    case "2":
                        Console.WriteLine("=== Register for Adoption Event ===");

                        Console.Write("Enter your name: ");
                        string name = Console.ReadLine();

                        Console.Write("Enter the Adoption Event ID to register for: ");
                        if (!int.TryParse(Console.ReadLine(), out int eventId))
                        {
                            Console.WriteLine("Invalid event ID input.");
                            return;
                        }

                        Participant participant = new Participant
                        {
                            Name = name,
                            AdoptionEventId = eventId
                        };
                        petsPals.RegisterEventParticipant(participant);
                        break;
                    case "3":
                        Console.WriteLine("=== Pet Addition Test ===");
                        Console.WriteLine("Choose pet type: 1. Dog  2. Cat");
                        string choice = Console.ReadLine();

                        Console.Write("Enter name: ");
                        string petname = Console.ReadLine();

                        Console.Write("Enter age: ");
                        if (!int.TryParse(Console.ReadLine(), out int age))
                        {
                            Console.WriteLine("Invalid age input.");
                            return;
                        }

                        Console.Write("Enter breed: ");
                        string breed = Console.ReadLine();

                        Pet pet = null;

                        if (choice == "1")
                        {
                            Console.Write("Enter dog breed: ");
                            string dogBreed = Console.ReadLine();
                            pet = new Dog(petname, age, breed, dogBreed);
                        }
                        else if (choice == "2")
                        {
                            Console.Write("Enter cat color: ");
                            string catColor = Console.ReadLine();
                            pet = new Cat(petname, age, breed, catColor);
                        }
                        else
                        {
                            Console.WriteLine("Invalid choice.");
                            return;
                        }
                        petsPals.AddPet(pet);
                        break;
                    case "4":
                        Console.WriteLine("=== List of Available Pets ===");
                        List<Pet> pets = petsPals.DisplayAvailablePets();
                        if (pets.Count > 0)
                        {
                            foreach (var eachPet in pets)
                            {
                                Console.WriteLine($"\nPet ID: {eachPet.PetId}");
                                Console.WriteLine($"Name: {eachPet.Name}");
                                Console.WriteLine($"Age: {eachPet.Age}");
                                Console.WriteLine($"Breed: {eachPet.Breed}");
                            }
                        }
                        else
                        {
                            Console.WriteLine("No pets available for adoption at the moment.");
                        }
                        break;
                    case "5":
                        Console.WriteLine("=== Adopt a Pet ===");
                        AdoptionEvent.HostEvent();
                        Console.Write("Participant ID: ");
                        int participantId = int.Parse(Console.ReadLine());
                        Console.Write("Pet ID to adopt: ");
                        int petId = int.Parse(Console.ReadLine());

                        petsPals.Adopt(participantId, petId);
                        break;
                    case "6":
                        Console.WriteLine("=== Cash Donation ===");

                        Console.Write("Enter your name: ");
                        string donorName = Console.ReadLine();

                        Console.Write("Enter donation amount: ");
                        if (!decimal.TryParse(Console.ReadLine(), out decimal amount))
                        {
                            Console.WriteLine("Invalid amount input.");
                            return;
                        }
                        CashDonation donation = new CashDonation(donorName, amount, DateTime.Now);
                        petsPals.RecordCashDonation(donation);
                        break;
                    case "7":
                        Console.WriteLine("=== Read From File ===");
                        Console.Write("Enter the full path to the pet data file: ");
                        string path = Console.ReadLine();
                        petsPals.ReadPetDataFromFile(path);
                        break;
                    case "8":
                        exit = true;
                        Console.WriteLine("Exiting...");
                        break;

                    default:
                        Console.WriteLine("Invalid option. Try again.");
                        break;
                }
            }           
        }
    }
}
