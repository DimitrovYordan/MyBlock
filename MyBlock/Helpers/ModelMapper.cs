using MyBlock.Models;
using MyBlock.Models.DataTransferObjects;

namespace MyBlock.Helpers
{
    public class ModelMapper : IMapper
    {
        public CommentDto MapCommentToDTO(Comment commentToMap)
        {
            CommentDto result = new CommentDto()
            {
                AuthorID = commentToMap.AuthorID,
                Author = commentToMap.Author.Username,
                Content = commentToMap.Content,
                TimePosted = commentToMap.TimePosted,
                ParentPostID = commentToMap.ParentPostID,
                ParentCommentID = commentToMap.ParentCommentID
            };

            return result;
        }

        public Comment MapDTOToComment(CommentDto dto)
        {
            return new Comment
            {
                AuthorID = dto.AuthorID,
                Content = dto.Content,
                ParentPostID = dto.ParentPostID,
                ParentCommentID = dto.ParentCommentID
            };

        }

        public Post MapDTOToPost(PostDto dto)
        {
            return new Post
            {
                AuthorID = dto.AuthorID,
                Title = dto.Title,
                Content = dto.Content
            };

        }

        public User MapDTOToUser(UserDto dto)
        {
            return new User
            {
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Email = dto.Email,
                Username = dto.Username,
                Password = dto.Password
            };

        }

        public PostDto MapPostToDTO(Post postToMap)
        {
            PostDto result = new PostDto()
            {
                AuthorID = postToMap.AuthorID,
                AuthorUsername = postToMap.Author.Username,
                Title = postToMap.Title,
                Content = postToMap.Content,
                TimePosted = postToMap.TimePosted
            };

            foreach (Comment comment in postToMap.Comments)
            {
                result.CommentsCount++;
            }

            return result;
        }

        public PostViewModel MapPostToViewModel(Post postToMap)
        {
            PostViewModel result = new PostViewModel()
            {
                Id = postToMap.Id,
                Title = postToMap.Title,
                Content = postToMap.Content,
                AuthorID = postToMap.AuthorID,
                Author = postToMap.Author,
                Rating = postToMap.Rating,
                TimePosted = postToMap.TimePosted,
                Comments = postToMap.Comments,
                LikedByUsers = postToMap.LikedByUsers,
                DislikedByUsers = postToMap.DislikedByUsers
            };

            return result;
        }

        public Post MapPostToViewModelToPost(PostViewModel model)
        {
            var result = new Post
            {
                Title = model.Title,
                Content = model.Content,
                Author = model.Author,
                AuthorID = model.AuthorID
            };

            return result;
        }

        public User MapUser(RegisterViewModel model)
        {
            return new User
            {
                Username = model.Username,
                Password = model.Password,
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email
            };

        }

        public UserDto MapUserToDto(User userToMap)
        {
            UserDto result = new UserDto()
            {
                Username = userToMap.Username,
                FirstName = userToMap.FirstName,
                LastName = userToMap.LastName,
                Email = userToMap.Email,
                Password = userToMap.Password
            };

            return result;
        }

    }
}