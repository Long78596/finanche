using APISYMBOL.Model;

namespace APISYMBOL.Interface
{
    public interface IFMPService
    {
        Task<Stock> FindStockBySymbolAsync(string symbol);
    }
}
