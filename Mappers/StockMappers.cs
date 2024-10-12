using APISYMBOL.Dto.StockDto;
using APISYMBOL.Model;
using System.Runtime.CompilerServices;

namespace APISYMBOL.Mappers
{
    public static class StockMappers
    {
        public static StockDto ToStockDto(this Stock stockModel)
        {
            return new StockDto { 
                Id=stockModel.Id,
                Symbol=stockModel.Symbol,
                CompanyName=stockModel.CompanyName,
                Purchase=stockModel.Purchase,
                LastDiv=stockModel.LastDiv,
                Industry=stockModel.Industry,
                MarketCap=stockModel.MarketCap,
                Comments=stockModel.Comments.Select(c=>c.TocommentDto()).ToList(),

            };

        }
        public static Stock ToStockFromCreateDTO(this CreateDtoRequest createDto)
        {
            return new Stock
            {
                Symbol = createDto.Symbol,
                CompanyName = createDto.CompanyName,
                Purchase = createDto.Purchase,
                LastDiv = createDto.LastDiv,
                Industry = createDto.Industry,
                MarketCap = createDto.MarketCap,
                

            };
        }
        public static Stock ToStockFromFMP(this FMPStock fmpStock)
        {
            return new Stock
            {
                Symbol = fmpStock.symbol,
                CompanyName = fmpStock.companyName,
                Purchase = (decimal)fmpStock.price,
                LastDiv = (decimal)fmpStock.lastDiv,
                Industry = fmpStock.industry,
                MarketCap = fmpStock.mktCap
            };
        }
    }
}
