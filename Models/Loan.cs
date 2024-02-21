﻿using HomeBankingNet8V3.Models;

namespace HomeBankingNet8V3.Models
{
    public class Loan
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public double MaxAmount { get; set; }
        public string Payments { get; set; }
        public ICollection<ClientLoan> ClientLoans { get; set; }
    }
}