using APISYMBOL.Data;
using APISYMBOL.Interface;
using APISYMBOL.Migrations;
using APISYMBOL.Model;
using Microsoft.EntityFrameworkCore;

namespace APISYMBOL.Repository
{
    public class PortfolioRepository : IPortfolioRepository
    {
        private readonly ApplicationDBContext _dbContext;
        public PortfolioRepository(ApplicationDBContext dBContext)
        {
            _dbContext = dBContext;
        }

        public async Task<Portfolio> CreateAsync(Portfolio portfolio)
        {
            await _dbContext.Portfolios.AddAsync(portfolio);
            await _dbContext.SaveChangesAsync();
            return portfolio;
        }

        public async Task<Portfolio> DeleteAsync(AppUser appUser, string symbol)
        {
            var portfolioModel = await _dbContext.Portfolios.FirstOrDefaultAsync(x => x.AppUserId == appUser.Id && x.Stock.Symbol.ToLower() == symbol.ToLower());
            if(portfolioModel == null)
            {
                return null;
            }
            _dbContext.Portfolios.Remove(portfolioModel);
            await _dbContext.SaveChangesAsync();
            return portfolioModel;
        
        }

        public async  Task<List<Stock>> GetUserPortfolio(AppUser user)
        {
            return await  _dbContext.Portfolios.Where(x => x.AppUserId == user.Id).Select(stock => new Stock
            {
                Id = stock.StockId,
                Symbol = stock.Stock.Symbol,
                CompanyName = stock.Stock.CompanyName,
                Purchase = stock.Stock.Purchase,
                LastDiv = stock.Stock.LastDiv,
                Industry = stock.Stock.Industry,
                MarketCap = stock.Stock.MarketCap
            }).ToListAsync();
        }
    }
}
