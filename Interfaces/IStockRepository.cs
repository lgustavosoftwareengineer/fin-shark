using FinShark.Models;

namespace FinShark.Interfaces
{
    public interface IStockRepository
    {
        public Task<List<Stock>> GetAllAsync();
    }
}