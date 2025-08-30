using FootballCrazeSweepstakes.Models;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Diagnostics;

namespace FootballCrazeSweepstakes
{
    public partial class Dashboard : Form
    {
        private AppSettings _config;
        private readonly ILogger<Dashboard> _logger;
        private readonly IFootballCrazeSweepstakesRepository _footballCrazeSweepstakesRepository;
        private readonly IFootballCrazeSweepstakesService _footballCrazeSweepstakesService;
        public Dashboard(IOptions<AppSettings> config, ILogger<Dashboard> logger, IOptions<SmtpSettings> smtpSettings, IFootballCrazeSweepstakesRepository footballCrazeSweepstakesRepository, IFootballCrazeSweepstakesService footballCrazeSweepstakesService)
        {
            _config = config.Value;
            _logger = logger;
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
            uploadETickets.Visible = false;
            var uploadedETickets = _footballCrazeSweepstakesService.UploadETickets(DateTime.Now.Year);
            var eTicketsAdded = _footballCrazeSweepstakesRepository.AddETickets(DateTime.Now.Year, uploadedETickets, new CancellationToken());
            var pdfsDeleted = _footballCrazeSweepstakesService.DeleteETicketPdfFiles();
            ticketsAvailableForSale.Text = "E-Tickets available " + _footballCrazeSweepstakesRepository.AvailableETicketsForSale(DateTime.Now.Year).ToString();
            uploadETickets.Visible = true;
        }

        private void testEmail_Click(object sender, EventArgs e)
        {
            testEmail.Visible = false;
            var uploadedPurchases = new List<SweepstakePurchase>();

            var purchase = new SweepstakePurchase()
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
            var purchasedETickets = new List<SweepstakePurchaseETicket>();
            var purchesedETicket = new SweepstakePurchaseETicket()
            {
                SweepstakePurchase = purchase,
                Eticket = _footballCrazeSweepstakesRepository.GetTestETicket(DateTime.Now.Year)

            };
            purchasedETickets.Add(purchesedETicket);
            _footballCrazeSweepstakesService.EmailETickets(_config, purchasedETickets);
            testEmail.Visible = true;

        }

        private void emailAddress_TextChanged(object sender, EventArgs e)
        {

        }

        private void instructionsClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            var doc = AppDomain.CurrentDomain.BaseDirectory + $@"App_Data\FootballCrazeSweepstakesWorkflow.docx";
            Process.Start(new ProcessStartInfo { FileName = doc, UseShellExecute = true });
        }

        private void linkToLog_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            var logfile = AppDomain.CurrentDomain.BaseDirectory + $@"App_Data\\errorlog.txt";
            Process.Start(new ProcessStartInfo { FileName = logfile, UseShellExecute = true });
        }

        private void uploadGemPurchases_Click(object sender, EventArgs e)
        {
            var sweepstakePurchases = _footballCrazeSweepstakesRepository.SaveSweepstakePurchases(DateTime.Now.Year);
            var purchasedETickets = _footballCrazeSweepstakesRepository.SavePurchasedETickets(DateTime.Now.Year, sweepstakePurchases);
            if (purchasedETickets.Any())
            {
                _footballCrazeSweepstakesService.EmailETickets(_config, purchasedETickets);
            }
            var deletetempFile = _footballCrazeSweepstakesService.DeleteGemsPaymentExport();
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void sendYearEndPurchasedTickets_Click(object sender, EventArgs e)
        {
            var purchasedTickets = _footballCrazeSweepstakesRepository.GetPurchasedETickets(DateTime.Now.Year);
        }
    }
}
