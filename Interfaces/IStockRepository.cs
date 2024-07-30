using FinShark.Dtos.Stock;
using FinShark.Models;
using FinShark.QueryObjects;

namespace FinShark.Interfaces
{
    public interface IStockRepository
    {
        Task<List<Stock>> GetAllAsync(GetAllStocksQueryObject queryObject);
        Task<Stock?> GetByIdAsync(int id);
        Task<Stock> CreateAsync(Stock stockModel);
        Task<Stock?> UpdateAsync(int id, UpdateStockRequestDto stockDto);
        Task<Stock?> DeleteAsync(int id);
        Task<bool> StockExist(int id);
    }
}