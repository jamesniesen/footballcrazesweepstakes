using CsvHelper;
using FootballCrazeSweepstakes.Models;
using Microsoft.Extensions.Logging;
using System.Globalization;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;

namespace FootballCrazeSweepstakes
{
    public class FootballCrazeSweepstakesService : IFootballCrazeSweepstakesService
    {
        private readonly ILogger<FootballCrazeSweepstakesService> _logger;
        public FootballCrazeSweepstakesService(ILogger<FootballCrazeSweepstakesService> logger)
        {
            _logger = logger;
        }


        public int DeleteETicketPdfFiles()
        {
            var pdfsDeleted = 0;
            var eTicketUploadDirectory = AppDomain.CurrentDomain.BaseDirectory + $@"App_Data\\ETicketsToUpload\\";
            var eTickets = new List<ETicket>();
            foreach (var filePath in Directory.EnumerateFiles(eTicketUploadDirectory))
            {
                File.Delete(filePath);
                pdfsDeleted++;
            }
            return pdfsDeleted;
        }

        public int DeleteGemsPaymentExport()
        {
            var deleted = 0;
            var gemsPurchasesToUploadDirectory = AppDomain.CurrentDomain.BaseDirectory + $@"App_Data\\GemsPurchasesToUpload\\";
            foreach (var filePath in Directory.EnumerateFiles(gemsPurchasesToUploadDirectory))
            {
                File.Delete(filePath);
                deleted++;
            }
            return deleted;
        }

        public void EmailETickets(AppSettings _config, List<SweepstakePurchaseETicket> purchasedTickets)
        {
            try
            {
                var ticketsByMember = purchasedTickets.GroupBy(pt => pt.SweepstakePurchaseId);
                foreach (var purchase in ticketsByMember)
                {
                    var emailTo = purchase.First().SweepstakePurchase.Email;
                    var wording = purchase.Count() == 1 ? " is" : "s are";
                    using var mail = new MailMessage();
                    // Add attachments:
                    var ticketNumbers = "<ul>";
                    foreach (var item in purchase)
                    {
                        ticketNumbers += "<li>" + item.Eticket.TicketNumber + "</li>";
                        var ct = new System.Net.Mime.ContentType(MediaTypeNames.Application.Pdf);
                        ct.Name = "Ticket Number " + item.Eticket.TicketNumber;
                        var pdf = new MemoryStream(item.Eticket.FileData);
                        Attachment data = new Attachment(pdf, ct);
                        mail.Attachments.Add(data);
                    }
                    ticketNumbers += "</ul>";
                    mail.IsBodyHtml = true;
                    mail.From = new MailAddress(_config.SmptSettings.SendTicketEmailFrom);
                    mail.To.Add(emailTo);
                    mail.Subject = "Your Football Craze Sweepstake Ticket" + wording + " Attached";
                    mail.Body = "Dear " + purchase.First().SweepstakePurchase.MemberFirstName + "," +
                        "<p>Thanks for supporting St Francis Xavier. Your ticket number" + wording + ":" +
                        ticketNumbers + "</p>" +
                        "<p>If you have any questions please email us at athletics@sfxcrossplains.org or" +
                        " call us at (608) 798 - 4723.</p>Thanks and good luck!<br/><br/>" +
                        "<h3>Football Craze Sweepstakes</h3>" +
                        "<a href=\"https://footballcrazesweepstakes.gemsbrain.com\">https://footballcrazesweepstakes.gemsbrain.com</a><br/>" +
                    "(608) 798 - 4723";
                    try
                    {
                        using var smtp = new SmtpClient(_config.SmptSettings.Address, _config.SmptSettings.PortNumber);
                        smtp.Credentials = new NetworkCredential(_config.SmptSettings.UserName, _config.SmptSettings.Password);
                        smtp.EnableSsl = true;
                        smtp.Send(mail);
                    }
                    catch (Exception e)
                    {
                        var errorMsg = purchase.First().SweepstakePurchase.MemberName + " Not Emailed. Exception " + e.Message;
                        _logger.Log(LogLevel.Error, errorMsg);
                    }
                }
            }
            catch(Exception e)
            {
                _logger.Log(LogLevel.Error, e.Message);
            }

        }

        public void EmailNewPurchasesSummary(AppSettings _config, List<PaymentSummaryCsv> csvData)
        {
            using var mail = new MailMessage();
            mail.IsBodyHtml = true;
            mail.From = new MailAddress(_config.SmptSettings.SendTicketEmailFrom);
            mail.To.Add(_config.SendPurchaseSummaryEmailTo);
            mail.Subject = "Football Craze Sweepstake Total Purchases - " + DateTime.Now.ToString("MM/dd/yyyy") + " Attached";
            mail.Body = "Attached are all online eTicketpurchases for " + DateTime.Now.Year;
            using (MemoryStream ms = new MemoryStream())
            {
                using (TextWriter tw = new StreamWriter(ms))
                using (CsvWriter csv = new CsvWriter(tw, CultureInfo.InvariantCulture))
                {
                    csv.WriteRecords(csvData); // Converts error records to CSV

                    tw.Flush(); // flush the buffered text to stream
                    ms.Seek(0, SeekOrigin.Begin); // reset stream position
                    mail.Attachments.Add(new Attachment(ms, "ETicketYearEndPurchases_" + DateTime.Now.ToString("MM-dd-yyyy") + ".csv")); 
                }
                try
                {
                    using var smtp = new SmtpClient(_config.SmptSettings.Address, _config.SmptSettings.PortNumber);
                    smtp.Credentials = new NetworkCredential(_config.SmptSettings.UserName, _config.SmptSettings.Password);
                    smtp.EnableSsl = true;
                    smtp.Send(mail);
                }
                catch (Exception e)
                {
                    _logger.Log(LogLevel.Error, e.Message);
                }
            }

        }

        public List<PaymentSummaryCsv> GetPaymentSummaryInfo(List<SweepstakePurchase> purchases, List<SweepstakePurchaseETicket> purchasedTickets)
        {
            var paymentRecords = new List<PaymentSummaryCsv>();
            purchases.ForEach(purchase =>
            {
                var tickets = purchasedTickets
                    .Where(t => t.SweepstakePurchaseId == purchase.Id)
                    .Select(t => t.Eticket) .ToList();
                var ticketNumbers = System.String.Join(", ", tickets);
                var paymentRecord = new PaymentSummaryCsv()
                {
                    DatePurchased = purchase.Date,
                    FirstName = purchase.MemberFirstName,
                    LastName = purchase.MemberLastName,
                    Address = purchase.Address,
                    City = purchase.City,
                    State = purchase.State,
                    Zip = purchase.Zip,
                    Email = purchase.Email,
                    Phone = purchase.Phone,
                    NumberOfTicketsPurchased = purchase.PurchaseOption,
                    AmountPaid = purchase.Price.ToString(),
                    TicketNumbers = ticketNumbers
                };
                paymentRecords.Add(paymentRecord);
            });
            return paymentRecords;
        }

        public IEnumerable<ETicket> UploadETickets(int year)
        {
            var eTickets = new List<ETicket>();
            try
            {
                var eTicketUploadDirectory = AppDomain.CurrentDomain.BaseDirectory + $@"App_Data\\ETicketsToUpload\\";
                
                foreach (var filePath in Directory.EnumerateFiles(eTicketUploadDirectory))
                {
                    try
                    {
                        var file = new FileInfo(filePath);
                        var eTicket = new ETicket()
                        {
                            TicketNumber = Path.GetFileNameWithoutExtension(file.Name),
                            FileType = Path.GetExtension(Path.GetFileName(file.Name)),
                            Year = year,
                            Created = DateTime.Now,
                            Modified = DateTime.Now,
                            FileData = File.ReadAllBytes(filePath)
                        };
                        eTickets.Add(eTicket);
                    }
                    catch (Exception e)
                    {
                        _logger.Log(LogLevel.Error, e.Message);
                    }
                }
            }
            catch(Exception e)
            {
                _logger.Log(LogLevel.Error, e.Message);
            }

            return eTickets;
        }

    }
}
