namespace FootballCrazeSweepstakes.Models
{
    public class SweepstakePurchaseETicket
    {
        public int Id { get; set; }
        public int SweepstakePurchaseId { get; set; }
        public int ETicketId { get; set; }
        public int Year { get; set; }

        // Navigation
        public virtual required SweepstakePurchase SweepstakePurchase { get; set; }
        public virtual required ETicket Eticket { get; set; }
    }
}
