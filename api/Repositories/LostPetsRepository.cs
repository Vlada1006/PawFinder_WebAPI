using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.DTOs.LostPets;
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


            switch (query.SortBy)
            {
                case "name":
                    lostPets = !query.IsDescending ? lostPets.OrderBy(s => s.PetName) : lostPets.OrderByDescending(s => s.PetName);
                    break;
                case "age":
                    lostPets = !query.IsDescending ? lostPets.OrderBy(s => s.Age) : lostPets.OrderByDescending(s => s.Age);
                    break;
                case "location":
                    lostPets = !query.IsDescending ? lostPets.OrderBy(s => s.LastLocation) : lostPets.OrderByDescending(s => s.LastLocation);
                    break;
                case "date":
                    lostPets = !query.IsDescending ? lostPets.OrderBy(s => s.DateLost) : lostPets.OrderByDescending(s => s.DateLost);
                    break;
                default:
                    break;
            }

            lostPets = lostPets.Skip(query.Size * (query.Page - 1)).Take(query.Size);

            return await lostPets.ToListAsync();

        }

        public async Task<LostPet?> GetLostPetById(int id)
        {
            var lostPet = await _db.LostPets.FirstOrDefaultAsync(u => u.PetId == id);

            if (lostPet == null)
            {
                return null;
            }
            return lostPet;
        }

        public async Task<LostPet> CreateLostPet(LostPet lostPetModel)
        {
            await _db.LostPets.AddAsync(lostPetModel);
            await _db.SaveChangesAsync();

            return lostPetModel;
        }

        public async Task<LostPet?> UpdateLostPet(int id, LostPetUpdateRequestDTO lostPetDTO)
        {
            var existingLostPet = await _db.LostPets.FirstOrDefaultAsync(u => u.PetId == id);

            if (existingLostPet == null)
            {
                return null;
            }

            existingLostPet.PetName = lostPetDTO.PetName;
            existingLostPet.Breed = lostPetDTO.Breed;
            existingLostPet.Age = lostPetDTO.Age;
            existingLostPet.Description = lostPetDTO.Description;
            existingLostPet.LastLocation = lostPetDTO.LastLocation;
            existingLostPet.DateLost = lostPetDTO.DateLost;
            existingLostPet.ContactInfo = lostPetDTO.ContactInfo;
            existingLostPet.PhotoUrl = lostPetDTO.PhotoUrl;
            existingLostPet.Status = lostPetDTO.Status;

            await _db.SaveChangesAsync();
            return existingLostPet;


        }
    }
}