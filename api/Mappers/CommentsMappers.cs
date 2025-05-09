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
    }
}