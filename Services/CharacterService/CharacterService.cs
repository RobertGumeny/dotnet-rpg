using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using dotnet_rpg.Dtos.Character;

namespace dotnet_rpg.Services.CharacterService
{
    public class CharacterService : ICharacterService
    {
        private static List<Character> characters = new List<Character>()
        {
            new Character(),
            new Character { Id = 1, Name = "Rollo" }
        };
        private readonly IMapper _mapper;

        public CharacterService(IMapper mapper)
        {
            _mapper = mapper;
        }

        public async Task<ServiceResponse<List<GetCharacterDto>>> CreateCharacter(CreateCharacterDto newCharacter)
        {
            var response = new ServiceResponse<List<GetCharacterDto>>();
            Character character = _mapper.Map<Character>(newCharacter);
            character.Id = characters.Max(c => c.Id) + 1;
            characters.Add(character);
            response.Data = characters.Select(c => _mapper.Map<GetCharacterDto>(c)).ToList();
            return response;
        }

        public async Task<ServiceResponse<List<GetCharacterDto>>> DeleteCharacter(int id)
        {
            ServiceResponse<List<GetCharacterDto>> response = new ServiceResponse<List<GetCharacterDto>>();
            try
            {
                Character? character = characters.First(c => c.Id == id);

                if (character != null)
                {
                    characters.Remove(character);
                    response.Data = characters.Select(c => _mapper.Map<GetCharacterDto>(c)).ToList();
                }
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
                throw;
            }

            return response;
        }

        public async Task<ServiceResponse<List<GetCharacterDto>>> GetAllCharacters()
        {
            return new ServiceResponse<List<GetCharacterDto>> { Data = characters.Select(c => _mapper.Map<GetCharacterDto>(c)).ToList() };
        }

        public async Task<ServiceResponse<GetCharacterDto>> GetCharacterById(int id)
        {
            var response = new ServiceResponse<GetCharacterDto>();
            var character = characters.FirstOrDefault(c => c.Id == id);
            response.Data = _mapper.Map<GetCharacterDto>(character);
            return response;
        }

        public async Task<ServiceResponse<GetCharacterDto>> UpdateCharacter(UpdateCharacterDto updatedCharacter)
        {
            ServiceResponse<GetCharacterDto> response = new ServiceResponse<GetCharacterDto>();
            try
            {
                Character? character = characters.FirstOrDefault(c => c.Id == updatedCharacter.Id);

                if (character != null)
                {
                    _mapper.Map(updatedCharacter, character);
                    response.Data = _mapper.Map<GetCharacterDto>(character);
                }
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
                throw;
            }

            return response;
        }
    }
}