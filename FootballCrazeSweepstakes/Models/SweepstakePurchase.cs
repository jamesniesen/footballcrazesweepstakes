using System.ComponentModel.DataAnnotations;

namespace FootballCrazeSweepstakes.Models
{
    public class SweepstakePurchase
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        [MaxLength(200)]
        public required string MemberName { get; set; }
        [MaxLength(100)]
        public required string MemberFirstName { get; set; }
        [MaxLength(100)]
        public required string MemberLastName { get; set; }
        [MaxLength(200)]
        public required string Purchase { get; set; }
        [MaxLength(200)]
        public required string PurchaseOption { get; set; }
        public decimal Price { get; set; }
        public decimal CustomerCharged { get; set; }
        public decimal Refunded { get; set; }
        [MaxLength(50)]
        public required string PaymentMethod { get; set; }
        public required string TransactionId { get; set; }
        [MaxLength(50)]
        public required string Status { get; set; }
        [MaxLength(100)]
        public required string Email { get; set; }
        [MaxLength(50)]
        public required string Phone { get; set; }
        [MaxLength(200)]
        public required string Address { get; set; }
        [MaxLength(50)]
        public required string City { get; set; }
        [MaxLength(50)]
        public required string State { get; set; }
        [MaxLength(25)]
        public required string Zip { get; set; }
        public DateTime? TicketEmailedOn { get; set; }
    }
}
