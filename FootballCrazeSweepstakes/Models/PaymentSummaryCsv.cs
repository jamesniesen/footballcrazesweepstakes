using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballCrazeSweepstakes.Models
{
    public class PaymentSummaryCsv
    {
        public DateTime DatePurchased { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required string Email { get; set; }
        public required string Phone { get; set; }
        public required string Address { get; set; }
        public required string City { get; set; }
        public required string State { get; set; }
        public required string Zip { get; set; }
        public required string NumberOfTicketsPurchased { get; set; }
        public required string AmountPaid { get; set; }
        public required string TicketNumbers { get; set; }
    }
}
