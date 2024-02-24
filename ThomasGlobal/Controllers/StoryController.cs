using Application.DTO.StoryDtos;
using Application.Repositories;
using AutoMapper;
using Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Principal;

namespace ThomasGlobal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StoryController : ControllerBase
    {
        private readonly IStoryRepository _storyRepository;
        private readonly IAccountRepository _accountRepository;
        private readonly IMapper _mapper;

        public StoryController(IStoryRepository strokesRepository, IMapper mapper, IAccountRepository accountRepository)
        {
            _storyRepository = strokesRepository;
            _mapper = mapper;
            _accountRepository = accountRepository;
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetAllStories()
        {
            IEnumerable<Story> stories = await _storyRepository.GetAllAsync(x => true);

            return Ok(stories);
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetStory(Guid storyId)
        {
            Story story = await _storyRepository.GetAsync(storyId);

            if (story == null) return NotFound("Story not found");

            return Ok(story);
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> CreateStory([FromForm] StoryCreateDto storyCreate)
        {
            if(!ModelState.IsValid) return BadRequest(ModelState);

            var imagesFolderPath = "images";
            if (!Directory.Exists(imagesFolderPath))
            {
                Directory.CreateDirectory(imagesFolderPath);
            }

            Story story  = _mapper.Map<Story>(storyCreate);

            Account account = await _accountRepository.GetAsync(storyCreate.AccountId);

            if (account == null) return BadRequest("Account not found");

            if (storyCreate.ImageFile == null || storyCreate.ImageFile.Length == 0)
                return BadRequest("Invalid file");

            // Generate a unique file name
            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(storyCreate.ImageFile.FileName);

            // Get the file path for saving
            var filePath = Path.Combine("images", fileName);

            // Save the file to the specified path
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await storyCreate.ImageFile.CopyToAsync(stream);
            }

            // Save the file path in the database
            var imageEntity = new Image
            {
                FileName = fileName,
                FilePath = filePath,
                Story = story

            };

            story.Account = account;
            story.Image = imageEntity;
            story.Story_Image = imageEntity.FilePath;

            story = await _storyRepository.CreateAsync(story);

            if (story == null) return BadRequest("Creation story failed");

            StoryGetDto storyGetDto = _mapper.Map<StoryGetDto>(story);

            return Created("",storyGetDto);
        }
    }
}
