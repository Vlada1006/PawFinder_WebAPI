using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Models;

namespace api.DTOs
{
    public class LostPetDTO
    {
        public int PetId { get; set; }
        public string PetName { get; set; } = string.Empty;
        public string Breed { get; set; } = string.Empty;
        public int? Age { get; set; }
        public string Description { get; set; } = string.Empty;
        public string LastLocation { get; set; } = string.Empty;
        public DateTime DateLost { get; set; }
        public string ContactInfo { get; set; } = string.Empty;
        public string PhotoUrl { get; set; } = string.Empty;
        public PetStatusEnum Status { get; set; }
        public List<Comment> Comments { get; set; } = new List<Comment>();
    }
}