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
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
        
            var comments = await _commentRepo.GetAllAsync();

            var commentsDto = comments.Select(s => s.ToCommentDto());

            return Ok(commentsDto);

        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById([FromRoute] int id) {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);   
                     
            var comment = await _commentRepo.GetByIdAsync(id);

            if (comment == null) {
                return NotFound();
            }

            return Ok(comment.ToCommentDto());
        }

        [HttpPost("{stockId:int}")]
        public async Task<IActionResult> Create([FromRoute] int stockId, [FromBody] CreateCommentRequestDto commentDto) {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!await _stockRepo.StockExist(stockId)) {
                return BadRequest("Stock does not exist");
            }

            var comment = await _commentRepo.CreateAsync(commentDto.ToCommentFromCreateDto(stockId));


            return CreatedAtAction(nameof(GetById), new {id = comment.Id}, comment.ToCommentDto());
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateCommentRequestDto commentDto) {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var comment = await _commentRepo.UpdateAsync(id, commentDto);
            
            if (comment == null) {
                return NotFound("Comment not found");
            }

            return Ok(comment.ToCommentDto());
        }


        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id) {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var deletedComment = await _commentRepo.DeleteAsync(id);

            if (deletedComment == null) {
                return NotFound("Comment not found");
            }

            return NoContent();
        }
    }
}