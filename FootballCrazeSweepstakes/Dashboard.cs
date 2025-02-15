using FootballCrazeSweepstakes.Models;
using Microsoft.Extensions.Options;
using System.ComponentModel.Design;

namespace FootballCrazeSweepstakes
{
    public partial class Dashboard : Form
    {
        private AppSettings _config;
        private readonly IFootballCrazeSweepstakesRepository _footballCrazeSweepstakesRepository;
        private readonly IFootballCrazeSweepstakesService _footballCrazeSweepstakesService;
        public Dashboard(IOptions<AppSettings> config, IOptions<SmtpSettings> smtpSettings, IFootballCrazeSweepstakesRepository footballCrazeSweepstakesRepository, IFootballCrazeSweepstakesService footballCrazeSweepstakesService)
        {
            _config = config.Value;
            _config.SmptSettings = smtpSettings.Value;
            _footballCrazeSweepstakesRepository = footballCrazeSweepstakesRepository;
            _footballCrazeSweepstakesService = footballCrazeSweepstakesService;
            InitializeComponent();
        }
        private void Dashboard_Load(object sender, EventArgs e)
        {
            var mySettings = _config;
            sweepstakeYear.Text = "Sweepstake Year: " + DateTime.Now.Year.ToString();
            ticketsAvailableForSale.Text = "E-Tickets available " + _footballCrazeSweepstakesRepository.AvailableETicketsForSale(DateTime.Now.Year).ToString();
        }

        private void uploadETickets_Click(object sender, EventArgs e)
        {
           var uploadedETickets = _footballCrazeSweepstakesService.UploadETickets(DateTime.Now.Year);
           var eTicketsAdded = _footballCrazeSweepstakesRepository.AddETickets(DateTime.Now.Year, uploadedETickets, new CancellationToken());
           var pdfsDeleted = _footballCrazeSweepstakesService.DeleteETicketPdfFiles();
           ticketsAvailableForSale.Text = "E-Tickets available " + _footballCrazeSweepstakesRepository.AvailableETicketsForSale(DateTime.Now.Year).ToString();
        }

        private void testEmail_Click(object sender, EventArgs e)
        {
            //var purchaseUploads = _footballCrazeSweepstakesService.UploadSweepstakePurchases(DateTime.Now.Year);
            var uploadedPurchases = new List<SweepstakePurchase>();
            
            var purchase =  new SweepstakePurchase()
            {
                Address = "test street",
                City = "Cross Plains",
                CustomerCharged = 20,
                Date = DateTime.Now,
                Email = emailAddress.Text,
                MemberFirstName = "Firstname",
                MemberLastName = "LastName",
                MemberName = "Firstname Lastname",
                PaymentMethod = "Credit Card",
                Phone = "608-555-5555",
                Purchase = "Purchase Tickets " + DateTime.Now.Year,
                Price = 20,
                PurchaseOption = "Purchase 1 ticket",
                Refunded = 0,
                State = "WI",
                TransactionId = "9907136410",
                Status = "Received",
                Zip = "53528"

            };
            uploadedPurchases.Add(purchase);
            //var purchasedETickets =_footballCrazeSweepstakesRepository.SavePurchasedETickets(DateTime.Now.Year, uploadedPurchases, new CancellationToken());
            var purchasedETickets = new List<SweepstakePurchaseETicket>();
            var purchesedETicket = new SweepstakePurchaseETicket()
            {
                SweepstakePurchase = purchase,
                Eticket = _footballCrazeSweepstakesRepository.GetTestETicket(DateTime.Now.Year)

            };
            purchasedETickets.Add(purchesedETicket);
            _footballCrazeSweepstakesService.EmailETickets(_config, purchasedETickets);
        }

        private void emailAddress_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
