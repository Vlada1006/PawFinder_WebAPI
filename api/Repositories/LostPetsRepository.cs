using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Helpers;
using api.Interfaces;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Repositories
{
    public class LostPetsRepository : ILostPetsInterface
    {
        private readonly AppDbContext _db;

        public LostPetsRepository(AppDbContext db)
        {
            _db = db;
        }

        public async Task<List<LostPet>> GetLostPets(LostPetsQueryParams query)
        {
            var lostPets = _db.LostPets.AsQueryable();

            if (!string.IsNullOrEmpty(query.NameSearchTerm))
            {
                lostPets = lostPets.Where(p => p.PetName.ToLower().Contains(query.NameSearchTerm.ToLower()));
            }

            if (!string.IsNullOrEmpty(query.LocationSearchTerm))
            {
                lostPets = lostPets.Where(p => p.LastLocation.ToLower().Contains(query.LocationSearchTerm.ToLower()));
            }

            if (!string.IsNullOrEmpty(query.Name))
            {
                lostPets = lostPets.Where(u => u.PetName == query.Name);
            }

            if (!string.IsNullOrEmpty(query.Breed))
            {
                lostPets = lostPets.Where(u => u.Breed == query.Breed);
            }

            //the way for dropbar in UI
            if (query.Status.HasValue)
            {
                lostPets = lostPets.Where(u => u.Status == query.Status.Value);
            }

            lostPets = lostPets.Skip(query.Size * (query.Page - 1)).Take(query.Size);

            return await lostPets.ToListAsync();

        }

        public async Task<LostPet> GetLostPetById(int id)
        {
            var lostPet = await _db.LostPets.FirstOrDefaultAsync(u => u.PetId == id);

            if (lostPet == null)
            {
                return null;
            }
            return lostPet;
        }
    }
}