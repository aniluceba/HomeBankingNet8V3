﻿namespace HomeBankingNet8V3.Models
{
    public class ClientLoanDTO
    {
        public long Id { get; set; }
        public double Amount { get; set; }
        public string Payments { get; set; }
        public long ClientId { get; set; }
        public long LoanId { get; set; }
        public string Name { get; set; }
    }
}