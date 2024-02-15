﻿using System.ComponentModel.DataAnnotations.Schema;

namespace HomeBankingNet8V3.Models
{
    public class Card
    {
        public long Id { get; set; }

        public string CardHolder { get; set; }

        public string Type { get; set; }

        public string Color { get; set; }

        public string Number { get; set; }
        
        public int Cvv { get; set; }

        public DateTime FromDate { get; set; }

        public DateTime ThruDate { get; set; }

        public long ClientId { get; set; }

        [NotMapped] public object Loans { get; set; }
    }
}
