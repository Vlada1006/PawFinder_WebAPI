using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.DTOs;
using api.DTOs.LostPets;
using api.Models;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace api.Mappers
{
    public static class LostPetMappers
    {
        public static LostPetDTO ToLostPetsDto(this LostPet lostPetModel)
        {
            return new LostPetDTO
            {
                PetId = lostPetModel.PetId,
                PetName = lostPetModel.PetName,
                Breed = lostPetModel.Breed,
                Age = lostPetModel.Age,
                Description = lostPetModel.Description,
                LastLocation = lostPetModel.LastLocation,
                DateLost = lostPetModel.DateLost,
                ContactInfo = lostPetModel.ContactInfo,
                PhotoUrl = lostPetModel.PhotoUrl,
                Status = lostPetModel.Status,
                //add comments here when comment dto is ready
            };
        }

        public static LostPet ToLostPetFromCreateDto(this LostPetCreateRequestDTO lostPetDTO)
        {
            return new LostPet
            {
                PetName = lostPetDTO.PetName,
                Breed = lostPetDTO.Breed,
                Age = lostPetDTO.Age,
                Description = lostPetDTO.Description,
                LastLocation = lostPetDTO.LastLocation,
                DateLost = lostPetDTO.DateLost,
                ContactInfo = lostPetDTO.ContactInfo,
                PhotoUrl = lostPetDTO.PhotoUrl,
                Status = lostPetDTO.Status,
            };
        }
    }
}