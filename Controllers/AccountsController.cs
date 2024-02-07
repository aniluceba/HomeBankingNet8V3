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

        // Constructor que recibe el repositorio de cuentas
        public AccountsController(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
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

