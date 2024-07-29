using FinShark.Dtos.Comment;
using FinShark.Interfaces;
using FinShark.Mappers;
using Microsoft.AspNetCore.Mvc;

namespace FinShark.Controllers
{
    [Route("api/comments")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly ICommentRepository _commentRepo;
        private readonly IStockRepository _stockRepo;

        public CommentController(ICommentRepository commentRepo, IStockRepository stockRepo)
        {
            _commentRepo = commentRepo;
            _stockRepo = stockRepo;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll() {
            var comments = await _commentRepo.GetAllAsync();

            var commentsDto = comments.Select(s => s.ToCommentDto());

            return Ok(commentsDto);

        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id) {
            var comment = await _commentRepo.GetByIdAsync(id);

            if (comment == null) {
                return NotFound();
            }

            return Ok(comment.ToCommentDto());
        }

        [HttpPost("{stockId}")]
        public async Task<IActionResult> Create([FromRoute] int stockId, [FromBody] CreateCommentRequestDto commentDto) {
            if (!await _stockRepo.StockExist(stockId)) {
                return BadRequest("Stock does not exist");
            }

            var commentModel = await _commentRepo.CreateAsync(commentDto.ToCommentFromCreateDto(stockId));


            return CreatedAtAction(nameof(GetById), new {id = commentModel.Id}, commentModel.ToCommentDto());
        }
    }
}