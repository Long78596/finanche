using APISYMBOL.Extensions;
using APISYMBOL.Interface;
using APISYMBOL.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace APISYMBOL.Controllers
{
    [Route("api/portfolio")]
    [ApiController]
    public class PortfolioController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IStockRepository _stockRepository;
        private readonly IPortfolioRepository _Repository;
        public PortfolioController(UserManager<AppUser> userManager,IStockRepository stockRepository,IPortfolioRepository portfolioRepository)
        {
            _userManager = userManager;
            _stockRepository = stockRepository;
            _Repository = portfolioRepository;
        }
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetUserPortfolio()
        {
            var username = User.GetUsername();
            var appUser = await _userManager.FindByNameAsync(username);
            var userPortfolio = await _Repository.GetUserPortfolio(appUser);
            return Ok(userPortfolio);
        }
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AddPortfolio(string symbol)
        {
            var username = User.GetUsername();
            var appUser = await _userManager.FindByNameAsync(username);
            var stock = await _stockRepository.GetBySymbolAsync(symbol);
            if (stock == null) return BadRequest("Stock not found");
            var userPortfolio = await _Repository.GetUserPortfolio(appUser);
            if (userPortfolio.Any(e => e.Symbol.ToLower() == symbol.ToLower())) return BadRequest("Cannot add symbol");

            var portfolioModel = new Portfolio { 

                StockId=stock.Id,
                AppUserId=appUser.Id,
            };
            await _Repository.CreateAsync(portfolioModel);
            if(portfolioModel == null)
            {
                return StatusCode(500, "Could not create");
            }
            else
            {
                return Created();
            }


        }
        [HttpDelete]
        [Authorize]
        public async Task<IActionResult> DeletePort(string symbol)
        {
            var username = User.GetUsername();
            var appUser = await _userManager.FindByNameAsync(username);
            var userPortfolio = await _Repository.GetUserPortfolio(appUser);
            var filteredStock = userPortfolio.Where(s => s.Symbol.ToLower() == symbol.ToLower());
            if (filteredStock.Count() == 1)
            {
                await _Repository.DeleteAsync(appUser, symbol);
            }
            else
            {
                return BadRequest("Stock not in your portFolio");
            }
            return Ok();
        }
    }
}
