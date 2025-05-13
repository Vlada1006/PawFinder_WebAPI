using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.DTOs.Comments;
using api.Models;

namespace api.Interfaces
{
    public interface ICommentsInterface
    {
        public Task<List<Comment>> GetComments();
        public Task<Comment?> GetCommentById(int id);
        public Task<Comment> CreateComment(Comment commentModel);
        public Task<Comment?> UpdateComment(int id, UpdateCommentRequestDTO commentDTO);
        public Task<Comment?> PartialUpdateComment(int id, PartialUpdateCommentRequestDTO updateDTO);
    }
}