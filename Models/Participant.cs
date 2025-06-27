using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Participant
    {
        public int ParticipantId { get; set; }
        public string Name { get; set; }
        [ForeignKey("AdoptionEvent")]
        public int AdoptionEventId { get; set; }
        public AdoptionEvent AdoptionEvent { get; set; }
        [ForeignKey("pets")]
        public int? PetId {  get; set; }
        public Pet pets { get; set; }
    }
}
