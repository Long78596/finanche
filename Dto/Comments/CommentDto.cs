using APISYMBOL.Model;
using System.ComponentModel.DataAnnotations.Schema;
namespace APISYMBOL.Dto.Comments

{
    public class CommentDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public DateTime createOn { get; set; } = DateTime.Now;
        public string CreateBy { get; set; } = string.Empty;
        public int? StockId { get; set; }
    }
}
