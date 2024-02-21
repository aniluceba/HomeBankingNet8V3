using HomeBankingNet8V3.Models;

namespace HomeBankingNet8V3.Models.DTO
{
    public class TransactionDTO
    {
        public long Id { get; set; }

        public string Type { get; set; }

        public double Amount { get; set; }

        public string Description { get; set; }

        public DateTime Date { get; set; }

    }
}

