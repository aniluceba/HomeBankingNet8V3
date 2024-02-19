using HomeBankingNet8V3.Models;
using HomeBankingNet8V3.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HomeBankingNet8V3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IClientRepository _clientRepository;

        // Constructor que recibe el repositorio de cuentas
        public AccountsController(IAccountRepository accountRepository, IClientRepository clientRepository)
        {
            _accountRepository = accountRepository;
            _clientRepository = clientRepository;
        }

        [HttpPost]
        public IActionResult CreateAccount()
        {
            // Obtiene el ID del cliente autenticado
            var clientId = User.Claims.FirstOrDefault(c => c.Type == "ClientId")?.Value;
            if (clientId == null)
            {
                return Unauthorized();
            }

            // Obtiene el cliente autenticado
            var client = _clientRepository.GetClientById(clientId);
            if (client == null)
            {
                return NotFound();
            }

            // Verifica si el cliente ya tiene 3 cuentas registradas
            var existingAccountsCount = _accountRepository.GetAccountsByClientId(clientId).Count();
            if (existingAccountsCount >= 3)
            {
                return StatusCode(403, "El cliente ya tiene 3 cuentas registradas.");
            }

            // Genera un número de cuenta aleatorio
            var random = new Random();
            var accountNumber = $"VIN-{random.Next(100000, 999999)}";

            // Crea una nueva cuenta con saldo 0
            var newAccount = new Account
            {
                AccountNumber = accountNumber,
                ClientId = client.Id,
                Balance = 0
            };

            // Guarda la cuenta en el repositorio
            _accountRepository.AddAccount(newAccount);

            // Retorna una respuesta "201 creada"
            return StatusCode(201, newAccount);
        }
    }
}

// Método para obtener todas las cuentas
[HttpGet]
        public ActionResult<AccountDTO> GetAllAccounts()
        {
            var accounts = _accountRepository.GetAllAccounts();
            var accountDTOs = new List<AccountDTO>();

            foreach (var account in accounts)
            {
                var accountDTO = new AccountDTO
                {
                    Id = account.Id,
                    Number = account.Number,
                    Balance = account.Balance,
                    CreationDate = account.CreationDate
                };
                accountDTOs.Add(accountDTO);
            }

            var accountListDTO = new AccountDTO
            {
               Accounts = accountDTOs
            };

            return Ok(accountListDTO);
        }

        // Método para obtener una cuenta por su ID
        [HttpGet("{id}")]
        public ActionResult<AccountDTO> GetAccountById(int id)
        {
            var account = _accountRepository.GetAccountById(id);

            if (account == null)
            {
                return NotFound();
            }

            var accountDTO = new AccountDTO
            {
                Id = account.Id,
                Number = account.Number,
                Balance = account.Balance,
                CreationDate = account.CreationDate
            };

            return Ok(accountDTO);
        }
    }
}

