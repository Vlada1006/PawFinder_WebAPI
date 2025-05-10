using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Interfaces;
using Microsoft.AspNetCore.Mvc;
using api.Mappers;
using api.Helpers;
using api.DTOs.LostPets;
using Microsoft.AspNetCore.JsonPatch;
using api.Models;

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
        [Route("{id:int}")]
        public async Task<IActionResult> GetLostPetById(int id)
        {
            var lostPet = await _lostPetsRepo.GetLostPetById(id);

            if (lostPet == null)
            {
                return NotFound();
            }

            var lostPetDTO = lostPet.ToLostPetsDto();
            return Ok(lostPetDTO);
        }

        [HttpPost]
        public async Task<IActionResult> CreateLostPet([FromBody] LostPetCreateRequestDTO lostPetDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var lostPetModel = lostPetDTO.ToLostPetFromCreateDto();
            await _lostPetsRepo.CreateLostPet(lostPetModel);

            return CreatedAtAction(nameof(GetLostPetById), new { id = lostPetModel.PetId }, lostPetModel.ToLostPetsDto());
        }

        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> UpdateLostPet(int id, [FromBody] LostPetUpdateRequestDTO updateDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var lostPetModel = await _lostPetsRepo.UpdateLostPet(id, updateDTO);

            if (lostPetModel == null)
            {
                return BadRequest();
            }

            return Ok(lostPetModel.ToLostPetsDto());
        }

        [HttpPatch]
        [Route("{id}")]
        public async Task<IActionResult> PartialUpdateLostPet(int id, [FromBody] JsonPatchDocument<LostPetPartialUpdateRequestDTO> patchDoc)
        {
            if (!ModelState.IsValid || patchDoc == null)
            {
                return BadRequest(ModelState);
            }

            var patchDTO = new LostPetPartialUpdateRequestDTO();

            patchDoc.ApplyTo(patchDTO, ModelState);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var petToUpdate = await _lostPetsRepo.PartialUpdateLostPet(id, patchDTO);

            if (petToUpdate == null)
            {
                return NotFound();
            }
            return Ok(petToUpdate);
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteLostPet(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _lostPetsRepo.DeleteLostPet(id);
            return Ok("Yay! +1 happy pet!");
        }

        [HttpDelete]
        [Route("multiple")]
        public async Task<IActionResult> DeleteMultipleLostPets([FromQuery] int[] ids)
        {
            if (ids == null || ids.Length == 0)
            {
                return BadRequest("No IDs provided!");
            }

            var lostPetsToDelete = await _lostPetsRepo.DeleteMultipleLostPets(ids);

            if (lostPetsToDelete == null || !lostPetsToDelete.Any())
            {
                return NotFound("There are no pets with these IDs!");
            }

            return Ok("These pet's info was deleted successfully!");

        }
    }
}