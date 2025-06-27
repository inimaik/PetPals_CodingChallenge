using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class ItemDonation : Donation
    {
        public string ItemType { get; set; }

        public ItemDonation(string donorName, decimal amount, string itemType) : base(donorName, amount)
        {
            ItemType = itemType;
        }

        public override void RecordDonation()
        {
            Console.WriteLine($"Item donation of {ItemType} is done by {DonorName}");
        }
    }
}
