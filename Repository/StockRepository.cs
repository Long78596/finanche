using APISYMBOL.Data;
using APISYMBOL.Dto.StockDto;
using APISYMBOL.Helper;
using APISYMBOL.Interface;
using APISYMBOL.Model;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace APISYMBOL.Repository
{
    public class StockRepository : IStockRepository
    {
        private readonly ApplicationDBContext _dbContext;
        public StockRepository(ApplicationDBContext dBContext)
        {
            _dbContext = dBContext;
        }

        public async Task<Stock?> CreateAsync(Stock stockmodel)
        {
            await _dbContext.Stocks.AddAsync(stockmodel);
            await _dbContext.SaveChangesAsync();
            return stockmodel;
        }

        public async Task<Stock?> DeleteAsync(int Id)
        {
            var stockModel = await _dbContext.Stocks.FirstOrDefaultAsync(x => x.Id == Id);
            if (stockModel == null)
            {
                return null;
            }
            _dbContext.Stocks.Remove(stockModel);
            await _dbContext.SaveChangesAsync();
            return stockModel;
        }

        public async Task<List<Stock>> GetAllAsync(QueryObject query)
        {
var stock=  _dbContext.Stocks.Include(t => t.Comments).ThenInclude(a=>a.AppUser).AsQueryable();
            if (!string.IsNullOrWhiteSpace(query.CompanyName))
            {
                stock = stock.Where(s => s.CompanyName.Contains(query.CompanyName));


            }
            if (!string.IsNullOrWhiteSpace(query.Symbol))
            {
                stock = stock.Where(s => s.Symbol.Contains(query.Symbol));


            }
            if (!string.IsNullOrWhiteSpace(query.SortBy))
            {
                if(query.SortBy.Equals("Symbol", StringComparison.OrdinalIgnoreCase))
                {
                    stock = query.IsDecsending ? stock.OrderByDescending(s => s.Symbol) : stock.OrderBy(c => c.Symbol);
                }
            }
            var skipNumber = (query.PageNumber - 1) * query.PageSize;

            return await stock.Skip(skipNumber).Take(query.PageSize).ToListAsync();
        }

        public async  Task<Stock?> GetById(int Id)
        {
            return await _dbContext.Stocks.Include(c=>c.Comments).FirstOrDefaultAsync(c=>c.Id==Id);
        }

        public async Task<Stock?> GetBySymbolAsync(string symbol)
        {
            return await _dbContext.Stocks.FirstOrDefaultAsync(s => s.Symbol == symbol);
        }

        public Task<bool> StockExixts(int id)
        {
            return _dbContext.Stocks.AnyAsync(s => s.Id == id);
        }

        public async Task<Stock?> UpdateAsync(UpdateDtorequest UpdateDto, int Id)
        {
            var existingStock = await _dbContext.Stocks.FirstOrDefaultAsync(x => x.Id == Id);
            if(existingStock == null)
            {
                return null;
            }
            existingStock.Symbol = UpdateDto.Symbol;
            existingStock.CompanyName = UpdateDto.CompanyName;
            existingStock.Purchase = UpdateDto.Purchase;
            existingStock.LastDiv = UpdateDto.LastDiv;
            existingStock.Industry = UpdateDto.Industry;
            existingStock.MarketCap = UpdateDto.MarketCap;
            await _dbContext.SaveChangesAsync();
            return existingStock;
        }
    }
}
