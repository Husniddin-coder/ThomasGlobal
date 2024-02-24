using Application.DTO.AccountDtos;
using Application.Repositories;
using AutoMapper;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ThomasGlobal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountRepository _accountRepo;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _hostingEnvironment;
        private readonly ILogger _logger;

        public AccountController(IAccountRepository accountRepo, IMapper mapper, IWebHostEnvironment hostingEnvironment, ILogger<AccountController> logger)
        {
            _accountRepo = accountRepo;
            _mapper = mapper;
            _hostingEnvironment = hostingEnvironment;
            _logger = logger;
        }

        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> GetAllAccount()
        {
            var accounts = await _accountRepo.GetAllAsync(x => true)
                .Result.Include(x => x.Products)
                .ThenInclude(x => x.Comments)
                .Include(x => x.Products)
                .ThenInclude(x => x.Comments)
                .Include(x => x.Basket)
                .Include(x => x.Stories)
                .Include(x => x.Image).ToListAsync();

            IEnumerable<AccountGetDto> accountGetDtos = _mapper.Map<IEnumerable<AccountGetDto>>(accounts);

            return Ok(accountGetDtos);
        }

        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> GetAccount([FromQuery] Guid id)
        {
            Account? account = await _accountRepo.GetAllAsync(x => x.Id == id)
                .Result.Include(x => x.Products)
                .Include(x => x.Basket)
                .Include(x => x.Stories).FirstOrDefaultAsync();

            if (account == null) return NotFound("Account not Found");

            AccountGetDto accountGetDto = _mapper.Map<AccountGetDto>(account);

            return Ok(accountGetDto);
        }

        [HttpPost("[action]")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> CreateAccount([FromForm] AccountCreateDto accountCreate)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var imagesFolderPath = "images";
            if (!Directory.Exists(imagesFolderPath))
            {
                Directory.CreateDirectory(imagesFolderPath);
            }

            Account account = _mapper.Map<Account>(accountCreate);

            if (accountCreate.file == null || accountCreate.file.Length == 0)
                return BadRequest("Invalid file");

            // Generate a unique file name
            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(accountCreate.file.FileName);

            // Get the file path for saving
            var filePath = Path.Combine("images", fileName);

            // Save the file to the specified path
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await accountCreate.file.CopyToAsync(stream);
            }

            // Save the file path in the database
            var imageEntity = new Image
            {
                FileName = fileName,
                FilePath = filePath,
                Account = account,

            };

            account.Image = imageEntity;
            account.Account_image = filePath;

            account = await _accountRepo.CreateAsync(account);

            if (account == null) return BadRequest("Creation Failed");

            AccountGetDto accountGet = _mapper.Map<AccountGetDto>(account);

            return Created("", accountGet);
        }

        [HttpPut("[action]")]
        public async Task<IActionResult> UpdateAccount([FromBody] AccountUpdateDto updateDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            Account account = _mapper.Map<Account>(updateDto);

            account = await _accountRepo.UpdateAsync(account);

            if (account == null) return BadRequest("Updating failed");

            AccountGetDto accountGetDto = _mapper.Map<AccountGetDto>(account);

            return Ok(accountGetDto);
        }

        [HttpDelete("[action]")]
        public async Task<IActionResult> DeleteAccount([FromQuery] Guid id)
        {
            Account account = await _accountRepo.GetAsync(id);

            if (System.IO.File.Exists(account.Account_image))
            {
                System.IO.File.Delete(account.Account_image);
            }
            bool result = await _accountRepo.DeleteAsync(id);

            return Ok(result);
        }
    }
}
