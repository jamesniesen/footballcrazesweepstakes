namespace FootballCrazeSweepstakes
{
    public class AppSettings
    {
        public required string AthleticsEmailAddress { get; set; }
        public  required string AthleticsPhone { get; set; }
        public required string SweepstakesLink { get; set; }
        public required SmtpSettings SmptSettings { get; set; }

    }
    public class SmtpSettings
    {
        public required string Address { get; set; }
        public required int PortNumber { get; set; }
        public required  string UserName { get; set; }
        public required string Password { get; set; }
        public required string SendTicketEmailFrom { get; set; }
    }
}