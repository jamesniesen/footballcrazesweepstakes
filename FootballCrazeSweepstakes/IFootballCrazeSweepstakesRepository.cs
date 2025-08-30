using FootballCrazeSweepstakes.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballCrazeSweepstakes
{
    public interface IFootballCrazeSweepstakesRepository
    {
        int AvailableETicketsForSale(int sweepstakeYear);
        Task<IEnumerable<ETicket>> AddETickets(int year, IEnumerable<ETicket> uploadedETickets, CancellationToken cancellationToken);
        List<SweepstakePurchaseETicket> SavePurchasedETickets(int year, List<SweepstakePurchase> uploadedPurchases);
        List<SweepstakePurchase> SaveSweepstakePurchases(int year);
        ETicket GetTestETicket(int year);
        List<SweepstakePurchaseETicket> GetPurchasedETickets(int year);
    }
}
