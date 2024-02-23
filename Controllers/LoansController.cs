﻿using HomeBankingNet8V3.Models;
using HomeBankingNet8V3.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace HomeBankingNet8V3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoansController : ControllerBase
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IClientRepository _clientRepository;
        private readonly ILoanRepository _loanRepository;
        private readonly ITransactionRepository _transactionRepository;
        private readonly IClientLoanRepository _clientLoanRepository;
        public LoansController(IAccountRepository accountRepository, IClientRepository clientRepository, ILoanRepository loanRepository,
            ITransactionRepository transactionRepository, IClientLoanRepository clientLoanRepository)
        {
            _accountRepository = accountRepository;
            _clientRepository = clientRepository;
            _loanRepository = loanRepository;
            _transactionRepository = transactionRepository;
            _clientLoanRepository = clientLoanRepository;
        }
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                var loans = _loanRepository.GetAllLoans();
                var LoansDto = new List<LoanDTO>();
                foreach (Loan loan in loans)
                {
                    var newLoanDto = new LoanDTO
                    {
                        Id = loan.Id,
                        Name = loan.Name,
                        Payments = loan.Payments,
                        MaxAmount = loan.MaxAmount,
                    };
                    LoansDto.Add(newLoanDto);
                }
                return StatusCode(200, LoansDto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost]
        public IActionResult Post([FromBody] LoanApplicationDTO applicationDto)
        {
            try
            {

                string email = User.FindFirst("Client") != null ? User.FindFirst("Client").Value : string.Empty;
              
                Client client = _clientRepository.FindByEmail(email);
                if (client == null)
                {
                    return StatusCode(403, "La cuenta del cliente no existe");
                }

                if (applicationDto.Amount <= 0)
                {
                    return StatusCode(403, "El monto no puede ser de cero pesos");
                }
                Loan loan = _loanRepository.FindById(applicationDto.LoanId);
                if (loan == null)
                {
                    return StatusCode(403, "El prestamo que usted quiere solicitar no existe");
                }
                if (applicationDto.Amount > loan.MaxAmount)
                {
                    return StatusCode(403, "El prestamo solicitado excede el monto permitido");
                }

                if (applicationDto.Payments.IsNullOrEmpty())
                {
                    return StatusCode(403, "Cuotas no puede quedar vacio");
                }
                var newPaymentValues = loan.Payments.Split(',').Select(s => s.Trim()).ToList();
                if (!newPaymentValues.Contains(applicationDto.Payments.ToString()))
                {
                    return BadRequest("La cantidad de cuentas ingresadas no es valida");
                }



                Account toAccount = _accountRepository.FindByNumber(applicationDto.ToAccountNumber);
                if (toAccount == null)
                {
                    return StatusCode(403, "La cuenta que quiere acceder no existe");
                }
                if (toAccount.ClientId != client.Id)
                {
                    return StatusCode(403, "La cuenta solicitada no pertenece al cliente");
                }
                double finalAmount = applicationDto.Amount * 1.2;

                _clientLoanRepository.Save(new ClientLoan
                {
                    ClientId = toAccount.ClientId,
                    Amount = finalAmount,
                    Payments = applicationDto.Payments,
                    LoanId = applicationDto.LoanId,
                });

                _transactionRepository.Save(new Transaction
                {
                    Type = TransactionType.CREDIT,
                    Amount = applicationDto.Amount,
                    Description = "Loan approved " + loan.Name,
                    AccountId = toAccount.Id,
                    Date = DateTime.Now,

                });


                toAccount.Balance = toAccount.Balance + applicationDto.Amount;

                _accountRepository.Save(toAccount);

                return Created("Prestamo creada con Exito", toAccount);

            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
    
