using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.DTOs.Comments;
using api.Interfaces;
using api.Mappers;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [ApiController]
    [Route("api/comments")]
    public class CommentController : ControllerBase
    {
        private readonly ICommentsInterface _commentRepo;
        public CommentController(ICommentsInterface commentRepo)
        {
            _commentRepo = commentRepo;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllComments(CommentForGetAllDTO commentForGetDTO)
        {
            var commentsModel = await _commentRepo.GetComments();
            var commentDTO = commentsModel.Select(s => s.ToCommentForGetAllDto());

            return Ok(commentDTO);
        }
    }
}