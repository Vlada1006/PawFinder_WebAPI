using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.DTOs.Comments;
using api.Interfaces;
using api.Mappers;
using Azure;
using Microsoft.AspNetCore.JsonPatch;
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

        [HttpPost]
        [Route("{petId:int}")]
        public async Task<IActionResult> CreateComment([FromBody] CreateCommentRequestDTO createDTO, [FromRoute] int petId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var commentModel = createDTO.ToCreateCommentRequestDto(petId);
            await _commentRepo.CreateComment(commentModel);

            return CreatedAtAction(nameof(GetCommentById), new { id = commentModel.CommentId }, commentModel.ToCommentDto());
        }

        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> UpdateComment([FromRoute] int id, [FromBody] UpdateCommentRequestDTO updateDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var commentToUpdate = await _commentRepo.UpdateComment(id, updateDTO);

            if (commentToUpdate == null)
            {
                return NotFound("Comment not found.");
            }

            return Ok(commentToUpdate.ToCommentDto());
        }

        [HttpPatch]
        [Route("{id:int}")]
        public async Task<IActionResult> PartialUpdateComment(int id, [FromBody] JsonPatchDocument<PartialUpdateCommentRequestDTO> patchDoc)
        {
            if (!ModelState.IsValid || patchDoc == null)
            {
                return BadRequest(ModelState);
            }

            var patchDTO = new PartialUpdateCommentRequestDTO();

            patchDoc.ApplyTo(patchDTO, ModelState);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var commentToUpdate = await _commentRepo.PartialUpdateComment(id, patchDTO);

            if (commentToUpdate == null)
            {
                return NotFound("There's no such comment.");
            }

            return Ok(commentToUpdate);
        }
    }
}