using dotnet_rpg.Dtos.Character;
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
        public async Task<ServiceResponse<List<GetCharacterDto>>> GetAll()
        {
            return await getAllCharacters();
        }

        [HttpGet("{id}")]
        public async Task<ServiceResponse<GetCharacterDto>> Get(int id)
        {
            return await _characterService.GetCharacterById(id);
        }

        [HttpPost]
        public async Task<ServiceResponse<List<GetCharacterDto>>> Create(CreateCharacterDto newCharacter)
        {
            await _characterService.CreateCharacter(newCharacter);
            return await getAllCharacters();
        }

        [HttpPut]
        public async Task<ServiceResponse<GetCharacterDto>> Update(UpdateCharacterDto updatedCharacter)
        {
            return await _characterService.UpdateCharacter(updatedCharacter);
        }

        [HttpDelete("{id}")]
        public async Task<ServiceResponse<List<GetCharacterDto>>> Delete(int id)
        {
            return await _characterService.DeleteCharacter(id);
        }

        private async Task<ServiceResponse<List<GetCharacterDto>>> getAllCharacters()
        {
           return await _characterService.GetAllCharacters();
        }
    }
}