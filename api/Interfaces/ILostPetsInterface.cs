using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.DTOs.LostPets;
using api.Helpers;
using api.Models;
using Microsoft.AspNetCore.JsonPatch;

namespace api.Interfaces
{
    public interface ILostPetsInterface
    {
        public Task<List<LostPet>> GetLostPets(LostPetsQueryParams query);
        public Task<LostPet?> GetLostPetById(int id);
        public Task<LostPet> CreateLostPet(LostPet lostPetModel);
        public Task<LostPet?> UpdateLostPet(int id, LostPetUpdateRequestDTO lostPetDTO);
        public Task<LostPet?> PartialUpdateLostPet(int id, LostPetPartialUpdateRequestDTO patchDTO);
        public Task<LostPet?> DeleteLostPet(int id);
    }
}