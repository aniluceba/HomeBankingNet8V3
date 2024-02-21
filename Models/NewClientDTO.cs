using HomeBankingNet8V3.Models;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace HomeBankingNet8V3.Models.DTO
{
    public class NewClientDTO
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
    }
}
