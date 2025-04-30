using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Helpers;
using api.Models;

namespace api.Interfaces
{
    public interface ILostPetsInterface
    {
        public Task<List<LostPet>> GetLostPets(LostPetsQueryParams query);
        public Task<LostPet> GetLostPetById(int id);
    }
}