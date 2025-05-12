using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;
using api.Data;
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
    }
}