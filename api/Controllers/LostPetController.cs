using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Interfaces;
using Microsoft.AspNetCore.Mvc;
using api.Mappers;
using api.Helpers;

namespace api.Controllers
{
    [ApiController]
    [Route("api/lostPets")]
    public class LostPetController : ControllerBase
    {
        private readonly ILostPetsInterface _lostPetsRepo;
        public LostPetController(ILostPetsInterface lostPetsRepo)
        {
            _lostPetsRepo = lostPetsRepo;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllLostPets([FromQuery] LostPetsQueryParams query)
        {
            var lostPets = await _lostPetsRepo.GetLostPets(query);
            var lostPetsDTO = lostPets.Select(s => s.ToLostPetsDto());

            return Ok(lostPetsDTO);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetLostPetById(int id)
        {
            var lostPet = await _lostPetsRepo.GetLostPetById(id);

            if (lostPet == null)
            {
                return BadRequest();
            }
            return Ok(lostPet);
        }

    }
}