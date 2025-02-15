using System.ComponentModel.DataAnnotations;

namespace FootballCrazeSweepstakes.Models
{
    public class ETicket
    {
        public int Id { get; set; }

        [MaxLength(25)]
        public required string TicketNumber { get; set; }

        [MaxLength(100)]
        public required string FileType { get; set; }

        public required byte[] FileData { get; set; }
        public int Year { get; set; }
        public bool Purchased { get; set; }
        public DateTime Created { get; set; }
        public DateTime Modified { get; set; }
    }
}
