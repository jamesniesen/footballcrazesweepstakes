using FootballCrazeSweepstakes.Models;

namespace FootballCrazeSweepstakes
{
    public interface IFootballCrazeSweepstakesService
    {
        IEnumerable<ETicket> UploadETickets(int year);
        int DeleteETicketPdfFiles();
        void EmailETickets(AppSettings _config, List<SweepstakePurchaseETicket> purchasedTickets);
        List<SweepstakePurchase> UploadSweepstakePurchases(int year);
            
    }
}
