using System;

using MyBlock.Helpers;
using MyBlock.Models;
using MyBlock.Services.Interfaces;
using MyBlock.Exceptions;
using MyBlock.Models.Relational_Classes;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace MyBlock.Controllers
{
    public class PostController : Controller
    {
        private readonly IPostsService postsService;
        private readonly ICommentsService commentsService;
        private readonly AuthManager authManager;
        private readonly IMapper modelMapper;

        public PostController(IPostsService postsService, ICommentsService commentsService, AuthManager authManager, IMapper mapper)
        {
            this.postsService = postsService;
            this.commentsService = commentsService;
            this.authManager = authManager;
            this.modelMapper = mapper;
        }

        [HttpGet]
        public IActionResult Index(PostQueryParams postQueryParams)
        {
            var posts = this.postsService.FilterBy(postQueryParams);

            return this.View(posts);
        }

        [HttpGet]
        public IActionResult Create()
        {
            var mdoel = new PostViewModel();

            return this.View(mdoel);
        }

        [HttpPost]
        public IActionResult Create(PostViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            try
            {
                Post post = this.modelMapper.MapPostToViewModelToPost(model);
                post.Author = authManager.CurrentUser;
                post.AuthorID = authManager.CurrentUser.Id;
                post.Rating = 0;
                this.postsService.Create(post);

                return RedirectToAction("Details", "Post");
            }
            catch (UnauthorizedOperationException ex)
            {
                this.Response.StatusCode = StatusCodes.Status401Unauthorized;
                this.ViewData["ErrorMessage"] = ex.Message;

                return this.View("Error");
            }

        }

        [HttpGet]
        public IActionResult Details([FromRoute] int id)
        {
            if (this.authManager.CurrentUser == null)
            {
                return this.RedirectToAction("Login", "Home");
            }

            try
            {
                var post = this.postsService.GetById(id);
                var postViewModel = this.modelMapper.MapPostToViewModel(post);

                return this.View(postViewModel);
            }
            catch (EntityNotFoundException ex)
            {
                this.Response.StatusCode = StatusCodes.Status404NotFound;
                this.ViewData["ErrorMessage"] = ex.Message;

                return this.View("Error");
            }

        }

        [HttpPost]
        public IActionResult Details([FromRoute] int id, PostViewModel postViewModel)
        {
            var user = this.authManager.CurrentUser;
            if (!String.IsNullOrEmpty(postViewModel.newComment))
            {
                Comment comment = new Comment()
                {
                    Content = postViewModel.newComment,
                    AuthorID = user.Id,
                    ParentPostID = postViewModel.Id,
                    TimePosted = DateTime.Now
                };

                this.commentsService.Create(comment);
            }
            else if (postViewModel.Like == 1)
            {
                var like = new LikedByUser() { PostId = postViewModel.Id, UserId = this.authManager.CurrentUser.Id };
                this.postsService.AddLike(like);
                var dislike = this.postsService.GetDislikeId(postViewModel.Id, this.authManager.CurrentUser.Id);
                if (dislike != null)
                {
                    this.postsService.RemoveDislike(dislike);
                }

            }
            else if (postViewModel.Dislike == 1)
            {
                var dislike = new DislikedByUser() { PostId = postViewModel.Id, UserId = this.authManager.CurrentUser.Id };
                this.postsService.AddDislike(dislike);
                var likeToRemove = this.postsService.GetLikeId(postViewModel.Id, this.authManager.CurrentUser.Id);
                if (likeToRemove != null)
                {
                    this.postsService.RemoveLike(likeToRemove);
                }

            }

            return this.RedirectToAction("Details", "Post", new { postViewModel.Id });
        }

        [HttpGet]
        public IActionResult Delete([FromRoute] int id)
        {
            try
            {
                if (this.authManager.CurrentUser == null)
                {
                    return this.RedirectToAction("Login", "Home");
                }

                var post = this.postsService.GetById(id);

                return this.View(post);
            }
            catch (EntityNotFoundException ex)
            {
                this.Response.StatusCode = StatusCodes.Status404NotFound;
                this.ViewData["ErrorMessage"] = ex.Message;

                return this.View("Error");
            }

        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed([FromRoute] int id)
        {
            try
            {
                var user = this.authManager.CurrentUser;
                this.postsService.Delete(id, user);

                return this.RedirectToAction("Index", "Post");
            }
            catch (EntityNotFoundException ex)
            {
                this.Response.StatusCode = StatusCodes.Status404NotFound;
                this.ViewData["ErrorMessage"] = ex.Message;

                return this.View("Error");
            }
            catch (UnauthorizedOperationException ex)
            {
                this.Response.StatusCode = StatusCodes.Status401Unauthorized;
                this.ViewData["ErrorMessage"] = ex.Message;

                return this.View("Error");
            }

        }

        [HttpGet]
        public IActionResult DeleteComment([FromRoute] int id)
        {
            try
            {
                if (this.authManager.CurrentUser == null)
                {
                    return this.RedirectToAction("Login", "Home");
                }

                var comment = this.commentsService.GetById(id);

                return this.View(comment);
            }
            catch (EntityNotFoundException ex)
            {
                this.Response.StatusCode = StatusCodes.Status404NotFound;
                this.ViewData["ErrorMessage"] = ex.Message;

                return this.View("Error");
            }

        }

        [HttpPost, ActionName("DeleteComment")]
        public IActionResult DeleteCommentConfirmed([FromRoute] int id)
        {
            try
            {
                var user = this.authManager.CurrentUser;
                var userId = this.commentsService.GetById(id).ParentPostID;
                this.commentsService.Delete(id, user);

                return this.RedirectToAction("Details", "Post", new { userId });
            }
            catch (EntityNotFoundException ex)
            {
                this.Response.StatusCode = StatusCodes.Status404NotFound;
                this.ViewData["ErrorMessage"] = ex.Message;

                return this.View("Error");
            }
            catch (UnauthorizedOperationException ex)
            {
                this.Response.StatusCode = StatusCodes.Status401Unauthorized;
                this.ViewData["ErrorMessage"] = ex.Message;

                return this.View("Error");
            }

        }

        [HttpGet]
        public IActionResult Edit([FromRoute] int id)
        {
            try
            {
                var post = this.postsService.GetById(id);

                return this.View(post);
            }
            catch (EntityNotFoundException ex)
            {
                this.Response.StatusCode = StatusCodes.Status404NotFound;
                this.ViewData["ErrorMessage"] = ex.Message;

                return this.View("Error");
            }

        }

        [HttpPost]
        public IActionResult Edit([FromRoute] int id, Post model)
        {
            try
            {
                this.postsService.Update(id, model, authManager.CurrentUser);

                return RedirectToAction("Details", "Post", new { model.Id });
            }
            catch (UnauthorizedOperationException)
            {
                this.ModelState.AddModelError("Content", "Unable to edit the post, either due to access restrictions or due to invalid input");

                return this.View(model);
            }

        }

    }
}