using FinShark.Data;
using FinShark.Dtos.Stock;
using FinShark.Interfaces;
using FinShark.Models;
using FinShark.QueryObjects;
using Microsoft.EntityFrameworkCore;

namespace FinShark.Repositories
{
    public class StockRepository : IStockRepository
    {
        private readonly ApplicationDBContext _context;

        public StockRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<Stock> CreateAsync(Stock stock)
        {
            await _context.Stocks.AddAsync(stock);
            await _context.SaveChangesAsync();

            return stock;
        }

        public async Task<Stock?> DeleteAsync(int id)
        {
            var stock = await _context.Stocks.FirstOrDefaultAsync(x => x.Id == id);

            if (stock == null) {
                return null;
            }

            _context.Stocks.Remove(stock);
            await _context.SaveChangesAsync();

            return stock;
        }


        public async  Task<List<Stock>> GetAllAsync(GetAllStocksQueryObject queryObject)
        
        {
            var stocksQuery = _context.Stocks.AsQueryable();

            if (!string.IsNullOrWhiteSpace(queryObject.CompanyName)) {
                stocksQuery = stocksQuery.Where(s => s.CompanyName.Contains(queryObject.CompanyName));
            }

            if (!string.IsNullOrWhiteSpace(queryObject.Symbol)) {
                stocksQuery = stocksQuery.Where(s => s.Symbol.Contains(queryObject.Symbol));
            }


            if (!string.IsNullOrWhiteSpace(queryObject.SortBy)) {
                if (queryObject.SortBy.Equals("Symbol", StringComparison.OrdinalIgnoreCase)) {
                    stocksQuery = queryObject.IsDescending ? stocksQuery.OrderByDescending(s => s.Symbol) : stocksQuery.OrderBy(s => s.Symbol);
                }
            }

            return await stocksQuery.Include(s => s.Comments).ToListAsync();
        }

        public async Task<Stock?> GetByIdAsync(int id)
        {
            return await _context.Stocks.Include(s => s.Comments).FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task<bool> StockExist(int id)
        {
            return await _context.Stocks.AnyAsync(s => s.Id == id);
        }

        public async Task<Stock?> UpdateAsync(int id, UpdateStockRequestDto stockDto)
        {
            var existingStock = await _context.Stocks.FirstOrDefaultAsync(x => x.Id == id);

            if (existingStock == null) {
                return null;
            }

            existingStock.Symbol = stockDto.Symbol;
            existingStock.CompanyName = stockDto.CompanyName;
            existingStock.Purchase = stockDto.Purchase;
            existingStock.LastDiv = stockDto.LastDiv;
            existingStock.Industry = stockDto.Industry;
            existingStock.MarketCap = stockDto.MarketCap;

            await _context.SaveChangesAsync();

            return existingStock;

        }
    }
}