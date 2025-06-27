using Models;
using MyExceptions;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils;

namespace Dao
{
    public class PetPalsCRUD : IPetPalsCRUD
    {
        //connection string
        static string connectionString = DbPropertyUtil.GetConnectionString();
        static SqlConnection con = DbConnUtil.GetConnectionObject(connectionString);

        //fetch all available pets from pets table in a list...catch NullReferenceException
        public List<Pet> DisplayAvailablePets()
        {
            List<Pet> availablePets = new List<Pet>();
            try
            {
                con.Open();

                SqlCommand cmd = new SqlCommand("SELECT PetId, Name, Age, Breed FROM Pets WHERE Adopted = 0", con);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    try
                    {
                        int petId = (int)reader["PetId"];
                        string name = reader["Name"].ToString();
                        int age = (int)reader["Age"];
                        string breed = reader["Breed"].ToString();

                        Pet pet = new Pet(name, age, breed)
                        {
                            PetId = petId
                        };

                        availablePets.Add(pet);
                    }
                    catch (NullReferenceException ex)
                    {
                        Console.WriteLine("Pet information is missing: " + ex.Message);
                    }
                }

                reader.Close();
            }
            catch (SqlException ex)
            {
                Console.WriteLine("Database error: " + ex.Message);
            }
            finally
            {
                con.Close();
            }

            return availablePets;
        }

        //insert data into donations table...user input donor information and donation amount...custom exception Insufficient Fund

        public void RecordCashDonation(CashDonation donation)
        {
            try
            {
                if (donation.Amount < 10)
                    throw new InsufficientFundsException("Minimum donation is $10.");

                con.Open();
                SqlCommand cmd = new SqlCommand(
                    "INSERT INTO Donations (DonorName, Amount, DonationDate) VALUES (@name, @amount, @date)", con);
                cmd.Parameters.AddWithValue("@name", donation.DonorName);
                cmd.Parameters.AddWithValue("@amount", donation.Amount);
                cmd.Parameters.AddWithValue("@date", donation.DonationDate);
                cmd.ExecuteNonQuery();

                Console.WriteLine("Donation recorded.");
            }
            catch (InsufficientFundsException ife)
            {
                Console.WriteLine("Donation Error: " + ife.Message);
            }
            catch (SqlException ex)
            {
                Console.WriteLine("Database error: " + ex.Message);
            }
            finally
            {
                con.Close();
            }
        }
        //fetch upcoming adoption events from adoptionEvents table
        public List<AdoptionEvent> GetUpcomingAdoptionEvents()
        {
            AdoptionEventContext context= new AdoptionEventContext();
            try
            {
                    DateTime today = DateTime.Today;
                    var events = context.AdoptionEvents
                        .Where(e => e.EventDate >= today)
                        .OrderBy(e => e.EventDate)
                        .ToList();

                    if (!events.Any())
                    {
                        Console.WriteLine("No upcoming events found.");
                    }

                    return events;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error retrieving events: " + ex.Message);
                return new List<AdoptionEvent>();
            }
        }

        //insert into participants table..user input details to participate in event
        public void RegisterEventParticipant(Participant participantName)
        {
            AdoptionEventContext context = new AdoptionEventContext();
            try
            {
                var existingEvent = context.AdoptionEvents.FirstOrDefault(e => e.AdoptionEventId == participantName.AdoptionEventId);

                    if (existingEvent == null)
                    {
                        Console.WriteLine("Event not found.");
                        return;
                    }

                    var participant = new Participant
                    {
                        Name = participantName.Name,
                        AdoptionEventId = participantName.AdoptionEventId
                    };

                    context.Participants.Add(participant);
                    context.SaveChanges();

                    Console.WriteLine("Participant registered successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Unexpected error: " + ex.Message);
            }

        }
        public void Adopt(int participantId, int petId)
        {
            try
            {
                con.Open();

                // Check pet adoption status
                SqlCommand checkCmd = new SqlCommand("SELECT Adopted FROM Pets WHERE PetId = @petId", con);
                checkCmd.Parameters.AddWithValue("@petId", petId);
                object result = checkCmd.ExecuteScalar();

                if (result == null)
                    throw new AdoptionException("Pet not found.");

                int adoptedValue = Convert.ToInt32(result);
                if (adoptedValue == 0)
                    throw new AdoptionException("Pet has already been adopted.");

                // Update pet status
                SqlCommand updatePetCmd = new SqlCommand("UPDATE Pets SET Adopted = 1 WHERE PetId = @petId", con);
                updatePetCmd.Parameters.AddWithValue("@petId", petId);
                updatePetCmd.ExecuteNonQuery();

                SqlCommand checkParticipantCmd = new SqlCommand("SELECT COUNT(*) FROM Participants WHERE ParticipantId = @participantId", con);
                checkParticipantCmd.Parameters.AddWithValue("@participantId", participantId);
                int count = (int)checkParticipantCmd.ExecuteScalar();


                if (count == 0)
                    throw new AdoptionException("Participant not found! Pet adoption failed.");

                SqlCommand updateParticipantCmd = new SqlCommand("UPDATE Participants SET PetId = @petId WHERE ParticipantId = @participantId", con);
                updateParticipantCmd.Parameters.AddWithValue("@petId", petId);
                updateParticipantCmd.Parameters.AddWithValue("@participantId", participantId);
                updateParticipantCmd.ExecuteNonQuery();
            }
            //To try unit testing for exception..remove catch block
            catch(AdoptionException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (SqlException ex)
            {
                throw new AdoptionException("Database error while updating pet: " + ex.Message);
            }
            finally
            {
                con.Close();
            }
        }

        public void AddPet(Pet pet)
        {
            try
            {
                if (pet.Age <= 0)
                    throw new InvalidPetAgeException("Pet age must be a positive number.");

                con.Open();
                SqlCommand cmd = new SqlCommand(
                    "INSERT INTO Pets (Name, Age, Breed) OUTPUT INSERTED.PetId VALUES (@name, @age, @breed)", con);
                cmd.Parameters.AddWithValue("@name", pet.Name);
                cmd.Parameters.AddWithValue("@age", pet.Age);
                cmd.Parameters.AddWithValue("@breed", pet.Breed);
                int petId = (int)cmd.ExecuteScalar(); //getting pet id from database since it is auto incremented

                if (pet is Dog dog)
                {
                    SqlCommand dogCmd = new SqlCommand("INSERT INTO Dogs (DogId, DogBreed) VALUES (@id, @breed)", con);
                    dogCmd.Parameters.AddWithValue("@id", petId);
                    dogCmd.Parameters.AddWithValue("@breed", dog.DogBreed);
                    dogCmd.ExecuteNonQuery();
                }
                else if (pet is Cat cat)
                {
                    SqlCommand catCmd = new SqlCommand("INSERT INTO Cats (CatId, CatColor) VALUES (@id, @color)", con);
                    catCmd.Parameters.AddWithValue("@id", petId);
                    catCmd.Parameters.AddWithValue("@color", cat.CatColor);
                    catCmd.ExecuteNonQuery();
                }

                Console.WriteLine("Pet added successfully!");
            }
            catch (InvalidPetAgeException ex)
            {
                Console.WriteLine("Invalid input: " + ex.Message);
            }
            catch (SqlException ex)
            {
                Console.WriteLine("Database error: " + ex.Message);
            }
            finally
            {
                con.Close();
            }
        }
        public void ReadPetDataFromFile(string filePath)
        {
            try
            {
                Console.WriteLine($"Reading pets from file: {filePath}\n");

                // Using File class
                string[] lines = File.ReadAllLines(filePath);

                foreach (string line in lines)
                {
                    Console.WriteLine("Pet Info: " + line);
                }
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("Error: The file was not found.");
            }
            catch (IOException ex)
            {
                Console.WriteLine("I/O Error while reading the file: " + ex.Message);
            }
        }
    }
}



