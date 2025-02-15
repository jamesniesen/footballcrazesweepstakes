using FootballCrazeSweepstakes.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace FootballCrazeSweepstakes
{
    public class FootballCrazeSweepstakesRepository : IFootballCrazeSweepstakesRepository
    {
        private readonly ApplicationDbContext _dbContext;
        public FootballCrazeSweepstakesRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
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
            var existingTicketNumbers = _dbContext.ETicket
                .Where(et => et.Year == year)
                .Select(et => et.TicketNumber).ToList();
            foreach(var ticket in uploadedETickets)
            {
                try
                {
                    if (!existingTicketNumbers.Where(et => et == ticket.TicketNumber).Any())
                    {
                        eTicketsAdded.Add(ticket);
                        _dbContext.ETicket.Add(ticket);
                    }
                }
                catch (Exception ex)
                {
                    var error = new Error()
                    {
                        Process = nameof(ProcessTypeEnum.ETicketUpload),
                        Message = ex.Message,
                        Date = DateTime.Now
                    };
                    _dbContext.Error.Add(error);
                }
            }
            await _dbContext.SaveChangesAsync(cancellationToken);
            return eTicketsAdded;
        }
        public async Task<List<SweepstakePurchaseETicket>> SavePurchasedETickets(int year, List<SweepstakePurchase> uploadedPurchases, CancellationToken cancellation)
        {
            var purchasedETickets = new List<SweepstakePurchaseETicket>();
            var existingTransactionIds = _dbContext.SweepstakePurchase
                .Where(sp => sp.Date.Year == year).Select(sp => sp.TransactionId).ToList();

            foreach (var purchase in uploadedPurchases)
            {
                try
                {
                    if (!existingTransactionIds.Contains(purchase.TransactionId))
                    {
                        _dbContext.SweepstakePurchase.Add(purchase);
                        var numberOfTicketsPurchased = int.Parse(purchase.PurchaseOption.Split(" ")[1]);
                        for (int i = 1; i <= numberOfTicketsPurchased; i++)
                        {
                            var availableTicket = _dbContext.ETicket.First(e => !e.Purchased);
                            var purchasedETicket = new SweepstakePurchaseETicket()
                            {
                                SweepstakePurchase = purchase,
                                Eticket = availableTicket
                            };
                            _dbContext.SweepstakePurchaseETicket.Add(purchasedETicket);
                            availableTicket.Purchased = true;
                            _dbContext.ETicket.Update(availableTicket);
                            purchasedETickets.Add(purchasedETicket);
                        }

                    }
                    else
                    {
                        var error = new Error()
                        {
                            Process = nameof(ProcessTypeEnum.ETicketPurchase),
                            Message = "The Transaction ID: " + purchase.TransactionId + " already exists",
                            Date = DateTime.Now
                        };
                        _dbContext.Error.Add(error);
                    }
                }
                catch (Exception ex) 
                {
                    var error = new Error()
                    {
                        Process = nameof(ProcessTypeEnum.ETicketPurchase),
                        Message = ex.Message,
                        Date = DateTime.Now
                    };
                    _dbContext.Error.Add(error);
                }

            }
            await _dbContext.SaveChangesAsync();

            return purchasedETickets;
        }

        public ETicket GetTestETicket(int year)
        {
            return _dbContext.ETicket.First(x => x.Year == year);
        }
    }
}
