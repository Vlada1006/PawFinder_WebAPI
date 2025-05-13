using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.DTOs.Comments;
using api.Models;

namespace api.Mappers
{
    public static class CommentsMappers
    {
        public static CommentDTO ToCommentDto(this Comment commentModel)
        {
            return new CommentDTO
            {
                Title = commentModel.Title,
                Content = commentModel.Content,
                CreatedOn = commentModel.CreatedOn
            };
        }

        public static CommentForGetAllDTO ToCommentForGetAllDto(this Comment commentModel)
        {
            return new CommentForGetAllDTO
            {
                CommentId = commentModel.CommentId,
                Title = commentModel.Title,
                Content = commentModel.Content,
                CreatedOn = commentModel.CreatedOn,
                PetId = commentModel.PetId
            };
        }

        public static Comment ToCreateCommentRequestDto(this CreateCommentRequestDTO createDTO, int petId)
        {
            return new Comment
            {
                Title = createDTO.Title,
                Content = createDTO.Content,
                PetId = petId
            };
        }

        public static Comment ToUpdateCommentRequestDto(this UpdateCommentRequestDTO updateDTO)
        {
            return new Comment
            {
                Title = updateDTO.Title,
                Content = updateDTO.Content,
            };
        }

        public static Comment ToPartialUpdateCommentRequestDto(this PartialUpdateCommentRequestDTO updateDTO)
        {
            return new Comment
            {
                Title = updateDTO.Title ?? string.Empty,
                Content = updateDTO.Content ?? string.Empty,
            };
        }
    }
}