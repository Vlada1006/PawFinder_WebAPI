using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Models;

namespace api.DTOs.LostPets
{
    public class LostPetPartialUpdateRequestDTO
    {
        public string? PetName { get; set; }
        public string? Breed { get; set; }
        public int Age { get; set; }
        public string? Description { get; set; }
        public string? LastLocation { get; set; }
        public string? ContactInfo { get; set; }
        public string? PhotoUrl { get; set; }
        public PetStatusEnum Status { get; set; }
    }
}