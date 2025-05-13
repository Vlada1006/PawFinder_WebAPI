using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;
using api.Data;
using api.DTOs.Comments;
using api.Interfaces;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Repositories
{
    public class CommentRepository : ICommentsInterface
    {
        private readonly AppDbContext _db;

        public CommentRepository(AppDbContext db)
        {
            _db = db;
        }

        public async Task<List<Comment>> GetComments()
        {
            var comments = await _db.Comments.ToListAsync();

            return comments;
        }

        public async Task<Comment?> GetCommentById(int id)
        {
            var comment = await _db.Comments.FirstOrDefaultAsync(u => u.CommentId == id);

            if (comment == null)
            {
                return null;
            }

            return comment;
        }


        public async Task<Comment> CreateComment(Comment commentModel)
        {
            await _db.Comments.AddAsync(commentModel);
            await _db.SaveChangesAsync();

            return commentModel;
        }

        public async Task<Comment?> UpdateComment(int id, UpdateCommentRequestDTO commentDTO)
        {
            var existingComment = await _db.Comments.FirstOrDefaultAsync(u => u.CommentId == id);

            if (existingComment == null)
            {
                return null;
            }

            existingComment.Title = commentDTO.Title;
            existingComment.Content = commentDTO.Content;

            await _db.SaveChangesAsync();

            return existingComment;
        }

        public async Task<Comment?> PartialUpdateComment(int id, PartialUpdateCommentRequestDTO patchCommentsDTO)
        {
            var existingComment = await _db.Comments.FirstOrDefaultAsync(u => u.CommentId == id);

            if (existingComment == null)
            {
                return null;
            }

            if (patchCommentsDTO.Title != null) existingComment.Title = patchCommentsDTO.Title;
            if (patchCommentsDTO.Content != null) existingComment.Content = patchCommentsDTO.Content;

            await _db.SaveChangesAsync();

            return existingComment;
        }

        public async Task<Comment?> DeleteComment(int id)
        {
            var commentToDelete = await _db.Comments.FirstOrDefaultAsync(u => u.CommentId == id);

            if (commentToDelete == null)
            {
                return null;
            }

            _db.Comments.Remove(commentToDelete);

            await _db.SaveChangesAsync();

            return commentToDelete;
        }

        public async Task<IEnumerable<Comment?>> DeleteMultipleComments(int[] ids)
        {
            var comments = new List<Comment>();

            foreach (var id in ids)
            {
                var comment = await _db.Comments.FirstOrDefaultAsync(u => u.CommentId == id);

                if (comment == null)
                {
                    continue;
                }

                comments.Add(comment);
            }

            if (comments.Count == 0)
            {
                return Enumerable.Empty<Comment>();
            }

            _db.Comments.RemoveRange(comments);
            await _db.SaveChangesAsync();

            return comments;
        }
    }
}