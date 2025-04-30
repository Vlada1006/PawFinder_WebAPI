using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Interfaces;
using Microsoft.AspNetCore.Mvc;
using api.Mappers;

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
        public async Task<IActionResult> GetAllPets()
        {
            var lostPets = await _lostPetsRepo.GetLostPets();
            var lostPetsDTO = lostPets.Select(s => s.ToLostPetsDto());

            return Ok(lostPetsDTO);
        }

    }
}