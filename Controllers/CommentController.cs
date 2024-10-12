using APISYMBOL.Dto.Comments;
using APISYMBOL.Extensions;
using APISYMBOL.Helper;
using APISYMBOL.Interface;
using APISYMBOL.Mappers;
using APISYMBOL.Model;
using APISYMBOL.Repository;
using APISYMBOL.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace APISYMBOL.Controllers
{
    [Route("api/comment")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly ICommentRepository _commentRepo;
        private readonly IStockRepository _stockRepo;
        private readonly UserManager<AppUser> _userManager;
        private readonly IFMPService _fMPService;

        public CommentController(ICommentRepository commentRepo, IStockRepository stockRepository, UserManager<AppUser> userManager, IFMPService fMPService)
        {
            _commentRepo = commentRepo;
            _stockRepo = stockRepository;
            _userManager = userManager;
            _fMPService = fMPService;
        }
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAll([FromQuery]CommentQueryObject query)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var comment = await _commentRepo.GetAllAsync(query);
            var commentDto = comment.Select(s => s.TocommentDto());
            return Ok(commentDto);
        }
        [Route("{Id:int}")]
        [HttpGet]
        public async Task<IActionResult> GetById([FromRoute] int Id)
        {
            var comment = await _commentRepo.GetById(Id);
            if (comment == null)
            {
                return NotFound();
            }
            return Ok(comment.TocommentDto());
        }
        [HttpPost]
        [Route("{symbol:alpha}")]
        public async Task<IActionResult> Create([FromRoute] string symbol, CreateCommentDto commentDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var stock = await _stockRepo.GetBySymbolAsync(symbol);

            if (stock == null)
            {
                stock = await _fMPService.FindStockBySymbolAsync(symbol);
                if (stock == null)
                {
                    return BadRequest("Stock does not exists");
                }
                else
                {
                    await _stockRepo.CreateAsync(stock);
                }
            }

            var username = User.GetUsername();
            var appUser = await _userManager.FindByNameAsync(username);

            var commentModel = commentDto.TocommentFromCreate(stock.Id);
            commentModel.AppUserId = appUser.Id;
            await _commentRepo.CreatAsync(commentModel);
            return CreatedAtAction(nameof(GetById), new { id = commentModel.Id }, commentModel.TocommentDto());
        }
        [HttpPut]
        [Route("{Id:int}")]
        public async Task<IActionResult> Update([FromRoute] int Id, [FromBody] UpdateCommentRequestDto updateDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var comment = await _commentRepo.UpdateAsync(Id, updateDto.TocommentFromUpdate());
            if(comment == null)
            {
              return   NotFound("Comment no notfound");
            }
            return Ok(comment.TocommentDto());
        }
        [HttpDelete]
        [Route("{Id:int}")]
        public async Task<IActionResult> Delete([FromRoute]int Id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var commentModel = await _commentRepo.DeleteAsync(Id);
            if (commentModel == null)
            {
                return NotFound();
            }
            return Ok(commentModel);
        }
    }
}
