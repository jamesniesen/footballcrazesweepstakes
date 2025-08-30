using FootballCrazeSweepstakes.Models;

namespace FootballCrazeSweepstakes
{
    public interface IFootballCrazeSweepstakesService
    {
        IEnumerable<ETicket> UploadETickets(int year);
        int DeleteETicketPdfFiles();
        int DeleteGemsPaymentExport();
        void EmailETickets(AppSettings _config, List<SweepstakePurchaseETicket> purchasedTickets);
        List<PaymentSummaryCsv> GetPaymentSummaryInfo(List<SweepstakePurchase> purchases, List<SweepstakePurchaseETicket> purchasedTickets);
        void EmailNewPurchasesSummary(AppSettings _config, List<PaymentSummaryCsv> csvData);

    }
}
