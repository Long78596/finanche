using APISYMBOL.Dto.Comments;
using APISYMBOL.Model;
using System.Runtime.CompilerServices;

namespace APISYMBOL.Mappers
{
    public static  class CommentMapperscs
    {
        public static CommentDto TocommentDto( this Comment commentModel)
        {
            return new CommentDto { 
                Id=commentModel.Id,
                Title=commentModel.Title,
                Content=commentModel.Content,
               createOn=commentModel.createOn,
               CreateBy=commentModel.AppUser.UserName,
                StockId=commentModel.StockId,
            };

        }
        public static Comment TocommentFromCreate(this CreateCommentDto commentmodel, int stockId)
        {
            return new Comment
            {
                Title = commentmodel.Title,
                Content = commentmodel.Content,
                StockId=stockId,
            };

        }
        public static Comment TocommentFromUpdate(this UpdateCommentRequestDto commentmodel)
        {
            return new Comment
            {
                Title = commentmodel.Title,
                Content = commentmodel.Content,
            
            };

        }
    }
}
