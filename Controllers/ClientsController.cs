﻿using HomeBankingNet8V3.Models;
using HomeBankingNet8V3.Repositories;
using HomeBankingnNet8V3.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HomeBankingNet8V3.Controllers

{
    [Route("api/[controller]")]

    [ApiController]

    public class ClientsController : ControllerBase
    {
        private IClientRepository _clientRepository;

        public ClientsController(IClientRepository clientRepository)
        {
            _clientRepository = clientRepository;
        }

        [HttpGet]

        public IActionResult Get()
        {
            try
            {
                var clients = _clientRepository.GetAllClients();
                var clientsDTO = new List<ClientDTO>();

                foreach (Client client in clients)
                {
                    var newClientDTO = new ClientDTO
                    {
                        Id = client.Id,
                        Email = client.Email,
                        FirstName = client.FirstName,
                        LastName = client.LastName,
                        Accounts = client.Accounts.Select(ac => new AccountDTO
                        {
                            Id = ac.Id,
                            Number = ac.Number,
                            Balance = ac.Balance,
                            CreationDate = ac.CreationDate,
                        }).ToList(),
                        Credits = client.ClientLoans.Select(cl => new ClientLoanDTO
                        {
                            Id = cl.Id,
                            LoanId = cl.LoanId,
                            Name = cl.Loan.Name,
                            Amount = cl.Amount,
                            Payments = cl.Payments,
                        }).ToList(),
                        Cards = client.Cards.Select(c => new CardDTO
                        {
                            Id = c.Id,
                            CardHolder = c.CardHolder,
                            Color = c.Color.ToString(),
                            Type = c.Type.ToString(),
                            Cvv = c.Cvv,
                            FromDate = c.FromDate,
                            Number = c.Number,
                            ThruDate = c.ThruDate,
                        }).ToList()
                    };

                    clientsDTO.Add(newClientDTO);

                }

                return Ok(clientsDTO);

            }

            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost]

        public IActionResult Post([FromBody] NewClientDTO newClient)
        {
            try
            {
                if (newClient.FirstName.IsNullOrEmpty() || newClient.LastName.IsNullOrEmpty() || newClient.Email.IsNullOrEmpty())
                {
                    return BadRequest("Alguno de los datos ingresados es erróneo, intentelo nuevamente.");
                }

                var newclient = new Client
                {
                    FirstName = newClient.FirstName,
                    LastName = newClient.LastName,
                    Email = newClient.Email,
                };

                _clientRepository.Save(newclient);

                return CreatedAtAction(nameof(Get), new { id = newclient.Id }, newclient);
            }

            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }

        }

        [HttpGet("{id}")]

        public IActionResult Get(long id)
        {
            try
            {
                var client = _clientRepository.FindById(id);
                if (client == null)
                {
                    return Forbid();
                }
                var clientDTO = new ClientDTO

                {
                    Id = client.Id,
                    Email = client.Email,
                    FirstName = client.FirstName,
                    LastName = client.LastName,
                    Accounts = client.Accounts.Select(ac => new AccountDTO
                    {
                        Id = ac.Id,
                        Number = ac.Number,
                        Balance = ac.Balance,
                        CreationDate = ac.CreationDate,
                    }).ToList(),
                    Credits = client.ClientLoans.Select(cl => new ClientLoanDTO
                    {
                        Id = cl.Id,
                        LoanId = cl.LoanId,
                        Name = cl.Loan.Name,
                        Amount = cl.Amount,
                        Payments = cl.Payments,
                    }).ToList(),
                    Cards = client.Cards.Select(c => new CardDTO
                    {
                        Id = c.Id,
                        CardHolder = c.CardHolder,
                        Color = c.Color.ToString(),
                        Type = c.Type.ToString(),
                        Cvv = c.Cvv,
                        FromDate = c.FromDate,
                        Number = c.Number,
                        ThruDate = c.ThruDate,
                    }).ToList()
                };

                return Ok(clientDTO);

            }

            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}