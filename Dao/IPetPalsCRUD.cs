using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dao
{
    public interface IPetPalsCRUD
    {
        List<Pet> DisplayAvailablePets();
        void AddPet(Pet pet);
        void Adopt(int participantId, int petId);
        void RecordCashDonation(CashDonation donation);
        List<AdoptionEvent> GetUpcomingAdoptionEvents();
        void RegisterEventParticipant(Participant participantName);
        void ReadPetDataFromFile(string filePath);
    }
}
