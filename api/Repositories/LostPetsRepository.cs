using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
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

        public async Task<List<LostPet>> GetLostPets()
        {
            return await _db.LostPets.ToListAsync();
        }
    }
}