using APISYMBOL.Dto.StockDto;
using APISYMBOL.Helper;
using APISYMBOL.Model;
using System.Threading.Tasks;

namespace APISYMBOL.Interface
{
    public interface IStockRepository
    {
      Task<List<Stock>> GetAllAsync(QueryObject query);
        Task<Stock?> GetById(int Id);
        Task<Stock?> GetBySymbolAsync(string symbol);
        Task<Stock?> CreateAsync(Stock stockmodel);
        Task<Stock?> UpdateAsync(UpdateDtorequest UpdateDto, int Id);
        Task<Stock?> DeleteAsync(int Id);
        Task<bool> StockExixts(int id);

    }
}
