using FinShark.Data;
using FinShark.Dtos.Stock;
using FinShark.Interfaces;
using FinShark.Mappers;
using FinShark.QueryObjects;
using Microsoft.AspNetCore.Mvc;

namespace FinShark.Controllers
{   
    [Route("api/stock")]
    [ApiController]
    public class StockController : ControllerBase
    {

        private readonly ApplicationDBContext _context;
        private readonly IStockRepository _stockRepository;

        public StockController(ApplicationDBContext context, IStockRepository stockRepository)
        {
            _context = context;
            _stockRepository = stockRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] GetAllStocksQueryObject queryObject) {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var stocks = await _stockRepository.GetAllAsync(queryObject);
            
            var stocksDto = stocks.Select(s => s.ToStockDto());

            return Ok(stocksDto);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById([FromRoute] int id) {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            
            var stock = await _stockRepository.GetByIdAsync(id);

            if (stock == null) {
                return NotFound();
            }

            return Ok(stock.ToStockDto());
        }


        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateStockRequestDto stockDto) {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
                        
            var stockModel = stockDto.ToStockFromCreateDto();
            
            await _stockRepository.CreateAsync(stockModel);

            return CreatedAtAction(nameof(GetById), new {id = stockModel.Id}, stockModel.ToStockDto());
        }


        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateStockRequestDto updateDto) {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
                        
            var stockModel = await _stockRepository.UpdateAsync(id, updateDto);

            if (stockModel == null) {
                return NotFound();
            }

            return Ok(stockModel.ToStockDto());
        }

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id) {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);            

            var stockModel = await _stockRepository.DeleteAsync(id);

            if (stockModel == null) {
                return NotFound();
            }

            return NoContent();
        }
        
    }
}