using FootballCrazeSweepstakes.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;

namespace FootballCrazeSweepstakes
{
    public class FootballCrazeSweepstakesRepository : IFootballCrazeSweepstakesRepository
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly ILogger<FootballCrazeSweepstakesRepository> _logger;
        public FootballCrazeSweepstakesRepository(ApplicationDbContext dbContext, ILogger<FootballCrazeSweepstakesRepository> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }
        public int AvailableETicketsForSale(int sweepstakeYear)
        {
            var availableTickets = 0;
            var uploadedETickets = _dbContext.ETicket.AsNoTracking()
                .Where(et => et.Year == sweepstakeYear).ToList();
            if (uploadedETickets.Any()) 
            {
                availableTickets = uploadedETickets.Count();
                var purchasedETickets = _dbContext.SweepstakePurchaseETicket.AsNoTracking()
                   .Where(et => et.Year == sweepstakeYear).ToList();
                if (purchasedETickets.Any())
                {
                    availableTickets = availableTickets - purchasedETickets.Count();
                }
            }
            return availableTickets;
        }
        public async Task<IEnumerable<ETicket>> AddETickets(int year, IEnumerable<ETicket> uploadedETickets, CancellationToken cancellationToken)
        {
            var eTicketsAdded = new List<ETicket>();
            try
            {
                var existingTicketNumbers = _dbContext.ETicket
                    .Where(et => et.Year == year)
                    .Select(et => et.TicketNumber).ToList();
                foreach (var ticket in uploadedETickets)
                {
                    try
                    {
                        if (!existingTicketNumbers.Where(et => et == ticket.TicketNumber).Any())
                        {
                            eTicketsAdded.Add(ticket);
                            _dbContext.ETicket.Add(ticket);
                        }
                        else
                        {
                            _logger.Log(LogLevel.Warning, "Ticket# " + ticket.TicketNumber + " already exists");
                        }
                    }
                    catch (Exception e)
                    {

                        _logger.Log(LogLevel.Error, e.Message);
                    }
                }
                await _dbContext.SaveChangesAsync(cancellationToken);
            }
            catch (Exception e)
            {
                _logger.Log(LogLevel.Error, e.Message);
            }

            return eTicketsAdded;
        }
        public List<SweepstakePurchaseETicket> SavePurchasedETickets(int year, List<SweepstakePurchase> uploadedPurchases)
        {
            var purchasedETickets = new List<SweepstakePurchaseETicket>();
           // var existingTransactionIds = _dbContext.SweepstakePurchase
           //     .Where(sp => sp.Date.Year == year).Select(sp => sp.TransactionId).ToList();

            foreach (var purchase in uploadedPurchases)
            {
                try
                {
                 //   if (!existingTransactionIds.Contains(purchase.TransactionId))
                 //   {
                       // _dbContext.SweepstakePurchase.Add(purchase);
                      //  _dbContext.SaveChanges();
                        var numberOfTicketsPurchased = int.Parse(purchase.PurchaseOption.Split(" ")[1]);
                        for (int i = 1; i <= numberOfTicketsPurchased; i++)
                        {
                            var availableTicket = _dbContext.ETicket.First(e => !e.Purchased);
                            availableTicket.Purchased = true;
                            _dbContext.ETicket.Update(availableTicket);
                          //  _dbContext.SaveChangesAsync();
                            var purchasedETicket = new SweepstakePurchaseETicket()
                            {
                               // SweepstakePurchaseId = purchase.Id,
                               // ETicketId = availableTicket.Id,
                                SweepstakePurchase = purchase,
                                Eticket = availableTicket,
                                Year = year
                            };
                            _dbContext.SweepstakePurchaseETicket.Add(purchasedETicket);
                            _dbContext.SaveChanges();
                            purchasedETickets.Add(purchasedETicket);
                        }

                   // }
                }
                catch (Exception ex) 
                {
                    var error = new Error()
                    {
                        Process = nameof(ProcessTypeEnum.ETicketPurchase),
                        Message = ex.Message,
                        Date = DateTime.Now
                    };
                }

            }
            return purchasedETickets;
        }

        public ETicket GetTestETicket(int year)
        {
            return _dbContext.ETicket.First(x => x.Year == year);
        }

        public List<SweepstakePurchase> SaveSweepstakePurchases(int year)
        {
            var gemsPurchasesToUploadDirectory = AppDomain.CurrentDomain.BaseDirectory + $@"App_Data\\GemsPurchasesToUpload\\";
            var uploadedPurchases = new List<SweepstakePurchase>();
            var existingTransactionIds = _dbContext.SweepstakePurchase.Select(sp => sp.TransactionId).ToList();
            try
            {
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
                                if (!existingTransactionIds.Contains(rowColumns[10]))
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
                                    _dbContext.SweepstakePurchase.Update(purchase);
                                    _dbContext.SaveChanges();
                                    uploadedPurchases.Add(purchase);
                                }
                            }
                            catch (Exception e)
                            {
                                _logger.Log(LogLevel.Error, "An error occurred processing " + rowColumns[1] + "'s payment (usually there are missing requried fields such as Address, Email, Phone....).  Please review the Gem's Excel file. Exception: " + e.Message);
                            }

                        }
                        rowList.Clear();
                    }
                }
            }
            catch (Exception e)
            {
                _logger.Log(LogLevel.Error, e.Message);
            }

            return uploadedPurchases.Where(up => up.Date.Year == year).ToList();
        }

        public List<SweepstakePurchaseETicket> GetPurchasedETickets(int year)
        {
            throw new NotImplementedException();
        }
    }
}
