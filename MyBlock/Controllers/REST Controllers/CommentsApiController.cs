using System.Collections.Generic;

using MyBlock.Exceptions;
using MyBlock.Helpers;
using MyBlock.Models;
using MyBlock.Models.DataTransferObjects;
using MyBlock.Services.Interfaces;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MyBlock.Controllers.REST_Controllers
{
    [ApiController]
    [Route("api/comments")]
    public class CommentsApiController : ControllerBase
    {
        private readonly ICommentsService commentsService;
        private readonly IMapper modelMapper;
        private readonly AuthManager authManager;

        public CommentsApiController(ICommentsService commentsService, IMapper modelMapper, AuthManager authManager)
        {
            this.commentsService = commentsService;
            this.modelMapper = modelMapper;
            this.authManager = authManager;
        }

        [HttpGet("")]
        public IActionResult GetAllComments()
        {
            try
            {
                List<Comment> comments = this.commentsService.GetAll();

                List<CommentDto> commentDtos = new List<CommentDto>();
                foreach (Comment comment in comments)
                {
                    commentDtos.Add(this.modelMapper.MapCommentToDTO(comment));
                }

                return this.StatusCode(StatusCodes.Status200OK, commentDtos);
            }
            catch (EntityNotFoundException ex)
            {
                return this.StatusCode(StatusCodes.Status404NotFound, ex.Message);
            }

        }

        [HttpGet("{id}")]
        public IActionResult GetCommentById(int id)
        {
            try
            {
                Comment comment = this.commentsService.GetById(id);

                CommentDto commentDto = this.modelMapper.MapCommentToDTO(comment);

                return this.StatusCode(StatusCodes.Status200OK, commentDto);
            }
            catch (EntityNotFoundException ex)
            {
                return this.StatusCode(StatusCodes.Status404NotFound, ex.Message);
            }

        }

        [HttpPost("")]
        public IActionResult CreateComment([FromBody] CommentDto dto)
        {
            try
            {
                Comment comment = this.modelMapper.MapDTOToComment(dto);

                Comment createComment = this.commentsService.Create(comment);

                return this.StatusCode(StatusCodes.Status201Created, createComment);
            }
            catch (DuplicateEntityException ex)
            {
                return this.StatusCode(StatusCodes.Status409Conflict, ex.Message);
            }

        }

        [HttpPut("{id}")]
        public IActionResult UpdateComment(int id, [FromBody] CommentDto dto, [FromHeader] string user)
        {
            try
            {
                User name = this.authManager.TryGetUser(user);

                Comment comment = this.modelMapper.MapDTOToComment(dto);
                Comment updatedComment = this.commentsService.Update(id, comment, name);

                return this.StatusCode(StatusCodes.Status200OK, updatedComment);
            }
            catch (UnauthorizedOperationException ex)
            {
                return this.StatusCode(StatusCodes.Status401Unauthorized, ex.Message);
            }
            catch (EntityNotFoundException ex)
            {
                return this.StatusCode(StatusCodes.Status404NotFound, ex.Message);
            }
            catch (DuplicateEntityException ex)
            {
                return this.StatusCode(StatusCodes.Status409Conflict, ex.Message);
            }

        }

    }
}