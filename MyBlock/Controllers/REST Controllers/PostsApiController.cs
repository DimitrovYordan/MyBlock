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
    [Route("api/posts")]
    public class PostsApiController : ControllerBase
    {
        public readonly IPostsService postsService;
        public readonly IMapper modelMapper;
        public readonly AuthManager authManager;

        public PostsApiController(IPostsService postsService, IMapper modelMapper, AuthManager authManager)
        {
            this.postsService = postsService;
            this.modelMapper = modelMapper;
            this.authManager = authManager;
        }

        [HttpGet("")]
        public IActionResult GetPosts()
        {
            List<Post> posts = this.postsService.GetAll();

            List<PostDto> postDtos = new List<PostDto>();
            foreach (Post post in posts)
            {
                postDtos.Add(this.modelMapper.MapPostToDTO(post));
            }

            return this.StatusCode(StatusCodes.Status200OK, postDtos);
        }

        [HttpGet("{id}")]
        public IActionResult GetPostById(int id)
        {
            try
            {
                Post post = this.postsService.GetById(id);

                PostDto postDto = this.modelMapper.MapPostToDTO(post);

                return this.StatusCode(StatusCodes.Status200OK, postDto);
            }
            catch (EntityNotFoundException ex)
            {
                return this.StatusCode(StatusCodes.Status404NotFound, ex.Message);
            }

        }

        [HttpPost("")]
        public IActionResult CreatePost([FromBody] PostDto postDto)
        {
            try
            {
                Post post = this.modelMapper.MapDTOToPost(postDto);

                Post createdPost = this.postsService.Create(post);

                return this.StatusCode(StatusCodes.Status201Created, createdPost);
            }
            catch (DuplicateEntityException ex)
            {
                return this.StatusCode(StatusCodes.Status409Conflict, ex.Message);
            }

        }

        [HttpPut("{id}")]
        public IActionResult UpdatePost(int id, [FromBody] PostDto postDto, [FromHeader] string user)
        {
            try
            {
                User name = this.authManager.TryGetUser(user);

                Post post = this.modelMapper.MapDTOToPost(postDto);
                Post updatedPost = this.postsService.Update(id, post, name);

                return this.StatusCode(StatusCodes.Status200OK, updatedPost);
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