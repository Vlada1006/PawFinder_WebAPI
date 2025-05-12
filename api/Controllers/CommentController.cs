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
        private readonly ILostPetsInterface _lostPetRepo;
        public CommentController(ICommentsInterface commentRepo, ILostPetsInterface lostPetRepo)
        {
            _commentRepo = commentRepo;
            _lostPetRepo = lostPetRepo;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllComments(CommentForGetAllDTO commentForGetDTO)
        {
            var commentsModel = await _commentRepo.GetComments();
            var commentDTO = commentsModel.Select(s => s.ToCommentForGetAllDto());

            return Ok(commentDTO);
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<IActionResult> GetCommentById([FromRoute] int id)
        {
            var comment = await _commentRepo.GetCommentById(id);

            if (comment == null)
            {
                return NotFound();
            }

            return Ok(comment.ToCommentDto());
        }
        //check if with deleting pet comments stay
        [HttpPost]
        [Route("{petId:int}")]
        public async Task<IActionResult> CreateComment([FromBody] CreateCommentRequestDTO createDTO, int petId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var commentModel = createDTO.ToCreateCommentRequestDto(petId);
            await _commentRepo.CreateComment(commentModel);

            return CreatedAtAction(nameof(GetCommentById), new { id = commentModel.CommentId }, commentModel.ToCommentDto());
        }
    }
}