using System.ComponentModel.DataAnnotations;

namespace APISYMBOL.Dto.Comments
{
    public class CreateCommentDto
    {
        [Required]
        [MinLength(5,ErrorMessage = "title must be 5 characters")]
        [MaxLength(280,ErrorMessage = "Title cannot be over 280 character")]
        public string Title { get; set; } = string.Empty;
        [Required]
        [MinLength(5,ErrorMessage = "Content must be 5  character")]
        [MaxLength(280,ErrorMessage = "Content cannot  be  over 5  character")]
        public string Content { get; set; } = string.Empty;
        
    }
}
