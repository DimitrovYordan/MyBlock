using MyBlock.Models;
using MyBlock.Models.DataTransferObjects;

namespace MyBlock.Helpers
{
    public interface IMapper
    {
        User MapDTOToUser(UserDto dto);
        Comment MapDTOToComment(CommentDto dto);
        Post MapDTOToPost(PostDto dto);
        UserDto MapUserToDto(User userToMap);
        PostDto MapPostToDTO(Post postToMap);
        CommentDto MapCommentToDTO(Comment commentToMap);
        User MapUser(RegisterViewModel model);
        PostViewModel MapPostToViewModel(Post postToMap);
        Post MapPostToViewModelToPost(PostViewModel model);
    }
}