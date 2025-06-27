using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class CashDonation : Donation
    {
        public DateTime DonationDate { get; set; }

        public CashDonation(string donorName, decimal amount, DateTime donationDate) : base(donorName, amount)
        {
            DonationDate = donationDate;
        }
        public override void RecordDonation()
        {
            Console.WriteLine($"Cash Donation of ${Amount} from {DonorName} on {DonationDate:yyyy-MM-dd}");
        }
    }
}
