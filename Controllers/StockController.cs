using APISYMBOL.Data;
using APISYMBOL.Dto.StockDto;
using APISYMBOL.Helper;
using APISYMBOL.Interface;
using APISYMBOL.Mappers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APISYMBOL.Controllers
{
    [Route("api/stock")]
    [ApiController]
    public class StockController : ControllerBase
    {
        private readonly ApplicationDBContext _dbContext;
        private readonly IStockRepository _stockRep;
        public StockController(ApplicationDBContext context, IStockRepository stockRepository)
        {
            _dbContext = context;
            _stockRep = stockRepository;

        }
        [Route("Index")]
        [HttpGet]
        [Authorize]

        public  async Task<IActionResult> Index([FromQuery] QueryObject query)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            var stocks = await _stockRep.GetAllAsync(query);
            var stockDto=  stocks.Select(s=>s.ToStockDto()).ToList();
            return Ok(stocks);
        }
        [HttpGet("{Id:int}")]
        public async Task<IActionResult> GetById([FromRoute] int Id) {
            var Stock = await _stockRep.GetById(Id);
            if(Stock== null)
            {
                return NotFound();
            }
            
                return Ok(Stock.ToStockDto());
            
            
        }
        [HttpPost]
        public  async Task<IActionResult> Create([FromBody] CreateDtoRequest stockDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var stock = stockDto.ToStockFromCreateDTO();
            await _stockRep.CreateAsync(stock);

            return CreatedAtAction(nameof(GetById), new { id = stock.Id }, stock.ToStockDto());
        }
        [HttpPut]
        [Route("{Id}")]
        public async  Task<IActionResult> Update([FromRoute] int Id, [FromBody] UpdateDtorequest stockDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var stockModel = await _stockRep.UpdateAsync(stockDto, Id );
            if(stockModel == null)
            {
                return NotFound();
            }
           
            return Ok(stockModel.ToStockDto().ToString());

        }
        [HttpDelete]
        [Route("{Id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int Id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var stockModel = await  _stockRep.DeleteAsync(Id);
            if(stockModel == null)
            {
                return NotFound();
            }
          
            return NoContent();
        }
    }
}
