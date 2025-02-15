using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace FootballCrazeSweepstakes.Models
{
    public class ApplicationDbContext : DbContext
    {
        public string DbPath { get; }

        public ApplicationDbContext()
        {
            DbPath = AppDomain.CurrentDomain.BaseDirectory + $@"App_Data\\FootballCrazeSweepstakes.db";
        }
        public DbSet<ETicket> ETicket { get; set; }
        public DbSet<SweepstakePurchase> SweepstakePurchase { get; set; }
        public DbSet<SweepstakePurchaseETicket> SweepstakePurchaseETicket { get; set; }
        public DbSet<Error> Error { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseSqlite($"Data Source={DbPath}");
    }
}
