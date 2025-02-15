namespace FootballCrazeSweepstakes.Models
{
    public class Error
    {
        public int Id { get; set; }
        public required string Process { get; set; }
        public required string Message    { get; set; }
        public DateTime Date { get; set; }
        public bool Tracked { get; set; }

    }
}
