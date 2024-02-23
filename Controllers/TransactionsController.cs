using HomeBankingNet8V3.Models;
using HomeBankingNet8V3.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HomeBankingNet8V3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionsController : ControllerBase
    {
        private readonly IClientRepository _clientRepository;
        private readonly IAccountRepository _accountRepository;
        private readonly ITransactionRepository _transactionRepository;

        public TransactionsController(IClientRepository clientRepository, IAccountRepository accountRepository,
            ITransactionRepository transactionRepository)
        {
            _accountRepository = accountRepository;
            _clientRepository = clientRepository;
            _transactionRepository = transactionRepository;
        }

        [HttpPost]
        public IActionResult Post([FromBody] TransferDTO transferDto)
        {
            try
            {
                string email = User.FindFirst("Client") != null ? User.FindFirst("Client").Value : string.Empty;
                if (email == string.Empty)
                {
                    return StatusCode(403, "El email no contiene nada");
                }
                Client client = _clientRepository.FindByEmail(email);
                if (client == null)
                {
                    return StatusCode(403, "La cuenta del cliente no existe");
                }
                if (transferDto.ToAccountNumber == transferDto.FromAccountNumber)
                {
                    return StatusCode(403, "Ambas cuentas de destino y origen no pueden ser las mismas");
                }
                if (transferDto.FromAccountNumber == string.Empty || transferDto.ToAccountNumber == string.Empty)
                {
                    return StatusCode(403, "La cuenta de destino o origen no se encuentra");
                }
                if (transferDto.Amount <= 0)
                {
                    return StatusCode(403, "Usted tiene que tranferir una cantidad mayor a Cero Pesos");
                }
                if (transferDto.Description == string.Empty)
                {
                    return StatusCode(403, "El contenido esta vacio");
                }

                Account fromAccount = _accountRepository.FindByNumber(transferDto.FromAccountNumber);
                if (fromAccount == null)
                {
                    return StatusCode(403, "La cuenta inicial es inexistente");
                }

                if ((fromAccount.Balance < transferDto.Amount))
                {
                    return StatusCode(403, "No posee fondos suficientes");
                }

                Account toAccount = _accountRepository.FindByNumber(transferDto.ToAccountNumber);
                if (toAccount == null)
                {
                    return StatusCode(403, "La cuenta de destino no existe");
                }

                _transactionRepository.Save(new Transaction
                {
                    Type = TransactionType.DEBIT,
                    Amount = transferDto.Amount * -1,
                    Description = transferDto.Description + " " + toAccount.Number,
                    AccountId = fromAccount.Id,
                    Date = DateTime.Now,
                });

                _transactionRepository.Save(new Transaction
                {
                    Type = TransactionType.CREDIT,
                    Amount = transferDto.Amount,
                    Description = transferDto.Description + " " + fromAccount.Number,
                    AccountId = toAccount.Id,
                    Date = DateTime.Now,

                });

                fromAccount.Balance = fromAccount.Balance - transferDto.Amount;

                _accountRepository.Save(fromAccount);

                toAccount.Balance = toAccount.Balance + transferDto.Amount;

                _accountRepository.Save(toAccount);

                return Created("Transaccion creada con Exito", fromAccount);

            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
