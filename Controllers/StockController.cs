using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FinShark.Data;
using FinShark.Dtos.Stock;
using FinShark.Mappers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FinShark.Controllers
{   
    [Route("api/stock")]
    [ApiController]
    public class StockController : ControllerBase
    {

        private readonly ApplicationDBContext _context;
        public StockController(ApplicationDBContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetAll() {

            var stocks = _context.Stocks.ToList().Select(s => s.ToStockDto());

            return Ok(stocks);
        }

        [HttpGet("{id}")]
        public IActionResult GetById([FromRoute] int id) {
            var stock = _context.Stocks.Find(id);

            if (stock != null) {
                return Ok(stock.ToStockDto());
            }

            return NotFound();
        }


        [HttpPost]
        public IActionResult Create([FromBody] CreateStockRequestDto stockDto) {
            var stockModel = stockDto.ToStockFromCreateDto();
            
            _context.Stocks.Add(stockModel);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetById), new {id = stockModel.Id}, stockModel.ToStockDto());
        }
        
    }
}