using FootballCrazeSweepstakes.Models;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;

namespace FootballCrazeSweepstakes
{
    public class FootballCrazeSweepstakesService : IFootballCrazeSweepstakesService
    {
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

        public void EmailETickets(AppSettings _config, List<SweepstakePurchaseETicket> purchasedTickets)
        {
            var ticketsByMember = purchasedTickets.GroupBy(pt => pt.SweepstakePurchaseId);
            var errors = new List<Error>();
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
                    "<p>Thanks for supporting St Francis Xavier. Your ticket" + wording + ":" +
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
                catch (Exception ex)
                {
                    var error = new Error()
                    {
                        Process = nameof(ProcessTypeEnum.ETicketPurchase),
                        Message = purchase.First().SweepstakePurchase.MemberName + " Not Emailed. Exception " + ex.Message,
                        Date = DateTime.Now
                    };
                    errors.Add(error);
                }
            }
            // todo create repo.SaveEmailTicketErrors(errors);
        }

        public IEnumerable<ETicket> UploadETickets(int year)
        {
            //ToDo automatically create directory if doesn't exist.  See Prior Auth
            var eTicketUploadDirectory = AppDomain.CurrentDomain.BaseDirectory + $@"App_Data\\ETicketsToUpload\\";
            var eTickets = new List<ETicket>();
            foreach (var filePath in Directory.EnumerateFiles(eTicketUploadDirectory))
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
            return eTickets;
        }

        public List<SweepstakePurchase> UploadSweepstakePurchases(int year)
        {
            var gemsPurchasesToUploadDirectory = AppDomain.CurrentDomain.BaseDirectory + $@"App_Data\\GemsPurchasesToUpload\\";
            var uploadedPurchases = new List<SweepstakePurchase>();
            
            foreach (var filePath in Directory.EnumerateFiles(gemsPurchasesToUploadDirectory))
            {
                var rowList = new List<string>();
                ISheet sheet;
                var fileBytes = File.ReadAllBytes(filePath);
                var memoryStream = new MemoryStream(fileBytes);
                memoryStream.Position = 0;
                XSSFWorkbook xssWorkbook = new XSSFWorkbook(memoryStream);
                sheet = xssWorkbook.GetSheetAt(0);
                IRow headerRow = sheet.GetRow(0);
                int cellCount = headerRow.LastCellNum;

                // skip the last row in Gem's Excel:
                for (int i = (sheet.FirstRowNum + 1); i < sheet.LastRowNum; i++)
                {
                    IRow row = sheet.GetRow(i);
                    if (row == null) continue;
                    if (row.Cells.All(d => d.CellType == CellType.Blank)) continue;
                    for (int j = row.FirstCellNum; j < cellCount; j++)
                    {
                        if (row.GetCell(j) != null)
                        {
                            if (!string.IsNullOrEmpty(row.GetCell(j).ToString()) && !string.IsNullOrWhiteSpace(row.GetCell(j).ToString()))
                            {
                                rowList.Add(row.GetCell(j).ToString());
                            }
                        }
                    }
                    if (rowList.Count > 0)
                    {
                        var rowColumns = rowList.ToArray();
                        try
                        {

                                var purchase = new SweepstakePurchase()
                                {
                                    Date = DateTime.Parse(rowColumns[0]),
                                    MemberName = rowColumns[1],
                                    MemberFirstName = rowColumns[2],
                                    MemberLastName = rowColumns[3],
                                    Purchase = rowColumns[4],
                                    PurchaseOption = rowColumns[5],
                                    Price = Decimal.Parse(rowColumns[6]),
                                    CustomerCharged = Decimal.Parse(rowColumns[7]),
                                    Refunded = Decimal.Parse(rowColumns[8]),
                                    PaymentMethod = rowColumns[9],
                                    TransactionId = rowColumns[10],
                                    Status = rowColumns[11],
                                    Email = rowColumns[12],
                                    Phone = rowColumns[13],
                                    Address = rowColumns[14],
                                    City = rowColumns[15],
                                    State = rowColumns[16],
                                    Zip = rowColumns[17],
                                    TicketEmailedOn = DateTime.Now
                                };
                            uploadedPurchases.Add(purchase);
                            
                        }
                        catch
                        {
                         //   uploadErrors.Add("An error occurred processing " + rowColumns[1] + "'s payment (usually there are missing requried fields such as Email, Phone....).  Please review the Gem's Excel file.");
                        }

                    }
                }

            }
            return uploadedPurchases.Where(up => up.Date.Year == year).ToList();
        }
    }
}
