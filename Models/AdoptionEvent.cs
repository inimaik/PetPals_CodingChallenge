using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class AdoptionEvent : IAdoptable
    {
        public int AdoptionEventId { get; set; }
        public string EventName { get; set; }
        public DateTime EventDate { get; set; }
        public List<IAdoptable> Participants { get; set; }
        public static void HostEvent()
        {
            Console.WriteLine("Adoption Event Hosted!");
            Console.WriteLine("Participants are welcome to adopt pets now.");
        }
        public void RegisterParticipant(IAdoptable participant)
        {
            Participants.Add(participant);
        }
        public void Adopt()
        {
            Console.WriteLine("Pet adopted");
        }
    }
}
//        Attributes:
//• Participants(List of IAdoptable) : A list of participants(shelters and adopters) in the adoption
//event.
//Methods:
//• HostEvent(): Hosts the adoption event.
//• RegisterParticipant(IAdoptable participant): Registers a participant for the event.
