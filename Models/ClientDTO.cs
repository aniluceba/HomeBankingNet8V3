﻿using HomeBankingNet8V3.Models;

using System.Collections.Generic;

using System.Text.Json.Serialization;


namespace HomeBankingNet8V3.dtos
{
    public class ClientDTO

    {
        [JsonIgnore]
        public long Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public ICollection<AccountDTO> Accounts { get; set; }
        public ICollection<ClientLoanDTO> Loans { get; set; }
    }
}
