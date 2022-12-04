using dotnet_rpg.Services.CharacterService;
using Microsoft.AspNetCore.Mvc;

namespace dotnet_rpg.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CharacterController : ControllerBase
    {
        private readonly ICharacterService _characterService;

        public CharacterController(ICharacterService characterService)
        {
            _characterService = characterService;
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult<List<Character>>> GetAll()
        {
            return Ok(await getAllCharacters());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Character>> Get(int id)
        {
            return Ok(await _characterService.GetCharacterById(id));
        }

        [HttpPost]
        public async Task<ActionResult<List<Character>>> CreateCharacter(Character newCharacter)
        {
            await _characterService.CreateCharacter(newCharacter);
            return Ok(getAllCharacters());
        }

        private async Task<List<Character>> getAllCharacters()
        {
           return await _characterService.GetAllCharacters();
        }
    }
}