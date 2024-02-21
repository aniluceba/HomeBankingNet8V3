using HomeBankingNet8V3.Models;
using System.Collections.Generic;
using System;

namespace HomeBankingNet8V3.Models
{
    public class AccountDTO
    {
        public long Id { get; set; }

        public string Number { get; set; }

        public DateTime CreationDate { get; set; }

        public double Balance { get; set; }

        public ICollection<TransactionDTO> Transaction { get; set; }
    }
}
